using System;
using System.Collections.Generic;

namespace Sexy.WidgetsLib
{
	public class PASpriteDef
	{
		public virtual void Dispose()
		{
		}

		public int GetLabelFrame(string theLabel)
		{
			string text = theLabel.ToUpper();
			if (!this.mLabels.ContainsKey(text))
			{
				return -1;
			}
			return this.mLabels[text];
		}

		public void GetLabelFrameRange(string theLabel, ref int theStart, ref int theEnd)
		{
			theStart = this.GetLabelFrame(theLabel);
			theEnd = -1;
			if (theStart == -1)
			{
				return;
			}
			string text = theLabel.ToUpper();
			Dictionary<string, int>.Enumerator enumerator = this.mLabels.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text2 = text;
				KeyValuePair<string, int> keyValuePair = enumerator.Current;
				if (text2 != keyValuePair.Key)
				{
					KeyValuePair<string, int> keyValuePair2 = enumerator.Current;
					if (keyValuePair2.Value > theStart)
					{
						if (theEnd >= 0)
						{
							KeyValuePair<string, int> keyValuePair3 = enumerator.Current;
							if (keyValuePair3.Value >= theEnd)
							{
								continue;
							}
						}
						KeyValuePair<string, int> keyValuePair4 = enumerator.Current;
						theEnd = keyValuePair4.Value - 1;
					}
				}
			}
			if (theEnd < 0)
			{
				theEnd = this.mFrames.Count - 1;
			}
		}

		public string mName;

		public List<PAFrame> mFrames = new List<PAFrame>();

		public int mWorkAreaStart;

		public int mWorkAreaDuration;

		public Dictionary<string, int> mLabels = new Dictionary<string, int>();

		public List<PAObjectDef> mObjectDefVector = new List<PAObjectDef>();

		public float mAnimRate;
	}
}
