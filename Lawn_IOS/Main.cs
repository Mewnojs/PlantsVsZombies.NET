using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Globalization;
using System;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.GamerServices;
using Lawn;
using System.Collections.Generic;
using Foundation;

namespace Sexy
{
    public class Main : Game
    {
        //private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;

        public static Main Instance;

        private static void SetupForResolution()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName == "fr")
            {
                Constants.Language = Constants.LanguageIndex.fr;
            }
            else if (cultureInfo.TwoLetterISOLanguageName == "de")
            {
                Constants.Language = Constants.LanguageIndex.de;
            }
            else if (cultureInfo.TwoLetterISOLanguageName == "es")
            {
                Constants.Language = Constants.LanguageIndex.es;
            }
            else if (cultureInfo.TwoLetterISOLanguageName == "it")
            {
                Constants.Language = Constants.LanguageIndex.it;
            }
            //else if (cultureInfo.TwoLetterISOLanguageName == "zh" && new RegionInfo(cultureInfo.Name).TwoLetterISORegionName == "CN")
            //{
            //    Constants.Language = Constants.LanguageIndex.zh_cn;
            //}
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
            Instance = this;
            //_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            applicationStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Main.SetupTileSchedule();
            Main.graphics = Graphics.GetNew(this);
            // Main.SetLowMem();
            Main.graphics.IsFullScreen = false;
            Guide.SimulateTrialMode = false;
            // Main.graphics.PreferredBackBufferWidth = 800;
            // Main.graphics.PreferredBackBufferHeight = 480;
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
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        }

        public void CompensateForSlowUpdate()
        {

        }

        public void OnResize(object sender, EventArgs e)
        {
            int DefaultW = Constants.BackBufferSize.Y;
            int DefaultH = Constants.BackBufferSize.X;
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
            var newWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var newHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            if (bounds.Width == newWidth && bounds.Height == newHeight)
            {
                //return;
            }
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.mScreenScales.Init(newWidth, newHeight, DefaultW, DefaultH);
                Graphics.Resized();
            }
            GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = newWidth;
            GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = newHeight;
            GraphicsState.mGraphicsDeviceManager.ApplyChanges();
            {
                // = GraphicsState.mGraphicsDeviceManager.UpdateTouchPanel();
                GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth = newWidth;
                GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight = newHeight;
                //TouchPanel.DisplayWidth = newWidth;
                //TouchPanel.DisplayHeight = newHeight;
                touchPanelNeedsUpdate = true;
            }
        }

        private bool touchPanelNeedsUpdate = true;

        public static bool RunWhenLocked { get; set; }

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

        internal string FetchApplicationStoragePath()
        {
            return applicationStoragePath;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);
            Main.GamerServicesComp = new GamerServicesComponent(this);
            base.Components.Add(Main.GamerServicesComp);
            ReportAchievement.Initialise();
            base.Initialize();
            // Window initialization
            int ww = Constants.BackBufferSize.Y;
            int wh = Constants.BackBufferSize.X;
            GlobalStaticVars.gSexyAppBase.mScreenScales.Init(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, Constants.BackBufferSize.Y, Constants.BackBufferSize.X);
            Main.graphics.PreferredBackBufferWidth = ww;
            Main.graphics.PreferredBackBufferHeight = wh;
            GraphicsState.mGraphicsDeviceManager.ApplyChanges();
            //
            // IME Support
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler = new MonoGame.IMEHelper.IosIMEHandler(this);
            GlobalStaticVars.gSexyAppBase.mWidgetManager.mIMEHandler.TextInput += (s, e) =>
            {
                Debug.OutputDebug<string>(String.Format("input:{0}", e.Character));
                GlobalStaticVars.gSexyAppBase.mWidgetManager.KeyChar(new SexyChar(e.Character));
            };
        }

        protected override void LoadContent()
        {
            //_spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsState.Init();
            Main.SetupForResolution();
            GlobalStaticVars.initialize(this);
            GlobalStaticVars.mGlobalContent.LoadSplashScreen();
            GlobalStaticVars.gSexyAppBase.StartLoadingThread();
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            GlobalStaticVars.mGlobalContent.cleanUp();
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
                throw new Exception("Exit Game");
            }
            HandleInput(gameTime);
            GlobalStaticVars.gSexyAppBase.UpdateApp();
            if (!Main.trialModeChecked)
            {
                Main.trialModeChecked = true;
                bool flag = Main.trialModeCachedValue;
                // Main.SetLowMem();
                Main.trialModeCachedValue = false; // Guide.IsTrialMode;
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

        //private static void SetLowMem()
        //{
        //    //object obj;
        //    //DeviceExtendedProperties.TryGetValue("DeviceTotalMemory", ref obj);
        //    Main.DO_LOW_MEMORY_OPTIONS = false;//(Main.LOW_MEMORY_DEVICE = ((long)obj / 1024L / 1024L <= 256L));
        //    Main.LOW_MEMORY_DEVICE = false;
        //}

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
            if (touchPanelNeedsUpdate)
            {
                TouchPanel.DisplayWidth = GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
                TouchPanel.DisplayHeight = GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight;
                touchPanelNeedsUpdate = false;
            }
            MouseState msstate = Mouse.GetState();
            _Touch mstouch = default(_Touch);
            // mstouch.location = new CGPoint(msstate.Position.X, msstate.Position.Y);
            ScreenScales s = GlobalStaticVars.gSexyAppBase.mScreenScales;
            mstouch.location = s.InvMapTouch(new CGPoint(msstate.Position.X, msstate.Position.Y));
            if (msstate.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseDown((int)mstouch.location.x, (int)mstouch.location.y, 1);
            }
            else if (msstate.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseUp((int)mstouch.location.x, (int)mstouch.location.y, 1);
            }
            if (msstate.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseDown((int)mstouch.location.x, (int)mstouch.location.y, -1);
            }
            else if (msstate.RightButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseUp((int)mstouch.location.x, (int)mstouch.location.y, -1);
            }
            if (msstate.ScrollWheelValue != previousMouseState.ScrollWheelValue)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseWheel(msstate.ScrollWheelValue - previousMouseState.ScrollWheelValue);
            }
            if (previousMouseState.Position != msstate.Position)
            {
                GlobalStaticVars.gSexyAppBase.mWidgetManager.MouseMove((int)mstouch.location.x, (int)mstouch.location.y);
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
                //touch.location.mX = touchLocation.Position.X;
                //touch.location.mY = touchLocation.Position.Y;
                touch.location.mX = s.InvMapTouchX(touchLocation.Position.X);
                touch.location.mY = s.InvMapTouchY(touchLocation.Position.Y);
                TouchLocation touchLocation2;
                if (touchLocation.TryGetPreviousLocation(out touchLocation2))
                {
                    touch.previousLocation = s.InvMapTouch(new CGPoint(touchLocation2.Position.X, touchLocation2.Position.Y));
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
            previousKeyboardState = keys;
            previousMouseState = msstate;
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
            if (GlobalStaticVars.gSexyAppBase != null)
            {
                GlobalStaticVars.gSexyAppBase.LostFocus();
                if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
                {
                    GlobalStaticVars.gSexyAppBase.mMusicInterface.PauseMusic();
                }
                GlobalStaticVars.gSexyAppBase.AppEnteredBackground();
            }
            base.OnDeactivated(sender, args);
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

        public static GamerServicesComponent GamerServicesComp;

        public static bool trialModeChecked = false;

        private static bool trialModeCachedValue = true;

        internal static Graphics graphics;

        private int mFrameCnt;

        private static bool startedProfiler;

        private static bool wantToSuppressDraw;

        private GamePadState previousGamepadState = default(GamePadState);
        private MouseState previousMouseState = default(MouseState);
        private KeyboardState previousKeyboardState = default(KeyboardState);

        public string applicationStoragePath;
    }
}
