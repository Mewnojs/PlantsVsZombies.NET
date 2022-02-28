using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class Graphics3D
	{
		public Graphics3D(Graphics inGraphics, RenderDevice3D inRenderDevice, HRenderContext inRenderContext)
		{
			this.mGraphics = inGraphics;
			this.mRenderDevice = inRenderDevice;
			this.mRenderContext = inRenderContext;
		}

		protected void SetAsCurrentContext()
		{
			this.mRenderDevice.SetCurrentContext(this.mRenderContext);
		}

		public Graphics Get2D()
		{
			return this.mGraphics;
		}

		public RenderDevice3D GetRenderDevice()
		{
			return this.mRenderDevice;
		}

		public bool SupportsPixelShaders()
		{
			return this.mRenderDevice.SupportsPixelShaders();
		}

		public bool SupportsVertexShaders()
		{
			return this.mRenderDevice.SupportsVertexShaders();
		}

		public bool SupportsCubeMaps()
		{
			return this.mRenderDevice.SupportsCubeMaps();
		}

		public bool SupportsVolumeMaps()
		{
			return this.mRenderDevice.SupportsVolumeMaps();
		}

		public bool SupportsImageRenderTargets()
		{
			return this.mRenderDevice.SupportsImageRenderTargets();
		}

		public uint GetMaxTextureStages()
		{
			return this.mRenderDevice.GetMaxTextureStages();
		}

		public void AdjustVertexUVsEx(uint theVertexFormat, SexyVertex[] theVertices, int theVertexCount, int theVertexSize)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.AdjustVertexUVsEx(theVertexFormat, theVertices, theVertexCount, theVertexSize);
		}

		public void DrawPrimitiveEx(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend, uint theFlags)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.DrawPrimitiveEx(theVertexFormat, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, tx, ty, blend, theFlags);
		}

		public void DrawPrimitive(uint theVertexFormat, Graphics3D.EPrimitiveType thePrimitiveType, SexyVertex2D[] theVertices, int thePrimitiveCount, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend, uint theFlags)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.DrawPrimitiveEx((uint)SexyVertex2D.FVF, thePrimitiveType, theVertices, thePrimitiveCount, theColor, theDrawMode, tx, ty, blend, theFlags);
		}

		public void SetBltDepth(float inDepth)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetBltDepth(inDepth);
		}

		public void PushTransform(SexyMatrix3 theTransform)
		{
			this.PushTransform(theTransform, false);
		}

		public void PushTransform(SexyMatrix3 theTransform, bool concatenate)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.PushTransform(theTransform, concatenate);
		}

		public void PopTransform()
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.PopTransform();
		}

		public void PopTransform(ref SexyMatrix3 theTransform)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.PopTransform(ref theTransform);
		}

		public void ClearColorBuffer()
		{
			this.ClearColorBuffer(SexyColor.Black);
		}

		public void ClearColorBuffer(SexyColor inColor)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.ClearColorBuffer(inColor);
		}

		public void ClearDepthBuffer()
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.ClearDepthBuffer();
		}

		public void ClearStencilBuffer(int inStencil)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.ClearStencilBuffer(inStencil);
		}

		public void SetDepthState(Graphics3D.ECompareFunc inDepthTestFunc, bool inDepthWriteEnabled)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetDepthState(inDepthTestFunc, inDepthWriteEnabled);
		}

		public void SetAlphaTest(Graphics3D.ECompareFunc inAlphaTestFunc, int inRefAlpha)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetAlphaTest(inAlphaTestFunc, inRefAlpha);
		}

		public void SetColorWriteState(int inWriteRedEnabled, int inWriteGreenEnabled, int inWriteBlueEnabled, int inWriteAlphaEnabled)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetColorWriteState(inWriteRedEnabled, inWriteGreenEnabled, inWriteBlueEnabled, inWriteAlphaEnabled);
		}

		public void SetWireframe(int inWireframe)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetWireframe(inWireframe);
		}

		public void SetBlend(Graphics3D.EBlendMode inSrcBlend, Graphics3D.EBlendMode inDestBlend)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetBlend(inSrcBlend, inDestBlend);
		}

		public void SetBackfaceCulling(int inCullClockwise, int inCullCounterClockwise)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetBackfaceCulling(inCullClockwise, inCullCounterClockwise);
		}

		public void SetLightingEnabled(int inLightingEnabled, bool inSetDefaultMaterialState)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetLightingEnabled(inLightingEnabled);
			if (inLightingEnabled != 0 && inSetDefaultMaterialState)
			{
				this.SetMaterialAmbient(SexyColor.White);
				this.SetMaterialDiffuse(SexyColor.White, 0);
				this.SetMaterialSpecular(SexyColor.White);
				this.SetMaterialEmissive(SexyColor.Black);
			}
		}

		public void SetLightEnabled(int inLightIndex, int inEnabled)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetLightEnabled(inLightIndex, inEnabled);
		}

		public void SetPointLight(int inLightIndex, SexyVector3 inPos, Graphics3D.LightColors inColors, float inRange, SexyVector3 inAttenuation)
		{
			this.SetAsCurrentContext();
			throw new NotSupportedException();
		}

		public void SetDirectionalLight(int inLightIndex, SexyVector3 inDir, Graphics3D.LightColors inColors)
		{
			this.SetAsCurrentContext();
			throw new NotSupportedException();
		}

		public void SetGlobalAmbient(SexyColor inColor)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetGlobalAmbient(inColor);
		}

		public void SetMaterialAmbient(SexyColor inColor)
		{
			this.SetMaterialAmbient(inColor, -1);
		}

		public void SetMaterialAmbient(SexyColor inColor, int inVertexColorComponent)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetMaterialAmbient(inColor, inVertexColorComponent);
		}

		public void SetMaterialDiffuse(SexyColor inColor)
		{
			this.SetMaterialDiffuse(inColor, -1);
		}

		public void SetMaterialDiffuse(SexyColor inColor, int inVertexColorComponent)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetMaterialDiffuse(inColor, inVertexColorComponent);
		}

		public void SetMaterialSpecular(SexyColor inColor, int inVertexColorComponent)
		{
			this.SetMaterialSpecular(inColor, inVertexColorComponent, 0f);
		}

		public void SetMaterialSpecular(SexyColor inColor)
		{
			this.SetMaterialSpecular(inColor, -1, 0f);
		}

		public void SetMaterialSpecular(SexyColor inColor, int inVertexColorComponent, float inPower)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetMaterialSpecular(inColor, inVertexColorComponent, inPower);
		}

		public void SetMaterialEmissive(SexyColor inColor)
		{
			this.SetMaterialEmissive(inColor, -1);
		}

		public void SetMaterialEmissive(SexyColor inColor, int inVertexColorComponent)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetMaterialEmissive(inColor, inVertexColorComponent);
		}

		public void SetWorldTransform(SexyMatrix4 inMatrix)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetWorldTransform(inMatrix);
		}

		public void SetViewTransform(SexyMatrix4 inMatrix)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetViewTransform(inMatrix);
		}

		public void SetProjectionTransform(SexyMatrix4 inMatrix)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetProjectionTransform(inMatrix);
		}

		public void SetTextureTransform(int inTextureIndex, SexyMatrix4 inMatrix)
		{
			this.SetTextureTransform(inTextureIndex, inMatrix, 2);
		}

		public void SetTextureTransform(int inTextureIndex, SexyMatrix4 inMatrix, int inNumDimensions)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureTransform(inTextureIndex, inMatrix, inNumDimensions);
		}

		public bool SetTexture(int inTextureIndex, Image inImage)
		{
			this.SetAsCurrentContext();
			return this.mRenderDevice.SetTexture(inTextureIndex, inImage);
		}

		public void SetTextureWrap(int inTextureIndex, bool inWrap)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureWrap(inTextureIndex, inWrap, inWrap);
		}

		public void SetTextureWrap(int inTextureIndex, bool inWrapU, bool inWrapV)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureWrap(inTextureIndex, inWrapU, inWrapV);
		}

		public void SetTextureLinearFilter(int inTextureIndex, bool inLinear)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureLinearFilter(inTextureIndex, inLinear);
		}

		public void SetTextureCoordSource(int inTextureIndex, int inUVComponent)
		{
			this.SetTextureCoordSource(inTextureIndex, inUVComponent, Graphics3D.ETexCoordGen.TEXCOORDGEN_NONE);
		}

		public void SetTextureCoordSource(int inTextureIndex, int inUVComponent, Graphics3D.ETexCoordGen inTexGen)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureCoordSource(inTextureIndex, inUVComponent, inTexGen);
		}

		public void SetTextureFactor(int inTextureFactor)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetTextureFactor(inTextureFactor);
		}

		public void SetViewport(int theX, int theY, int theWidth, int theHeight, float theMinZ)
		{
			this.SetViewport(theX, theY, theWidth, theHeight, theMinZ, 1f);
		}

		public void SetViewport(int theX, int theY, int theWidth, int theHeight)
		{
			this.SetViewport(theX, theY, theWidth, theHeight, 0f, 1f);
		}

		public void SetViewport(int theX, int theY, int theWidth, int theHeight, float theMinZ, float theMaxZ)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.SetViewport((int)this.mGraphics.mTransX + theX, (int)this.mGraphics.mTransY + theY, theWidth, theHeight, theMinZ, theMaxZ);
		}

		public RenderEffect GetEffect(RenderEffectDefinition inDefinition)
		{
			return this.mRenderDevice.GetEffect(inDefinition);
		}

		public void Set3DTransformState(SexyCoords3 inWorldCoords, Graphics3D.Camera inCamera)
		{
			SexyMatrix4 sexyMatrix = new SexyMatrix4();
			inWorldCoords.GetOutboundMatrix(sexyMatrix);
			this.SetWorldTransform(sexyMatrix);
			inCamera.GetViewMatrix(sexyMatrix);
			this.SetViewTransform(sexyMatrix);
			inCamera.GetProjectionMatrix(sexyMatrix);
			this.SetProjectionTransform(sexyMatrix);
		}

		public void SetMasking(Graphics3D.EMaskMode inMaskMode, int inAlphaRef, float inFrontDepth)
		{
			this.SetMasking(inMaskMode, inAlphaRef, inFrontDepth, 0.5f);
		}

		public void SetMasking(Graphics3D.EMaskMode inMaskMode, int inAlphaRef)
		{
			this.SetMasking(inMaskMode, inAlphaRef, 0.25f, 0.5f);
		}

		public void SetMasking(Graphics3D.EMaskMode inMaskMode)
		{
			this.SetMasking(inMaskMode, 0, 0.25f, 0.5f);
		}

		public void SetMasking(Graphics3D.EMaskMode inMaskMode, int inAlphaRef, float inFrontDepth, float inBackDepth)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.Flush(0U);
			switch (inMaskMode)
			{
			case Graphics3D.EMaskMode.MASKMODE_NONE:
				this.mRenderDevice.SetStencilState(Graphics3D.ECompareFunc.COMPARE_ALWAYS, 1, false, Graphics3D.ETestResultFunc.TEST_Replace, Graphics3D.ETestResultFunc.TEST_Replace);
				return;
			case Graphics3D.EMaskMode.MASKMODE_WRITE_MASKONLY:
				this.mRenderDevice.SetStencilState(Graphics3D.ECompareFunc.COMPARE_NEVER, 1, true, Graphics3D.ETestResultFunc.TEST_Replace, Graphics3D.ETestResultFunc.TEST_Replace);
				return;
			case Graphics3D.EMaskMode.MASKMODE_WRITE_MASKANDCOLOR:
				this.mRenderDevice.SetStencilState(Graphics3D.ECompareFunc.COMPARE_ALWAYS, 1, true, Graphics3D.ETestResultFunc.TEST_Replace, Graphics3D.ETestResultFunc.TEST_Replace);
				return;
			case Graphics3D.EMaskMode.MASKMODE_TEST_INSIDE:
			case Graphics3D.EMaskMode.MASKMODE_TEST_OUTSIDE:
				if (inMaskMode == Graphics3D.EMaskMode.MASKMODE_TEST_OUTSIDE)
				{
					this.mRenderDevice.SetStencilState(Graphics3D.ECompareFunc.COMPARE_NOTEQUAL, 1, true, Graphics3D.ETestResultFunc.TEST_Keep, Graphics3D.ETestResultFunc.TEST_Keep);
					return;
				}
				this.mRenderDevice.SetStencilState(Graphics3D.ECompareFunc.COMPARE_EQUAL, 1, true, Graphics3D.ETestResultFunc.TEST_Keep, Graphics3D.ETestResultFunc.TEST_Keep);
				return;
			default:
				return;
			}
		}

		public void ClearMask()
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.ClearDepthBuffer();
		}

		protected Graphics mGraphics;

		protected RenderDevice3D mRenderDevice;

		protected HRenderContext mRenderContext;

		public enum EBlendMode
		{
			BLEND_ZERO = 1,
			BLEND_ONE,
			BLEND_SRCCOLOR,
			BLEND_INVSRCCOLOR,
			BLEND_SRCALPHA,
			BLEND_INVSRCALPHA,
			BLEND_DESTCOLOR = 9,
			BLEND_INVDESTCOLOR,
			BLEND_SRCALPHASAT,
			BLEND_DEFAULT = 65535
		}

		public enum ECompareFunc
		{
			COMPARE_NEVER = 1,
			COMPARE_LESS,
			COMPARE_EQUAL,
			COMPARE_LESSEQUAL,
			COMPARE_GREATER,
			COMPARE_NOTEQUAL,
			COMPARE_GREATEREQUAL,
			COMPARE_ALWAYS
		}

		public enum ETestResultFunc
		{
			TEST_Keep,
			TEST_Zero,
			TEST_Replace,
			TEST_Increment,
			TEST_Decrement,
			TEST_IncrementSaturation,
			TEST_DecrementSaturation,
			TEST_Invert
		}

		public enum ETexCoordGen
		{
			TEXCOORDGEN_NONE,
			TEXCOORDGEN_CAMERASPACENORMAL,
			TEXCOORDGEN_CAMERASPACEPOSITION,
			TEXCOORDGEN_CAMERASPACEREFLECTIONVECTOR
		}

		public enum EPrimitiveType
		{
			PT_PointList = 1,
			PT_LineList,
			PT_LineStrip,
			PT_TriangleList,
			PT_TriangleStrip,
			PT_TriangleFan
		}

		public enum EDrawPrimitiveFlags
		{
			DPF_NoAdjustUVs = 1,
			DPF_NoHalfPixelOffset,
			DPF_DiscardVerts = 4
		}

		public enum EMaskMode
		{
			MASKMODE_NONE,
			MASKMODE_WRITE_MASKONLY,
			MASKMODE_WRITE_MASKANDCOLOR,
			MASKMODE_TEST_INSIDE,
			MASKMODE_TEST_OUTSIDE
		}

		public class LightColors
		{
			public LightColors()
			{
				this.mDiffuse = new SexyColor(SexyColor.White);
				this.mSpecular = new SexyColor(SexyColor.Black);
				this.mAmbient = new SexyColor(SexyColor.Black);
				this.mAutoScale = 1f;
			}

			public SexyColor mDiffuse = default(SexyColor);

			public SexyColor mSpecular = default(SexyColor);

			public SexyColor mAmbient = default(SexyColor);

			public float mAutoScale;
		}

		public abstract class Camera
		{
			public Camera()
			{
				this.mZNear = 1f;
				this.mZFar = 10000f;
			}

			public float GetZNear()
			{
				return this.mZNear;
			}

			public float GetZFar()
			{
				return this.mZFar;
			}

			public void GetViewMatrix(SexyMatrix4 outM)
			{
			}

			public abstract void GetProjectionMatrix(SexyMatrix4 outM);

			public abstract bool IsOrtho();

			public abstract bool IsPerspective();

			protected float mZNear;

			protected float mZFar;
		}

		public class PerspectiveCamera : Graphics3D.Camera
		{
			public PerspectiveCamera()
			{
				this.mProjS = new SexyVector3(0f, 0f, 0f);
				this.mProjT = 0f;
			}

			public PerspectiveCamera(float inFovDegrees, float inAspectRatio, float inZNear)
				: this(inFovDegrees, inAspectRatio, inZNear, 10000f)
			{
			}

			public PerspectiveCamera(float inFovDegrees, float inAspectRatio)
				: this(inFovDegrees, inAspectRatio, 1f, 10000f)
			{
			}

			public PerspectiveCamera(float inFovDegrees, float inAspectRatio, float inZNear, float inZFar)
			{
				this.Init(inFovDegrees, inAspectRatio, inZNear, inZFar);
			}

			public void Init(float inFovDegrees, float inAspectRatio, float inZNear)
			{
				this.Init(inFovDegrees, inAspectRatio, inZNear, 10000f);
			}

			public void Init(float inFovDegrees, float inAspectRatio)
			{
				this.Init(inFovDegrees, inAspectRatio, 1f, 10000f);
			}

			public void Init(float inFovDegrees, float inAspectRatio, float inZNear, float inZFar)
			{
				float num = SexyMath.DegToRad(inFovDegrees * 0.5f);
				float num2 = num / inAspectRatio;
				this.mProjS.y = (float)(Math.Cos((double)num2) / Math.Sin((double)num2));
				this.mProjS.x = this.mProjS.y / inAspectRatio;
				this.mProjS.z = inZFar / (inZFar - inZNear);
				this.mProjT = -this.mProjS.z * inZNear;
				this.mZNear = inZNear;
				this.mZFar = inZFar;
			}

			public override void GetProjectionMatrix(SexyMatrix4 outM)
			{
			}

			public override bool IsOrtho()
			{
				return false;
			}

			public override bool IsPerspective()
			{
				return true;
			}

			public SexyVector3 EyeToScreen(SexyVector3 inEyePos)
			{
				SexyVector3 result = default(SexyVector3);
				float num = -inEyePos.z;
				result.x = inEyePos.x * this.mProjS.x / num;
				result.y = inEyePos.y * this.mProjS.y / num;
				result.z = (num * this.mProjS.z + this.mProjT) / this.mZFar;
				result.x = result.x * 0.5f + 0.5f;
				result.y = result.y * -0.5f + 0.5f;
				return result;
			}

			public SexyVector3 ScreenToEye(SexyVector3 inScreenPos)
			{
				float num = (inScreenPos.x - 0.5f) * 2f;
				float num2 = (inScreenPos.y - 0.5f) * -2f;
				SexyVector3 sexyVector = new SexyVector3(num * this.mZNear / this.mProjS.x, num2 * this.mZNear / this.mProjS.y, -this.mZNear);
				SexyVector3 impliedObject = new SexyVector3(num * this.mZFar / this.mProjS.x, num2 * this.mZFar / this.mProjS.y, -this.mZFar);
				return sexyVector + (impliedObject - sexyVector) * inScreenPos.z;
			}

			protected SexyVector3 mProjS = default(SexyVector3);

			protected float mProjT;
		}

		public class OffCenterPerspectiveCamera : Graphics3D.Camera
		{
			public OffCenterPerspectiveCamera()
			{
				this.mProjS = new SexyVector3(0f, 0f, 0f);
				this.mProjT = 0f;
			}

			public OffCenterPerspectiveCamera(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY, float inZNear)
				: this(inFovDegrees, inAspectRatio, inOffsetX, inOffsetY, inZNear, 10000f)
			{
			}

			public OffCenterPerspectiveCamera(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY)
				: this(inFovDegrees, inAspectRatio, inOffsetX, inOffsetY, 1f, 10000f)
			{
			}

			public OffCenterPerspectiveCamera(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY, float inZNear, float inZFar)
			{
				this.Init(inFovDegrees, inAspectRatio, inOffsetX, inOffsetY, inZNear, inZFar);
			}

			public void Init(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY, float inZNear)
			{
				this.Init(inFovDegrees, inAspectRatio, inOffsetX, inOffsetY, inZNear, 10000f);
			}

			public void Init(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY)
			{
				this.Init(inFovDegrees, inAspectRatio, inOffsetX, inOffsetY, 1f, 10000f);
			}

			public void Init(float inFovDegrees, float inAspectRatio, float inOffsetX, float inOffsetY, float inZNear, float inZFar)
			{
				float num = SexyMath.DegToRad(inFovDegrees * 0.5f);
				float num2 = num / inAspectRatio;
				float num3 = (float)(Math.Cos((double)num2) / Math.Sin((double)num2));
				float num4 = num3 / inAspectRatio;
				float num5 = inZNear / num4;
				float num6 = inZNear / num3;
				this.mLeft = inOffsetX - num5;
				this.mRight = inOffsetX + num5;
				this.mTop = inOffsetY + num6;
				this.mBottom = inOffsetY - num6;
				this.mProjS.y = num3;
				this.mProjS.x = num4;
				this.mProjS.z = inZFar / (inZFar - inZNear);
				this.mProjT = -this.mProjS.z * inZNear;
				this.mZNear = inZNear;
				this.mZFar = inZFar;
			}

			public override void GetProjectionMatrix(SexyMatrix4 outM)
			{
			}

			public override bool IsOrtho()
			{
				return false;
			}

			public override bool IsPerspective()
			{
				return true;
			}

			public SexyVector3 EyeToScreen(SexyVector3 inEyePos)
			{
				SexyVector3 result = default(SexyVector3);
				float num = -inEyePos.z;
				result.x = inEyePos.x * this.mProjS.x / num;
				result.y = inEyePos.y * this.mProjS.y / num;
				result.z = (num * this.mProjS.z + this.mProjT) / this.mZFar;
				result.x = result.x * 0.5f + 0.5f;
				result.y = result.y * -0.5f + 0.5f;
				return result;
			}

			public SexyVector3 ScreenToEye(SexyVector3 inScreenPos)
			{
				float num = (inScreenPos.x - 0.5f) * 2f;
				float num2 = (inScreenPos.y - 0.5f) * -2f;
				SexyVector3 sexyVector = new SexyVector3(num * this.mZNear / this.mProjS.x, num2 * this.mZNear / this.mProjS.y, -this.mZNear);
				SexyVector3 impliedObject = new SexyVector3(num * this.mZFar / this.mProjS.x, num2 * this.mZFar / this.mProjS.y, -this.mZFar);
				return sexyVector + (impliedObject - sexyVector) * inScreenPos.z;
			}

			protected SexyVector3 mProjS = default(SexyVector3);

			protected float mProjT;

			protected float mLeft;

			protected float mRight;

			protected float mTop;

			protected float mBottom;
		}

		public class OrthoCamera : Graphics3D.Camera
		{
			public OrthoCamera()
			{
				this.mProjS = new SexyVector3(0f, 0f, 0f);
				this.mProjT = 0f;
				this.mWidth = 0f;
				this.mHeight = 0f;
			}

			public OrthoCamera(float inWidth, float inHeight, float inZNear)
				: this(inWidth, inHeight, inZNear, 10000f)
			{
			}

			public OrthoCamera(float inWidth, float inHeight)
				: this(inWidth, inHeight, 1f, 10000f)
			{
			}

			public OrthoCamera(float inWidth, float inHeight, float inZNear, float inZFar)
			{
				this.Init(inWidth, inHeight, inZNear, inZFar);
			}

			public void Init(float inWidth, float inHeight, float inZNear)
			{
				this.Init(inWidth, inHeight, inZNear, 10000f);
			}

			public void Init(float inWidth, float inHeight)
			{
				this.Init(inWidth, inHeight, 1f, 10000f);
			}

			public void Init(float inWidth, float inHeight, float inZNear, float inZFar)
			{
				this.mWidth = inWidth;
				this.mHeight = inHeight;
				this.mProjS.y = 2f / this.mHeight;
				this.mProjS.x = 2f / this.mWidth;
				this.mProjS.z = 1f / (inZFar - inZNear);
				this.mProjT = -this.mProjS.z * inZNear;
				this.mZNear = inZNear;
				this.mZFar = inZFar;
			}

			public override void GetProjectionMatrix(SexyMatrix4 outM)
			{
			}

			public override bool IsOrtho()
			{
				return true;
			}

			public override bool IsPerspective()
			{
				return false;
			}

			public SexyVector3 EyeToScreen(SexyVector3 inEyePos)
			{
				SexyVector3 result = default(SexyVector3);
				result.x = inEyePos.x * this.mProjS.x;
				result.y = inEyePos.y * this.mProjS.y;
				result.z = (this.mProjS.z + this.mProjT) / this.mZFar;
				result.x = result.x * 0.5f + 0.5f;
				result.y = result.y * -0.5f + 0.5f;
				return result;
			}

			public SexyVector3 ScreenToEye(SexyVector3 inScreenPos)
			{
				float num = (inScreenPos.x - 0.5f) * 2f;
				float num2 = (inScreenPos.y - 0.5f) * -2f;
				SexyVector3 sexyVector = new SexyVector3(num / this.mProjS.x, num2 / this.mProjS.y, -this.mZNear);
				SexyVector3 impliedObject = new SexyVector3(sexyVector.x, sexyVector.y, -this.mZFar);
				return sexyVector + (impliedObject - sexyVector) * inScreenPos.z;
			}

			protected SexyVector3 mProjS = default(SexyVector3);

			protected float mProjT;

			protected float mWidth;

			protected float mHeight;
		}

		public interface Spline
		{
			SexyVector3 Evaluate(float inTime);
		}

		public class CatmullRomSpline : Graphics3D.Spline
		{
			public CatmullRomSpline()
			{
			}

			public CatmullRomSpline(Graphics3D.CatmullRomSpline inSpline)
			{
				this.mPoints = inSpline.mPoints;
			}

			public CatmullRomSpline(List<SexyVector3> inPoints)
			{
				this.mPoints = inPoints;
			}

			public SexyVector3 Evaluate(float inTime)
			{
				return default(SexyVector3);
			}

			public List<SexyVector3> mPoints = new List<SexyVector3>();
		}
	}
}
