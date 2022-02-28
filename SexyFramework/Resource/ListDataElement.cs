using System;
using System.Collections.Generic;

namespace Sexy.Resource
{
	public class ListDataElement : DataElement
	{
		public ListDataElement()
		{
			this.mIsList = true;
		}

		public ListDataElement(ListDataElement theListDataElement)
		{
			this.mIsList = true;
			for (int i = 0; i < theListDataElement.mElementVector.Count; i++)
			{
				this.mElementVector.Add(theListDataElement.mElementVector[i].Duplicate());
			}
		}

		public override void Dispose()
		{
			for (int i = 0; i < this.mElementVector.Count; i++)
			{
				if (this.mElementVector[i] != null)
				{
					this.mElementVector[i].Dispose();
				}
			}
			base.Dispose();
		}

		public ListDataElement CopyFrom(ListDataElement theListDataElement)
		{
			for (int i = 0; i < this.mElementVector.Count; i++)
			{
				if (this.mElementVector[i] != null)
				{
					this.mElementVector[i].Dispose();
				}
			}
			this.mElementVector.Clear();
			for (int j = 0; j < theListDataElement.mElementVector.Count; j++)
			{
				this.mElementVector.Add(theListDataElement.mElementVector[j].Duplicate());
			}
			return this;
		}

		public override DataElement Duplicate()
		{
			return new ListDataElement(this);
		}

		public List<DataElement> mElementVector = new List<DataElement>();
	}
}
