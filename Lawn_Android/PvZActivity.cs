using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace Lawn_Android
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        Immersive = true,
        MultiProcess = true,
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class PvZActivity : MonoGame.IMEHelper.AndroidGameActivityIME//AndroidGameActivity
    {
        private Sexy.Main _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Sexy.GlobalStaticVars.gPvZActivity = this;

#if !DEBUG

            try
            {
#endif
                _game = new Sexy.Main();
                _view = _game.Services.GetService(typeof(View)) as View;
                _view.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Fullscreen;//| (StatusBarVisibility)SystemUiFlags.HideNavigation | (StatusBarVisibility)SystemUiFlags.ImmersiveSticky;

                SetContentView(_view);
                _game.Run();
#if !DEBUG
            }
            catch (Exception err)
            {
                var aDialog = new AlertDialog.Builder(this);
                aDialog.SetTitle(err.GetType().ToString());
                aDialog.SetMessage(err.Message);
                aDialog.SetPositiveButton("Close", delegate { });
                aDialog.Show();
            }
#endif

        }

        public void OnException(object sender, Exception err)
        {
            var aDialog = new AlertDialog.Builder(this);
            aDialog.SetTitle(err.GetType().ToString());
            aDialog.SetMessage(err.Message);
            aDialog.SetPositiveButton("Close", delegate { });
            aDialog.Show();
        }

        internal string GetIronPythonStdLibPath(Version version)
        {
            string libFolderPath = "IronPython/Libs";
            string libFolderExtPath = GetExternalFilesDir(libFolderPath).AbsolutePath;
            string packedlibFileName = $"IronPython.StdLib.{version.Major}.{version.Minor}.{version.Build}.zip";
            string verInfoFileName = "info.txt";
            string verInfoFilePath = Path.Combine(libFolderExtPath, verInfoFileName);
            if (File.Exists(verInfoFilePath)) 
            {
                using (var s = new StreamReader(verInfoFilePath)) {
                    string str = s.ReadToEnd();
                    if (str.StartsWith(packedlibFileName))
                        return libFolderExtPath; 
                }
            }

            int index = Array.IndexOf(Assets.List(libFolderPath), packedlibFileName);
            if (index != -1)
            {
                using (var packedlibStream = new MemoryStream())
                {
                    Assets.Open(Path.Combine(libFolderPath, packedlibFileName)).CopyTo(packedlibStream);
                    packedlibStream.Position = 0;
                    new FastZip().ExtractZip(
                        packedlibStream,
                        GetExternalFilesDir(libFolderPath).AbsolutePath,
                        FastZip.Overwrite.Always,
                        (w) => { return true; },
                        null, null, true, true
                    );
                };
                using (var verInfoWriter = new StreamWriter(verInfoFilePath))
                {
                    verInfoWriter.WriteLine(packedlibFileName);
                    verInfoWriter.Flush();
                }
                return libFolderExtPath;
            }
            else 
            {
                throw new FileNotFoundException($"{packedlibFileName} not found in ANDROID_ASSET/{libFolderPath}");
            }
        }

        internal void ConfigureWorkDirAsLocalData()
        {
            Directory.SetCurrentDirectory(GetExternalFilesDir("").AbsolutePath);
        }
    }
}
