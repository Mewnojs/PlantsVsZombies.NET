using System;
using System.Globalization;
using System.Diagnostics;
using Lawn;
//using Microsoft.Phone.Info;
//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Sexy
{
    public class Main : Game
    {
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
            {
                AtlasResources.mAtlasResources = new AtlasResources_480x800();
                //Constants.Load1080x1920();

                //Constants.LoadVirtual_480x800();
                Constants.Load480x800();
                return;
            }
            throw new Exception("Unsupported Resolution");
        }

        public Main()
        {
            Main.SetupTileSchedule();
            Main.graphics = Graphics.GetNew(this);
            //Main.SetLowMem();
            Main.graphics.IsFullScreen = true;
            Guide.SimulateTrialMode = false;

            //GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = 800;
            //GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = 480;

            GraphicsState.mGraphicsDeviceManager.SupportedOrientations = Constants.SupportedOrientations;
            GraphicsState.mGraphicsDeviceManager.DeviceCreated += new EventHandler<EventArgs>(this.graphics_DeviceCreated);
            GraphicsState.mGraphicsDeviceManager.DeviceReset += new EventHandler<EventArgs>(this.graphics_DeviceReset);
            GraphicsState.mGraphicsDeviceManager.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(this.mGraphicsDeviceManager_PreparingDeviceSettings);
            base.TargetElapsedTime = TimeSpan.FromSeconds(1.0/30);
            base.Exiting += new EventHandler<EventArgs>(this.Main_Exiting);

            //base.Window.AllowUserResizing = true;
            base.Window.ClientSizeChanged += new EventHandler<EventArgs>(this.OnResize);

            //PhoneApplicationService.Current.UserIdleDetectionMode = 0;
            //PhoneApplicationService.Current.Launching += new EventHandler<LaunchingEventArgs>(this.Game_Launching);
            //PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(this.Game_Activated);
            //PhoneApplicationService.Current.Closing += new EventHandler<ClosingEventArgs>(this.Current_Closing);
            //PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(this.Current_Deactivated);
        }

        internal static string FetchIronPythonStdLib(Version version)
        {
            return GlobalStaticVars.gPvZActivity.GetIronPythonStdLibPath(version);

        }

        internal static void IronPythonConfigureWorkDir()
        {
            GlobalStaticVars.gPvZActivity.ConfigureWorkDirAsLocalData();

        }

        //private void Current_Deactivated(object sender, DeactivatedEventArgs e)
        //{
        //	GlobalStaticVars.gSexyAppBase.Tombstoned();
        //}


        //private void Current_Closing(object sender, ClosingEventArgs e)
        //{
        //	//PhoneApplicationService.Current.State.Clear();
        //}


        //private void Game_Activated(object sender, ActivatedEventArgs e)
        //{
        //}


        //private void Game_Launching(object sender, LaunchingEventArgs e)
        //{
        //	PhoneApplicationService.Current.State.Clear();
        //}

        public void OnResize(object sender, EventArgs e)
        {
            int DefaultW = 800;
            int DefaultH = 480;
            Rectangle bounds = Window.ClientBounds;
            int W, H;
            if ((bounds.Height * DefaultW) >= (bounds.Width * DefaultH))
            {
                W = bounds.Width;
                H = (int)(DefaultH * bounds.Width / DefaultW);
            }
            else
            {
                W = (int)(DefaultW * bounds.Height / DefaultH);
                H = bounds.Height;
            }
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.mScreenScales.Init(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, DefaultW, DefaultH);
                Graphics.Resized();
            }

            GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            GraphicsState.mGraphicsDeviceManager.ApplyChanges();
        }

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

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Window.OrientationChanged += new EventHandler<EventArgs>(this.Window_OrientationChanged);
            ReportAchievement.Initialise();
            base.Initialize();
#if !DEBUG
            try
            {
#endif
                LawnMod.IronPyInteractive.Serve();
#if !DEBUG
            }
            catch (Exception e)
            {
                Sexy.GlobalStaticVars.gPvZActivity.OnException(null, e);
            }
#endif
            // Window scaling
            GlobalStaticVars.gSexyAppBase.mScreenScales.Init(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, 800, 480);
            Main.graphics.PreferredBackBufferWidth = 800;
            Main.graphics.PreferredBackBufferHeight = 480;
            GraphicsState.mGraphicsDeviceManager.ApplyChanges();
            // IME Support
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler = new MonoGame.IMEHelper.AndroidIMEHandler(this);
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler.TextInput += (s, e) =>
            {
                if (e.Character >= ' ')
                {
                    GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyChar(new SexyChar(e.Character));
                }
                else 
                {
                    GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyDown((KeyCode)e.Key);
                    GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyUp((KeyCode)e.Key);
                }
            };

        }

        protected override void LoadContent()
        {
            GraphicsState.Init();
            Main.SetupForResolution();
            GlobalStaticVars.initialize(this);
            GlobalStaticVars.mGlobalContent.LoadSplashScreen();
            GlobalStaticVars.gSexyAppBase.StartLoadingThread();
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
            if (_gameTimer != null)
            {
                _gameTimer.Reset();
                _gameTimer.Start();
            }
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
                GlobalStaticVars.gSexyAppBase.WantsToExit = false;
                Main_Exiting(null, null);
                base.Exit();
                Activity.Finish();
                Process.GetCurrentProcess().Kill();
            }
            this.HandleInput(gameTime);
            GlobalStaticVars.gSexyAppBase.UpdateApp();
            if (!Main.trialModeChecked)
            {
                Main.trialModeChecked = true;
                bool flag = Main.trialModeCachedValue;
                //Main.SetLowMem();
                Main.trialModeCachedValue = false; // Guide.IsTrialMode;
                if (flag != Main.trialModeCachedValue && flag)
                {
                    this.LeftTrialMode();
                }
            }

            base.Update(gameTime);
        }


        //private static void SetLowMem()
        //{
        //	object obj;
        //	DeviceExtendedProperties.TryGetValue("DeviceTotalMemory", ref obj);
        //	Main.DO_LOW_MEMORY_OPTIONS = (Main.LOW_MEMORY_DEVICE = ((long)obj / 1024L / 1024L <= 256L));
        //	Main.LOW_MEMORY_DEVICE = false;
        //}

        private void LeftTrialMode()
        {
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.LeftTrialMode();
            }
            this.Window_OrientationChanged(null, null);
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
            this.SetupInterfaceOrientation();
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
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            if (state.Buttons.Back == ButtonState.Pressed && this.previousGamepadState.Buttons.Back == ButtonState.Released)
            {
                GlobalStaticVars.gSexyAppBase.BackButtonPress();
            }
            TouchCollection state2 = TouchPanel.GetState();




            ScreenScales s = GlobalStaticVars.gSexyAppBase.mScreenScales;
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
            

            this.previousGamepadState = state;
            
        }
        

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            //GlobalStaticVars.gSexyAppBase.KeyChar(new SexyChar(e.Character));

            //this.Window.Title = builder.ToString();
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
            if (GlobalStaticVars.gSexyAppBase != null)//
            {//
                GlobalStaticVars.gSexyAppBase.LostFocus();
                if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
                {
                    GlobalStaticVars.gSexyAppBase.mMusicInterface.PauseMusic();
                }
                GlobalStaticVars.gSexyAppBase.AppEnteredBackground();
                base.OnDeactivated(sender, args);
            }//
        }

        public static bool IsInTrialMode
        {
            get
            {
                return Main.trialModeCachedValue;
            }
        }

        private static SexyTransform2D orientationTransform;

        private static UI_ORIENTATION orientationUsed;

        private static bool newOrientation;

        public static bool trialModeChecked = false;

        private static bool trialModeCachedValue = true;

        internal static Graphics graphics;

        private int mFrameCnt;

        private static bool startedProfiler;

        private static bool wantToSuppressDraw;

        private GamePadState previousGamepadState = default(GamePadState);

        private MouseState previousMouseState = default(MouseState);

        private KeyboardState previousKeyboardState = default(KeyboardState);

        public static GamerServicesComponent GamerServicesComp;

        private Stopwatch _gameTimer = new Stopwatch();

    }
}
namespace Sexy
{
    public static partial class GlobalStaticVars
    {
        public static Lawn_Android.PvZActivity gPvZActivity;
    }
}