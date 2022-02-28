using System;
using System.Collections.Generic;
using System.IO;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Drivers
{
	public abstract class IAppDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool InitAppDriver();

		public abstract void Start();

		public abstract void Init();

		public abstract bool UpdateAppStep(ref bool updated);

		public abstract void ClearUpdateBacklog(bool relaxForASecond);

		public abstract void Shutdown();

		public abstract void DoExit(int theCode);

		public abstract void Remove3DData(MemoryImage theMemoryImage);

		public abstract void BeginPopup();

		public abstract void EndPopup();

		public abstract int MsgBox(string theText, string theTitle, int theFlags);

		public abstract void Popup(string theString);

		public abstract bool OpenURL(string theURL, bool shutdownOnOpen);

		public abstract string GetGameSEHInfo();

		public abstract void SEHOccured();

		public abstract void GetSEHWebParams(DefinesMap theDefinesMap);

		public abstract void DoParseCmdLine();

		public abstract void ParseCmdLine(string theCmdLine);

		public abstract void HandleCmdLineParam(string theParamName, string theParamValue);

		public abstract void StartLoadingThread();

		public abstract double GetLoadingThreadProgress();

		public abstract void CopyToClipboard(string theString);

		public abstract string GetClipboard();

		public abstract void SetCursor(int theCursorNum);

		public abstract int GetCursor();

		public abstract void EnableCustomCursors(bool enabled);

		public abstract void SetCursorImage(int theCursorNum, Image theImage);

		public abstract void SwitchScreenMode();

		public abstract void SwitchScreenMode(bool wantWindowed);

		public abstract void SwitchScreenMode(bool wantWindowed, bool is3d, bool force);

		public abstract bool KeyDown(int theKey);

		public abstract bool DebugKeyDown(int theKey);

		public abstract bool DebugKeyDownAsync(int theKey, bool ctrlDown, bool altDown);

		public abstract bool Is3DAccelerated();

		public abstract bool Is3DAccelerationSupported();

		public abstract bool Is3DAccelerationRecommended();

		public abstract void Set3DAcclerated(bool is3D, bool reinit);

		public abstract bool IsUIOrientationAllowed(UI_ORIENTATION theOrientation);

		public abstract UI_ORIENTATION GetUIOrientation();

		public abstract bool IsSystemUIShowing();

		public abstract void ShowKeyboard();

		public abstract void HideKeyboard();

		public abstract bool CheckSignature(SexyBuffer theBuffer, string theFileName);

		public abstract bool ReloadAllResources();

		public abstract bool ConfigGetSubKeys(string theKeyName, List<string> theSubKeys);

		public abstract bool ConfigReadString(string theValueName, ref string theString);

		public abstract bool ConfigReadInteger(string theValueName, ref int theValue);

		public abstract bool ConfigReadBoolean(string theValueName, ref bool theValue);

		public abstract bool ConfigReadData(string theValueName, ref byte[] theValue, ref ulong theLength);

		public abstract bool ConfigWriteString(string theValueName, string theString);

		public abstract bool ConfigWriteInteger(string theValueName, int theValue);

		public abstract bool ConfigWriteBoolean(string theValueName, bool theValue);

		public abstract bool ConfigWriteData(string theValueName, byte[] theValue, ulong theLength);

		public abstract bool ConfigEraseKey(string theKeyName);

		public abstract void ConfigEraseValue(string theValueName);

		public abstract void ReadFromConfig();

		public abstract void WriteToConfig();

		public abstract bool WriteBufferToFile(string theFileName, SexyBuffer theBuffer);

		public abstract bool ReadBufferFromFile(string theFileName, SexyBuffer theBuffer, bool dontWriteToDemo);

		public abstract bool WriteBytesToFile(string theFileName, object theData, ulong theDataLen);

		public abstract DeviceImage GetOptimizedImage(string theFileName, bool commitBits, bool allowTriReps);

		public abstract DeviceImage GetOptimizedImage(Stream stream, bool commitBits, bool allowTriReps);

		public abstract object GetOptimizedRenderData(string theFileName);

		public abstract DeviceImage GetOptimizedImageFromData(string theFileName, bool commitBits, bool allowTriReps, int width, int height);

		public abstract bool ShouldPauseUpdates();

		public abstract void Draw();

		public abstract int GetAppTime();

		public abstract Localization.LanguageType GetAppLanguage();
	}
}
