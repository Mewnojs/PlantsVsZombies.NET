using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Resource;

namespace Sexy.WidgetsLib
{
	public class PopAnimModParser : DescParser
	{
		public override bool Error(string theError)
		{
			if (GlobalMembers.gSexyApp != null)
			{
				string text = this.mErrorHeader + theError;
				if (this.mCurrentLine.Length > 0)
				{
					object obj = text;
					text = string.Concat(new object[] { obj, " on Line ", this.mCurrentLineNum, ":\r\n\r\n", this.mCurrentLine });
				}
				GlobalMembers.gSexyAppBase.Popup(text);
			}
			return false;
		}

		public void SetParamHelper(PASpriteDef theSpriteDef, string theCmdName, int theCmdNum, string theNewParam)
		{
			if (theCmdNum == 0)
			{
				return;
			}
			for (int i = 0; i < theSpriteDef.mFrames.Count; i++)
			{
				PAFrame paframe = theSpriteDef.mFrames[i];
				for (int j = 0; j < paframe.mCommandVector.Count; j++)
				{
					PACommand pacommand = paframe.mCommandVector[j];
					if (PopAnim.WildcardMatches(pacommand.mCommand, theCmdName))
					{
						if (theCmdNum <= 1)
						{
							pacommand.mParam = theNewParam;
						}
						if (theCmdNum >= 0)
						{
							theCmdNum--;
							if (theCmdNum == 0)
							{
								return;
							}
						}
					}
				}
			}
		}

		public PopAnimModParser()
		{
			this.mCmdSep = 2;
		}

		public override bool HandleCommand(ListDataElement theParams)
		{
			string text = ((SingleDataElement)theParams.mElementVector[0]).mString.ToString();
			int num = theParams.mElementVector.Count - 1;
			if (text == "SetPamFile")
			{
				if (this.mPassNum != 1)
				{
					return true;
				}
				if (num != 1)
				{
					return this.Error("Invalid Number of Parameters");
				}
				if (this.mPopAnim.mModPamFile.Length == 0)
				{
					string mModPamFile = "";
					if (base.DataToString(theParams.mElementVector[1], ref mModPamFile))
					{
						this.Error("Invalid Paramater Type");
					}
					this.mPopAnim.mModPamFile = mModPamFile;
				}
			}
			else if (text == "Remap")
			{
				if (this.mPassNum != 1)
				{
					return true;
				}
				if (num != 2)
				{
					return this.Error("Invalid Number of Parameters");
				}
				string theWildcard = "";
				if (base.DataToString(theParams.mElementVector[1], ref theWildcard))
				{
					this.Error("Invalid Paramater Type");
				}
				string theReplacement = "";
				if (base.DataToString(theParams.mElementVector[2], ref theReplacement))
				{
					this.Error("Invalid Paramater Type");
				}
				this.mPopAnim.Load_AddRemap(theWildcard, theReplacement);
			}
			else if (text == "Colorize" || text == "HueShift")
			{
				if (this.mPassNum != 2)
				{
					return true;
				}
				string theWildcard2 = "";
				if (base.DataToString(theParams.mElementVector[1], ref theWildcard2))
				{
					this.Error("Invalid Paramater Type");
				}
				bool flag = false;
				for (int i = 0; i < this.mPopAnim.mImageVector.Count; i++)
				{
					PAImage paimage = this.mPopAnim.mImageVector[i];
					for (int j = 0; j < paimage.mImages.Count; j++)
					{
						if (PopAnim.WildcardMatches(paimage.mImageName, theWildcard2))
						{
							if (text == "Colorize")
							{
								List<int> list = new List<int>();
								if (base.DataToIntVector(theParams.mElementVector[2], ref list) || (list.Count != 3 && list.Count != 4))
								{
									this.Error("Invalid Paramater Type");
								}
								if (list.Count == 3)
								{
									new SexyColor(list[0], list[1], list[2]);
								}
								else
								{
									new SexyColor(list[0], list[1], list[2], list[3]);
								}
							}
							else
							{
								int num2 = 0;
								if (base.DataToInt(theParams.mElementVector[2], ref num2))
								{
									return false;
								}
							}
							flag = true;
						}
					}
				}
				if (flag)
				{
					return this.Error("Unable to locate specified element");
				}
			}
			else
			{
				if (!(text == "SetParam"))
				{
					this.Error("Unknown Command");
					return false;
				}
				if (this.mPassNum != 2)
				{
					return true;
				}
				if (num != 2)
				{
					return this.Error("Invalid Number of Parameters");
				}
				string text2 = "";
				if (base.DataToString(theParams.mElementVector[1], ref text2))
				{
					this.Error("Invalid Paramater Type");
				}
				string theNewParam = "";
				if (base.DataToString(theParams.mElementVector[2], ref theNewParam))
				{
					this.Error("Invalid Paramater Type");
				}
				int theCmdNum = -1;
				int num3 = text2.IndexOf('[');
				if (num3 != -1)
				{
					theCmdNum = int.Parse(text2) + num3 + 1;
					text2 = text2.Substring(0, num3);
				}
				this.SetParamHelper(this.mPopAnim.mMainAnimDef.mMainSpriteDef, text2, theCmdNum, theNewParam);
				for (int k = 0; k < this.mPopAnim.mMainAnimDef.mSpriteDefVector.Count; k++)
				{
					this.SetParamHelper(this.mPopAnim.mMainAnimDef.mSpriteDefVector[k], text2, theCmdNum, theNewParam);
				}
			}
			return true;
		}

		public int mPassNum;

		public PopAnim mPopAnim;

		public string mErrorHeader;
	}
}
