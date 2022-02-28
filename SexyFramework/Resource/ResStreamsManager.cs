using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Resource
{
	public class ResStreamsManager
	{
		public int InitializeWithRSB(string theFileName)
		{
			throw new NotImplementedException();
		}

		public bool IsInitialized()
		{
			return false;
		}

		public bool IsGroupLoaded(string theGroupName)
		{
			throw new NotImplementedException();
		}

		public bool IsGroupLoaded(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public ResStreamsManager.GroupStatus GetGroupStatus(string theGroupName)
		{
			throw new NotImplementedException();
		}

		public ResStreamsManager.GroupStatus GetGroupStatus(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public bool LoadGroup(string theGroupName)
		{
			throw new NotImplementedException();
		}

		public bool LoadGroup(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public bool ForceLoadGroup(string theGroupName, string theDbgReason)
		{
			throw new NotImplementedException();
		}

		public bool ForceLoadGroup(int theGroupId, string theDbgReason)
		{
			throw new NotImplementedException();
		}

		public bool CanLoadGroup(string theGroupName)
		{
			throw new NotImplementedException();
		}

		public bool CanLoadGroup(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public bool DeleteGroup(string theGroupName)
		{
			throw new NotImplementedException();
		}

		public bool DeleteGroup(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public bool HasError()
		{
			throw new NotImplementedException();
		}

		public bool HasGlobalFileIndex()
		{
			throw new NotImplementedException();
		}

		public int GetGroupForFile(string theFileName)
		{
			throw new NotImplementedException();
		}

		public int GetLoadedGroupForFile(string theFileName)
		{
			throw new NotImplementedException();
		}

		public bool GetResidentFileBuffer(int theGroupId, string theFileName, ref byte[] theBuffer, ref int theSize)
		{
			throw new NotImplementedException();
		}

		public PFILE GetPakFileFromResidentBuffer(int theGroupId, string theFileName)
		{
			throw new NotImplementedException();
		}

		public bool GetImage(int theGroupId, string theFileName, ref Image img)
		{
			throw new NotImplementedException();
		}

		public bool LoadResourcesManifest(ResourceManager theManager)
		{
			throw new NotImplementedException();
		}

		public int GetBytesLoadedForGroup(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public int GetTotalBytesForGroup(int theGroupId)
		{
			throw new NotImplementedException();
		}

		public ResStreamsManager(SexyAppBase theApp)
		{
			throw new NotImplementedException();
		}

		public virtual void Dispose()
		{
		}

		public void Update()
		{
			throw new NotImplementedException();
		}

		public void DebugDraw(Graphics g, Rect aRegion)
		{
			throw new NotImplementedException();
		}

		internal void ForceLoadGroup(string theGroup)
		{
			throw new NotImplementedException();
		}

		internal int LookupGroup(string p)
		{
			throw new NotImplementedException();
		}

		public enum GroupStatus
		{
			NOT_RESIDENT,
			DELETING,
			PREPARING,
			RESIDENT
		}
	}
}
