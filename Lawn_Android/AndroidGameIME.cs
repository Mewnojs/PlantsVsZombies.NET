using Android.Views;
using Android.Text;
using Android.Views.InputMethods;
using AG = Android.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Widget;
using Android.App;
using Android.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.IMEHelper
{
    public class AndroidGameActivityIME : AndroidGameActivity, ViewTreeObserver.IOnGlobalLayoutListener
    {
        public static AndroidGameActivityIME Instance;

        public AndroidGameActivityIME()
        {
            Instance = this;
        }

        private AG.Point _ScreenSize = new AG.Point();
        public AG.Point ScreenSize
        {
            get { return _ScreenSize; }
            private set { _ScreenSize = value; }
        }
        public Rect _KeyboardRect = new Rect();
        public Rect KeyboardRect
        {
            get { return _KeyboardRect; }
            private set { _KeyboardRect = value; }
        }
        public int KeyboardHeight { get { return KeyboardRect.Height(); } }

        public virtual void OnGlobalLayout()
        {
            WindowManager.DefaultDisplay.GetSize(ScreenSize);
            Window.DecorView.GetWindowVisibleDisplayFrame(KeyboardRect);
        }
    }

    public class AndroidIMEHandler : IMEHandler
    {
        private const int IME_FLAG_NO_EXTRACT_UI = 0x10000000;

        private AndroidGameActivityIME GameActivityIME { get { return AndroidGameActivityIME.Instance; } }
        private EditText editText;
        private InputMethodManager inputMethodManager;

        private AG.Point ScreenSize { get { return GameActivityIME.ScreenSize; } }
        private Rect KeyboardRect { get { return GameActivityIME.KeyboardRect; } }

        public override int VirtualKeyboardHeight { get { return GameActivityIME.KeyboardHeight; } }

        public AndroidIMEHandler(Game game, bool showDefaultIMEWindow = false) : base(game, showDefaultIMEWindow)
        {
        }

        public override bool Enabled { get; protected set; }

        public override void PlatformInitialize()
        {
            if (GameActivityIME == null)
                throw new System.Exception("You have to change Activity's base class to AndroidGameActivityIME before initializing IMEHandler.");

            inputMethodManager = (InputMethodManager)GameActivityIME.GetSystemService(Activity.InputMethodService);

            editText = new EditText(GameActivityIME);
            editText.SetMaxLines(1);
            editText.InputType = InputTypes.ClassText;
            editText.ImeOptions = (ImeAction)((int)(ImeAction.Done) | IME_FLAG_NO_EXTRACT_UI);
            editText.SetBackgroundColor(AG.Color.Transparent);

            var layoutParams = new RelativeLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            //layoutParams.TopMargin = -200; // Move editText view off from screen.
            GameActivityIME.AddContentView(editText, layoutParams);
            editText.SetY(-300);

            GameActivityIME.CurrentFocus.ViewTreeObserver.AddOnGlobalLayoutListener(GameActivityIME);
            editText.TextChanged += EditText_TextChanged;
            editText.KeyPress += EditText_KeyPress;
        }

        private void EditText_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Del)
            {
                var key = Keys.Back;
                OnTextInput(new TextInputEventArgs((char)key, key));
            }
        }

        private void EditText_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var c in e.Text)
                OnTextInput(new TextInputEventArgs(c, KeyboardUtil.ToXna(c)));

            editText.TextChanged -= EditText_TextChanged;
            editText.Text = string.Empty;
            editText.TextChanged += EditText_TextChanged;
        }

        public override void StartTextComposition()
        {
            if (Enabled)
                return;

            editText.RequestFocus();
            inputMethodManager.ShowSoftInput(editText, ShowFlags.Implicit);
            Enabled = true;
        }

        public override void StopTextComposition()
        {
            if (!Enabled)
                return;

            inputMethodManager.HideSoftInputFromWindow(GameActivityIME.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
            Enabled = false;
        }

        const int KeyboardHideOffset = 80;

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touchCollection)
            {
                if (TouchLocationState.Pressed == touchLocation.State)
                {
                    if (touchLocation.Position.Y < ((ScreenSize.Y - VirtualKeyboardHeight) - KeyboardHideOffset))
                        StopTextComposition();
                }
            }
        }
    }
}
