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
        ConfigurationChanges = ConfigChanges.Orientation
        | ConfigChanges.Keyboard
        | ConfigChanges.KeyboardHidden
        | ConfigChanges.ScreenSize
        // Configs NOT ACTUALLY handled, but since Destroying the activity leads to crash, here to pretend handling them
        | ConfigChanges.Mnc
        | ConfigChanges.Mcc
        | ConfigChanges.Locale
        | ConfigChanges.UiMode
        | ConfigChanges.Navigation
        | ConfigChanges.Orientation
        | ConfigChanges.ScreenLayout
        | ConfigChanges.SmallestScreenSize
        | ConfigChanges.Density
        | ConfigChanges.FontScale
        | ConfigChanges.ColorMode
        | ConfigChanges.Touchscreen
        | ConfigChanges.LayoutDirection
    )]
    public class PvZActivity : MonoGame.IMEHelper.AndroidGameActivityIME//AndroidGameActivity
    {
        private Sexy.Main _game;
        private View _view;
        public static Action<Exception> ExceptionReporter = (e) => { };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Sexy.GlobalStaticVars.gPvZActivity = this;
            //#if !DEBUG

            try
            {
                //#endif
                _game = new Sexy.Main();
                _view = _game.Services.GetService(typeof(View)) as View;
                _view.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Fullscreen;//| (StatusBarVisibility)SystemUiFlags.HideNavigation | (StatusBarVisibility)SystemUiFlags.ImmersiveSticky;

                SetContentView(_view);
                _game.Run();
                //#if !DEBUG
            }
            catch (Exception err)
            {
                OnException(this, err);
            }
            //#endif

        }

        protected override void OnDestroy()
        {
            //(_view.Parent as ViewGroup).RemoveView(_view);  // 解绑_view以便重新使用
            Sexy.Debug.Log("Destroy...");
            base.OnDestroy();
            Sexy.GlobalStaticVars.gSexyAppBase.AppExit();
            _game.Exit();
        }

        public void OnException(object sender, Exception err)
        {
#if !DEBUG
            ExceptionReporter(err);
#endif
            var aDialog = new AlertDialog.Builder(this);
            aDialog.SetTitle(err.GetType().ToString());
            aDialog.SetMessage($"{err.Message}\n{err.StackTrace}");
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
                using (var s = new StreamReader(verInfoFilePath))
                {
                    string str = s.ReadToEnd();
                    if (str.StartsWith(packedlibFileName))
                        return libFolderExtPath;
                }
            }
            
            string result = ExtractZipFromAsset(libFolderPath, packedlibFileName, libFolderExtPath);
            
            using (var verInfoWriter = new StreamWriter(verInfoFilePath))
            {
                verInfoWriter.WriteLine(packedlibFileName);
                verInfoWriter.Flush();
            }
            return result;
        }

        private string ExtractZipFromAsset(string assetPathDir, string assetPathFileName, string extDestPathDir) 
        {
            int index = Array.IndexOf(Assets.List(assetPathDir), assetPathFileName);
            if (index != -1)
            {
                using (var packedlibStream = new MemoryStream())
                {
                    Assets.Open(Path.Combine(assetPathDir, assetPathFileName)).CopyTo(packedlibStream);
                    packedlibStream.Position = 0;
                    var fastZip = new FastZip();
                    fastZip.ExtractZip(
                        packedlibStream,
                        extDestPathDir,
                        FastZip.Overwrite.Always,
                        null,
                        null, null, false, true // 由于奇怪的兼容性问题(.NET 6修复)，在部分系统上无法还原时间等属性
                    );
                };
                return extDestPathDir;
            }
            else
            {
                throw new FileNotFoundException($"{assetPathFileName} not found in ANDROID_ASSET/{assetPathDir}");
            }
        }

        internal void ConfigureWorkDirAsLocalData()
        {
            Directory.SetCurrentDirectory(GetExternalFilesDir("").AbsolutePath);
        }

        internal void ExtractCustoms()
        {
            string custFolderPath = "cust";
            string custFolderExtPath = GetExternalFilesDir(custFolderPath).AbsolutePath;
            string verInfoFileName = "info.txt";
            string verInfoFilePath = Path.Combine(custFolderExtPath, verInfoFileName);
            if (Directory.Exists(custFolderExtPath)) 
            {
                if (File.Exists(verInfoFilePath))
                {
                    using (var s = new StreamReader(verInfoFilePath))
                    {
                        string str = s.ReadToEnd();
                        if (str.StartsWith(Lawn.LawnApp.AppVersionNumber))
                            return;
                    }
                }
                // Give space for new cust 
                Directory.Delete(custFolderExtPath, true);
            }
            Directory.CreateDirectory(custFolderExtPath);

            try
            {
                string result = ExtractZipFromAsset(custFolderPath, "cust.zip", custFolderExtPath);
            }
            catch (FileNotFoundException) 
            {
                ; // This version does not have `cust.zip`. Do nothing.
            }
            
            using (var verInfoWriter = new StreamWriter(verInfoFilePath))
            {
                verInfoWriter.WriteLine(Lawn.LawnApp.AppVersionNumber);
                verInfoWriter.Flush();
            }
            return;
        }
    }
}
