using System;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public abstract class RenderDevice3D : RenderDevice
	{
		public int Flush()
		{
			return this.Flush(2U);
		}

		public abstract int Flush(uint inFlushFlags);

		public abstract int Present(Rect theSrcRect, Rect theDestRect);

		public abstract uint GetCapsFlags();

		public bool SupportsPixelShaders()
		{
			return (this.GetCapsFlags() & 2U) != 0U;
		}

		public bool SupportsVertexShaders()
		{
			return (this.GetCapsFlags() & 4U) != 0U;
		}

		public bool SupportsCubeMaps()
		{
			return (this.GetCapsFlags() & 32U) != 0U;
		}

		public bool SupportsVolumeMaps()
		{
			return (this.GetCapsFlags() & 64U) != 0U;
		}

		public bool SupportsImageRenderTargets()
		{
			return (this.GetCapsFlags() & 8U) != 0U;
		}

		public abstract uint GetMaxTextureStages();

		public abstract string GetInfoString(RenderDevice3D.EInfoString inInfoStr);

		public abstract void GetBackBufferDimensions(ref uint outWidth, ref uint outHeight);

		public abstract int SceneBegun();

		public abstract bool CreateImageRenderData(ref MemoryImage inImage);

		public abstract void RemoveImageRenderData(MemoryImage inImage);

		public abstract int RecoverImageBitsFromRenderData(MemoryImage inImage);

		public abstract int GetTextureMemorySize(MemoryImage theImage);

		public abstract PixelFormat GetTextureFormat(MemoryImage theImage);

		public abstract Image SwapScreenImage(ref DeviceImage ioSrcImage, ref RenderSurface ioSrcSurface, uint flags);

		public abstract void CopyScreenImage(DeviceImage ioDstImage, uint flags);

		public abstract void AdjustVertexUVsEx(uint theVertexFormat, SexyVertex[] theVertices, int theVertexCount, int theVertexSize);

		public void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend)
		{
			this.DrawPrimitiveEx(theVertexFormat, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, tx, ty, blend, 0U);
		}

		public void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx, float ty)
		{
			this.DrawPrimitiveEx(theVertexFormat, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, tx, ty, true, 0U);
		}

		public void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx)
		{
			this.DrawPrimitiveEx(theVertexFormat, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, tx, 0f, true, 0U);
		}

		public void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode)
		{
			this.DrawPrimitiveEx(theVertexFormat, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, 0f, 0f, true, 0U);
		}

		public abstract void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend, uint theFlags);

		public abstract void SetBltDepth(float inDepth);

		public void PushTransform(SexyMatrix3 theTransform)
		{
			this.PushTransform(theTransform, true);
		}

		public abstract void PushTransform(SexyMatrix3 theTransform, bool concatenate);

		public abstract void PopTransform();

		public abstract void PopTransform(ref SexyMatrix3 theTransform);

		public abstract void ClearColorBuffer(SexyColor inColor);

		public abstract void ClearDepthBuffer();

		public abstract void ClearStencilBuffer(int inStencil);

		public abstract void SetDepthState(Graphics3D.ECompareFunc inDepthTestFunc, bool inDepthWriteEnabled);

		public abstract void SetStencilState(Graphics3D.ECompareFunc inStencilTestFunc, int inRefStencil, bool inStencilEnable, Graphics3D.ETestResultFunc passFunc, Graphics3D.ETestResultFunc failFunc);

		public abstract void SetAlphaTest(Graphics3D.ECompareFunc inAlphaTestFunc, int inRefAlpha);

		public abstract void SetColorWriteState(int inWriteRedEnabled, int inWriteGreenEnabled, int inWriteBlueEnabled, int inWriteAlphaEnabled);

		public abstract void SetWireframe(int inWireframe);

		public abstract void SetBlend(Graphics3D.EBlendMode inSrcBlend, Graphics3D.EBlendMode inDestBlend);

		public abstract void SetBackfaceCulling(int inCullClockwise, int inCullCounterClockwise);

		public abstract void SetLightingEnabled(int inLightingEnabled);

		public abstract void SetLightEnabled(int inLightIndex, int inEnabled);

		public abstract void SetGlobalAmbient(SexyColor inColor);

		public void SetMaterialAmbient(SexyColor inColor)
		{
			this.SetMaterialAmbient(inColor, -1);
		}

		public abstract void SetMaterialAmbient(SexyColor inColor, int inVertexColorComponent);

		public void SetMaterialDiffuse(SexyColor inColor)
		{
			this.SetMaterialDiffuse(inColor, -1);
		}

		public abstract void SetMaterialDiffuse(SexyColor inColor, int inVertexColorComponent);

		public void SetMaterialSpecular(SexyColor inColor, int inVertexColorComponent)
		{
			this.SetMaterialSpecular(inColor, inVertexColorComponent, 0f);
		}

		public void SetMaterialSpecular(SexyColor inColor)
		{
			this.SetMaterialSpecular(inColor, -1, 0f);
		}

		public abstract void SetMaterialSpecular(SexyColor inColor, int inVertexColorComponent, float inPower);

		public void SetMaterialEmissive(SexyColor inColor)
		{
			this.SetMaterialEmissive(inColor, -1);
		}

		public abstract void SetMaterialEmissive(SexyColor inColor, int inVertexColorComponent);

		public abstract void SetWorldTransform(SexyMatrix4 inMatrix);

		public abstract void SetViewTransform(SexyMatrix4 inMatrix);

		public abstract void SetProjectionTransform(SexyMatrix4 inMatrix);

		public void SetTextureTransform(int inTextureIndex, SexyMatrix4 inMatrix)
		{
			this.SetTextureTransform(inTextureIndex, inMatrix, 2);
		}

		public abstract void SetTextureTransform(int inTextureIndex, SexyMatrix4 inMatrix, int inNumDimensions);

		public abstract void SetViewport(int theX, int theY, int theWidth, int theHeight, float theMinZ, float theMaxZ);

		public abstract void SetRenderRect(int theX, int theY, int theWidth, int theHeight);

		public abstract bool SetTexture(int inTextureIndex, Image inImage);

		public abstract void SetTextureWrap(int inTextureIndex, bool inWrapU, bool inWrapV);

		public void SetTextureLinearFilter(int inTextureIndex)
		{
			this.SetTextureLinearFilter(inTextureIndex, true);
		}

		public abstract void SetTextureLinearFilter(int inTextureIndex, bool inLinear);

		public void SetTextureCoordSource(int inTextureIndex, int inUVComponent)
		{
			this.SetTextureCoordSource(inTextureIndex, inUVComponent, Graphics3D.ETexCoordGen.TEXCOORDGEN_NONE);
		}

		public abstract void SetTextureCoordSource(int inTextureIndex, int inUVComponent, Graphics3D.ETexCoordGen inTexGen);

		public abstract void SetTextureFactor(int inTextureFactor);

		public abstract RenderEffect GetEffect(RenderEffectDefinition inDefinition);

		public virtual bool ReloadEffects()
		{
			return false;
		}

		public virtual bool ReloadEffects(int inDebug)
		{
			return this.ReloadEffects();
		}

		public virtual void SetBltFilter(RenderDevice3D.FBltFilter inFilter, IntPtr inContext)
		{
		}

		public virtual void SetDrawPrimFilter(RenderDevice3D.FDrawPrimFilter inFilter, IntPtr inContext)
		{
		}

		public virtual bool LoadMesh(Mesh theMesh)
		{
			throw new NotImplementedException();
		}

		public virtual void RenderMesh(Mesh theMesh, SexyMatrix4 theMatrix, SexyColor theColor)
		{
			this.RenderMesh(theMesh, theMatrix, theColor, true);
		}

		public virtual void RenderMesh(Mesh theMesh, SexyMatrix4 theMatrix)
		{
			this.RenderMesh(theMesh, theMatrix, SexyColor.White, true);
		}

		public virtual void RenderMesh(Mesh theMesh, SexyMatrix4 theMatrix, SexyColor theColor, bool doSetup)
		{
			throw new NotImplementedException();
		}

		public enum EFlushFlags
		{
			FLUSHF_BufferedTris = 1,
			FLUSHF_CurrentScene,
			FLUSHF_ManagedResources_Immediate = 4,
			FLUSHF_ManagedResources_OnPresent = 8,
			FLUSHF_BufferedState = 16
		}

		public enum ECapsFlags
		{
			CAPF_SingleImageTexture = 1,
			CAPF_PixelShaders,
			CAPF_VertexShaders = 4,
			CAPF_ImageRenderTargets = 8,
			CAPF_AutoWindowedVSync = 16,
			CAPF_CubeMaps = 32,
			CAPF_VolumeMaps = 64,
			CAPF_CopyScreenImage = 128,
			CAPF_LastLockScreenImage = 256
		}

		public enum EInfoString
		{
			INFOSTRING_Adapter,
			INFOSTRING_DrvProductVersion,
			INFOSTRING_DisplayMode,
			INFOSTRING_BackBuffer,
			INFOSTRING_TextureMemory,
			INFOSTRING_DrvResourceManager,
			INFOSTRING_DrvProductFeatures
		}

		public delegate int FBltFilter(IntPtr theContext, int thePrimType, uint thePrimCount, SexyVertex2D theVertices, int theVertexSize, Rect[] theClipRect);

		public delegate int FDrawPrimFilter(IntPtr theContext, int thePrimType, uint thePrimCount, SexyVertex2D theVertices, int theVertexSize);
	}
}
