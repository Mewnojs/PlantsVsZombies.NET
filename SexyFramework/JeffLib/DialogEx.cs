using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.WidgetsLib;

namespace JeffLib
{
	public class DialogEx : Dialog
	{
		public DialogEx(Image theComponentImage, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
			: base(theComponentImage, theButtonComponentImage, theId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode)
		{
			this.mFlushPriority = -1;
			this.mDrawScale.SetConstant(1.0);
		}

		public virtual void PreDraw(Graphics g)
		{
			this.mWidgetManager.FlushDeferredOverlayWidgets(this.mFlushPriority);
			Graphics3D graphics3D = ((g != null) ? g.Get3D() : null);
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Translate(-g.mTransX - (float)(this.mWidth / 2), -g.mTransY - (float)(this.mHeight / 2));
				sexyTransform2D.Scale((float)this.mDrawScale, (float)this.mDrawScale);
				sexyTransform2D.Translate(g.mTransX + (float)(this.mWidth / 2), g.mTransY + (float)(this.mHeight / 2));
				graphics3D.PushTransform(sexyTransform2D);
			}
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			this.PreDraw(g);
			base.DrawAll(theFlags, g);
			this.PostDraw(g);
		}

		public virtual void PostDraw(Graphics g)
		{
			Graphics3D graphics3D = ((g != null) ? g.Get3D() : null);
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				graphics3D.PopTransform();
			}
		}

		public override void Update()
		{
			base.Update();
			if (!this.mDrawScale.HasBeenTriggered())
			{
				this.MarkDirty();
			}
			if (!this.mDrawScale.IncInVal() && this.mDrawScale == 0.0)
			{
				this.CloseDialog();
				GlobalMembers.gSexyAppBase.KillDialog(this);
			}
		}

		public virtual void CloseDialog()
		{
		}

		public int mFlushPriority;

		public CurvedVal mDrawScale = new CurvedVal();
	}
}
