using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Drivers.Graphics
{
	public class BaseXNAStateManager : RenderStateManager
	{
		public BaseXNAStateManager(ref GraphicsDeviceManager theDevice)
		{
			this.mDevice = theDevice;
			this.mXNABlendState = BlendState.AlphaBlend;
			this.mXNARasterizerState = RasterizerState.CullNone;
			this.mXNADepthStencilState = DepthStencilState.Default;
			this.mXNATextureSlots = null;
			this.mXNASamplerStateSlots = SamplerState.LinearClamp;
			this.mXNAProjectionMatrix = Matrix.CreateOrthographicOffCenter(0f, (float)GlobalMembers.gSexyAppBase.mWidth, (float)GlobalMembers.gSexyAppBase.mHeight, 0f, -1000f, 1000f);
			this.mXNAViewMatrix = Matrix.CreateLookAt(new Vector3(0f, 0f, 300f), Vector3.Zero, Vector3.Up);
			this.mXNAWorldMatrix = Matrix.Identity;
			new Viewport(0, 0, GlobalMembers.gSexyAppBase.mWidth, GlobalMembers.gSexyAppBase.mHeight);
			this.mStatckSrcBlendState = new Stack<Graphics3D.EBlendMode>();
			this.mStatckDestBlendState = new Stack<Graphics3D.EBlendMode>();
			this.mStatckRasterizerState = new Stack<RasterizerState>();
			this.mStatckDepthStencilState = new Stack<DepthStencilState>();
			this.mStatckSamplerState = new Stack<SamplerState>();
			this.mStatckProjectionMatrix = new Stack<Matrix>();
			this.mStatckViewMatrix = new Stack<Matrix>();
			this.mStatckWorldMatrix = new Stack<Matrix>();
			this.mStatckViewPort = new Stack<Viewport>();
		}

		public override void Init()
		{
		}

		public override void Reset()
		{
		}

		protected void InitRenderState(ulong inIndex, ref string inStateName, ulong inHardwareDefaultValue, bool inHasContextDefault, ulong inContextDefaultValue, string inValueEnumName)
		{
			string inName = string.Format("RS:0", inStateName);
			if (inHasContextDefault)
			{
				this.mRenderStates[(int)inIndex].Init(new RenderStateManager.StateValue(inHardwareDefaultValue), new RenderStateManager.StateValue(inContextDefaultValue), inName, inValueEnumName);
				return;
			}
			this.mRenderStates[(int)inIndex].Init(new RenderStateManager.StateValue(inHardwareDefaultValue), inName, inValueEnumName);
		}

		protected void InitRenderStateFloat(ulong inIndex, ref string inStateName, float inDefaultValue)
		{
			this.InitRenderState(inIndex, ref inStateName, (ulong)inDefaultValue, false, 0UL, null);
		}

		protected void InitTextureStageState(ulong inFirstStage, ulong inLastStage, ulong inIndex, string inStateName, ulong inDefaultValue, bool inHasContextDefault, ulong inContextDefaultValue, string inValueEnumName)
		{
			for (ulong num = inFirstStage; num <= inLastStage; num += 1UL)
			{
				string inName = string.Format("TSS:0[1]", inStateName, num);
				if (inHasContextDefault)
				{
					this.mTextureStageStates[(int)inIndex][(int)num].Init(new RenderStateManager.StateValue(inDefaultValue), new RenderStateManager.StateValue(inContextDefaultValue), inName, inValueEnumName);
				}
				else
				{
					this.mTextureStageStates[(int)inIndex][(int)num].Init(new RenderStateManager.StateValue(inDefaultValue), inName, inValueEnumName);
				}
			}
		}

		protected void InitTextureStageStateFloat(ulong inFirstStage, ulong inLastStage, ulong inIndex, string inStateName, float inDefaultValue)
		{
			this.InitTextureStageState(inFirstStage, inLastStage, inIndex, inStateName, (ulong)inDefaultValue, false, 0UL, null);
		}

		protected void InitSamplerState(ulong inFirstStage, ulong inLastStage, ulong inIndex, string inStateName, ulong inDefaultValue, bool inHasContextDefault, ulong inContextDefaultValue, string inValueEnumName)
		{
			for (ulong num = inFirstStage; num <= inLastStage; num += 1UL)
			{
				string inName = string.Format("SS:1[2]", inStateName, num);
				if (inHasContextDefault)
				{
					this.mSamplerStates[(int)inIndex][(int)num].Init(new RenderStateManager.StateValue(inDefaultValue), new RenderStateManager.StateValue(inContextDefaultValue), inName, inValueEnumName);
				}
				else
				{
					this.mSamplerStates[(int)inIndex][(int)num].Init(new RenderStateManager.StateValue(inDefaultValue), inName, inValueEnumName);
				}
			}
		}

		protected void InitSamplerStateFloat(ulong inFirstStage, ulong inLastStage, ulong inIndex, string inStateName, float inDefaultValue)
		{
			this.InitSamplerState(inFirstStage, inLastStage, inIndex, inStateName, (ulong)inDefaultValue, false, 0UL, null);
		}

		protected void InitStates()
		{
			for (uint num = 0U; num < 256U; num += 1U)
			{
				this.mRenderStates.Add(new RenderStateManager.State(this, 0U, num));
			}
			this.mRenderStates[7].Init(1UL, 0UL, "ZENABLE", "");
			this.mRenderStates[14].Init(1UL, 0UL, "ZWRITEENABLE", "");
			this.mRenderStates[15].Init(0UL, 0UL, "ALPHATESTENABLE", "");
			this.mRenderStates[23].Init(7UL, "ZFUNC", "");
			this.mRenderStates[24].Init(0UL, 0UL, "ALPHAREF", "");
			this.mRenderStates[25].Init(8UL, 8UL, "ALPHAFUNC", "");
			this.mRenderStates[19].Init(1UL, "SRCBLEND", "Sexy::Graphics3D::EBlendMode");
			this.mRenderStates[20].Init(1UL, "DESTBLEND", "Sexy::Graphics3D::EBlendMode");
			this.mRenderStates[168].Init(15UL, "COLORWRITE", "");
			for (uint num2 = 0U; num2 < 11U; num2 += 1U)
			{
				this.mLightStates.Add(new List<RenderStateManager.State>());
			}
			for (uint num3 = 0U; num3 < 11U; num3 += 1U)
			{
				for (uint num4 = 0U; num4 < 8U; num4 += 1U)
				{
					this.mLightStates[(int)num3].Add(new RenderStateManager.State(this, 3U, num3, num4));
				}
			}
			for (ulong num5 = 0UL; num5 < 8UL; num5 += 1UL)
			{
				this.mLightStates[0][(int)num5].Init(0UL, string.Format("LIGHT:ENABLED[0]", num5));
				this.mLightStates[1][(int)num5].Init(0UL, string.Format("LIGHT:TYPE[0]", num5), "D3DLIGHTTYPE");
				this.mLightStates[2][(int)num5].Init(new RenderStateManager.StateValue(1f, 1f, 10f, 0f), string.Format("LIGHT:DIFFUSE[0]", num5));
				this.mLightStates[3][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("LIGHT:SPECULAR[0]", num5));
				this.mLightStates[4][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("LIGHT:AMBIENT[0]", num5));
				this.mLightStates[5][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("LIGHT:POSITION[0]", num5));
				this.mLightStates[6][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 1f, 0f), string.Format("LIGHT:DIRECTION[0]", num5));
				this.mLightStates[7][(int)num5].Init(0UL, string.Format("LIGHT:RANGE[%d]", num5));
				this.mLightStates[8][(int)num5].Init(0UL, string.Format("LIGHT:FALLOFF[%d]", num5));
				this.mLightStates[9][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("LIGHT:ATTENUATION[0]", num5));
				this.mLightStates[10][(int)num5].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("LIGHT:ANGLES[0]", num5));
			}
			for (uint num6 = 0U; num6 < 512U; num6 += 1U)
			{
				this.mTransformStates.Add(new List<RenderStateManager.State>());
			}
			for (uint num7 = 0U; num7 < 512U; num7 += 1U)
			{
				for (uint num8 = 0U; num8 < 4U; num8 += 1U)
				{
					this.mTransformStates[(int)num7].Add(new RenderStateManager.State(this, 6U, num7, num8));
				}
			}
			for (uint num9 = 0U; num9 < 512U; num9 += 1U)
			{
				string text;
				if (num9 == 0U)
				{
					text = "WORLD";
				}
				else if (num9 == 1U)
				{
					text = "VIEW";
				}
				else if (num9 == 2U)
				{
					text = "PROJECTION";
				}
				else if (num9 == 11U)
				{
					text = "ORTHOPROJECTION";
				}
				else if (num9 >= 3U && num9 <= 10U)
				{
					text = string.Format("TEXTURE0", num9 - 3U);
				}
				else
				{
					text = string.Format("0", num9);
				}
				for (uint num10 = 0U; num10 < 4U; num10 += 1U)
				{
					this.mTransformStates[(int)num9][(int)num10].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("TRANSFORM:0[1]", text, num10));
				}
			}
			for (uint num11 = 0U; num11 < 6U; num11 += 1U)
			{
				this.mViewportStates.Add(new RenderStateManager.State(this, 7U, num11));
			}
			this.mViewportStates[0].Init(0UL, "VIEWPORT:X");
			this.mViewportStates[1].Init(0UL, "VIEWPORT:Y");
			this.mViewportStates[2].Init((ulong)((long)GlobalMembers.gSexyAppBase.mWidth), "VIEWPORT:WIDTH");
			this.mViewportStates[3].Init((ulong)((long)GlobalMembers.gSexyAppBase.mHeight), "VIEWPORT:HEIGHT");
			this.mViewportStates[4].Init(0UL, "VIEWPORT_MINZ");
			this.mViewportStates[5].Init(1UL, "VIEWPORT_MAXZ");
			for (uint num12 = 0U; num12 < 21U; num12 += 1U)
			{
				this.mMiscStates.Add(new List<RenderStateManager.State>());
			}
			for (uint num13 = 0U; num13 < 14U; num13 += 1U)
			{
				this.mMiscStates[(int)num13].Add(new RenderStateManager.State(this, 8U, num13));
			}
			for (uint num14 = 0U; num14 < 8U; num14 += 1U)
			{
				this.mMiscStates[14].Add(new RenderStateManager.State(this, 8U, 14U, num14));
			}
			for (uint num15 = 0U; num15 < 32U; num15 += 1U)
			{
				this.mMiscStates[15].Add(new RenderStateManager.State(this, 8U, 15U, num15));
			}
			for (uint num16 = 0U; num16 < 256U; num16 += 1U)
			{
				this.mMiscStates[16].Add(new RenderStateManager.State(this, 8U, 16U, num16));
			}
			for (uint num17 = 0U; num17 < 4U; num17 += 1U)
			{
				this.mMiscStates[17].Add(new RenderStateManager.State(this, 8U, 17U, num17));
			}
			for (uint num18 = 0U; num18 < 8U; num18 += 1U)
			{
				this.mMiscStates[18].Add(new RenderStateManager.State(this, 8U, 18U, num18));
			}
			for (uint num19 = 0U; num19 < 8U; num19 += 1U)
			{
				this.mMiscStates[19].Add(new RenderStateManager.State(this, 8U, 19U, num19));
				this.mMiscStates[20].Add(new RenderStateManager.State(this, 8U, 20U, num19));
			}
			this.mMiscStates[0][0].Init(0UL, "MISC:VERTEXFORMAT");
			this.mMiscStates[1][0].Init(0UL, "MISC:VERTEXSIZE");
			this.mMiscStates[3][0].Init(0UL, "MISC:SHADERPROGRAM_ORTHO");
			this.mMiscStates[4][0].Init(0UL, "MISC:SHADERPROGRAM_3D");
			this.mMiscStates[10][0].Init(0UL, 0UL, "MISC:BLTDEPTH");
			this.mMiscStates[11][0].Init(0UL, "MISC:3DMODE");
			this.mMiscStates[12][0].Init(0UL, "MISC:CULLMODE");
			this.mMiscStates[8][0].Init(65535UL, "MISC:SRCBLENDOVERRIDE", "Sexy::Graphics3D::EBlendMode");
			this.mMiscStates[9][0].Init(65535UL, "MISC:DESTBLENDOVERRIDE", "Sexy::Graphics3D::EBlendMode");
			this.mMiscStates[10][0].Init(0UL, "MISC:BLTDEPTH");
			this.mMiscStates[13][0].Init(0UL, "MISC:USE_TEXSCALE");
			for (uint num20 = 0U; num20 < 8U; num20 += 1U)
			{
				this.mMiscStates[14].Add(new RenderStateManager.State(this, 8U, 14U, num20));
			}
			for (uint num21 = 0U; num21 < 8U; num21 += 1U)
			{
				this.mMiscStates[14][(int)num21].Init(0UL, string.Format("MISC:TEXTURE[0]", num21));
			}
			for (uint num22 = 0U; num22 < 8U; num22 += 1U)
			{
				this.mMiscStates[19].Add(new RenderStateManager.State(this, 8U, 19U, num22));
				this.mMiscStates[20].Add(new RenderStateManager.State(this, 8U, 20U, num22));
			}
			for (uint num23 = 0U; num23 < 8U; num23 += 1U)
			{
				this.mMiscStates[19][(int)num23].Init(new RenderStateManager.StateValue(0f, 0f, 0f, 0f), string.Format("MISC:ATLASENABLEDANDBASE[0]", num23));
				this.mMiscStates[20][(int)num23].Init(new RenderStateManager.StateValue(0f, 0f, 1f, 1f), string.Format("MISC:ATLASUV[0]", num23));
			}
		}

		protected void ResetStates(List<RenderStateManager.State> list)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Reset();
			}
		}

		protected void ResetStatesList(List<List<RenderStateManager.State>> list)
		{
			foreach (List<RenderStateManager.State> list2 in list)
			{
				this.ResetStates(list2);
			}
		}

		protected void ResetStates()
		{
			this.ResetStates(this.mRenderStates);
			this.ResetStatesList(this.mTextureStageStates);
			this.ResetStatesList(this.mSamplerStates);
			this.ResetStatesList(this.mLightStates);
			this.ResetStates(this.mMaterialStates);
			this.ResetStatesList(this.mStreamStates);
			this.ResetStatesList(this.mTransformStates);
			this.ResetStates(this.mViewportStates);
			this.ResetStatesList(this.mMiscStates);
		}

		public void SetRenderState(ulong inRS, ulong inValue)
		{
			this.mRenderStates[(int)inRS].SetValue(inValue);
		}

		private void SetTextureStageState(ulong inStage, ulong inTSS, ulong inValue)
		{
			this.mTextureStageStates[(int)inTSS][(int)inStage].SetValue(inValue);
		}

		public void SetSamplerState(ulong inSampler, ulong inSS, ulong inValue)
		{
			this.mSamplerStates[(int)inSS][(int)inSampler].SetValue(inValue);
		}

		public void SetSamplerState(SamplerState state)
		{
			if (this.mXNASamplerStateSlots != state)
			{
				this.mStateDirty = true;
			}
			this.mXNALastSamplerStateSlots = this.mXNASamplerStateSlots;
			this.mXNASamplerStateSlots = state;
		}

		public void SetRasterizerState(RasterizerState state)
		{
			if (this.mXNARasterizerState != state)
			{
				this.mStateDirty = true;
			}
			this.mXNARasterizerState = state;
		}

		public void SetBlendStateState(BlendState state)
		{
			if (this.mXNABlendState.AlphaDestinationBlend != state.AlphaDestinationBlend || this.mXNABlendState.ColorDestinationBlend != state.ColorDestinationBlend)
			{
				this.mStateDirty = true;
			}
			this.mXNALastBlendState = this.mXNABlendState;
			this.mXNABlendState = state;
		}

		public void SetBlendOverride(Graphics3D.EBlendMode src, Graphics3D.EBlendMode dest)
		{
			if (this.mSrcBlendMode != src || this.mDestBlendMode != dest)
			{
				this.mStateDirty = true;
			}
			this.mSrcBlendMode = src;
			this.mDestBlendMode = dest;
		}

		public void SetDepthStencilState(DepthStencilState state)
		{
			if (this.mXNADepthStencilState != state)
			{
				this.mStencilStateDirty = true;
			}
			this.mXNALastStencilState = this.mXNADepthStencilState;
			this.mXNADepthStencilState = state;
		}

		public void SetProjectionTransform(Matrix mat)
		{
			if (this.mXNAProjectionMatrix != mat)
			{
				this.mProjectMatrixDirty = true;
			}
			this.mXNALastProjectionMatrix = this.mXNAProjectionMatrix;
			this.mXNAProjectionMatrix = mat;
		}

		public void SetViewTransform(Matrix mat)
		{
			this.mXNAViewMatrix = mat;
		}

		public void SetWorldTransform(Matrix mat)
		{
			this.mXNALastWorldMatrix = this.mXNAWorldMatrix;
			this.mXNAWorldMatrix = mat;
		}

		public void SetTexture(Texture2D texture)
		{
			if (texture != this.mXNATextureSlots)
			{
				this.mTextureStateDirty = true;
			}
			else
			{
				this.mTextureStateDirty = false;
			}
			this.mLastXNATextureSlots = this.mXNATextureSlots;
			this.mXNATextureSlots = texture;
		}

		private void SetLightEnabled(ulong inLightIndex, bool inEnabled)
		{
		}

		private void SetMaterialAmbient(SexyColor inColor, int inVertexColorComponent)
		{
		}

		private void SetMaterialDiffuse(SexyColor inColor, int inVertexColorComponent)
		{
		}

		private void SetMaterialSpecular(SexyColor inColor, int inVertexColorComponent, float inPower)
		{
		}

		private void SetMaterialEmissive(SexyColor inColor, int inVertexColorComponent)
		{
		}

		public void SetViewport(int inX, int inY, int inWidth, int inHeight, float inMinZ, float inMaxZ)
		{
			this.mXNAViewPort = new Viewport(inX, inY, inWidth, inHeight);
		}

		private void SetFVF(ulong inFVF)
		{
		}

		private void SetCurrentTexturePalette(ulong inPaletteIndex)
		{
		}

		private void SetScissorRect(Rect inRect)
		{
		}

		private void SetNPatchMode(float inSegments)
		{
		}

		private void SetTextureRemap(ulong inLogicalSampler, ulong inPhysicalSampler)
		{
		}

		private void SetPixelShaderConstantF(ulong inStartRegister, float[] inConstantData, ulong inVector4fCount)
		{
		}

		private void SetVertexShaderConstantF(ulong inStartRegister, float[] inConstantData, ulong inVector4fCount)
		{
		}

		private void SetClipPlane(ulong inIndex, float[] inPlane)
		{
		}

		private void SetBltDepth(float inDepth)
		{
		}

		public new void PushState()
		{
			this.mStatckSrcBlendState.Push(this.mSrcBlendMode);
			this.mStatckDestBlendState.Push(this.mDestBlendMode);
			this.mStatckRasterizerState.Push(this.mXNARasterizerState);
			this.mStatckDepthStencilState.Push(this.mXNADepthStencilState);
			this.mStatckSamplerState.Push(this.mXNASamplerStateSlots);
			this.mStatckProjectionMatrix.Push(this.mXNAProjectionMatrix);
			this.mStatckViewMatrix.Push(this.mXNAViewMatrix);
			this.mStatckWorldMatrix.Push(this.mXNAWorldMatrix);
			this.mStatckViewPort.Push(this.mXNAViewPort);
		}

		public new void PopState()
		{
			this.mSrcBlendMode = this.mStatckSrcBlendState.Pop();
			this.mDestBlendMode = this.mStatckDestBlendState.Pop();
			this.mXNARasterizerState = this.mStatckRasterizerState.Pop();
			this.mXNADepthStencilState = this.mStatckDepthStencilState.Pop();
			if (this.mXNASamplerStateSlots != this.mStatckSamplerState.Peek())
			{
				this.mStateDirty = true;
			}
			this.mXNALastSamplerStateSlots = this.mXNASamplerStateSlots;
			this.mXNASamplerStateSlots = this.mStatckSamplerState.Pop();
			this.mXNAProjectionMatrix = this.mStatckProjectionMatrix.Pop();
			this.mXNAViewMatrix = this.mStatckViewMatrix.Pop();
			this.mXNAWorldMatrix = this.mStatckWorldMatrix.Pop();
			this.mXNAViewPort = this.mStatckViewPort.Pop();
		}

		public void SetAtlasState(ulong inSampler, bool inEnabled, SexyVector2 inBase, SexyVector2 inU, SexyVector2 inV)
		{
			this.mAtalasEnabled = inEnabled;
			if (!this.mAtalasEnabled)
			{
				return;
			}
			this.mAtalasBase = inBase;
			this.mAtalasU = inU;
			this.mAtalasV = inV;
		}

		public bool GetAtlasState(ulong inSampler, ref SexyVector2 outBase, ref SexyVector2 outU, ref SexyVector2 outV)
		{
			if (!this.mAtalasEnabled)
			{
				return false;
			}
			outBase = this.mAtalasBase;
			outU = this.mAtalasU;
			outV = this.mAtalasV;
			return true;
		}

		public BlendState mXNABlendState;

		public RasterizerState mXNARasterizerState;

		public DepthStencilState mXNADepthStencilState;

		public DepthStencilState mXNALastStencilState;

		public BlendState mXNALastBlendState;

		public Texture2D mXNATextureSlots;

		public Texture2D mLastXNATextureSlots;

		public SamplerState mXNASamplerStateSlots;

		public SamplerState mXNALastSamplerStateSlots;

		public Matrix mXNAProjectionMatrix = Matrix.Identity;

		public Matrix mXNALastProjectionMatrix;

		public Matrix mXNAViewMatrix = Matrix.Identity;

		public Matrix mXNAWorldMatrix = Matrix.Identity;

		public Matrix mXNALastWorldMatrix;

		public Viewport mXNAViewPort;

		public List<RenderStateManager.State> mRenderStates;

		public List<List<RenderStateManager.State>> mTextureStageStates;

		public List<List<RenderStateManager.State>> mSamplerStates;

		public List<List<RenderStateManager.State>> mLightStates;

		public List<RenderStateManager.State> mMaterialStates;

		public List<List<RenderStateManager.State>> mStreamStates;

		public List<List<RenderStateManager.State>> mTransformStates;

		public List<RenderStateManager.State> mViewportStates;

		public List<List<RenderStateManager.State>> mMiscStates;

		public GraphicsDeviceManager mDevice;

		public Stack<Graphics3D.EBlendMode> mStatckSrcBlendState;

		public Stack<Graphics3D.EBlendMode> mStatckDestBlendState;

		public Stack<RasterizerState> mStatckRasterizerState;

		public Stack<DepthStencilState> mStatckDepthStencilState;

		public Stack<SamplerState> mStatckSamplerState;

		public Stack<Matrix> mStatckProjectionMatrix;

		public Stack<Matrix> mStatckViewMatrix;

		public Stack<Matrix> mStatckWorldMatrix;

		public Stack<Viewport> mStatckViewPort;

		public Stack<int> mStatckDrawMode;

		public bool mAtalasEnabled;

		public bool mStateDirty;

		public bool mTextureStateDirty;

		public bool mProjectMatrixDirty;

		public bool mStencilStateDirty;

		public int mDrawMode;

		public SexyVector2 mAtalasBase;

		public SexyVector2 mAtalasU;

		public SexyVector2 mAtalasV;

		public Graphics3D.EBlendMode mSrcBlendMode = Graphics3D.EBlendMode.BLEND_DEFAULT;

		public Graphics3D.EBlendMode mDestBlendMode = Graphics3D.EBlendMode.BLEND_DEFAULT;

		public enum XNA_TRANSFORM
		{
			OGL_TRANSFORM_WORLD,
			OGL_TRANSFORM_VIEW,
			OGL_TRANSFORM_PROJECTION,
			OGL_TRANSFORM_TEXTURE0,
			OGL_TRANSFORM_TEXTURE1,
			OGL_TRANSFORM_TEXTURE2,
			OGL_TRANSFORM_TEXTURE3,
			OGL_TRANSFORM_TEXTURE4,
			OGL_TRANSFORM_TEXTURE5,
			OGL_TRANSFORM_TEXTURE6,
			OGL_TRANSFORM_TEXTURE7,
			OGL_TRANSFORM_ORTHOPROJ,
			OGL_TRANSFORM_COUNT
		}

		public enum EStateGroup
		{
			SG_RS,
			SG_TSS,
			SG_SS,
			SG_LIGHT,
			SG_MATERIAL,
			SG_STREAM,
			SG_TRANSFORM,
			SG_VIEWPORT,
			SG_MISC,
			SG_SCISSOR,
			SG_COUNT
		}

		public enum EXNAStateGroup
		{
			SG_BLEND,
			SG_Raster,
			SG_Depth,
			SG_Sampler,
			SG_Project,
			SG_View,
			SG_World,
			SG_ViewPort,
			SG_Num
		}

		public enum ERenderStateConst
		{
			ST_COUNT_RS = 256,
			ST_COUNT_TSS = 48,
			ST_COUNT_SS = 16,
			ST_COUNT_TRANSFORM = 512
		}

		public enum ELightState
		{
			ST_LIGHT_ENABLED,
			ST_LIGHT_TYPE,
			ST_LIGHT_DIFFUSE,
			ST_LIGHT_SPECULAR,
			ST_LIGHT_AMBIENT,
			ST_LIGHT_POSITION,
			ST_LIGHT_DIRECTION,
			ST_LIGHT_RANGE,
			ST_LIGHT_FALLOFF,
			ST_LIGHT_ATTENUATION,
			ST_LIGHT_ANGLES,
			ST_COUNT_LIGHT
		}

		public enum EMaterialState
		{
			ST_MAT_DIFFUSE,
			ST_MAT_AMBIENT,
			ST_MAT_SPECULAR,
			ST_MAT_EMISSIVE,
			ST_MAT_POWER,
			ST_COUNT_MAT
		}

		public enum EStreamState
		{
			ST_STREAM_DATA,
			ST_STREAM_OFFSET,
			ST_STREAM_STRIDE,
			ST_STREAM_FREQ,
			ST_COUNT_STREAM
		}

		public enum EViewportState
		{
			ST_VIEWPORT_X,
			ST_VIEWPORT_Y,
			ST_VIEWPORT_WIDTH,
			ST_VIEWPORT_HEIGHT,
			ST_VIEWPORT_MINZ,
			ST_VIEWPORT_MAXZ,
			ST_COUNT_VIEWPORT
		}

		public enum EScissorState
		{
			ST_SCISSOR_ENABLE,
			ST_SCISSOR_X,
			ST_SCISSOR_Y,
			ST_SCISSOR_WIDTH,
			ST_SCISSOR_HEIGHT,
			ST_COUNT_SCISSOR
		}

		public enum EMiscState
		{
			ST_MISC_VERTEXFORMAT,
			ST_MISC_VERTEXSIZE,
			ST_MISC_INDICES,
			ST_MISC_SHADERPROGRAM_ORTHO,
			ST_MISC_SHADERPROGRAM_3D,
			ST_MISC_TEXTUREPALETTE,
			ST_MISC_SCISSORRECT,
			ST_MISC_NPATCHMODE,
			ST_MISC_SRCBLENDOVERRIDE,
			ST_MISC_DESTBLENDOVERRIDE,
			ST_MISC_BLTDEPTH,
			ST_MISC_3DMODE,
			ST_MISC_CULLMODE,
			ST_MISC_USE_TEXSCALE,
			ST_MISC_TEXTURE,
			ST_MISC_PIXELSHADERCONST,
			ST_MISC_VERTEXSHADERCONST,
			ST_MISC_CLIPPLANE,
			ST_MISC_TEXTUREREMAP,
			ST_MISC_ATLASENABLEDANDBASE,
			ST_MISC_ATLASUV,
			ST_COUNT_MISC,
			ST_COUNT_MISC_SINGLE = 14
		}
	}
}
