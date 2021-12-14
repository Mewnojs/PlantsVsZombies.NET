using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class AttachmentHolder
	{
		public void Dispose()
		{
			DisposeHolder();
		}

		public void InitializeHolder()
		{
			mAttachments.Clear();
		}

		public void DisposeHolder()
		{
			for (int i = 0; i < mAttachments.Count; i++)
			{
				mAttachments[i].PrepareForReuse();
			}
			mAttachments.Clear();
		}

		public Attachment AllocAttachment()
		{
			Attachment newAttachment = Attachment.GetNewAttachment();
			newAttachment.mActive = true;
			mAttachments.Add(newAttachment);
			return newAttachment;
		}

		public List<Attachment> mAttachments = new List<Attachment>(1024);
	}
}
