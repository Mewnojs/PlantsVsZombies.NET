using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class PropertiesParser
	{
		protected void Fail(string theErrorText)
		{
			if (!this.mHasFailed)
			{
				this.mHasFailed = true;
				int currentLineNum = this.mXMLParser.GetCurrentLineNum();
				this.mError = theErrorText;
				if (currentLineNum > 0)
				{
					this.mError += Common.StrFormat_(" on Line {0}", currentLineNum);
				}
				if (this.mXMLParser.GetFileName().Length != 0)
				{
					this.mError += Common.StrFormat_(" in File '{0}'", this.mXMLParser.GetFileName());
				}
			}
		}

		protected bool ParseSingleElement(ref string aString)
		{
			aString = "";
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(ref xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					goto Block_2;
				}
				if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
				{
					aString = xmlelement.mValue;
				}
				else if (xmlelement.mType == XMLElementType.TYPE_END)
				{
					return true;
				}
			}
			return false;
			Block_2:
			this.Fail("Unexpected Section: '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool ParseStringArray(ref List<string> theStringVector)
		{
			theStringVector.Clear();
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(ref xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue == "String"))
					{
						goto IL_55;
					}
					string empty = string.Empty;
					if (!this.ParseSingleElement(ref empty))
					{
						return false;
					}
					theStringVector.Add(empty);
				}
				else
				{
					if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_5;
					}
					if (xmlelement.mType == XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			IL_55:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_5:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool ParseProperties()
		{
			XMLElement xmlelement;
			string text2;
			string empty2;
			string empty3;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(ref xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue == "String")
					{
						string empty = string.Empty;
						if (!this.ParseSingleElement(ref empty))
						{
							return false;
						}
						string theId = xmlelement.mAttributes["id"];
						this.mApp.SetString(theId, empty);
					}
					else if (xmlelement.mValue == "StringArray")
					{
						List<string> list = new List<string>();
						if (!this.ParseStringArray(ref list))
						{
							return false;
						}
						string text = xmlelement.mAttributes["id"];
						this.mApp.mStringVectorProperties.Add(text, list);
					}
					else if (xmlelement.mValue == "Boolean")
					{
						text2 = string.Empty;
						if (!this.ParseSingleElement(ref text2))
						{
							return false;
						}
						text2 = Common.Upper(text2);
						bool theValue;
						if (text2 == "1" || text2 == "YES" || text2 == "ON" || text2 == "TRUE")
						{
							theValue = true;
						}
						else
						{
							if (!(text2 == "0") && !(text2 == "NO") && !(text2 == "OFF") && !(text2 == "FALSE"))
							{
								goto IL_160;
							}
							theValue = false;
						}
						string theId2 = xmlelement.mAttributes["id"];
						this.mApp.SetBoolean(theId2, theValue);
					}
					else if (xmlelement.mValue == "Integer")
					{
						empty2 = string.Empty;
						if (!this.ParseSingleElement(ref empty2))
						{
							return false;
						}
						int theValue2 = 0;
						if (!Common.StringToInt(empty2, ref theValue2))
						{
							goto Block_16;
						}
						string theId3 = xmlelement.mAttributes["id"];
						this.mApp.SetInteger(theId3, theValue2);
					}
					else
					{
						if (!(xmlelement.mValue == "Double"))
						{
							goto IL_28B;
						}
						empty3 = string.Empty;
						if (!this.ParseSingleElement(ref empty3))
						{
							return false;
						}
						double theValue3 = 0.0;
						if (!Common.StringToDouble(empty3, ref theValue3))
						{
							goto Block_19;
						}
						string theId4 = xmlelement.mAttributes["id"];
						this.mApp.SetDouble(theId4, theValue3);
					}
				}
				else
				{
					if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_20;
					}
					if (xmlelement.mType == XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			IL_160:
			this.Fail("Invalid Boolean Value: '" + text2 + "'");
			return false;
			Block_16:
			this.Fail("Invalid Integer Value: '" + empty2 + "'");
			return false;
			Block_19:
			this.Fail("Invalid Double Value: '" + empty3 + "'");
			return false;
			IL_28B:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_20:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool DoParseProperties()
		{
			if (!this.mXMLParser.HasFailed())
			{
				XMLElement xmlelement;
				for (;;)
				{
					xmlelement = new XMLElement();
					if (!this.mXMLParser.NextElement(ref xmlelement))
					{
						break;
					}
					if (xmlelement.mType == XMLElementType.TYPE_START)
					{
						if (!(xmlelement.mValue == "Properties"))
						{
							goto IL_47;
						}
						if (!this.ParseProperties())
						{
							break;
						}
					}
					else if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_5;
					}
				}
				goto IL_88;
				IL_47:
				this.Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_88;
				Block_5:
				this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_88:
			if (this.mXMLParser.HasFailed())
			{
				this.Fail(this.mXMLParser.GetErrorText());
			}
			this.mXMLParser.Dispose();
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		public PropertiesParser(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mHasFailed = false;
			this.mXMLParser = null;
		}

		public virtual void Dispose()
		{
		}

		public bool ParsePropertiesFile(string theFilename)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.OpenFile(theFilename);
			return this.DoParseProperties();
		}

		public bool ParsePropertiesBuffer(Buffer theBuffer)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.SetStringSource("");
			return this.DoParseProperties();
		}

		public string GetErrorText()
		{
			return this.mError;
		}

		public SexyAppBase mApp;

		public XMLParser mXMLParser;

		public string mError;

		public bool mHasFailed;
	}
}
