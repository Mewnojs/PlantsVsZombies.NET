using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

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
            mMainActivity = this;

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

        public static PvZActivity mMainActivity;
    }
}
