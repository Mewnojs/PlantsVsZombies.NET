using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class XMLElement
	{
		public XMLElementType mType;

		public string mSection;

		public string mValue;

		public string mValueEncoded;

		public string mInstruction;

		public Dictionary<string, string> mAttributes = new Dictionary<string, string>();

		public Dictionary<string, string> mAttributesEncoded = new Dictionary<string, string>();

		public List<Dictionary<string, string>.Enumerator> mAttributeIteratorList = new List<Dictionary<string, string>.Enumerator>();

		public List<Dictionary<string, string>.Enumerator> mAttributeEncodedIteratorList = new List<Dictionary<string, string>.Enumerator>();
	}
}
