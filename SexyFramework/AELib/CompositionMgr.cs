using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;

namespace Sexy.AELib
{
	public class CompositionMgr
	{
		public CompositionMgr()
		{
		}

		public CompositionMgr(CompositionMgr other)
		{
			this.CopyFrom(other);
		}

		public bool isValid()
		{
			foreach (Composition composition in this.mCompositions.Values)
			{
				if (!composition.isValid())
				{
					return false;
				}
			}
			return true;
		}

		public void CopyFrom(CompositionMgr other)
		{
			this.mLoadImageFunc = other.mLoadImageFunc;
			this.mPostLoadImageFunc = other.mPostLoadImageFunc;
			this.mPreLayerDrawFunc = other.mPreLayerDrawFunc;
			this.mCompositions = new Dictionary<string, Composition>();
			foreach (KeyValuePair<string, Composition> keyValuePair in other.mCompositions)
			{
				this.mCompositions.Add(keyValuePair.Key, new Composition(keyValuePair.Value));
			}
		}

		public bool LoadFromFile(string file_name)
		{
			List<Composition> list = new List<Composition>();
			if (!AECommon.LoadPAX(file_name, list, this.mLoadImageFunc, this.mPostLoadImageFunc))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].mLayerName.ToLower();
				if (!this.mCompositions.ContainsKey(text))
				{
					this.mCompositions.Add(text, null);
				}
				this.mCompositions[text] = new Composition(list[i]);
			}
			return true;
		}

		public void UpdateAll()
		{
			foreach (KeyValuePair<string, Composition> keyValuePair in this.mCompositions)
			{
				keyValuePair.Value.Update();
			}
		}

		public void Update(string comp_name)
		{
			if (this.mCompositions.ContainsKey(comp_name))
			{
				this.mCompositions[comp_name].Update();
			}
		}

		public void DrawAll(Graphics g)
		{
			this.DrawAll(g, null);
		}

		public void DrawAll(Graphics g, CumulativeTransform ctrans)
		{
			this.DrawAll(g, ctrans, 1f);
		}

		public void DrawAll(Graphics g, CumulativeTransform ctrans, float scale)
		{
			foreach (KeyValuePair<string, Composition> keyValuePair in this.mCompositions)
			{
				keyValuePair.Value.Draw(g, ctrans, -1, scale);
			}
		}

		public void Draw(Graphics g, string comp_name)
		{
			this.Draw(g, comp_name, null);
		}

		public void Draw(Graphics g, string comp_name, CumulativeTransform ctrans)
		{
			this.Draw(g, comp_name, ctrans, -1);
		}

		public void Draw(Graphics g, string comp_name, CumulativeTransform ctrans, int frame)
		{
			this.Draw(g, comp_name, ctrans, frame, 1f);
		}

		public void Draw(Graphics g, string comp_name, CumulativeTransform ctrans, int frame, float scale)
		{
			if (this.mCompositions.ContainsKey(comp_name.ToLower()))
			{
				this.mCompositions[comp_name.ToLower()].Draw(g, ctrans, frame, scale);
			}
		}

		public Composition GetComposition(string comp_name)
		{
			if (this.mCompositions.ContainsKey(comp_name.ToLower()))
			{
				return this.mCompositions[comp_name.ToLower()];
			}
			return null;
		}

		public void GetListOfComps(List<string> comp_names)
		{
			foreach (KeyValuePair<string, Composition> keyValuePair in this.mCompositions)
			{
				comp_names.Add(keyValuePair.Key);
			}
			comp_names.Sort();
		}

		public void GetAllCompositions(List<Composition> comps)
		{
			foreach (KeyValuePair<string, Composition> keyValuePair in this.mCompositions)
			{
				comps.Add(keyValuePair.Value);
			}
		}

		protected Dictionary<string, Composition> mCompositions = new Dictionary<string, Composition>();

		public AECommon.LoadCompImageFunc mLoadImageFunc = new AECommon.LoadCompImageFunc(Composition.DefaultLoadImageFunc);

		public AECommon.PostLoadCompImageFunc mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(Composition.DefaultPostLoadImageFunc);

		public AECommon.PreLayerDrawFunc mPreLayerDrawFunc;
	}
}
