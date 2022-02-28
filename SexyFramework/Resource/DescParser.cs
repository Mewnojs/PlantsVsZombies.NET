using System;
using System.Collections.Generic;
using System.Text;

namespace Sexy.Resource
{
	public abstract class DescParser : EncodingParser
	{
		public virtual bool Error(string theError)
		{
			this.mError = this.mError + "\n" + theError;
			return false;
		}

		public virtual DataElement Dereference(string theString)
		{
			string text = theString.ToUpper();
			if (this.mDefineMap.ContainsKey(text))
			{
				return this.mDefineMap[text];
			}
			return null;
		}

		public bool IsImmediate(string theString)
		{
			return (theString[0] >= '0' && theString[0] <= '9') || theString[0] == '-' || theString[0] == '+' || theString[0] == '\'' || theString[0] == '"';
		}

		public string Unquote(string theQuotedString)
		{
			if (theQuotedString[0] == '\'' || theQuotedString[0] == '"')
			{
				char c = theQuotedString[0];
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				bool flag2 = false;
				for (int i = 1; i < theQuotedString.Length - 1; i++)
				{
					if (flag2)
					{
						char c2 = theQuotedString[i];
						char c3 = c2;
						if (c3 != 'n')
						{
							if (c3 == 't')
							{
								c2 = '\t';
							}
						}
						else
						{
							c2 = '\n';
						}
						stringBuilder.Append(c2);
						flag2 = false;
					}
					else if (theQuotedString[i] == c)
					{
						if (flag)
						{
							stringBuilder.Append(c);
						}
						flag = true;
					}
					else if (theQuotedString[i] == '\\')
					{
						flag2 = true;
						flag = false;
					}
					else
					{
						stringBuilder.Append(theQuotedString[i]);
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}
			return theQuotedString;
		}

		public bool GetValues(ListDataElement theSource, ListDataElement theValues)
		{
			theValues.mElementVector.Clear();
			for (int i = 0; i < theSource.mElementVector.Count; i++)
			{
				if (theSource.mElementVector[i].mIsList)
				{
					ListDataElement listDataElement = new ListDataElement();
					theValues.mElementVector.Add(listDataElement);
					if (!this.GetValues((ListDataElement)theSource.mElementVector[i], listDataElement))
					{
						return false;
					}
				}
				else
				{
					string text = ((SingleDataElement)theSource.mElementVector[i]).mString.ToString();
					if (text.Length > 0)
					{
						if (text[0] == '\'' || text[0] == '"')
						{
							SingleDataElement singleDataElement = new SingleDataElement(this.Unquote(text));
							theValues.mElementVector.Add(singleDataElement);
						}
						else if (this.IsImmediate(text))
						{
							theValues.mElementVector.Add(new SingleDataElement(text));
						}
						else
						{
							string text2 = text.ToUpper();
							if (!this.mDefineMap.ContainsKey(text2))
							{
								this.Error("Unable to Dereference \"" + text + "\"");
								return false;
							}
							theValues.mElementVector.Add(this.mDefineMap[text2].Duplicate());
						}
					}
				}
			}
			return true;
		}

		public string DataElementToString(DataElement theDataElement, bool enclose)
		{
			if (theDataElement.mIsList)
			{
				ListDataElement listDataElement = (ListDataElement)theDataElement;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(enclose ? "(" : "");
				for (int i = 0; i < listDataElement.mElementVector.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(enclose ? ", " : " ");
					}
					stringBuilder.Append(this.DataElementToString(listDataElement.mElementVector[i], true));
				}
				stringBuilder.Append(enclose ? ")" : "");
				return stringBuilder.ToString();
			}
			SingleDataElement singleDataElement = (SingleDataElement)theDataElement;
			if (singleDataElement.mValue != null)
			{
				return singleDataElement.mString + "=" + this.DataElementToString(singleDataElement.mValue, true);
			}
			return singleDataElement.mString.ToString();
		}

		public bool DataToString(DataElement theSource, ref string theString)
		{
			theString = "";
			if (theSource.mIsList)
			{
				return false;
			}
			if (((SingleDataElement)theSource).mValue != null)
			{
				return false;
			}
			string text = ((SingleDataElement)theSource).mString.ToString();
			DataElement dataElement = this.Dereference(text);
			if (dataElement != null)
			{
				if (dataElement.mIsList)
				{
					return false;
				}
				theString = this.Unquote(((SingleDataElement)dataElement).mString.ToString());
			}
			else
			{
				theString = this.Unquote(text);
			}
			return true;
		}

		public bool DataToKeyAndValue(DataElement theSource, ref string theKey, ref DataElement theValue)
		{
			theKey = "";
			if (theSource.mIsList)
			{
				return false;
			}
			if (((SingleDataElement)theSource).mValue == null)
			{
				return false;
			}
			theValue = ((SingleDataElement)theSource).mValue;
			string text = ((SingleDataElement)theSource).mString.ToString();
			DataElement dataElement = this.Dereference(text);
			if (dataElement != null)
			{
				if (dataElement.mIsList)
				{
					return false;
				}
				theKey = this.Unquote(((SingleDataElement)dataElement).mString.ToString());
			}
			else
			{
				theKey = this.Unquote(text);
			}
			return true;
		}

		public bool DataToInt(DataElement theSource, ref int theInt)
		{
			theInt = 0;
			string theString = "";
			return this.DataToString(theSource, ref theString) && Common.StringToInt(theString, ref theInt);
		}

		public bool DataToDouble(DataElement theSource, ref double theDouble)
		{
			theDouble = 0.0;
			string aTempString = "";
			return this.DataToString(theSource, ref aTempString) && Common.StringToDouble(aTempString, ref theDouble);
		}

		public bool DataToBoolean(DataElement theSource, ref bool theBool)
		{
			theBool = false;
			string text = "";
			if (!this.DataToString(theSource, ref text))
			{
				return false;
			}
			if (text == "false" || text == "no" || text == "0")
			{
				theBool = false;
				return true;
			}
			if (text == "true" || text == "yes" || text == "1")
			{
				theBool = true;
				return true;
			}
			return false;
		}

		public bool DataToStringVector(DataElement theSource, ref List<string> theStringVector)
		{
			theStringVector.Clear();
			ListDataElement listDataElement = new ListDataElement();
			ListDataElement listDataElement2;
			if (theSource.mIsList)
			{
				if (!this.GetValues((ListDataElement)theSource, listDataElement))
				{
					return false;
				}
				listDataElement2 = listDataElement;
			}
			else
			{
				string text = ((SingleDataElement)theSource).mString.ToString();
				DataElement dataElement = this.Dereference(text);
				if (dataElement == null)
				{
					this.Error("Unable to Dereference \"" + text + "\"");
					return false;
				}
				if (!dataElement.mIsList)
				{
					return false;
				}
				listDataElement2 = (ListDataElement)dataElement;
			}
			for (int i = 0; i < listDataElement2.mElementVector.Count; i++)
			{
				if (listDataElement2.mElementVector[i].mIsList)
				{
					theStringVector.Clear();
					return false;
				}
				SingleDataElement singleDataElement = (SingleDataElement)listDataElement2.mElementVector[i];
				theStringVector.Add(singleDataElement.mString.ToString());
			}
			return true;
		}

		public bool DataToList(DataElement theSource, ref ListDataElement theValues)
		{
			if (theSource.mIsList)
			{
				return this.GetValues((ListDataElement)theSource, theValues);
			}
			DataElement dataElement = this.Dereference(((SingleDataElement)theSource).mString.ToString());
			if (dataElement == null || !dataElement.mIsList)
			{
				return false;
			}
			ListDataElement listDataElement = (ListDataElement)dataElement;
			theValues = listDataElement;
			return true;
		}

		public bool DataToIntVector(DataElement theSource, ref List<int> theIntVector)
		{
			theIntVector.Clear();
			List<string> list = new List<string>();
			if (!this.DataToStringVector(theSource, ref list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				int num = 0;
				if (!Common.StringToInt(list[i], ref num))
				{
					return false;
				}
				theIntVector.Add(num);
			}
			return true;
		}

		public bool DataToDoubleVector(DataElement theSource, ref List<double> theDoubleVector)
		{
			theDoubleVector.Clear();
			List<string> list = new List<string>();
			if (!this.DataToStringVector(theSource, ref list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				double num = 0.0;
				if (!Common.StringToDouble(list[i], ref num))
				{
					return false;
				}
				theDoubleVector.Add(num);
			}
			return true;
		}

		public bool ParseToList(string theString, ref ListDataElement theList, bool expectListEnd, ref int theStringPos)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			SingleDataElement singleDataElement = null;
			SingleDataElement singleDataElement2 = null;
			int num = 0;
			if (theStringPos == 0)
			{
				theStringPos = num;
			}
			while (theStringPos < theString.Length)
			{
				bool flag5 = false;
				char c = theString[theStringPos++];
				bool flag6 = c == ' ' || c == '\t' || c == '\n' || c == ',';
				if (flag3)
				{
					flag5 = true;
				}
				else
				{
					if (c == '\'' && !flag2)
					{
						flag = !flag;
					}
					else if (c == '"' && !flag)
					{
						flag2 = !flag2;
					}
					if (c == '\\')
					{
						flag3 = true;
					}
					else if (!flag && !flag2)
					{
						if (c == ')')
						{
							if (expectListEnd)
							{
								return true;
							}
							this.Error("Unexpected List End");
							return false;
						}
						else if (c == '(')
						{
							if (flag4)
							{
								singleDataElement2 = null;
								flag4 = false;
							}
							if (singleDataElement2 != null)
							{
								this.Error("Unexpected List Start");
								return false;
							}
							ListDataElement listDataElement = new ListDataElement();
							if (!this.ParseToList(theString, ref listDataElement, true, ref theStringPos))
							{
								return false;
							}
							if (singleDataElement != null)
							{
								singleDataElement.mValue = listDataElement;
								singleDataElement = null;
							}
							else
							{
								theList.mElementVector.Add(listDataElement);
							}
						}
						else if (c == '=')
						{
							singleDataElement = singleDataElement2;
							flag4 = true;
						}
						else if (flag6)
						{
							if (singleDataElement2 != null && singleDataElement2.mString.Length > 0)
							{
								flag4 = true;
							}
						}
						else
						{
							if (flag4)
							{
								singleDataElement2 = null;
								flag4 = false;
							}
							flag5 = true;
						}
					}
					else
					{
						if (flag4)
						{
							singleDataElement2 = null;
							flag4 = false;
						}
						flag5 = true;
					}
				}
				if (flag5)
				{
					if (singleDataElement2 == null)
					{
						singleDataElement2 = new SingleDataElement();
						if (singleDataElement != null)
						{
							singleDataElement.mValue = singleDataElement2;
							singleDataElement = null;
						}
						else
						{
							theList.mElementVector.Add(singleDataElement2);
						}
					}
					if (flag3)
					{
						singleDataElement2.mString.Append("\\");
						flag3 = false;
					}
					singleDataElement2.mString.Append(c);
				}
			}
			if (flag)
			{
				this.Error("Unterminated Single Quotes");
				return false;
			}
			if (flag2)
			{
				this.Error("Unterminated Double Quotes");
				return false;
			}
			if (expectListEnd)
			{
				this.Error("Unterminated List");
				return false;
			}
			return true;
		}

		public bool ParseDescriptorLine(string theDescriptorLine)
		{
			ListDataElement listDataElement = new ListDataElement();
			int num = 0;
			if (!this.ParseToList(theDescriptorLine, ref listDataElement, false, ref num))
			{
				return false;
			}
			if (listDataElement.mElementVector.Count > 0)
			{
				if (listDataElement.mElementVector[0].mIsList)
				{
					this.Error("Missing Command");
					return false;
				}
				if (!this.HandleCommand(listDataElement))
				{
					return false;
				}
			}
			return true;
		}

		public abstract bool HandleCommand(ListDataElement theParams);

		public DescParser()
		{
			this.mCmdSep = 1;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public virtual bool LoadDescriptor(string theFileName)
		{
			this.mCurrentLineNum = 0;
			int num = 0;
			bool flag = false;
			this.mError = "";
			this.mCurrentLine.Clear();
			if (!base.OpenFile(theFileName))
			{
				return this.Error("Unable to open file: " + theFileName);
			}
			while (!this.EndOfFile())
			{
				char c = '0';
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				for (;;)
				{
					EncodingParser.GetCharReturnType @char = this.GetChar(ref c);
					if (@char == EncodingParser.GetCharReturnType.END_OF_FILE)
					{
						break;
					}
					if (@char == EncodingParser.GetCharReturnType.INVALID_CHARACTER)
					{
						goto Block_3;
					}
					if (@char != EncodingParser.GetCharReturnType.SUCCESSFUL)
					{
						goto Block_4;
					}
					if (c != '\r')
					{
						if (c == '\n')
						{
							num++;
						}
						if ((c == ' ' || c == '\t') && flag3)
						{
							flag7 = true;
						}
						if (!flag3 || (c != ' ' && c != '\t' && c != '\n'))
						{
							if (flag3)
							{
								if ((this.mCmdSep & 2) != 0 && !flag7 && this.mCurrentLine.Length > 0)
								{
									goto Block_15;
								}
								if (c == '#')
								{
									flag2 = true;
								}
								flag3 = false;
							}
							if (c == '\n')
							{
								flag7 = false;
								flag3 = true;
							}
							if (c == '\n' && flag2)
							{
								flag2 = false;
							}
							else if (!flag2)
							{
								if (c == '\\' && (flag4 || flag5) && !flag6)
								{
									flag6 = true;
								}
								else
								{
									if (c == '\'' && !flag5 && !flag6)
									{
										flag4 = !flag4;
									}
									if (c == '"' && !flag4 && !flag6)
									{
										flag5 = !flag5;
									}
									if (c == ';' && (this.mCmdSep & 1) != 0 && !flag4 && !flag5)
									{
										break;
									}
									if (flag6)
									{
										this.mCurrentLine.Append('\\');
										flag6 = false;
									}
									if (this.mCurrentLine.Length == 0)
									{
										this.mCurrentLineNum = num + 1;
									}
									this.mCurrentLine.Append(c);
								}
							}
						}
					}
				}
				IL_1A5:
				if (this.mCurrentLine.Length <= 0)
				{
					continue;
				}
				if (!this.ParseDescriptorLine(this.mCurrentLine.ToString()))
				{
					flag = true;
					break;
				}
				this.mCurrentLine.Clear();
				continue;
				Block_3:
				return this.Error("Invalid Character");
				Block_15:
				this.PutChar(c);
				goto IL_1A5;
				Block_4:
				return this.Error("Internal Error");
			}
			this.mCurrentLine.Clear();
			this.mCurrentLineNum = 0;
			this.CloseFile();
			return !flag;
		}

		public virtual bool LoadDescriptor(byte[] buffer)
		{
			this.mCurrentLineNum = 0;
			int num = 0;
			bool flag = false;
			this.mError = "";
			this.mCurrentLine.Clear();
			if (buffer == null)
			{
				return this.Error("Unable to open file: ");
			}
			this.SetBytes(buffer);
			while (!this.EndOfFile())
			{
				char c = '0';
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				for (;;)
				{
					EncodingParser.GetCharReturnType @char = this.GetChar(ref c);
					if (@char == EncodingParser.GetCharReturnType.END_OF_FILE)
					{
						break;
					}
					if (@char == EncodingParser.GetCharReturnType.INVALID_CHARACTER)
					{
						goto Block_3;
					}
					if (@char != EncodingParser.GetCharReturnType.SUCCESSFUL)
					{
						goto Block_4;
					}
					if (c != '\r')
					{
						if (c == '\n')
						{
							num++;
						}
						if ((c == ' ' || c == '\t') && flag3)
						{
							flag7 = true;
						}
						if (!flag3 || (c != ' ' && c != '\t' && c != '\n'))
						{
							if (flag3)
							{
								if ((this.mCmdSep & 2) != 0 && !flag7 && this.mCurrentLine.Length > 0)
								{
									goto Block_15;
								}
								if (c == '#')
								{
									flag2 = true;
								}
								flag3 = false;
							}
							if (c == '\n')
							{
								flag7 = false;
								flag3 = true;
							}
							if (c == '\n' && flag2)
							{
								flag2 = false;
							}
							else if (!flag2)
							{
								if (c == '\\' && (flag4 || flag5) && !flag6)
								{
									flag6 = true;
								}
								else
								{
									if (c == '\'' && !flag5 && !flag6)
									{
										flag4 = !flag4;
									}
									if (c == '"' && !flag4 && !flag6)
									{
										flag5 = !flag5;
									}
									if (c == ';' && (this.mCmdSep & 1) != 0 && !flag4 && !flag5)
									{
										break;
									}
									if (flag6)
									{
										this.mCurrentLine.Append('\\');
										flag6 = false;
									}
									if (this.mCurrentLine.Length == 0)
									{
										this.mCurrentLineNum = num + 1;
									}
									this.mCurrentLine.Append(c);
								}
							}
						}
					}
				}
				IL_1A2:
				if (this.mCurrentLine.Length > 0)
				{
					if (!this.ParseDescriptorLine(this.mCurrentLine.ToString()))
					{
						flag = true;
					}
					this.mCurrentLine.Clear();
					continue;
				}
				continue;
				Block_3:
				return this.Error("Invalid Character");
				Block_15:
				this.PutChar(c);
				goto IL_1A2;
				Block_4:
				return this.Error("Internal Error");
			}
			this.mCurrentLine.Clear();
			this.mCurrentLineNum = 0;
			return !flag;
		}

		public virtual bool LoadDescriptorBuffered(string theFileName)
		{
			this.mCurrentLineNum = 0;
			int num = 0;
			bool flag = false;
			this.mCurrentLine.Clear();
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			if (!base.OpenFile(theFileName))
			{
				return false;
			}
			while (!this.EndOfFile())
			{
				char c = '0';
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				for (;;)
				{
					EncodingParser.GetCharReturnType @char = this.GetChar(ref c);
					if (@char == EncodingParser.GetCharReturnType.END_OF_FILE)
					{
						break;
					}
					if (@char == EncodingParser.GetCharReturnType.INVALID_CHARACTER)
					{
						return false;
					}
					if (@char != EncodingParser.GetCharReturnType.SUCCESSFUL)
					{
						return false;
					}
					if (c != '\r')
					{
						if (c == '\n')
						{
							num++;
						}
						if ((c == ' ' || c == '\t') && flag3)
						{
							flag7 = true;
						}
						if (!flag3 || (c != ' ' && c != '\t' && c != '\n'))
						{
							if (flag3)
							{
								if ((this.mCmdSep & 2) != 0 && !flag7 && this.mCurrentLine.Length > 0)
								{
									goto Block_15;
								}
								if (c == '#')
								{
									flag2 = true;
								}
								flag3 = false;
							}
							if (c == '\n')
							{
								flag7 = false;
								flag3 = true;
							}
							if (c == '\n' && flag2)
							{
								flag2 = false;
							}
							else if (!flag2)
							{
								if (c == '\\' && (flag4 || flag5) && !flag6)
								{
									flag6 = true;
								}
								else
								{
									if (c == '\'' && !flag5 && !flag6)
									{
										flag4 = !flag4;
									}
									if (c == '"' && !flag4 && !flag6)
									{
										flag5 = !flag5;
									}
									if (c == ';' && (this.mCmdSep & 1) != 0 && !flag4 && !flag5)
									{
										break;
									}
									if (flag6)
									{
										this.mCurrentLine.Append('\\');
										flag6 = false;
									}
									if (this.mCurrentLine.Length == 0)
									{
										this.mCurrentLineNum = num + 1;
									}
									this.mCurrentLine.Append(c);
								}
							}
						}
					}
				}
				IL_198:
				if (this.mCurrentLine.Length > 0)
				{
					list.Add(this.mCurrentLine.ToString());
					list2.Add(this.mCurrentLineNum);
					this.mCurrentLine.Clear();
					continue;
				}
				continue;
				Block_15:
				this.PutChar(c);
				goto IL_198;
			}
			this.mCurrentLine.Clear();
			this.mCurrentLineNum = 0;
			this.CloseFile();
			num = list.Count;
			for (int i = 0; i < num; i++)
			{
				this.mCurrentLineNum = list2[i];
				this.mCurrentLine.Clear();
				this.mCurrentLine.AppendLine(list[i]);
				if (!this.ParseDescriptorLine(this.mCurrentLine.ToString()))
				{
					flag = true;
					break;
				}
			}
			this.mCurrentLine.Clear();
			this.mCurrentLineNum = 0;
			return !flag;
		}

		public int mCmdSep;

		public string mError = "";

		public int mCurrentLineNum;

		public StringBuilder mCurrentLine = new StringBuilder();

		public Dictionary<string, DataElement> mDefineMap = new Dictionary<string, DataElement>();

		public enum ECMDSEP
		{
			CMDSEP_SEMICOLON = 1,
			CMDSEP_NO_INDENT
		}
	}
}
