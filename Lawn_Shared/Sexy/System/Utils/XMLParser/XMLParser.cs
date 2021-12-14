using System;
using System.Collections.Generic;
using System.Xml;

namespace Sexy
{
	public/*internal*/ class XMLParser : EncodingParser
	{
		protected void Fail(string theErrorText)
		{
			mHasFailed = true;
			mErrorText = theErrorText;
		}

		protected void Init()
		{
			mSection = "";
			mLineNum = 1;
			mHasFailed = false;
			mErrorText = "";
		}

		protected bool AddAttribute(XMLElement theElement, string theAttributeKey, string theAttributeValue)
		{
			bool result = true;
			if (theElement.mAttributes.ContainsKey(theAttributeKey))
			{
				result = false;
			}
			theElement.mAttributes[theAttributeKey] = theAttributeValue;
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			if (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == theAttributeKey && theAttributeKey != "/")
				{
					theElement.mAttributeIteratorList.Add(enumerator);
				}
			}
			return result;
		}

		protected bool AddAttributeEncoded(XMLElement theElement, string theAttributeKey, string theAttributeValue)
		{
			bool result = true;
			if (theElement.mAttributesEncoded.ContainsKey(theAttributeKey))
			{
				result = false;
			}
			theElement.mAttributesEncoded[theAttributeKey] = theAttributeValue;
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributesEncoded.GetEnumerator();
			if (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == theAttributeKey && theAttributeKey != "/")
				{
					theElement.mAttributeEncodedIteratorList.Add(enumerator);
				}
			}
			return result;
		}

		public XMLParser()
		{
			mLineNum = 0;
			mAllowComments = false;
			mFileName = string.Empty;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override bool OpenFile(string theFileName)
		{
			if (!base.OpenFile(theFileName))
			{
				mLineNum = 0;
				Fail("Unable to open file " + theFileName);
				return false;
			}
			mFileName = theFileName;
			Init();
			return true;
		}

		public virtual bool NextElement(ref XMLElement theElement)
		{
			string text7;
			for (;;)
			{
				theElement.mType = XMLElementType.TYPE_NONE;
				theElement.mSection = mSection;
				theElement.mValue = "";
				theElement.mValueEncoded = "";
				theElement.mAttributes.Clear();
				theElement.mAttributesEncoded.Clear();
				theElement.mInstruction = "";
				theElement.mAttributeIteratorList.Clear();
				theElement.mAttributeEncodedIteratorList.Clear();
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				char c;
				int length;
				int length2;
				string text5;
				for (;;)
				{
					c = '\0';
					int num;
					switch (GetChar(ref c))
					{
					case EncodingParser.GetCharReturnType.SUCCESSFUL:
						num = 1;
						break;
					case EncodingParser.GetCharReturnType.INVALID_CHARACTER:
						goto IL_BB;
					case EncodingParser.GetCharReturnType.END_OF_FILE:
						goto IL_D5;
					case EncodingParser.GetCharReturnType.FAILURE:
						goto IL_C8;
					default:
						goto IL_D5;
					}
					IL_D8:
					if (num != 1)
					{
						goto IL_766;
					}
					bool flag6 = false;
					if (c == '\n')
					{
						mLineNum++;
					}
					if (theElement.mType == XMLElementType.TYPE_COMMENT)
					{
						XMLElement xmlelement = theElement;
						xmlelement.mInstruction += c;
						length = theElement.mInstruction.Length;
						if (c == '>' && length >= 3 && theElement.mInstruction[length - 2] == '-' && theElement.mInstruction[length - 3] == '-')
						{
							goto Block_7;
						}
						continue;
					}
					else if (theElement.mType == XMLElementType.TYPE_INSTRUCTION)
					{
						if (theElement.mInstruction.Length != 0 || char.IsWhiteSpace(c))
						{
							XMLElement xmlelement2 = theElement;
							xmlelement2.mInstruction += c;
							length2 = theElement.mInstruction.Length;
							text5 = theElement.mInstruction;
						}
						else
						{
							XMLElement xmlelement3 = theElement;
							xmlelement3.mValue += c;
							length2 = theElement.mValue.Length;
							text5 = theElement.mValue;
						}
						if (c == '>' && length2 >= 2 && text5[length2 - 2] == '?')
						{
							goto Block_12;
						}
						continue;
					}
					else
					{
						if (c == '"')
						{
							flag2 = !flag2;
							if (theElement.mType == XMLElementType.TYPE_NONE || theElement.mType == XMLElementType.TYPE_ELEMENT)
							{
								flag6 = true;
							}
							if (!flag2)
							{
								flag3 = true;
							}
						}
						else if (!flag2)
						{
							if (c == '<')
							{
								if (theElement.mType == XMLElementType.TYPE_ELEMENT)
								{
									goto Block_18;
								}
								if (theElement.mType != XMLElementType.TYPE_NONE)
								{
									goto IL_2AB;
								}
								theElement.mType = XMLElementType.TYPE_START;
							}
							else
							{
								if (c == '>')
								{
									goto Block_20;
								}
								if (c == '/' && theElement.mType == XMLElementType.TYPE_START && theElement.mValue == "")
								{
									theElement.mType = XMLElementType.TYPE_END;
								}
								else if (c == '?' && theElement.mType == XMLElementType.TYPE_START && theElement.mValue == "")
								{
									theElement.mType = XMLElementType.TYPE_INSTRUCTION;
								}
								else if (char.IsWhiteSpace(c))
								{
									if (theElement.mValue != "")
									{
										flag = true;
									}
								}
								else
								{
									if (c <= ' ')
									{
										goto IL_602;
									}
									flag6 = true;
								}
							}
						}
						else
						{
							flag6 = true;
						}
						if (!flag6)
						{
							continue;
						}
						if (theElement.mType == XMLElementType.TYPE_NONE)
						{
							theElement.mType = XMLElementType.TYPE_ELEMENT;
						}
						if (theElement.mType != XMLElementType.TYPE_START)
						{
							if (flag)
							{
								XMLElement xmlelement4 = theElement;
								xmlelement4.mValue += " ";
								flag = false;
							}
							XMLElement xmlelement5 = theElement;
							xmlelement5.mValue += c;
							continue;
						}
						if (flag)
						{
							if (!flag4)
							{
								flag4 = true;
								flag5 = false;
							}
							else if (!flag2)
							{
								if ((!flag5 && c != '=') || (flag5 && (!string.IsNullOrEmpty(text2) || flag3)))
								{
									AddAttribute(theElement, XMLDecodeString(text), XMLDecodeString(text2));
									AddAttributeEncoded(theElement, text, text2);
									text = "";
									text2 = "";
									text3 = "";
									text4 = "";
								}
								else
								{
									flag4 = true;
								}
								flag5 = false;
							}
							flag = false;
						}
						if (!flag4)
						{
							XMLElement xmlelement6 = theElement;
							xmlelement6.mValue += c;
							if (theElement.mValue == "!--")
							{
								theElement.mType = XMLElementType.TYPE_COMMENT;
								continue;
							}
							continue;
						}
						else
						{
							if (!flag2 && c == '=')
							{
								flag5 = true;
								flag3 = false;
								continue;
							}
							if (!flag5)
							{
								text += c;
								continue;
							}
							text2 += c;
							continue;
						}
					}
					IL_D5:
					num = 0;
					goto IL_D8;
				}
				IL_77C:
				if (text.Length > 0)
				{
					AddAttribute(theElement, XMLDecodeString(text), XMLDecodeString(text2));
					AddAttribute(theElement, text, text2);
				}
				theElement.mValueEncoded = theElement.mValue;
				theElement.mValue = XMLDecodeString(theElement.mValue);
				if (theElement.mType != XMLElementType.TYPE_COMMENT || mAllowComments)
				{
					return true;
				}
				continue;
				Block_7:
				theElement.mInstruction = theElement.mInstruction.Substring(0, length - 3);
				goto IL_77C;
				Block_12:
				text5 = text5.Substring(0, length2 - 2);
				goto IL_77C;
				Block_18:
				PutChar(c);
				goto IL_77C;
				Block_20:
				if (theElement.mType == XMLElementType.TYPE_START)
				{
					bool flag7 = false;
					if (text == "/")
					{
						flag7 = true;
					}
					else
					{
						if (text.Length > 0)
						{
							text3 = XMLDecodeString(text);
							text4 = text;
							AddAttribute(theElement, XMLDecodeString(text), XMLDecodeString(text2));
							AddAttributeEncoded(theElement, text, text2);
							text = "";
							text2 = "";
						}
						if (text3.Length > 0)
						{
							string text6 = theElement.mAttributes[text3];
							int length3 = text6.Length;
							if (length3 > 0 && text6[length3 - 1] == '/')
							{
								AddAttribute(theElement, text3, XMLDecodeString(text6.Substring(0, length3 - 1)));
								flag7 = true;
							}
							text6 = theElement.mAttributesEncoded[text4];
							length3 = text6.Length;
							if (length3 > 0 && text6[length3 - 1] == '/')
							{
								AddAttributeEncoded(theElement, text4, text6.Substring(0, length3 - 1));
								flag7 = true;
							}
						}
						else
						{
							int length4 = theElement.mValue.Length;
							if (length4 > 0 && theElement.mValue[length4 - 1] == '/')
							{
								theElement.mValue = theElement.mValue.Substring(0, length4 - 1);
								flag7 = true;
							}
						}
					}
					if (flag7)
					{
						string theString = "</" + theElement.mValue + ">";
						PutString(theString);
						text = "";
					}
					if (mSection.Length != 0)
					{
						mSection += "/";
					}
					mSection += theElement.mValue;
					goto IL_77C;
				}
				if (theElement.mType != XMLElementType.TYPE_END)
				{
					goto IL_570;
				}
				int num2 = mSection.LastIndexOf('/');
				if (num2 == -1 && mSection.Length == 0)
				{
					goto Block_35;
				}
				text7 = mSection.Substring(num2 + 1);
				if (text7 != theElement.mValue)
				{
					goto Block_36;
				}
				if (num2 == -1)
				{
					mSection = mSection.Remove(0, mSection.Length);
					goto IL_77C;
				}
				mSection = mSection.Remove(num2, mSection.Length - num2);
				goto IL_77C;
			}
			IL_BB:
			Fail("Illegal Character");
			return false;
			IL_C8:
			Fail("Internal Error");
			return false;
			IL_2AB:
			Fail("Unexpected '<'");
			return false;
			Block_35:
			Fail("Unexpected End");
			return false;
			Block_36:
			Fail(string.Concat(new string[]
			{
				"End '",
				theElement.mValue,
				"' Doesn't Match Start '",
				text7,
				"'"
			}));
			return false;
			IL_570:
			Fail("Unexpected '>'");
			return false;
			IL_602:
			Fail("Illegal Character");
			return false;
			IL_766:
			if (theElement.mType != XMLElementType.TYPE_NONE)
			{
				Fail("Unexpected End of File");
			}
			return false;
		}

		private string XMLDecodeString(string s)
		{
			return XmlConvert.DecodeName(s);
		}

		public string GetErrorText()
		{
			return mErrorText;
		}

		public int GetCurrentLineNum()
		{
			return mLineNum;
		}

		public string GetFileName()
		{
			return mFileName;
		}

		public override void SetStringSource(string theString)
		{
			Init();
			base.SetStringSource(theString);
		}

		public void AllowComments(bool doAllow)
		{
			mAllowComments = doAllow;
		}

		public bool HasFailed()
		{
			return mHasFailed;
		}

		protected string mFileName;

		protected string mErrorText;

		protected int mLineNum;

		protected bool mHasFailed;

		protected bool mAllowComments;

		protected string mSection;
	}
}
