using System;
using System.Text;

namespace Sexy.Resource
{
	public class SingleDataElement : DataElement
	{
		public SingleDataElement()
		{
			this.mIsList = false;
			this.mValue = null;
		}

		public SingleDataElement(string theString)
		{
			this.mString = new StringBuilder(theString);
			this.mIsList = false;
			this.mValue = null;
		}

		public override void Dispose()
		{
			if (this.mValue != null && this.mValue != null)
			{
				this.mValue.Dispose();
			}
			base.Dispose();
		}

		public override DataElement Duplicate()
		{
			SingleDataElement singleDataElement = new SingleDataElement();
			singleDataElement.mString = this.mString;
			if (this.mValue != null)
			{
				singleDataElement.mValue = this.mValue.Duplicate();
			}
			return singleDataElement;
		}

		public StringBuilder mString = new StringBuilder();

		public DataElement mValue;
	}
}
