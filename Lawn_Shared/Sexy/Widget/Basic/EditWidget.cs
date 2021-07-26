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
			this.mTitle = TodStringFile.TodStringTranslate(title);
			this.mDescription = TodStringFile.TodStringTranslate(description);
			this.mId = theId;
			this.mEditListener = theEditListener;
			this.mFont = null;
			this.mMaxChars = -1;
			this.mPasswordChar = " ";
			this.mEditing = false;
			this.mAcceptsEmptyText = false;
			this.mFont = new Font();
			this.SetColors(EditWidget.gEditWidgetColors, 5);
		}

		protected string GetDisplayString()
		{
			if (this.mPasswordChar == " ")
			{
				return this.mString;
			}
			if (this.mPasswordDisplayString.Length != this.mString.Length)
			{
				this.mPasswordDisplayString = this.mPasswordDisplayString + this.mString.Length.ToString() + this.mPasswordChar;
			}
			return this.mPasswordDisplayString;
		}

		public virtual void SetFont(Font theFont)
		{
			this.mFont.Dispose();
			this.mFont = theFont.Duplicate();
		}

		public virtual void SetText(string theText)
		{
			this.mString = theText;
			this.MarkDirty();
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.RehupBounds();
		}

		public override void Draw(Graphics g)
		{
		}

		public override void Update()
		{
			if (this.callbackDone)
			{
				this.DoKeyboardCallback();
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
				Guide.BeginShowKeyboardInput(PlayerIndex.One, this.mTitle, this.mDescription, this.mString, new AsyncCallback(this.KeyboardCallback), null);
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			base.KeyChar(theChar);
		}

		private void KeyboardCallback(IAsyncResult result)
		{
			string text = Guide.EndShowKeyboardInput(result);
			this.inputCancelled = (text == null);
			if (!this.inputCancelled)
			{
				this.mString = text;
			}
			this.callbackDone = true;
		}

		private void DoKeyboardCallback()
		{
			this.callbackDone = false;
			if (this.mString == null)
			{
				this.LostFocus();
				this.EditingEnded(this.mString);
				return;
			}
			this.EditingEnded(this.mString);
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
			this.mEditing = false;
			this.mString = theString;
			this.mEditListener.EditWidgetText(this.mId + (this.inputCancelled ? 1000 : 0), this.mString);
		}

		public virtual bool ShouldChangeCharacters(int theRangeStart, int theRangeLength, string theReplacementChars)
		{
			return true;
		}

		public virtual bool ShouldClear()
		{
			bool flag = this.mEditListener.ShouldClear();
			if (flag)
			{
				this.mString = "";
			}
			return flag;
		}

		public override void Dispose()
		{
			this.mFont = null;
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
