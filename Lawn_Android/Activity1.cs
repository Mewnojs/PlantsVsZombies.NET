using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using System;

namespace DotNETPvZ_Android
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
    public class Activity1 : MonoGame.IMEHelper.AndroidGameActivityIME//AndroidGameActivity
    {
        private Sexy.Main _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Sexy.Main();
            _view = _game.Services.GetService(typeof(View)) as View;
            _view.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Fullscreen;//| (StatusBarVisibility)SystemUiFlags.HideNavigation | (StatusBarVisibility)SystemUiFlags.ImmersiveSticky;

            SetContentView(_view);
            _game.Run();
            

        }
    }
}
