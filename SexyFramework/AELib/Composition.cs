using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.AELib
{
	public class Composition : Layer
	{
		public Composition()
		{
		}

		public Composition(Composition other)
		{
			this.CopyFrom(other);
		}

		public void CopyFrom(Composition other)
		{
			base.CopyFrom(other);
			this.CopyLayersFrom(other);
			this.mMaxDuration = other.mMaxDuration;
			this.mLoadImageFunc = other.mLoadImageFunc;
			this.mPostLoadImageFunc = other.mPostLoadImageFunc;
			this.mPreLayerDrawFunc = other.mPreLayerDrawFunc;
			this.mPreLayerDrawData = other.mPreLayerDrawData;
			this.mUpdateCount = other.mUpdateCount;
			this.mLoop = other.mLoop;
			this.mMaxFrame = other.mMaxFrame;
		}

		public void CopyLayersFrom(Composition other)
		{
			this.mLayers.Clear();
			if (other.mLayers != null)
			{
				for (int i = 0; i < other.mLayers.Count; i++)
				{
					CompLayer compLayer = other.mLayers[i];
					this.mLayers.Add(new CompLayer(compLayer.mSource.Duplicate(), compLayer.mStartFrameOnComp, compLayer.mDuration, compLayer.mLayerOffsetStart));
				}
			}
		}

		public override Layer Duplicate()
		{
			return new Composition(this);
		}

		public bool Done()
		{
			return this.mUpdateCount >= this.mMaxDuration;
		}

		public override bool isValid()
		{
			foreach (CompLayer compLayer in this.mLayers)
			{
				if (!compLayer.mSource.isValid())
				{
					return false;
				}
			}
			return true;
		}

		public bool LoadFromFile(string file_name)
		{
			return this.LoadFromFile(file_name, "Main");
		}

		public bool LoadFromFile(string file_name, string main_composition_name)
		{
			List<Composition> list = new List<Composition>();
			if (!AECommon.LoadPAX(file_name, list, this.mLoadImageFunc, this.mPostLoadImageFunc) || list.Count == 0)
			{
				return false;
			}
			Composition composition = null;
			for (int i = 0; i < list.Count; i++)
			{
				Composition composition2 = list[i];
				if (composition2.mLayerName.ToLower().Equals(main_composition_name.ToLower()))
				{
					composition = composition2;
					break;
				}
			}
			if (composition == null)
			{
				composition = list[0];
			}
			this.CopyFrom(composition);
			return true;
		}

		public void AddLayer(CompLayer c)
		{
			this.mLayers.Add(new CompLayer(c));
		}

		public void AddLayer(Layer l, int start_frame, int duration, int layer_offset)
		{
			this.mLayers.Add(new CompLayer(l, start_frame, duration, layer_offset));
		}

		public void Update()
		{
			this.mUpdateCount++;
			if (this.mMaxFrame == -1)
			{
				this.mMaxFrame = this.mMaxDuration;
			}
			if (this.mLoop && this.mUpdateCount >= this.mMaxFrame)
			{
				this.Reset();
				this.mUpdateCount = 1;
			}
		}

		public override void Draw(Graphics g)
		{
			this.Draw(g, null);
		}

		public override void Draw(Graphics g, CumulativeTransform ctrans)
		{
			this.Draw(g, ctrans, -1);
		}

		public override void Draw(Graphics g, CumulativeTransform ctrans, int frame)
		{
			this.Draw(g, ctrans, frame, 1f);
		}

		public override void Draw(Graphics g, CumulativeTransform ctrans, int frame, float scale)
		{
			if (frame == -1)
			{
				frame = this.mUpdateCount;
			}
			bool flag = false;
			for (int i = this.mLayers.Count - 1; i >= 0; i--)
			{
				CompLayer compLayer = this.mLayers[i];
				if (frame >= compLayer.mStartFrameOnComp && frame < compLayer.mStartFrameOnComp + compLayer.mDuration)
				{
					int frame2 = frame - compLayer.mStartFrameOnComp + compLayer.mLayerOffsetStart;
					CumulativeTransform cumulativeTransform = ctrans;
					CumulativeTransform cumulativeTransform2 = new CumulativeTransform();
					if (ctrans == null)
					{
						ctrans = cumulativeTransform2;
					}
					else if (!flag)
					{
						flag = true;
						float num = 1f;
						float num2 = 0f;
						this.mOpacity.GetValue(frame, ref num);
						ctrans.mOpacity *= num;
						SexyTransform2D theMat = new SexyTransform2D(false);
						this.mAnchorPoint.GetValue(frame, ref num, ref num2);
						float num3 = num * scale;
						float num4 = num2 * scale;
						theMat.Translate(-num3, -num4);
						float sx = 1f;
						float sy = 1f;
						this.mScale.GetValue(frame, ref sx, ref sy);
						theMat.Scale(sx, sy);
						this.mRotation.GetValue(frame, ref num);
						if (num != 0f)
						{
							theMat.RotateRad(-num);
						}
						this.mPosition.GetValue(frame, ref num, ref num2);
						theMat.Translate(num * scale, num2 * scale);
						ctrans.mTrans *= theMat;
					}
					if (compLayer.mSource.IsLayerBase() && this.mPreLayerDrawFunc != null)
					{
						this.mPreLayerDrawFunc(g, compLayer.mSource, this.mPreLayerDrawData);
					}
					CumulativeTransform other = new CumulativeTransform(ctrans);
					if (this.mAdditive)
					{
						ctrans.mForceAdditive = true;
					}
					if (compLayer.mSource.NeedsTranslatedFrame())
					{
						compLayer.mSource.Draw(g, ctrans, frame2, scale);
					}
					else
					{
						compLayer.mSource.Draw(g, ctrans, frame, scale);
					}
					ctrans.CopyFrom(other);
					ctrans = cumulativeTransform;
				}
			}
		}

		public override void Reset()
		{
			this.mUpdateCount = 0;
			for (int i = 0; i < this.mLayers.Count; i++)
			{
				this.mLayers[i].mSource.Reset();
			}
		}

		public Layer GetLayerAtIdx(int idx)
		{
			return this.mLayers[idx].mSource;
		}

		public override bool NeedsTranslatedFrame()
		{
			return true;
		}

		public int GetMaxDuration()
		{
			return this.mMaxDuration;
		}

		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		public int GetNumLayers()
		{
			return this.mLayers.Count;
		}

		public override bool IsLayerBase()
		{
			return false;
		}

		public void SetMaxDuration(int m)
		{
			this.mMaxDuration = m;
		}

		public static void DefaultPostLoadImageFunc(SharedImageRef img, Layer l)
		{
		}

		public static SharedImageRef DefaultLoadImageFunc(string file_dir, string file_name)
		{
			return GlobalMembers.gSexyApp.GetSharedImage(file_dir + file_name);
		}

		public void Dispose()
		{
		}

		protected List<CompLayer> mLayers = new List<CompLayer>();

		protected int mMaxDuration;

		public AECommon.LoadCompImageFunc mLoadImageFunc = new AECommon.LoadCompImageFunc(Composition.DefaultLoadImageFunc);

		public AECommon.PostLoadCompImageFunc mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(Composition.DefaultPostLoadImageFunc);

		public AECommon.PreLayerDrawFunc mPreLayerDrawFunc;

		public object mPreLayerDrawData;

		public int mUpdateCount;

		public bool mLoop;

		public int mMaxFrame = -1;
	}
}
