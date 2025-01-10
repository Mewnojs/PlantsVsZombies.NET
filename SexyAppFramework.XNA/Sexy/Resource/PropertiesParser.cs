using System;
using System.Collections.Generic;

namespace Sexy
{
    public/*internal*/ class PropertiesParser
    {
        protected void Fail(string theErrorText)
        {
            if (!mHasFailed)
            {
                mHasFailed = true;
                int currentLineNum = mXMLParser.GetCurrentLineNum();
                mError = theErrorText;
                if (currentLineNum > 0)
                {
                    mError += Common.StrFormat_(" on Line {0}", currentLineNum);
                }
                if (mXMLParser.GetFileName().Length != 0)
                {
                    mError += Common.StrFormat_(" in File '{0}'", mXMLParser.GetFileName());
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
                if (!mXMLParser.NextElement(ref xmlelement))
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
            Fail("Unexpected Section: '" + xmlelement.mValue + "'");
            return false;
        }

        protected bool ParseStringArray(ref List<string> theStringVector)
        {
            theStringVector.Clear();
            XMLElement xmlelement;
            for (;;)
            {
                xmlelement = new XMLElement();
                if (!mXMLParser.NextElement(ref xmlelement))
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
                    if (!ParseSingleElement(ref empty))
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
            Fail("Invalid Section '" + xmlelement.mValue + "'");
            return false;
            Block_5:
            Fail("Element Not Expected '" + xmlelement.mValue + "'");
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
                if (!mXMLParser.NextElement(ref xmlelement))
                {
                    break;
                }
                if (xmlelement.mType == XMLElementType.TYPE_START)
                {
                    if (xmlelement.mValue == "String")
                    {
                        string empty = string.Empty;
                        if (!ParseSingleElement(ref empty))
                        {
                            return false;
                        }
                        string theId = xmlelement.mAttributes["id"];
                        mApp.SetString(theId, empty);
                    }
                    else if (xmlelement.mValue == "StringArray")
                    {
                        List<string> list = new List<string>();
                        if (!ParseStringArray(ref list))
                        {
                            return false;
                        }
                        string text = xmlelement.mAttributes["id"];
                        mApp.mStringVectorProperties.Add(text, list);
                    }
                    else if (xmlelement.mValue == "Boolean")
                    {
                        text2 = string.Empty;
                        if (!ParseSingleElement(ref text2))
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
                        mApp.SetBoolean(theId2, theValue);
                    }
                    else if (xmlelement.mValue == "Integer")
                    {
                        empty2 = string.Empty;
                        if (!ParseSingleElement(ref empty2))
                        {
                            return false;
                        }
                        int theValue2 = 0;
                        if (!Common.StringToInt(empty2, ref theValue2))
                        {
                            goto Block_16;
                        }
                        string theId3 = xmlelement.mAttributes["id"];
                        mApp.SetInteger(theId3, theValue2);
                    }
                    else
                    {
                        if (!(xmlelement.mValue == "Double"))
                        {
                            goto IL_28B;
                        }
                        empty3 = string.Empty;
                        if (!ParseSingleElement(ref empty3))
                        {
                            return false;
                        }
                        double theValue3 = 0.0;
                        if (!Common.StringToDouble(empty3, ref theValue3))
                        {
                            goto Block_19;
                        }
                        string theId4 = xmlelement.mAttributes["id"];
                        mApp.SetDouble(theId4, theValue3);
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
            Fail("Invalid Boolean Value: '" + text2 + "'");
            return false;
            Block_16:
            Fail("Invalid Integer Value: '" + empty2 + "'");
            return false;
            Block_19:
            Fail("Invalid Double Value: '" + empty3 + "'");
            return false;
            IL_28B:
            Fail("Invalid Section '" + xmlelement.mValue + "'");
            return false;
            Block_20:
            Fail("Element Not Expected '" + xmlelement.mValue + "'");
            return false;
        }

        protected bool DoParseProperties()
        {
            if (!mXMLParser.HasFailed())
            {
                XMLElement xmlelement;
                for (;;)
                {
                    xmlelement = new XMLElement();
                    if (!mXMLParser.NextElement(ref xmlelement))
                    {
                        break;
                    }
                    if (xmlelement.mType == XMLElementType.TYPE_START)
                    {
                        if (!(xmlelement.mValue == "Properties"))
                        {
                            goto IL_47;
                        }
                        if (!ParseProperties())
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
                Fail("Invalid Section '" + xmlelement.mValue + "'");
                goto IL_88;
                Block_5:
                Fail("Element Not Expected '" + xmlelement.mValue + "'");
            }
            IL_88:
            if (mXMLParser.HasFailed())
            {
                Fail(mXMLParser.GetErrorText());
            }
            mXMLParser.Dispose();
            mXMLParser = null;
            return !mHasFailed;
        }

        public PropertiesParser(SexyAppBase theApp)
        {
            mApp = theApp;
            mHasFailed = false;
            mXMLParser = null;
        }

        public virtual void Dispose()
        {
        }

        public bool ParsePropertiesFile(string theFilename)
        {
            mXMLParser = new XMLParser();
            mXMLParser.OpenFile(theFilename);
            return DoParseProperties();
        }

        public bool ParsePropertiesBuffer(Buffer theBuffer)
        {
            mXMLParser = new XMLParser();
            mXMLParser.SetStringSource("");
            return DoParseProperties();
        }

        public string GetErrorText()
        {
            return mError;
        }

        public SexyAppBase mApp;

        public XMLParser mXMLParser;

        public string mError;

        public bool mHasFailed;
    }
}
