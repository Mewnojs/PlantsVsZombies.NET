using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class AttachmentHolder
	{
		public void Dispose()
		{
			this.DisposeHolder();
		}

		public void InitializeHolder()
		{
			this.mAttachments.Clear();
		}

		public void DisposeHolder()
		{
			for (int i = 0; i < this.mAttachments.Count; i++)
			{
				this.mAttachments[i].PrepareForReuse();
			}
			this.mAttachments.Clear();
		}

		public Attachment AllocAttachment()
		{
			Attachment newAttachment = Attachment.GetNewAttachment();
			newAttachment.mActive = true;
			this.mAttachments.Add(newAttachment);
			return newAttachment;
		}

		public List<Attachment> mAttachments = new List<Attachment>(1024);
	}
}
