using System;

namespace Sexy.Drivers
{
	public abstract class IFileDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool InitFileDriver(SexyAppBase theApp);

		public abstract void InitSaveDataFolder();

		public virtual string FixPath(string theFileName)
		{
			return theFileName;
		}

		public abstract string GetSaveDataPath();

		public abstract string GetCurPath();

		public abstract string GetLoadDataPath();

		public abstract IFile CreateFile(string thePath);

		public abstract IFile CreateFileWithBuffer(string thePath, byte theBuffer, uint theBufferSize);

		public abstract IFile CreateFileDirect(string thePath);

		public virtual bool SupportsMemoryMappedFiles()
		{
			return false;
		}

		public virtual IFile CreateFileMemoryMapped(string thePath)
		{
			return null;
		}

		public abstract long GetFileSize(string thePath);

		public abstract long GetFileTime(string thePath);

		public abstract bool FileExists(string thePath, ref bool isFolder);

		public abstract bool MakeFolders(string theFolder);

		public abstract bool DeleteTree(string thePath);

		public abstract bool DeleteFile(string thePath);

		public virtual bool MoveFile(string thePathSrc, string thePathDest)
		{
			return false;
		}

		public abstract IFileSearch FileSearchStart(string theCriteria, FileSearchInfo outInfo);

		public abstract bool FileSearchNext(IFileSearch theSearch, FileSearchInfo theInfo);

		public abstract bool FileSearchEnd(IFileSearch theInfo);
	}
}
