using System;

namespace Sexy.GraphicsLib
{
	public abstract class RenderEffect
	{
		public virtual void Dispose()
		{
		}

		public abstract RenderDevice3D GetDevice();

		public abstract RenderEffectDefinition GetDefinition();

		public abstract void SetParameter(string inParamName, float[] inFloatData, uint inFloatCount);

		public abstract void SetParameter(string inParamName, float inFloatData);

		public void SetFloat(string inParamName, float inValue)
		{
			this.SetParameter(inParamName, inValue);
		}

		public void SetVector4(string inParamName, float[] inValue)
		{
			this.SetParameter(inParamName, inValue, 4U);
		}

		public void SetVector3(string inParamName, float[] inValue)
		{
			this.SetVector4(inParamName, new float[]
			{
				inValue[0],
				inValue[1],
				inValue[2],
				1f
			});
		}

		public virtual void SetMatrix(string inParamName, float[] inValue)
		{
			this.SetParameter(inParamName, inValue, 16U);
		}

		public abstract void GetParameterBySemantic(uint inSemantic, float[] outFloatData, uint inMaxFloatCount);

		public void SetCurrentTechnique(string inName)
		{
			this.SetCurrentTechnique(inName, true);
		}

		public abstract void SetCurrentTechnique(string inName, bool inCheckValid);

		public abstract string GetCurrentTechniqueName();

		public int Begin(out object outRunHandle)
		{
			return this.Begin(out outRunHandle, new HRenderContext());
		}

		public abstract int Begin(out object outRunHandle, HRenderContext inRenderContext);

		public abstract void BeginPass(object inRunHandle, int inPass);

		public abstract void EndPass(object inRunHandle, int inPass);

		public abstract void End(object inRunHandle);

		public abstract bool PassUsesVertexShader(int inPass);

		public abstract bool PassUsesPixelShader(int inPass);
	}
}
