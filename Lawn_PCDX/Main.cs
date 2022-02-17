using System;
using System.Globalization;
using Lawn;
//using Microsoft.Phone.Info;
//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading;
using System.Collections.Generic;

namespace Sexy
{
	public class Main : Game
	{
		public Main()
		{
			Main.SetupTileSchedule();
			Main.graphics = Graphics.GetNew(this);
			Main.SetLowMem();
			Main.graphics.IsFullScreen = false;
			Guide.SimulateTrialMode = false;
			Main.graphics.PreferredBackBufferWidth = 800;
			Main.graphics.PreferredBackBufferHeight = 480;
			GraphicsState.mGraphicsDeviceManager.SupportedOrientations = Constants.SupportedOrientations;
			GraphicsState.mGraphicsDeviceManager.DeviceCreated += new EventHandler<EventArgs>(graphics_DeviceCreated);
			GraphicsState.mGraphicsDeviceManager.DeviceReset += new EventHandler<EventArgs>(graphics_DeviceReset);
			GraphicsState.mGraphicsDeviceManager.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(mGraphicsDeviceManager_PreparingDeviceSettings);
			base.TargetElapsedTime = TimeSpan.FromSeconds(0.01);
			base.Exiting += new EventHandler<EventArgs>(Main_Exiting);
			Window.AllowUserResizing = true;
			base.Window.ClientSizeChanged += new EventHandler<EventArgs>(this.OnResize);
			//PhoneApplicationService.Current.UserIdleDetectionMode = 0;
			//PhoneApplicationService.Current.Launching += new EventHandler<LaunchingEventArgs>(this.Game_Launching);
			//PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(this.Game_Activated);
			//PhoneApplicationService.Current.Closing += new EventHandler<ClosingEventArgs>(this.Current_Closing);
			//PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(this.Current_Deactivated);
			IsMouseVisible = true;
			//IsFixedTimeStep = false;
		}

        public void OnResize(object sender, EventArgs e)
        {
            int DefaultW = 800;
            int DefaultH = 480;
            Rectangle bounds = Window.ClientBounds;
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.mScreenScales.Init(bounds.Width, bounds.Height, DefaultW, DefaultH);
                Graphics.Resized();
            }
        }

        /*private void Current_Deactivated(object sender, DeactivatedEventArgs e)
        {
            GlobalStaticVars.gSexyAppBase.Tombstoned();
        }

        private void Current_Closing(object sender, ClosingEventArgs e)
        {
            PhoneApplicationService.Current.State.Clear();
        }

        private void Game_Activated(object sender, ActivatedEventArgs e)
        {
        }

        private void Game_Launching(object sender, LaunchingEventArgs e)
        {
            PhoneApplicationService.Current.State.Clear();
        }*/

        public static bool RunWhenLocked
        {
            get;
            //{
            //	return PhoneApplicationService.Current.ApplicationIdleDetectionMode == 1;
            //}
            set;
            //{
            //	try
            //	{
            //		PhoneApplicationService.Current.ApplicationIdleDetectionMode = (value ? 1 : 0);
            //	}
            //	catch
            //	{
            //	}
            //}
        }

        private static void SetupTileSchedule()
        {
        }

        private void mGraphicsDeviceManager_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
        }

        private void graphics_DeviceReset(object sender, EventArgs e)
        {
        }

        private void graphics_DeviceCreated(object sender, EventArgs e)
        {
            base.GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
        }

        private void Main_Exiting(object sender, EventArgs e)
        {
            GlobalStaticVars.gSexyAppBase.AppExit();
        }

        internal static string FetchIronPythonStdLib(Version version)
        {
            return $"./IronPython.StdLib.{version.Major}.{version.Minor}.{version.Build}.zip";
        }

        internal static void IronPythonConfigureWorkDir()
        {
        }

        protected override void Initialize()
        {
            base.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);
            Main.GamerServicesComp = new GamerServicesComponent(this);
            base.Components.Add(Main.GamerServicesComp);
            ReportAchievement.Initialise();
            LawnMod.IronPyInteractive.Serve();
            base.Initialize();
            // Window Scaling
            var f = new SharpDX.Direct2D1.Factory();
            var ww = (int)(f.DesktopDpi.Width  / 96 * 800);
            var wh = (int)(f.DesktopDpi.Height / 96 * 480);
            GlobalStaticVars.gSexyAppBase.mScreenScales.Init(ww, wh, 800, 480);
            Main.graphics.PreferredBackBufferWidth = ww;
            Main.graphics.PreferredBackBufferHeight = wh;
            GraphicsState.mGraphicsDeviceManager.ApplyChanges();
            // IME Support
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler = new MonoGame.IMEHelper.WinFormsIMEHandler(this);
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler.TextInput += (s, e) =>
            {
                Debug.OutputDebug<string>(String.Format("input:{0}", e.Character));
                GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyChar(new SexyChar(e.Character));
            };
        }

        protected override void OnExiting(object sender, EventArgs args) 
        {
            LawnMod.IronPyInteractive.Stop();		
        }

        protected override void LoadContent()
        {
            GraphicsState.Init();
            Main.SetupForResolution();
            GlobalStaticVars.initialize(this);
            GlobalStaticVars.mGlobalContent.LoadSplashScreen();
            GlobalStaticVars.gSexyAppBase.StartLoadingThread();
            mWidgetManager = GlobalStaticVars.gSexyAppBase.mWidgetManager;
        }

        protected override void UnloadContent()
        {
            GlobalStaticVars.mGlobalContent.cleanUp();
        }

        protected override void BeginRun()
        {
            base.BeginRun();
        }

        public void CompensateForSlowUpdate()
        {
            //base.ResetElapsedTime();
        }

        public static bool LOW_MEMORY_DEVICE { get; private set; }

        public static bool DO_LOW_MEMORY_OPTIONS { get; private set; }

        protected override void Update(GameTime gameTime)
        {
            if (!base.IsActive)
            {
                return;
            }
            if (GlobalStaticVars.gSexyAppBase.WantsToExit)
            {
                base.Exit();
            }
            HandleInput(gameTime);
            GlobalStaticVars.gSexyAppBase.UpdateApp();
            if (!Main.trialModeChecked)
            {
                Main.trialModeChecked = true;
                bool flag = Main.trialModeCachedValue;
                Main.SetLowMem();
                Main.trialModeCachedValue = Guide.IsTrialMode;
                if (flag != Main.trialModeCachedValue && flag)
                {
                    LeftTrialMode();
                }
            }
            try
            {
                base.Update(gameTime);
            }
            catch (GameUpdateRequiredException)
            {
                GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
            }
        }

        private static void SetLowMem()
        {
            //object obj;
            //DeviceExtendedProperties.TryGetValue("DeviceTotalMemory", ref obj);
            Main.DO_LOW_MEMORY_OPTIONS = false;//(Main.LOW_MEMORY_DEVICE = ((long)obj / 1024L / 1024L <= 256L));
            Main.LOW_MEMORY_DEVICE = false;
        }

        private void LeftTrialMode()
        {
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.LeftTrialMode();
            }
            Window_OrientationChanged(null, null);
        }

        public static void SuppressNextDraw()
        {
            Main.wantToSuppressDraw = true;
        }

        public static SignedInGamer GetGamer()
        {
            if (Gamer.SignedInGamers.Count == 0)
            {
                return null;
            }
            return Gamer.SignedInGamers[PlayerIndex.One];
        }

        public static void NeedToSetUpOrientationMatrix(UI_ORIENTATION orientation)
        {
            Main.orientationUsed = orientation;
            Main.newOrientation = true;
        }

        private static void SetupOrientationMatrix(UI_ORIENTATION orientation)
        {
            Main.newOrientation = false;
        }

        private void Window_OrientationChanged(object sender, EventArgs e)
        {
            SetupInterfaceOrientation();
        }

        private void SetupInterfaceOrientation()
        {
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                if (base.Window.CurrentOrientation == DisplayOrientation.LandscapeLeft || base.Window.CurrentOrientation == DisplayOrientation.LandscapeRight)
                {
                    GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT);
                    return;
                }
                GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_PORTRAIT);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (Main.newOrientation)
            {
                Main.SetupOrientationMatrix(Main.orientationUsed);
            }
            lock (ResourceManager.DrawLocker)
            {
                base.GraphicsDevice.Clear(Color.Black);
                GlobalStaticVars.gSexyAppBase.DrawGame(gameTime);
                base.Draw(gameTime);
            }
        }

        public void HandleInput(GameTime gameTime)
        {
            if (LoadingScreen.IsLoading)
            {
                return;
            }
            MouseState msstate = Mouse.GetState();
            _Touch mstouch = default(_Touch);
            ScreenScales s = GlobalStaticVars.gSexyAppBase.mScreenScales;
            mstouch.location = s.InvMapTouch(new CGPoint(msstate.Position.X, msstate.Position.Y));
            if (msstate.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                mWidgetManager.MouseDown((int)mstouch.location.x, (int)mstouch.location.y, 1);
            }
            else if (msstate.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                mWidgetManager.MouseUp((int)mstouch.location.x, (int)mstouch.location.y, 1);
            }
            if (msstate.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                mWidgetManager.MouseDown((int)mstouch.location.x, (int)mstouch.location.y, -1);
            }
            else if (msstate.RightButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                mWidgetManager.MouseUp((int)mstouch.location.x, (int)mstouch.location.y, -1);
            }
            if (msstate.ScrollWheelValue != previousMouseState.ScrollWheelValue) 
            {
                mWidgetManager.MouseWheel(msstate.ScrollWheelValue - previousMouseState.ScrollWheelValue);
            }

            if (previousMouseState.Position != msstate.Position)
            {
                mWidgetManager.MouseMove((int)mstouch.location.x, (int)mstouch.location.y);
            }



            GamePadState state = GamePad.GetState(PlayerIndex.One);
            if (state.Buttons.Back == ButtonState.Pressed && previousGamepadState.Buttons.Back == ButtonState.Released)
            {
                GlobalStaticVars.gSexyAppBase.BackButtonPress();
            }
            TouchCollection state2 = TouchPanel.GetState();
            bool flag = false;
            foreach (TouchLocation touchLocation in state2)
            {
                _Touch touch = default(_Touch);
                touch.location.mX = s.InvMapTouchX(touchLocation.Position.X);
                touch.location.mY = s.InvMapTouchY(touchLocation.Position.Y);
                TouchLocation touchLocation2;
                if (touchLocation.TryGetPreviousLocation(out touchLocation2))
                {
                    touch.previousLocation = new CGPoint(touchLocation2.Position.X, touchLocation2.Position.Y);
                }
                else
                {
                    touch.previousLocation = touch.location;
                }
                touch.timestamp = gameTime.TotalGameTime.TotalSeconds;
                if (touchLocation.State == TouchLocationState.Pressed && !flag)
                {
                    GlobalStaticVars.gSexyAppBase.TouchBegan(touch);
                    flag = true;
                }
                else if (touchLocation.State == TouchLocationState.Moved)
                {
                    GlobalStaticVars.gSexyAppBase.TouchMoved(touch);
                }
                else if (touchLocation.State == TouchLocationState.Released)
                {
                    GlobalStaticVars.gSexyAppBase.TouchEnded(touch);
                }
                else if (touchLocation.State == TouchLocationState.Invalid)
                {
                    GlobalStaticVars.gSexyAppBase.TouchesCanceled();
                }
            }
            
            List<string> keynames = new List<string>();
            KeyboardState keys = Keyboard.GetState();
            foreach (Keys it in keys.GetPressedKeys())
            {
                if (previousKeyboardState.IsKeyUp(it))
                {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyDown((KeyCode)it);
                    keynames.Add(it.ToString());
                }
            }
            foreach (Keys it in previousKeyboardState.GetPressedKeys())
            {
                if (keys.IsKeyUp(it))
                {
                    GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyUp((KeyCode)it);
                } 
            }

            previousGamepadState = state;
            previousMouseState = msstate;
            previousKeyboardState = keys;
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            Main.trialModeChecked = false;
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.GotFocus();
                if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
                {
                    GlobalStaticVars.gSexyAppBase.mMusicInterface.ResumeMusic();
                }
            }
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            GlobalStaticVars.gSexyAppBase.LostFocus();
            if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
            {
                GlobalStaticVars.gSexyAppBase.mMusicInterface.PauseMusic();
            }
            GlobalStaticVars.gSexyAppBase.AppEnteredBackground();
            base.OnDeactivated(sender, args);
        }

        public static bool IsInTrialMode
        {
            get
            {
                return Main.trialModeCachedValue;
            }
        }

        private void GameSpecificCheatInputCheck()
        {
        }

        private static void SetupForResolution()
        {
            Strings.Culture = CultureInfo.CurrentCulture;
            if (Strings.Culture.TwoLetterISOLanguageName == "fr")
            {
                Constants.Language = Constants.LanguageIndex.fr;
            }
            else if (Strings.Culture.TwoLetterISOLanguageName == "de")
            {
                Constants.Language = Constants.LanguageIndex.de;
            }
            else if (Strings.Culture.TwoLetterISOLanguageName == "es")
            {
                Constants.Language = Constants.LanguageIndex.es;
            }
            else if (Strings.Culture.TwoLetterISOLanguageName == "it")
            {
                Constants.Language = Constants.LanguageIndex.it;
            }
            else
            {
                Constants.Language = Constants.LanguageIndex.en;
            }
            //if ((Main.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth == 480 && Main.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight == 800) || (Main.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth == 800 && Main.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight == 480))
            //{
                AtlasResources.mAtlasResources = new AtlasResources_480x800();
                Constants.Load480x800();
                return;
            //}
            throw new Exception("Unsupported Resolution");
        }

        private static SexyTransform2D orientationTransform;

        private static UI_ORIENTATION orientationUsed;

        private static bool newOrientation;

        public static GamerServicesComponent GamerServicesComp;

        public static bool trialModeChecked = false;

        private static bool trialModeCachedValue = true;

        internal static Graphics graphics;

        private int mFrameCnt;

        private static bool startedProfiler;

        private static bool wantToSuppressDraw;

        private GamePadState previousGamepadState = default(GamePadState);
        private MouseState previousMouseState = default(MouseState);
        private KeyboardState previousKeyboardState;

        private WidgetManager mWidgetManager;
    }
}
