using CoreGraphics;
using Foundation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using UIKit;

namespace MonoGame.IMEHelper
{
    internal class UIBackwardsTextField : UITextField
    {
        // A delegate type for hooking up change notifications.
        public delegate void DeleteBackwardEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event DeleteBackwardEventHandler DeleteBackwardPressed;
        public event EventHandler TextChanged;

        private bool UIBackwardsTextField_ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            if (textField.IsFirstResponder)
            {
                if (textField.TextInputMode == null || textField.TextInputMode.PrimaryLanguage == "emoji")
                    return false;
            }

            return true;
        }

        public UIBackwardsTextField(CGRect rect) : base(rect)
        {
            this.EditingChanged += UIBackwardsTextField_EditingChanged;
            this.ShouldChangeCharacters += UIBackwardsTextField_ShouldChangeCharacters;
        }

        private void UIBackwardsTextField_EditingChanged(object sender, EventArgs e)
        {
            if (MarkedTextRange == null || MarkedTextRange.IsEmpty)
            {
                if (TextChanged != null)
                    TextChanged(null, null);
            }
        }

        public void OnDeleteBackwardPressed()
        {
            if (DeleteBackwardPressed != null)
                DeleteBackwardPressed(null, null);
        }

        public override void DeleteBackward()
        {
            base.DeleteBackward();
            OnDeleteBackwardPressed();
        }
    }

    public class IosIMEHandler : IMEHandler
    {
        private UIWindow mainWindow;
        private UIViewController gameViewController;

        private UIBackwardsTextField textField;

        private int _virtualKeyboardHeight;

        public IosIMEHandler(Game game, bool showDefaultIMEWindow = false) : base(game, showDefaultIMEWindow)
        {
        }

        public override bool Enabled { get; protected set; }

        private bool TextField_ShouldReturn(UITextField textfield)
        {
            // Not found, so remove keyboard.
            textfield.ResignFirstResponder();

            // We do not want UITextField to insert line-breaks.
            return false;
        }

        public override void PlatformInitialize()
        {
            mainWindow = GameInstance.Services.GetService<UIWindow>();
            gameViewController = GameInstance.Services.GetService<UIViewController>();

            textField = new UIBackwardsTextField(new CGRect(0, -400, 200, 40));
            textField.KeyboardType = UIKeyboardType.Default;
            textField.ReturnKeyType = UIReturnKeyType.Done;
            textField.DeleteBackwardPressed += TextField_DeleteBackward;
            textField.TextChanged += TextField_TextChanged; ;
            textField.ShouldReturn += TextField_ShouldReturn;

            gameViewController.Add(textField);

            UIKeyboard.Notifications.ObserveWillShow((s, e) =>
            {
                _virtualKeyboardHeight = (int)e.FrameBegin.Height;
            });

            UIKeyboard.Notifications.ObserveWillHide((s, e) =>
            {
                _virtualKeyboardHeight = 0;
            });
        }

        private void TextField_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in textField.Text)
                OnTextInput(new TextInputEventArgs(c, KeyboardUtil.ToXna(c)));

            textField.Text = string.Empty;
        }

        private void TextField_DeleteBackward(object sender, EventArgs e)
        {
            var key = Keys.Back;
            OnTextInput(new TextInputEventArgs((char)key, key));
        }

        public override void StartTextComposition()
        {
            if (Enabled)
                return;

            textField.BecomeFirstResponder();
            Enabled = true;
        }

        public override void StopTextComposition()
        {
            if (!Enabled)
                return;

            textField.Text = string.Empty;
            textField.ResignFirstResponder();
            Enabled = false;
        }

        public override int VirtualKeyboardHeight { get { return _virtualKeyboardHeight; } }

        const int KeyboardHideOffset = 80;

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touchCollection)
            {
                if (TouchLocationState.Pressed == touchLocation.State)
                {
                    if (touchLocation.Position.Y < ((mainWindow.Frame.Y - _virtualKeyboardHeight) - KeyboardHideOffset))
                        StopTextComposition();
                }
            }
        }
    }
}