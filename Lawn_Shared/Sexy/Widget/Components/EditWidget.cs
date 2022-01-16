using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Sexy.TodLib;

namespace Sexy
{
    public class IMECompatibleWidget : Widget 
    {
        public bool mIMEEnabled;
        public TRect mIMEHotArea 
        {
            get 
            {
                return new TRect(mX, mY + mHeight, mWidth, mHeight);
            }
        }

        public int mOldY;
    }

	internal class EditWidget : IMECompatibleWidget
    {
		public EditWidget(int theId, EditListener theEditListener, string title, string description)
		{
			mId = theId;
			mEditListener = theEditListener;
			mFont = null;

			mHadDoubleClick = false;
			mHilitePos = -1;
			mLastModifyIdx = -1;
			mLeftPos = 0;
			mUndoCursor = 0;
			mUndoHilitePos = 0;
			mLastModifyIdx = 0;
			mBlinkAcc = 0;
			mCursorPos = 0;
			mShowingCursor = false;
			mDrawSelOverride = false;
			mMaxChars = -1;
			mMaxPixels = -1;
			mPasswordChar = new SexyChar('\0');
			mBlinkDelay = 40;
			mTitle = TodStringFile.TodStringTranslate(title);
			mDescription = TodStringFile.TodStringTranslate(description);
			mEditing = false;
			mAcceptsEmptyText = false;
			mFont = new Font();
			SetColors(EditWidget.gEditWidgetColors, 5);
            mString = "";
		}

		protected virtual void ProcessKey(KeyCode theKey, SexyChar theChar)
		{
			bool shiftDown = mWidgetManager.mKeyDown[(int)KeyCode.KEYCODE_SHIFT];
			bool controlDown = mWidgetManager.mKeyDown[(int)KeyCode.KEYCODE_CONTROL];

			if ((theKey == KeyCode.KEYCODE_SHIFT) || (theKey == KeyCode.KEYCODE_CONTROL))
			{
				return;
			}

			bool bigChange = false;
			bool removeHilite = !shiftDown;

			if (shiftDown && (mHilitePos == -1))
			{
				mHilitePos = mCursorPos;
			}

			string anOldString = new string(mString);
			int anOldCursorPos = mCursorPos;
			int anOldHilitePos = mHilitePos;
			if ((theChar == 3) || (theChar == 24))
			{
				// Copy	selection

				if ((mHilitePos != -1) && (mHilitePos != mCursorPos))
				{
					if (mCursorPos < mHilitePos)
					{
						throw new NotImplementedException();
						//mWidgetManager.mApp.CopyToClipboard(SexyStringToString(GetDisplayString().substr(mCursorPos, mHilitePos)));
					}
					else
					{
						throw new NotImplementedException();
						//mWidgetManager.mApp.CopyToClipboard(SexyStringToString(GetDisplayString().substr(mHilitePos, mCursorPos)));
					}

					if (theChar == 3)
					{
						removeHilite = false;
					}
					else
					{
						mString = mString.Substring(0, Math.Min(mCursorPos, mHilitePos)) + mString.Substring(Math.Max(mCursorPos, mHilitePos));
						mCursorPos = Math.Min(mCursorPos, mHilitePos);
						mHilitePos = -1;
						bigChange = true;
					}
				}
			}
			else if (theChar == 22)
			{
				// Paste selection

				throw new NotImplementedException();
				//string aBaseString = StringToSexyString(mWidgetManager.mApp.GetClipboard());

				/*if (aBaseString.length() > 0)
				{
					SexyString aString = new SexyString();

					for (uint i = 0; i < aBaseString.length(); i++)
					{
						if ((aBaseString[i] == '\r') || (aBaseString[i] == '\n'))
						{
							break;
						}

						if (mFont.CharWidth(aBaseString[i]) != 0 && mEditListener.AllowChar(mId, aBaseString[i]))
						{
							aString += aBaseString[i];
						}
					}

					if (mHilitePos == -1)
					{
						// Insert string where cursor is
						mString = mString.substr(0, mCursorPos) + aString + mString.substr(mCursorPos);
					}
					else
					{
						// Replace selection with new string
						mString = mString.substr(0, Math.Min(mCursorPos, mHilitePos)) + aString + mString.substr(Math.Max(mCursorPos, mHilitePos));
						mCursorPos = Math.Min(mCursorPos, mHilitePos);
						mHilitePos = -1;
					}

					mCursorPos += aString.length();

					bigChange = true;
				}*/
			}
			else if (theChar == 26)
			{
				// Undo

				mLastModifyIdx = -1;

				string aSwapString = mString;
				int aSwapCursorPos = mCursorPos;
				int aSwapHilitePos = mHilitePos;

				mString = mUndoString;
				mCursorPos = mUndoCursor;
				mHilitePos = mUndoHilitePos;

				mUndoString = aSwapString;
				mUndoCursor = aSwapCursorPos;
				mUndoHilitePos = aSwapHilitePos;

				removeHilite = false;
			}
			else if (theKey == KeyCode.KEYCODE_LEFT)
			{
				if (controlDown)
				{
					// Get to a word
					while ((mCursorPos > 0) && (!IsPartOfWord(mString[mCursorPos - 1])))
					{
						mCursorPos--;
					}

					// Go beyond the word
					while ((mCursorPos > 0) && (IsPartOfWord(mString[mCursorPos - 1])))
					{
						mCursorPos--;
					}
				}
				else if (shiftDown || (mHilitePos == -1))
				{
					mCursorPos--;
				}
				else
				{
					mCursorPos = Math.Min(mCursorPos, mHilitePos);
				}
			}
			else if (theKey == KeyCode.KEYCODE_RIGHT)
			{
				if (controlDown)
				{
					// Get to whitespace
					while ((mCursorPos < (int)mString.length() - 1) && (IsPartOfWord(mString[mCursorPos + 1])))
					{
						mCursorPos++;
					}

					// Go beyond the whitespace
					while ((mCursorPos < (int)mString.length() - 1) && (!IsPartOfWord(mString[mCursorPos + 1])))
					{
						mCursorPos++;
					}
				}
				if (shiftDown || (mHilitePos == -1))
				{
					mCursorPos++;
				}
				else
				{
					mCursorPos = Math.Max(mCursorPos, mHilitePos);
				}
			}
			else if (theKey == KeyCode.KEYCODE_BACK)
			{
				if (mString.length() > 0)
				{
					if ((mHilitePos != -1) && (mHilitePos != mCursorPos))
					{
						// Delete selection
						mString = mString.Substring(0, Math.Min(mCursorPos, mHilitePos)) + mString.Substring(Math.Max(mCursorPos, mHilitePos));
						mCursorPos = Math.Min(mCursorPos, mHilitePos);
						mHilitePos = -1;

						bigChange = true;
					}
					else
					{
						// Delete char behind cursor
						if (mCursorPos > 0)
						{
							mString = mString.Substring(0, mCursorPos - 1) + mString.Substring(mCursorPos);
						}
						else
						{
							mString = mString.Substring(mCursorPos);
						}
						mCursorPos--;
						mHilitePos = -1;

						if (mCursorPos != mLastModifyIdx)
						{
							bigChange = true;
						}
						mLastModifyIdx = mCursorPos - 1;
					}
				}
			}
			else if (theKey == KeyCode.KEYCODE_DELETE)
			{
				if (mString.length() > 0)
				{
					if ((mHilitePos != -1) && (mHilitePos != mCursorPos))
					{
						// Delete selection
						mString = mString.Substring(0, Math.Min(mCursorPos, mHilitePos)) + mString.Substring(Math.Max(mCursorPos, mHilitePos));
						mCursorPos = Math.Min(mCursorPos, mHilitePos);
						mHilitePos = -1;

						bigChange = true;
					}
					else
					{
						// Delete char in front of cursor
						if (mCursorPos < (int)mString.length())
						{
							mString = mString.Substring(0, mCursorPos) + mString.Substring(mCursorPos + 1);
						}

						if (mCursorPos != mLastModifyIdx)
						{
							bigChange = true;
						}
						mLastModifyIdx = mCursorPos;
					}
				}
			}
			else if (theKey == KeyCode.KEYCODE_HOME)
			{
				mCursorPos = 0;
			}
			else if (theKey == KeyCode.KEYCODE_END)
			{
				mCursorPos = mString.length();
			}
			else if (theKey == KeyCode.KEYCODE_RETURN)
			{
				mEditListener.EditWidgetText(mId, mString);
			}
			else
			{
				string aString = theChar.ToString();
				uint uTheChar = (uint)theChar;
				uint range = 127;
				if (/*GlobalStaticVars.gSexyAppBase.mbAllowExtendedChars*/true)
				{
					range = 255;
				}

				if ((uTheChar >= 32) && (uTheChar <= range) && (mFont.StringWidth(aString) > 0) && mEditListener.AllowChar(mId, new SexyChar(theChar)))
				{
					if ((mHilitePos != -1) && (mHilitePos != mCursorPos))
					{
						// Replace selection with new character
						mString = mString.Substring(0, Math.Min(mCursorPos, mHilitePos)) + theChar.ToString() + mString.Substring(Math.Max(mCursorPos, mHilitePos));
						mCursorPos = Math.Min(mCursorPos, mHilitePos);
						mHilitePos = -1;

						bigChange = true;
					}
					else
					{
						// Insert character where cursor is
						mString = mString.Substring(0, mCursorPos) + theChar.ToString() + mString.Substring(mCursorPos);

						if (mCursorPos != mLastModifyIdx + 1)
						{
							bigChange = true;
						}
						mLastModifyIdx = mCursorPos;
						mHilitePos = -1;
					}

					mCursorPos++;
					FocusCursor(false);
				}
				else
				{
					removeHilite = false;
				}
			}

			if ((mMaxChars != -1) && ((int)mString.length() > mMaxChars))
			{
				mString = mString.Substring(0, mMaxChars);
			}

			EnforceMaxPixels();

			if (mCursorPos < 0)
			{
				mCursorPos = 0;
			}
			else if (mCursorPos > (int)mString.length())
			{
				mCursorPos = mString.length();
			}

			if (anOldCursorPos != mCursorPos)
			{
				mBlinkAcc = 0;
				mShowingCursor = true;
			}

			FocusCursor(true);

			if (removeHilite || mHilitePos == mCursorPos)
			{
				mHilitePos = -1;
			}

			if (!mEditListener.AllowText(mId, ref mString))
			{
				mString = anOldString;
				mCursorPos = anOldCursorPos;
				mHilitePos = anOldHilitePos;
			}
			else if (bigChange)
			{
				//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
				//ORIGINAL LINE: mUndoString = anOldString;
				mUndoString = anOldString;
				mUndoCursor = anOldCursorPos;
				mUndoHilitePos = anOldHilitePos;
			}

			MarkDirty();
		}

		public virtual void FocusCursor(bool bigJump)
		{
			while (mCursorPos < mLeftPos)
			{
				if (bigJump)
				{
					mLeftPos = Math.Max(0, mLeftPos - 10);
				}
				else
				{
					mLeftPos = Math.Max(0, mLeftPos - 1);
				}
				MarkDirty();
			}

			if (mFont != null)
			{
				string aString = GetDisplayString();
				while ((mWidth - 8 > 0) && (mFont.StringWidth(aString.Substring(0, mCursorPos)) - mFont.StringWidth(aString.Substring(0, mLeftPos)) >= mWidth - 8))
				{
					if (bigJump)
					{
						mLeftPos = Math.Min(mLeftPos + 10, (int)mString.length() - 1);
					}
					else
					{
						mLeftPos = Math.Min(mLeftPos + 1, (int)mString.length() - 1);
					}

					MarkDirty();
				}
			}
		}

		protected string GetDisplayString()
		{
			if (mPasswordChar.value_type == '\0')
			{
				return mString;
			}
			if (mPasswordDisplayString.Length != mString.Length)
			{
				mPasswordDisplayString = mPasswordDisplayString + mString.Length.ToString() + mPasswordChar;
			}
			return mPasswordDisplayString;
		}

		protected virtual void HiliteWord()
		{
			string aString = GetDisplayString();

			if (mCursorPos < (int)aString.length())
			{
				// Find first space before word
				mHilitePos = mCursorPos;
				while ((mHilitePos > 0) && (IsPartOfWord(aString[mHilitePos - 1])))
				{
					mHilitePos--;
				}

				// Find first space after word
				while ((mCursorPos < (int)aString.length() - 1) && (IsPartOfWord(aString[mCursorPos + 1])))
				{
					mCursorPos++;
				}
				if (mCursorPos < (int)aString.length())
				{
					mCursorPos++;
				}
			}
		}

		public void EnforceMaxPixels()
		{
			if (mMaxPixels <= 0 && mWidthCheckList.Count == 0) // no width checking in effect
			{
				return;
			}

			if (mWidthCheckList.Count == 0)
			{
				while (mFont.StringWidth(mString) > mMaxPixels)
				{
					mString = mString.Substring(0, mString.length() - 1);
				}

				return;
			}

			foreach (var anItr in mWidthCheckList)
			{
				int aWidth = anItr.mWidth;
				if (aWidth <= 0)
				{
					aWidth = mMaxPixels;
					if (aWidth <= 0)
					{
						continue;
					}
				}

				while (anItr.mFont.StringWidth(mString) > aWidth)
				{
					mString = mString.Substring(0, mString.length() - 1);
				}
			}
		}

		public virtual bool IsPartOfWord(char theChar)
		{
			return (((theChar >= 'A') && (theChar <= 'Z')) || ((theChar >= 'a') && (theChar <= 'z')) || ((theChar >= '0') && (theChar <= '9')) || ((theChar >= (char)(0xC0)) && (theChar <= (char)(0xFF))) || (theChar == '_'));
		}

		public virtual void SetFont(Font theFont)
		{
			mFont.Dispose();
			mFont = theFont.Duplicate();
		}

		public virtual void SetText(string theText, bool leftPosToZero = true)
		{
			mString = theText;
			mCursorPos = mString.length();
			mHilitePos = 0;
			if (leftPosToZero)
			{
				mLeftPos = 0;
			}
			else
			{
				FocusCursor(true);
			}

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
			/*if (mFont == null)
			{
				mFont = new SysFont(mWidgetManager.mApp, "Arial Unicode MS", 10, false);
			}*/

			string aString = GetDisplayString();

			g.SetColor(mColors[(int)Colors.COLOR_BKG]);
			g.FillRect(0, 0, mWidth, mHeight);

			for (int i = 0; i < 2; i++)
			{
				Graphics aClipG = g.Create();
				aClipG.SetFont(mFont);

				if (i == 1)
				{
					int aCursorX = mFont.StringWidth(aString.Substring(0, mCursorPos)) - mFont.StringWidth(aString.Substring(0, mLeftPos));
					int aHiliteX = aCursorX + 2;
					if ((mHilitePos != -1) && (mCursorPos != mHilitePos))
					{
						aHiliteX = mFont.StringWidth(aString.Substring(0, mHilitePos)) - mFont.StringWidth(aString.Substring(0, mLeftPos));
					}

					if (!mShowingCursor)
					{
						aCursorX += 2;
					}

					aCursorX = Math.Min(Math.Max(0, aCursorX), mWidth - 8);
					aHiliteX = Math.Min(Math.Max(0, aHiliteX), mWidth - 8);

					aClipG.ClipRect(4 + Math.Min(aCursorX, aHiliteX), (mHeight - mFont.GetHeight()) / 2, Math.Abs(aHiliteX - aCursorX), mFont.GetHeight());
				}
				else
				{
					aClipG.ClipRect(4, 0, mWidth - 8, mHeight);
				}

				bool hasfocus = mHasFocus || mDrawSelOverride;
				if (i == 1 && hasfocus)
				{
					aClipG.SetColor(mColors[(int)Colors.COLOR_HILITE]);
					aClipG.FillRect(0, 0, mWidth, mHeight);
				}

				if (i == 0 || !hasfocus)
				{
					aClipG.SetColor(mColors[(int)Colors.COLOR_TEXT]);
				}
				else
				{
					aClipG.SetColor(mColors[(int)Colors.COLOR_HILITE_TEXT]);
				}
				aClipG.DrawString(aString.Substring(mLeftPos), 4, -4/*(int)( (mHeight + mFont.GetHeight()) / 2 - mFont.GetAscent())*/);

				if (aClipG != null)
				{
					aClipG.Dispose();
				}
			}

			g.SetColor(mColors[(int)Colors.COLOR_OUTLINE]);
			g.DrawRect(0, 0, mWidth - 1, mHeight - 1);
		}

		public override void Update()
		{
			base.Update();

			if (mHasFocus)
			{
				if (++mBlinkAcc > mBlinkDelay)
				{
					MarkDirty();
					mBlinkAcc = 0;
					mShowingCursor = !mShowingCursor;
				}
			}
		}

		public override bool WantsFocus()
		{
			return true;
		}

		public override void GotFocus()
		{
			base.GotFocus();
            var imeHandler = mWidgetManager.mIMEHandler;
            //if (!imeHandler.Enabled) 
            //{

            //imeHandler.SetTextInputRect(ref rect);

            mWidgetManager.mIMEHotWidget = this;
            //base.Resize(mX, (int)(imeHandler.VirtualKeyboardHeight == 0 ? mY : 100), mWidth, mHeight);
            //}
            //if (!Guide.IsVisible)
            //{
            //Guide.BeginShowKeyboardInput(PlayerIndex.One, mTitle, mDescription, mString, new AsyncCallback(KeyboardCallback), null);
            //}

            mShowingCursor = true;
			mBlinkAcc = 0;
			MarkDirty();
		}

        public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
        {
            base.MouseDown(x, y, theBtnNum, theClickCount);

            mHilitePos = -1;
            mCursorPos = GetCharAt(x, y);

            if (theClickCount > 1)
            {
                mHadDoubleClick = true;
                HiliteWord();
            }

            MarkDirty();

            FocusCursor(false);
        }
        public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
        {
            base.MouseUp(x, y, theBtnNum, theClickCount);
            if (mHilitePos == mCursorPos)
            {
                mHilitePos = -1;
            }

            if (mHadDoubleClick)
            {
                mHilitePos = -1;
                mCursorPos = GetCharAt(x, y);

                mHadDoubleClick = false;
                HiliteWord();
            }

            MarkDirty();
        }
        public override void MouseDrag(int x, int y)
        {
            base.MouseDrag(x, y);

            if (mHilitePos == -1)
            {
                mHilitePos = mCursorPos;
            }

            mCursorPos = GetCharAt(x, y);
            MarkDirty();

            FocusCursor(false);
        }
        public override void MouseEnter()
        {
            base.MouseEnter();
            
            //mWidgetManager.mApp.SetCursor(AnonymousEnum4.CURSOR_TEXT);
        }
        public override void MouseLeave()
        {
            base.MouseLeave();
            
            //mWidgetManager.mApp.SetCursor(AnonymousEnum4.CURSOR_POINTER);
        }
        public virtual int GetCharAt(int x, int y)
        {
            int aPos = 0;

            string aString = GetDisplayString();

            for (int i = mLeftPos; i < (int)aString.length(); i++)
            {
                string aLoSubStr = aString.Substring(mLeftPos, i - mLeftPos);
                string aHiSubStr = aString.Substring(mLeftPos, i - mLeftPos + 1);

                int aLoLen = mFont.StringWidth(aLoSubStr);
                int aHiLen = mFont.StringWidth(aHiSubStr);
                if (x >= (aLoLen + aHiLen) / 2 + 5)
                {
                    aPos = i + 1;
                }
            }

            return aPos;
        }

        public override void KeyChar(SexyChar theChar)
		{
			ProcessKey(KeyCode.KEYCODE_UNKNOWN, new SexyChar(theChar));
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

		public override void KeyDown(KeyCode theKey)
		{
			if ((((uint)theKey < 'A') || ((uint)theKey >= 'Z')) && mEditListener.AllowKey(mId, theKey))
			{
				ProcessKey(theKey, new SexyChar('\0'));
			}

			base.KeyDown(theKey);
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
			mShowingCursor = false;
            mWidgetManager.mIMEHotWidget = null;
			MarkDirty();
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
			{0, 0, 0},
			{0, 0, 0},
			{0, 0, 0},
			{0, 0, 0},
			{255, 255, 255}
		};

		public bool mEditing;

		public bool mAcceptsEmptyText;

		public string mTitle;

		public string mDescription;

		private bool callbackDone;

		private bool inputCancelled;

		// SexyAppFramework 1.3
		public int mId;
		public string mString;
		public string mPasswordDisplayString;
		public Font mFont;

		public class WidthCheck
		{
			public Font mFont;
			public int mWidth;
		}
		public LinkedList<WidthCheck> mWidthCheckList = new LinkedList<WidthCheck>();

		public EditListener mEditListener;
		public bool mShowingCursor;
		public bool mDrawSelOverride; // set this to true to draw selected text even when not in focus
		public bool mHadDoubleClick; // Used to fix a bug with double clicking to hilite a word after the widget manager started calling mouse drag before mouse down/up events
		public int mCursorPos;
		public int mHilitePos;
		public int mBlinkAcc;
		public int mBlinkDelay;
		public int mLeftPos;
		public int mMaxChars;
		public int mMaxPixels;
		public SexyChar mPasswordChar; //public string mPasswordChar;

		public string mUndoString;
		public int mUndoCursor;
		public int mUndoHilitePos;
		public int mLastModifyIdx;


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
