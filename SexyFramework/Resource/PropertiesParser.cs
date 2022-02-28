using System;
using System.Collections.Generic;

namespace Sexy.Resource
{
	public class PropertiesParser
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
					this.mError = this.mError + " on Line " + currentLineNum;
				}
				if (this.mXMLParser.GetFileName().Length <= 0)
				{
					this.mError = this.mError + " in File " + this.mXMLParser.GetFileName();
				}
			}
		}

		protected bool ParseSingleElement(string aString)
		{
			while (this.mXMLParser.NextElement(this.mXMLElement))
			{
				if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					this.Fail("Unexpected Section: '" + this.mXMLElement.mValue + "'");
					return false;
				}
				if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
				{
					aString = this.mXMLElement.mValue.ToString();
				}
				else if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_END)
				{
					return true;
				}
			}
			return false;
		}

		protected bool ParseStringArray(List<string> theStringVector)
		{
			theStringVector.Clear();
			while (this.mXMLParser.NextElement(this.mXMLElement))
			{
				if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (!(this.mXMLElement.mValue.ToString() == "String"))
					{
						this.Fail("Invalid Section '" + this.mXMLElement.mValue + "'");
						return false;
					}
					string text = "";
					if (!this.ParseSingleElement(text))
					{
						return false;
					}
					theStringVector.Add(text);
				}
				else
				{
					if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						this.Fail("Element Not Expected '" + this.mXMLElement.mValue + "'");
						return false;
					}
					if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
		}

		protected bool ParseProperties()
		{
			while (this.mXMLParser.NextElement(this.mXMLElement))
			{
				if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (this.mXMLElement.mValue.ToString() == "String")
					{
						string text = "";
						if (!this.ParseSingleElement(text))
						{
							return false;
						}
						string attribute = this.mXMLElement.GetAttribute("id");
						this.mApp.SetString(attribute, text);
					}
					else if (this.mXMLElement.mValue.ToString() == "StringArray")
					{
						List<string> list = new List<string>();
						if (!this.ParseStringArray(list))
						{
							return false;
						}
						string attribute2 = this.mXMLElement.GetAttribute("id");
						this.mApp.mStringVectorProperties[attribute2] = list;
					}
					else if (this.mXMLElement.mValue.ToString() == "Boolean")
					{
						string text2 = "";
						if (!this.ParseSingleElement(text2))
						{
							return false;
						}
						text2 = text2.ToUpper();
						bool boolValue;
						if (text2 == "1" || text2 == "YES" || text2 == "ON" || text2 == "TRUE")
						{
							boolValue = true;
						}
						else
						{
							if (!(text2 == "0") && !(text2 == "NO") && !(text2 == "OFF") && !(text2 == "FALSE"))
							{
								this.Fail("Invalid Boolean Value: '" + text2 + "'");
								return false;
							}
							boolValue = false;
						}
						string attribute3 = this.mXMLElement.GetAttribute("id");
						this.mApp.SetBoolean(attribute3, boolValue);
					}
					else if (this.mXMLElement.mValue.ToString() == "Integer")
					{
						string text3 = "";
						if (!this.ParseSingleElement(text3))
						{
							return false;
						}
						int anInt = 0;
						if (!Common.StringToInt(text3, ref anInt))
						{
							this.Fail("Invalid Integer Value: '" + text3 + "'");
							return false;
						}
						string attribute4 = this.mXMLElement.GetAttribute("id");
						this.mApp.SetInteger(attribute4, anInt);
					}
					else
					{
						if (!(this.mXMLElement.mValue.ToString() == "Double"))
						{
							this.Fail("Invalid Section '" + this.mXMLElement.mValue + "'");
							return false;
						}
						string text4 = "";
						if (!this.ParseSingleElement(text4))
						{
							return false;
						}
						double aDouble = 0.0;
						if (!Common.StringToDouble(text4, ref aDouble))
						{
							this.Fail("Invalid Double Value: '" + text4 + "'");
							return false;
						}
						string attribute5 = this.mXMLElement.GetAttribute("id");
						this.mApp.SetDouble(attribute5, aDouble);
					}
				}
				else
				{
					if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						this.Fail("Element Not Expected '" + this.mXMLElement.mValue + "'");
						return false;
					}
					if (this.mXMLElement.mType == XMLElement.XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
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
					if (!this.mXMLParser.NextElement(xmlelement))
					{
						break;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
					{
						if (!(xmlelement.mValue.ToString() == "Properties"))
						{
							goto IL_4B;
						}
						if (!this.ParseProperties())
						{
							break;
						}
					}
					else if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_5;
					}
				}
				goto IL_8C;
				IL_4B:
				this.Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_8C;
				Block_5:
				this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_8C:
			if (this.mXMLParser.HasFailed())
			{
				this.Fail(this.mXMLParser.GetErrorText());
			}
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		public PropertiesParser(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mHasFailed = false;
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

		public bool ParsePropertiesBuffer(byte[] theBuffer)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.SetBytes(theBuffer);
			return this.DoParseProperties();
		}

		public string GetErrorText()
		{
			return this.mError;
		}

		public SexyAppBase mApp;

		public string mError = "";

		public bool mHasFailed;

		private XMLParser mXMLParser;

		private XMLElement mXMLElement = new XMLElement();
	}
}
