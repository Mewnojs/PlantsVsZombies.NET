using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Sexy.TodLib;

namespace Sexy
{
	internal class EditWidget : Widget
	{
		public EditWidget(int theId, EditListener theEditListener, string title, string description)
		{
			mTitle = TodStringFile.TodStringTranslate(title);
			mDescription = TodStringFile.TodStringTranslate(description);
			mId = theId;
			mEditListener = theEditListener;
			mFont = null;
			mMaxChars = -1;
			mPasswordChar = " ";
			mEditing = false;
			mAcceptsEmptyText = false;
			mFont = new Font();
			SetColors(EditWidget.gEditWidgetColors, 5);
		}

		protected string GetDisplayString()
		{
			if (mPasswordChar == " ")
			{
				return mString;
			}
			if (mPasswordDisplayString.Length != mString.Length)
			{
				mPasswordDisplayString = mPasswordDisplayString + mString.Length.ToString() + mPasswordChar;
			}
			return mPasswordDisplayString;
		}

		public virtual void SetFont(Font theFont)
		{
			mFont.Dispose();
			mFont = theFont.Duplicate();
		}

		public virtual void SetText(string theText)
		{
			mString = theText;
			MarkDirty();
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			RehupBounds();
		}

		public override void Draw(Graphics g)
		{
		}

		public override void Update()
		{
			if (callbackDone)
			{
				DoKeyboardCallback();
			}
			base.Update();
		}

		public override bool WantsFocus()
		{
			return true;
		}

		public override void GotFocus()
		{
			base.GotFocus();
			if (!Guide.IsVisible)
			{
				Guide.BeginShowKeyboardInput(PlayerIndex.One, mTitle, mDescription, mString, new AsyncCallback(KeyboardCallback), null);
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			base.KeyChar(theChar);
		}

		private void KeyboardCallback(IAsyncResult result)
		{
			string text = Guide.EndShowKeyboardInput(result);
			inputCancelled = (text == null);
			if (!inputCancelled)
			{
				mString = text;
			}
			callbackDone = true;
		}

		private void DoKeyboardCallback()
		{
			callbackDone = false;
			if (mString == null)
			{
				LostFocus();
				EditingEnded(mString);
				return;
			}
			EditingEnded(mString);
		}

		public override void LostFocus()
		{
			base.LostFocus();
		}

		public virtual void RehupBounds()
		{
		}

		public virtual void EditingEnded(string theString)
		{
			mEditing = false;
			mString = theString;
			mEditListener.EditWidgetText(mId + (inputCancelled ? 1000 : 0), mString);
		}

		public virtual bool ShouldChangeCharacters(int theRangeStart, int theRangeLength, string theReplacementChars)
		{
			return true;
		}

		public virtual bool ShouldClear()
		{
			bool flag = mEditListener.ShouldClear();
			if (flag)
			{
				mString = "";
			}
			return flag;
		}

		public override void Dispose()
		{
			mFont = null;
		}

		internal static int[,] gEditWidgetColors = new int[,]
		{
			{
				0,
				0,
				0
			},
			{
				0,
				0,
				0
			},
			{
				250,
				250,
				250
			},
			{
				0,
				0,
				0
			},
			{
				255,
				255,
				255
			}
		};

		public int mId;

		public string mString;

		public string mPasswordDisplayString;

		public Font mFont;

		public EditListener mEditListener;

		public int mMaxChars;

		public string mPasswordChar;

		public bool mEditing;

		public bool mAcceptsEmptyText;

		public string mTitle;

		public string mDescription;

		private bool callbackDone;

		private bool inputCancelled;

		public enum Colors
		{
			COLOR_BKG,
			COLOR_OUTLINE,
			COLOR_TEXT,
			COLOR_HILITE,
			COLOR_HILITE_TEXT,
			NUM_COLORS
		}
	}
}
