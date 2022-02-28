using System;
using Sexy.GraphicsLib;
using Sexy.WidgetsLib;

namespace Sexy.Resource
{
	public class ResourceRef
	{
		public ResourceRef()
		{
		}

		public ResourceRef(ResourceRef theResourceRef)
		{
			this.mBaseResP = theResourceRef.mBaseResP;
			if (this.mBaseResP != null)
			{
				this.mBaseResP.mRefCount++;
			}
		}

		public virtual void Dispose()
		{
			this.Release();
		}

		public virtual ResourceRef CopyFrom(ResourceRef theResourceRef)
		{
			this.Release();
			this.mBaseResP = theResourceRef.mBaseResP;
			if (this.mBaseResP != null)
			{
				this.mBaseResP.mRefCount++;
			}
			return this;
		}

		public bool HasResource()
		{
			return this.mBaseResP != null;
		}

		public void Release()
		{
			if (this.mBaseResP != null && this.mBaseResP.mParent != null)
			{
				this.mBaseResP.mParent.Deref(this.mBaseResP);
			}
			this.mBaseResP = null;
		}

		public string GetId()
		{
			if (this.mBaseResP == null)
			{
				return "";
			}
			return this.mBaseResP.mId;
		}

		public SharedImageRef GetSharedImageRef()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Image)
			{
				return null;
			}
			return ((ImageRes)this.mBaseResP).mImage;
		}

		public Image GetImage()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Image)
			{
				return null;
			}
			return ((ImageRes)this.mBaseResP).mImage.GetImage();
		}

		public MemoryImage GetMemoryImage()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Image)
			{
				return null;
			}
			return ((ImageRes)this.mBaseResP).mImage.GetMemoryImage();
		}

		public MemoryImage GetDeviceImage()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Image)
			{
				return null;
			}
			return ((ImageRes)this.mBaseResP).mImage.GetDeviceImage();
		}

		public int GetSoundID()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Sound)
			{
				return 0;
			}
			return ((SoundRes)this.mBaseResP).mSoundId;
		}

		public Font GetFont()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Font)
			{
				return null;
			}
			return ((FontRes)this.mBaseResP).mFont;
		}

		public ImageFont GetImageFont()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_Font)
			{
				return null;
			}
			return (ImageFont)((FontRes)this.mBaseResP).mFont;
		}

		public PopAnim GetPopAnim()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_PopAnim)
			{
				return null;
			}
			return ((PopAnimRes)this.mBaseResP).mPopAnim;
		}

		public PIEffect GetPIEffect()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_PIEffect)
			{
				return null;
			}
			return ((PIEffectRes)this.mBaseResP).mPIEffect;
		}

		public RenderEffectDefinition GetRenderEffect()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_RenderEffect)
			{
				return null;
			}
			return ((RenderEffectRes)this.mBaseResP).mRenderEffectDefinition;
		}

		public GenericResFile GetGenericResFile()
		{
			if (this.mBaseResP == null || this.mBaseResP.mType != ResType.ResType_GenericResFile)
			{
				return null;
			}
			return ((GenericResFileRes)this.mBaseResP).mGenericResFile;
		}

		public BaseRes mBaseResP;
	}
}
