using System;
using Microsoft.Xna.Framework.Content;
using Sexy.Drivers.App;

namespace Sexy.Drivers.File
{
	public class XNAFileDriver : IFileDriver
	{
		public ContentManager GetContentManager()
		{
			return this.mContentManager;
		}

		public override bool InitFileDriver(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mContentManager = ((WP7AppDriver)this.mApp.mAppDriver).mContentManager;
			return this.mContentManager != null;
		}

		public override void InitSaveDataFolder()
		{
		}

		public override string GetSaveDataPath()
		{
			return "";
		}

		public override string GetCurPath()
		{
			return "";
		}

		public override string GetLoadDataPath()
		{
			throw new NotImplementedException();
		}

		public override IFile CreateFile(string thePath)
		{
			return new XNAFile(thePath, this);
		}

		public override IFile CreateFileWithBuffer(string thePath, byte theBuffer, uint theBufferSize)
		{
			throw new NotImplementedException();
		}

		public override IFile CreateFileDirect(string thePath)
		{
			throw new NotImplementedException();
		}

		public override long GetFileSize(string thePath)
		{
			throw new NotImplementedException();
		}

		public override long GetFileTime(string thePath)
		{
			throw new NotImplementedException();
		}

		public override bool FileExists(string thePath, ref bool isFolder)
		{
			throw new NotImplementedException();
		}

		public override bool MakeFolders(string theFolder)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteTree(string thePath)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteFile(string thePath)
		{
			throw new NotImplementedException();
		}

		public override bool MoveFile(string thePathSrc, string thePathDest)
		{
			return false;
		}

		public override IFileSearch FileSearchStart(string theCriteria, FileSearchInfo outInfo)
		{
			throw new NotImplementedException();
		}

		public override bool FileSearchNext(IFileSearch theSearch, FileSearchInfo theInfo)
		{
			throw new NotImplementedException();
		}

		public override bool FileSearchEnd(IFileSearch theInfo)
		{
			throw new NotImplementedException();
		}

		protected SexyAppBase mApp;

		protected ContentManager mContentManager;
	}
}
