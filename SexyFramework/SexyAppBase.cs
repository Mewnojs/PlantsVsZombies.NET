using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Sexy.Drivers;
using Sexy.Drivers.App;
using Sexy.Drivers.Audio;
using Sexy.Drivers.File;
using Sexy.Drivers.Leaderboard;
using Sexy.Drivers.Profile;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.Resource;
using Sexy.Sound;
using Sexy.WidgetsLib;

namespace Sexy
{
	public class SexyAppBase : ButtonListener, DialogListener
	{
		public SexyAppBase()
		{
			this.mFirstLaunch = false;
			this.mAppUpdated = false;
			this.mResStreamsManager = null;
			this.mGamepadLocked = -1;
			this.mAllowSwapScreenImage = true;
			this.mMaxUpdateBacklog = 200;
			this.mPauseWhenMoving = true;
			this.mGraphicsDriver = null;
			this.mProfileManager = null;
			this.mLeaderboardManager = null;
			this.InitFileDriver();
			this.mAppDriver = WP7AppDriver.CreateAppDriver(this);
			this.mGamepadDriver = XNAGamepadDriver.CreateGamepadDriver();
			this.mAudioDriver = WP7AudioDriver.CreateAudioDriver(this);
			this.mProfileDriver = IProfileDriver.CreateProfileDriver();
			GlobalMembers.gSexyAppBase = this;
			this.mAppDriver.InitAppDriver();
			this.mWidgetManager = new WidgetManager(this);
			Localization.InitLanguage();
		}

		public virtual void Dispose()
		{
			foreach (KeyValuePair<int, Dialog> keyValuePair in this.mDialogMap)
			{
				Widget value = keyValuePair.Value;
				if (value.mParent != null)
				{
					value.mParent.RemoveWidget(value);
				}
			}
			this.mDialogMap.Clear();
			this.mDialogList.Clear();
			this.mWidgetManager = null;
			this.mResourceManager = null;
			foreach (KeyValuePair<KeyValuePair<string, string>, SharedImage> keyValuePair2 in this.mSharedImageMap)
			{
				SharedImage value2 = keyValuePair2.Value;
				if (value2.mImage != null)
				{
					value2.mImage.Dispose();
					value2.mImage = null;
				}
			}
			this.mSharedImageMap.Clear();
			this.mAppDriver.Shutdown();
			this.mProfileManager = null;
			this.mLeaderboardManager = null;
			this.mAudioDriver = null;
			this.mGamepadDriver = null;
			this.mSaveGameDriver = null;
			this.mProfileDriver = null;
			this.mHttpDriver = null;
			this.mLeaderboardDriver = null;
			this.mAchievementDriver = null;
			this.mAppDriver = null;
			this.mResStreamsManager = null;
			this.mFileDriver = null;
			if (GlobalMembers.gFileDriver != null)
			{
				GlobalMembers.gFileDriver.Dispose();
			}
			GlobalMembers.gFileDriver = null;
			GlobalMembers.gSexyAppBase = null;
		}

		public virtual void ClearUpdateBacklog(bool relaxForASecond)
		{
			this.mAppDriver.ClearUpdateBacklog(relaxForASecond);
		}

		public virtual bool IsScreenSaver()
		{
			return this.mIsScreenSaver;
		}

		public virtual bool AppCanRestore()
		{
			return !this.mIsDisabled;
		}

		public virtual Dialog NewDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			return new Dialog(null, null, theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
		}

		public virtual Dialog DoDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			this.KillDialog(theDialogId);
			Dialog dialog = this.NewDialog(theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
			this.AddDialog(theDialogId, dialog);
			return dialog;
		}

		public Dialog GetDialog(int theDialogId)
		{
			if (this.mDialogMap.ContainsKey(theDialogId))
			{
				return this.mDialogMap[theDialogId];
			}
			return null;
		}

		public virtual bool KillDialog(int theDialogId, bool removeWidget, bool deleteWidget)
		{
			if (this.mDialogMap.ContainsKey(theDialogId))
			{
				Dialog dialog = this.mDialogMap[theDialogId];
				if (dialog.mResult == -1)
				{
					dialog.mResult = 0;
				}
				if (this.mDialogList.Contains(dialog))
				{
					this.mDialogList.Remove(dialog);
				}
				this.mDialogMap.Remove(theDialogId);
				if ((removeWidget || deleteWidget) && dialog.mParent != null)
				{
					dialog.mParent.RemoveWidget(dialog);
				}
				if (dialog.IsModal())
				{
					this.ModalClose();
					this.mWidgetManager.RemoveBaseModal(dialog);
				}
				if (deleteWidget)
				{
					this.SafeDeleteWidget(dialog);
				}
				return true;
			}
			return false;
		}

		public virtual bool KillDialog(int theDialogId)
		{
			return this.KillDialog(theDialogId, true, true);
		}

		public virtual bool KillDialog(Dialog theDialog)
		{
			return this.KillDialog(theDialog.mId);
		}

		public virtual int GetDialogCount()
		{
			return this.mDialogMap.Count;
		}

		public virtual void AddDialog(int theDialogId, Dialog theDialog, FlagsMod belowModalFlagsMod)
		{
			this.KillDialog(theDialogId);
			if (theDialog.mWidth == 0)
			{
				int num = this.mWidth / 2;
				theDialog.Resize((this.mWidth - num) / 2, this.mHeight / 5, num, theDialog.GetPreferredHeight(num));
			}
			this.mDialogMap[theDialogId] = theDialog;
			this.mDialogList.AddLast(theDialog);
			this.mWidgetManager.AddWidget(theDialog);
			if (theDialog.IsModal())
			{
				this.mWidgetManager.AddBaseModal(theDialog, belowModalFlagsMod);
				this.ModalOpen();
			}
		}

		public virtual void AddDialog(int theDialogId, Dialog theDialog)
		{
			this.AddDialog(theDialogId, theDialog, this.mWidgetManager.mDefaultBelowModalFlagsMod);
		}

		public virtual void AddDialog(Dialog theDialog)
		{
			this.AddDialog(theDialog.mId, theDialog);
		}

		public virtual void ModalOpen()
		{
		}

		public virtual void ModalClose()
		{
		}

		public virtual void DialogButtonPress(int theDialogId, int theButtonId)
		{
		}

		public virtual void DialogButtonDepress(int theDialogId, int theButtonId)
		{
		}

		public virtual void GotFocus()
		{
		}

		public virtual void LostFocus()
		{
		}

		public virtual void URLOpenFailed(string theURL)
		{
			this.mIsOpeningURL = false;
		}

		public void URLOpenSucceeded(string theURL)
		{
			this.mIsOpeningURL = false;
			if (this.mShutdownOnURLOpen)
			{
				this.Shutdown();
			}
		}

		public bool OpenURL(string theURL, bool shutdownOnOpen)
		{
			return this.mAppDriver.OpenURL(theURL, shutdownOnOpen);
		}

		public virtual void SetCursorImage(int theCursorNum, Image theImage)
		{
			this.mAppDriver.SetCursorImage(theCursorNum, theImage);
		}

		public virtual void SetCursor(ECURSOR eCURSOR)
		{
			this.mAppDriver.SetCursor((int)eCURSOR);
		}

		public virtual int GetCursor()
		{
			return this.mAppDriver.GetCursor();
		}

		public virtual void EnableCustomCursors(bool enabled)
		{
			this.mAppDriver.EnableCustomCursors(enabled);
		}

		public virtual double GetLoadingThreadProgress()
		{
			return this.mAppDriver.GetLoadingThreadProgress();
		}

		public virtual bool RegistryWriteString(string theValueName, string theString)
		{
			return this.mAppDriver.ConfigWriteString(theValueName, theString);
		}

		public virtual bool RegistryWriteInteger(string theValueName, int theValue)
		{
			return this.mAppDriver.ConfigWriteInteger(theValueName, theValue);
		}

		public virtual bool RegistryWriteBoolean(string theValueName, bool theValue)
		{
			return this.mAppDriver.ConfigWriteBoolean(theValueName, theValue);
		}

		public virtual bool RegistryWriteData(string theValueName, byte[] theValue, ulong theLength)
		{
			return this.mAppDriver.ConfigWriteData(theValueName, theValue, theLength);
		}

		public virtual void WriteToRegistry()
		{
			this.RegistryWriteInteger("MusicVolume", (int)(this.mMusicVolume * 100.0));
			this.RegistryWriteInteger("SfxVolume", (int)(this.mSfxVolume * 100.0));
			this.RegistryWriteInteger("Muted", (this.mMuteCount - this.mAutoMuteCount > 0) ? 1 : 0);
			this.RegistryWriteInteger("ScreenMode", this.mIsWindowed ? 0 : 1);
			this.RegistryWriteInteger("PreferredX", this.mPreferredX);
			this.RegistryWriteInteger("PreferredY", this.mPreferredY);
			this.RegistryWriteInteger("PreferredWidth", this.mPreferredWidth);
			this.RegistryWriteInteger("PreferredHeight", this.mPreferredHeight);
			this.RegistryWriteInteger("CustomCursors", this.mCustomCursorsEnabled ? 1 : 0);
			this.RegistryWriteInteger("InProgress", 0);
			this.RegistryWriteBoolean("WaitForVSync", this.mWaitForVSync);
			this.mAppDriver.WriteToConfig();
		}

		public virtual bool RegistryEraseKey(string _theKeyName)
		{
			return this.mAppDriver.ConfigEraseKey(_theKeyName);
		}

		public virtual void RegistryEraseValue(string _theValueName)
		{
			this.mAppDriver.ConfigEraseValue(_theValueName);
		}

		public virtual bool RegistryGetSubKeys(string theKeyName, List<string> theSubKeys)
		{
			return this.mAppDriver.ConfigGetSubKeys(theKeyName, theSubKeys);
		}

		public virtual bool RegistryReadString(string theKey, ref string theString)
		{
			return this.mAppDriver.ConfigReadString(theKey, ref theString);
		}

		public virtual bool RegistryReadInteger(string theKey, ref int theValue)
		{
			return this.mAppDriver.ConfigReadInteger(theKey, ref theValue);
		}

		public virtual bool RegistryReadBoolean(string theKey, ref bool theValue)
		{
			return this.mAppDriver.ConfigReadBoolean(theKey, ref theValue);
		}

		public virtual bool RegistryReadData(string theKey, byte[] theValue, ref ulong theLength)
		{
			return this.mAppDriver.ConfigReadData(theKey, ref theValue, ref theLength);
		}

		public virtual void ReadFromRegistry()
		{
			this.mReadFromRegistry = true;
			this.mRegKey = this.GetString("RegistryKey", this.mRegKey);
			if (this.mRegKey.Length == 0)
			{
				return;
			}
			int num = 0;
			if (this.RegistryReadInteger("MusicVolume", ref num))
			{
				this.mMusicVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("SfxVolume", ref num))
			{
				this.mSfxVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("Muted", ref num))
			{
				this.mMuteCount = num;
			}
			if (this.RegistryReadInteger("ScreenMode", ref num))
			{
				this.mIsWindowed = num == 0 && !this.mForceFullscreen;
			}
			this.RegistryReadInteger("PreferredX", ref this.mPreferredX);
			this.RegistryReadInteger("PreferredY", ref this.mPreferredY);
			this.RegistryReadInteger("PreferredWidth", ref this.mPreferredWidth);
			this.RegistryReadInteger("PreferredHeight", ref this.mPreferredHeight);
			if (this.RegistryReadInteger("CustomCursors", ref num))
			{
				this.EnableCustomCursors(num != 0);
			}
			this.RegistryReadBoolean("WaitForVSync", ref this.mWaitForVSync);
			if (this.RegistryReadInteger("InProgress", ref num))
			{
				this.mLastShutdownWasGraceful = num == 0;
			}
			if (!this.IsScreenSaver())
			{
				this.RegistryWriteInteger("InProgress", 1);
			}
			this.mAppDriver.ReadFromConfig();
		}

		public virtual bool WriteBytesToFile(string theFileName, byte[] theData, ulong theDataLen)
		{
			return this.mAppDriver.WriteBytesToFile(theFileName, theData, theDataLen);
		}

		public virtual bool WriteBufferToFile(string theFileName, SexyBuffer theBuffer)
		{
			return this.WriteBytesToFile(theFileName, theBuffer.GetDataPtr(), (ulong)((long)theBuffer.GetDataLen()));
		}

		public bool ReadBufferFromStream(string theFileName, ref SexyBuffer theBuffer)
		{
			theBuffer.Clear();
			try
			{
				Stream stream = TitleContainer.OpenStream("Content\\" + theFileName);
				byte[] array = new byte[stream.Length];
				stream.Read(array, 0, (int)stream.Length);
				stream.Close();
				theBuffer.SetData(array, array.Length);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public bool ReadBufferFromFile(string theFileName, ref SexyBuffer theBuffer)
		{
			PFILE pfile = new PFILE(theFileName, "rb");
			if (!pfile.Open())
			{
				return false;
			}
			byte[] data = pfile.GetData();
			int theCount = data.Length;
			theBuffer.Clear();
			theBuffer.SetData(data, theCount);
			pfile.Close();
			return true;
		}

		public virtual bool FileExists(string theFileName)
		{
			bool flag = false;
			return this.mFileDriver.FileExists(theFileName, ref flag);
		}

		public virtual bool EraseFile(string theFileName)
		{
			return this.mFileDriver.DeleteFile(theFileName);
		}

		public virtual void ShutdownHook()
		{
		}

		public virtual void Shutdown()
		{
			this.mAppDriver.Shutdown();
		}

		public virtual void DoExit(int theCode)
		{
			this.mAppDriver.DoExit(theCode);
		}

		public virtual void UpdateFrames()
		{
			this.mUpdateCount++;
			this.mGamepadDriver.Update();
			if (!this.mMinimized && this.mWidgetManager.UpdateFrame())
			{
				this.mFPSDirtyCount++;
			}
			if (this.mResStreamsManager != null)
			{
				this.mResStreamsManager.Update();
			}
			if (this.mSoundManager != null)
			{
				this.mSoundManager.Update();
			}
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.Update();
			}
			if (this.mSaveGameDriver != null)
			{
				this.mSaveGameDriver.Update();
			}
			if (this.mProfileManager != null)
			{
				this.mProfileManager.Update();
			}
			if (this.mHttpDriver != null)
			{
				this.mHttpDriver.Update();
			}
			if (this.mLeaderboardManager != null)
			{
				this.mLeaderboardManager.Update();
			}
			if (this.mAchievementDriver != null)
			{
				this.mAchievementDriver.Update();
			}
			this.CleanSharedImages();
		}

		public virtual void BeginPopup()
		{
			this.mAppDriver.BeginPopup();
		}

		public virtual void EndPopup()
		{
			this.mAppDriver.EndPopup();
		}

		public virtual int MsgBox(string theText, string theTitle, int theFlags)
		{
			return 0;
		}

		public virtual void Popup(string theString)
		{
			this.mAppDriver.Popup(theString);
		}

		public void SafeDeleteWidget(Widget theWidget)
		{
			SexyAppBase.WidgetSafeDeleteInfo widgetSafeDeleteInfo = new SexyAppBase.WidgetSafeDeleteInfo();
			widgetSafeDeleteInfo.mUpdateAppDepth = this.mUpdateAppDepth;
			widgetSafeDeleteInfo.mWidget = theWidget;
			this.mSafeDeleteList.AddLast(widgetSafeDeleteInfo);
		}

		public virtual bool KeyDown(int theKey)
		{
			return this.mAppDriver.KeyDown(theKey);
		}

		public virtual bool DebugKeyDown(int theKey)
		{
			return this.mAppDriver.DebugKeyDown(theKey);
		}

		public virtual bool DebugKeyDownAsync(int theKey, bool ctrlDown, bool altDown)
		{
			return false;
		}

		public virtual void ShowKeyboard()
		{
			this.mAppDriver.ShowKeyboard();
		}

		public virtual void HideKeyboard()
		{
			this.mAppDriver.HideKeyboard();
		}

		public virtual void TouchBegan(SexyAppBase.Touch theTouch)
		{
			this.mWidgetManager.TouchBegan(theTouch);
		}

		public virtual void TouchEnded(SexyAppBase.Touch theTouch)
		{
			this.mWidgetManager.TouchEnded(theTouch);
		}

		public virtual void TouchMoved(SexyAppBase.Touch theTouch)
		{
			this.mWidgetManager.TouchMoved(theTouch);
		}

		public virtual void TouchesCanceled()
		{
			this.mWidgetManager.TouchesCanceled();
		}

		public virtual void GamepadButtonDown(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			this.mWidgetManager.GamepadButtonDown(theButton, thePlayer, theFlags);
		}

		public virtual void GamepadButtonUp(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			this.mWidgetManager.GamepadButtonUp(theButton, thePlayer, theFlags);
		}

		public virtual void GamepadAxisMove(GamepadAxis theAxis, int thePlayer, float theAxisValue)
		{
			this.mWidgetManager.GamepadAxisMove(theAxis, thePlayer, theAxisValue);
		}

		public virtual bool IsUIOrientationAllowed(UI_ORIENTATION theOrientation)
		{
			return this.mAppDriver.IsUIOrientationAllowed(theOrientation);
		}

		public virtual void UIOrientationChanged(UI_ORIENTATION theOrientation)
		{
		}

		public virtual UI_ORIENTATION GetUIOrientation()
		{
			return this.mAppDriver.GetUIOrientation();
		}

		public virtual void CloseRequestAsync()
		{
		}

		public virtual void Done3dTesting()
		{
		}

		public virtual string NotifyCrashHook()
		{
			return "";
		}

		public virtual void DeleteNativeImageData()
		{
		}

		public virtual void DeleteExtraImageData()
		{
		}

		public virtual void ReInitImages()
		{
		}

		public virtual void LoadingThreadProc()
		{
		}

		public virtual void LoadingThreadCompleted()
		{
		}

		public virtual void StartLoadingThread()
		{
			this.mAppDriver.StartLoadingThread();
		}

		public virtual void SwitchScreenMode(bool wantWindowed, bool is3d, bool force)
		{
		}

		public virtual void SwitchScreenMode(bool wantWindowed)
		{
		}

		public virtual void SwitchScreenMode()
		{
		}

		public void ProcessSafeDeleteList()
		{
			foreach (SexyAppBase.WidgetSafeDeleteInfo widgetSafeDeleteInfo in this.mSafeDeleteList)
			{
				if (widgetSafeDeleteInfo.mWidget != null)
				{
					widgetSafeDeleteInfo.mWidget.Dispose();
					widgetSafeDeleteInfo.mWidget = null;
				}
			}
			this.mSafeDeleteList.Clear();
		}

		public virtual bool UpdateAppStep(ref bool updated)
		{
			return this.mAppDriver.UpdateAppStep(ref updated);
		}

		public virtual bool Update(int gameTime)
		{
			bool flag = false;
			while (this.UpdateAppStep(ref flag))
			{
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		public virtual void Draw(int time)
		{
			if (this.mAppDriver != null)
			{
				this.mAppDriver.Draw();
			}
		}

		public virtual string GetGameSEHInfo()
		{
			return this.mAppDriver.GetGameSEHInfo();
		}

		public virtual void PreTerminate()
		{
		}

		public virtual void Start()
		{
			this.mAppDriver.Start();
		}

		public virtual bool CheckSignature(SexyBuffer theBuffer, string theFileName)
		{
			return this.mAppDriver.CheckSignature(theBuffer, theFileName);
		}

		public virtual bool LoadProperties(string theFileName, bool required, bool checkSig, bool needsLocaleCorrection)
		{
			SexyBuffer buffer = new SexyBuffer();
			if (!this.ReadBufferFromFile(theFileName, ref buffer))
			{
				bool flag = false;
				if (needsLocaleCorrection && this.mResourceManager != null)
				{
					buffer.Clear();
					flag = this.ReadBufferFromFile(this.mResourceManager.GetLocaleFolder(true) + theFileName, ref buffer);
				}
				if (!flag)
				{
					if (!required)
					{
						return true;
					}
					this.Popup(this.GetString("UNABLE_OPEN_PROPERTIES", "Unable to open properties file ") + theFileName);
					return false;
				}
			}
			if (checkSig && !this.CheckSignature(buffer, theFileName))
			{
				this.Popup(this.GetString("PROPERTIES_SIG_FAILED", "Signature check failed on ") + theFileName + "'");
				return false;
			}
			PropertiesParser propertiesParser = new PropertiesParser(this);
			if (!propertiesParser.ParsePropertiesBuffer(buffer.GetDataPtr()))
			{
				this.Popup(propertiesParser.GetErrorText());
				return false;
			}
			return true;
		}

		public virtual bool LoadProperties()
		{
			return this.LoadProperties("properties/default.xml", true, false, true);
		}

		public virtual void LoadResourceManifest()
		{
			if (!this.mResourceManager.ParseResourcesFile("properties/resources.xml"))
			{
				this.ShowResourceError(true);
			}
		}

		public virtual void ShowResourceError(bool doExit)
		{
			this.Popup(this.mResourceManager.GetErrorText());
			if (doExit)
			{
				this.DoExit(0);
			}
		}

		public virtual bool ReloadAllResources()
		{
			return this.mAppDriver.ReloadAllResources();
		}

		public virtual bool GetBoolean(string theId, bool theDefault)
		{
			if (this.mBoolProperties.ContainsKey(theId))
			{
				return this.mBoolProperties[theId];
			}
			return theDefault;
		}

		public virtual bool GetBoolean(string theId)
		{
			return this.GetBoolean(theId, false);
		}

		public virtual int GetInteger(string theId)
		{
			return this.GetInteger(theId, 0);
		}

		public virtual int GetInteger(string theId, int theDefault)
		{
			if (this.mIntProperties.ContainsKey(theId))
			{
				return this.mIntProperties[theId];
			}
			return theDefault;
		}

		public virtual double GetDouble(string theId)
		{
			return this.GetDouble(theId, 0.0);
		}

		public virtual double GetDouble(string theId, double theDefault)
		{
			if (this.mDoubleProperties.ContainsKey(theId))
			{
				return this.mDoubleProperties[theId];
			}
			return theDefault;
		}

		public virtual string GetString(string theId, string theDefault)
		{
			if (this.mStringProperties.ContainsKey(theId))
			{
				return this.mStringProperties[theId];
			}
			return theDefault;
		}

		public virtual string GetString(string theId)
		{
			return this.GetString(theId, "");
		}

		public virtual List<string> GetStringVector(string theId)
		{
			if (this.mStringVectorProperties.ContainsKey(theId))
			{
				return this.mStringVectorProperties[theId];
			}
			return new List<string>();
		}

		public virtual void SetString(string anID, string value)
		{
			if (!this.mStringProperties.ContainsKey(anID))
			{
				this.mStringProperties[anID] = value;
			}
		}

		public virtual void SetBoolean(string anID, bool boolValue)
		{
			if (!this.mBoolProperties.ContainsKey(anID))
			{
				this.mBoolProperties[anID] = boolValue;
			}
		}

		public virtual void SetInteger(string anID, int anInt)
		{
			if (!this.mIntProperties.ContainsKey(anID))
			{
				this.mIntProperties[anID] = anInt;
			}
		}

		public virtual void SetDouble(string anID, double aDouble)
		{
			if (!this.mDoubleProperties.ContainsKey(anID))
			{
				this.mDoubleProperties[anID] = aDouble;
			}
		}

		public virtual void DoParseCmdLine()
		{
		}

		public virtual void ParseCmdLine(string theCmdLine)
		{
		}

		public virtual void HandleCmdLineParam(string theParamName, string theParamValue)
		{
		}

		public virtual void PreDisplayHook()
		{
		}

		public virtual void PreDDInterfaceInitHook()
		{
		}

		public virtual void PostDDInterfaceInitHook()
		{
		}

		public virtual bool ChangeDirHook(string theIntendedPath)
		{
			return false;
		}

		public virtual void InitPropertiesHook()
		{
		}

		public virtual void InitHook()
		{
		}

		public virtual MusicInterface CreateMusicInterface()
		{
			if (this.mNoSoundNeeded || this.mAudioDriver == null)
			{
				return new MusicInterface();
			}
			return this.mAudioDriver.CreateMusicInterface();
		}

		public virtual void Init()
		{
			if (this.mAudioDriver != null)
			{
				this.mAudioDriver.InitAudioDriver();
			}
			if (this.mAppDriver != null)
			{
				this.mAppDriver.Init();
			}
			if (this.mProfileDriver != null)
			{
				this.mProfileDriver.Init();
			}
			if (this.mSaveGameDriver != null)
			{
				this.mSaveGameDriver.Init();
			}
			if (this.mLeaderboardDriver != null)
			{
				this.mLeaderboardDriver.Init();
			}
			if (this.mAchievementDriver != null)
			{
				this.mAchievementDriver.Init();
			}
			if (this.mGamepadDriver != null)
			{
				this.mGamepadDriver.InitGamepadDriver(this);
			}
			long ticks = DateTime.Now.Ticks;
			string languageSuffix = Localization.GetLanguageSuffix(Localization.GetCurrentLanguage());
			string text = "properties/resources/resources" + languageSuffix + ".xml";
			this.mResourceManager = ((XNAFileDriver)this.mFileDriver).GetContentManager().Load<ResourceManager>(text);
			this.mResourceManager.mApp = this;
			long ticks2 = DateTime.Now.Ticks;
		}

		public virtual void HandleGameAlreadyRunning()
		{
		}

		public virtual DeviceImage GetImage(string theFileName, bool commitBits, bool allowTriReps, bool isInAtlas)
		{
			if (isInAtlas)
			{
				allowTriReps = false;
			}
			if (!isInAtlas && this.mResStreamsManager != null && this.mResStreamsManager.IsInitialized())
			{
				string theFileName2 = theFileName + ".ptx";
				int groupForFile = this.mResStreamsManager.GetGroupForFile(theFileName2);
				if (groupForFile != -1 && (this.mResStreamsManager.IsGroupLoaded(groupForFile) || this.mResStreamsManager.ForceLoadGroup(groupForFile, theFileName)))
				{
					Image image = new Image();
					if (this.mResStreamsManager.GetImage(groupForFile, theFileName2, ref image))
					{
						return (DeviceImage)image;
					}
				}
			}
			if (!isInAtlas)
			{
				DeviceImage optimizedImage = this.mAppDriver.GetOptimizedImage(theFileName, commitBits, allowTriReps);
				if (optimizedImage != null)
				{
					return optimizedImage;
				}
			}
			if (isInAtlas)
			{
				DeviceImage deviceImage = new DeviceImage();
				if (!allowTriReps)
				{
					deviceImage.AddImageFlags(ImageFlags.ImageFlag_NoTriRep);
				}
				deviceImage.mWidth = (deviceImage.mHeight = 0);
				deviceImage.mFilePath = theFileName;
				return deviceImage;
			}
			throw new NotSupportedException();
		}

		protected virtual MemoryImage GetImageByName(string name)
		{
			SharedImageRef sharedImageRef = this.mResourceManager.LoadImage(name);
			if (sharedImageRef != null)
			{
				return sharedImageRef.GetMemoryImage();
			}
			return null;
		}

		public virtual void ColorizeImage(SharedImageRef theImage, SexyColor theColor)
		{
		}

		public virtual DeviceImage CreateColorizedImage(Image theImage, SexyColor theColor)
		{
			return null;
		}

		public virtual DeviceImage CopyImage(Image theImage, Rect theRect)
		{
			return null;
		}

		public virtual DeviceImage CopyImage(Image theImage)
		{
			return null;
		}

		public virtual void MirrorImage(Image theImage)
		{
		}

		public virtual void FlipImage(Image theImage)
		{
		}

		public virtual void RotateImageHue(MemoryImage theImage, int theDelta)
		{
		}

		public virtual void AddMemoryImage(MemoryImage memoryImage)
		{
			if (this.mGraphicsDriver == null)
			{
				return;
			}
			new AutoCrit(this.mImageSetCritSect);
			this.mMemoryImageSet.Add(memoryImage);
		}

		public virtual void RemoveMemoryImage(MemoryImage theMemoryImage)
		{
			if (this.mGraphicsDriver == null)
			{
				return;
			}
			new AutoCrit(this.mImageSetCritSect);
			if (this.mMemoryImageSet.Contains(theMemoryImage))
			{
				this.mMemoryImageSet.Remove(theMemoryImage);
			}
			this.Remove3DData(theMemoryImage);
		}

		public virtual void Remove3DData(MemoryImage theMemoryImage)
		{
			this.mAppDriver.Remove3DData(theMemoryImage);
		}

		public virtual SharedImageRef SetSharedImage(string theFileName, string theVariant, DeviceImage anImage)
		{
			bool flag = false;
			return this.SetSharedImage(theFileName, theVariant, anImage, ref flag);
		}

		public virtual SharedImageRef SetSharedImage(string theFileName, string theVariant, DeviceImage theImage, ref bool isNew)
		{
			string text = theFileName.ToUpper();
			string text2 = theVariant.ToUpper();
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(text, text2);
			SharedImageRef sharedImageRef = new SharedImageRef();
			if (this.mSharedImageMap.ContainsKey(keyValuePair))
			{
				SharedImage sharedImage = new SharedImage();
				new AutoCrit(this.mCritSect);
				this.mSharedImageMap[keyValuePair] = sharedImage;
				sharedImageRef.mSharedImage = sharedImage;
				isNew = true;
			}
			else
			{
				sharedImageRef.mSharedImage = this.mSharedImageMap[keyValuePair];
			}
			if (sharedImageRef.mSharedImage.mImage != theImage)
			{
				sharedImageRef.mSharedImage.mImage.Dispose();
				sharedImageRef.mSharedImage.mImage = theImage;
			}
			return sharedImageRef;
		}

		public virtual SharedImageRef CheckSharedImage(string theFileName, string theVariant)
		{
			int num = theFileName.IndexOf('|');
			string text;
			if (num != -1)
			{
				ResourceRef imageRef = this.mResourceManager.GetImageRef(theFileName.Substring(num + 1));
				if (imageRef.HasResource())
				{
					return imageRef.GetSharedImageRef();
				}
				text = theFileName.Substring(0, num);
			}
			else
			{
				text = theFileName;
			}
			string text2 = text.ToUpper();
			string text3 = theVariant.ToUpper();
			SharedImageRef result = new SharedImageRef();
			new AutoCrit(this.mCritSect);
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(text2, text3);
			if (this.mSharedImageMap.ContainsKey(keyValuePair))
			{
				result = new SharedImageRef(this.mSharedImageMap[keyValuePair]);
			}
			return result;
		}

		public virtual SharedImageRef GetSharedImage(string aPath)
		{
			bool flag = false;
			return this.GetSharedImage(aPath, "", ref flag, true, false);
		}

		public virtual SharedImageRef GetSharedImage(string theFileName, string theVariant, ref bool isNew, bool allowTriReps, bool isInAtlas)
		{
			int num = theFileName.IndexOf('|');
			string text;
			if (num != -1)
			{
				ResourceRef imageRef = this.mResourceManager.GetImageRef(theFileName.Substring(num + 1));
				if (imageRef.HasResource())
				{
					return imageRef.GetSharedImageRef();
				}
				text = theFileName.Substring(0, num);
			}
			else
			{
				text = theFileName;
			}
			string text2 = text.ToUpper();
			string text3 = theVariant.ToUpper();
			new AutoCrit(this.mCritSect);
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(text2, text3);
			SharedImageRef sharedImageRef;
			if (this.mSharedImageMap.ContainsKey(keyValuePair))
			{
				sharedImageRef = new SharedImageRef(this.mSharedImageMap[keyValuePair]);
				sharedImageRef.mSharedImage.mLoading = true;
			}
			else
			{
				this.mSharedImageMap[keyValuePair] = new SharedImage();
				sharedImageRef = new SharedImageRef(this.mSharedImageMap[keyValuePair]);
				sharedImageRef.mSharedImage.mLoading = true;
				isNew = true;
			}
			if (sharedImageRef != null)
			{
				if (isInAtlas)
				{
					allowTriReps = false;
				}
				if (text.Length > 0 && text[0] == '!')
				{
					sharedImageRef.mSharedImage.mImage = new DeviceImage();
					if (!allowTriReps)
					{
						sharedImageRef.mSharedImage.mImage.AddImageFlags(ImageFlags.ImageFlag_NoTriRep);
					}
				}
				else
				{
					sharedImageRef.mSharedImage.mImage = this.GetImage(text, false, allowTriReps, isInAtlas);
				}
				sharedImageRef.mSharedImage.mLoading = false;
			}
			return sharedImageRef;
		}

		public virtual void CleanSharedImages()
		{
			new AutoCrit(this.mCritSect);
			if (this.mCleanupSharedImages)
			{
				foreach (KeyValuePair<KeyValuePair<string, string>, SharedImage> keyValuePair in this.mSharedImageMap)
				{
					SharedImage value = keyValuePair.Value;
					if (value.mImage != null && value.mRefCount == 0)
					{
						value.mImage.Dispose();
						value.mImage = null;
						List<KeyValuePair<string, string>> list = this.mRemoveList;
						list.Add(keyValuePair.Key);
					}
				}
				for (int i = 0; i < this.mRemoveList.Count; i++)
				{
					this.mSharedImageMap.Remove(this.mRemoveList[i]);
				}
				this.mRemoveList.Clear();
				this.mCleanupSharedImages = false;
			}
		}

		public ulong HSLToRGB(int h, int s, int l)
		{
			double num = (double)((l < 128) ? (l * (255 + s) / 255) : (l + s - l * s / 255));
			int num2 = (int)((double)(2 * l) - num);
			int num3 = 6 * h / 256;
			int num4 = (int)((double)num2 + (num - (double)num2) * (double)((h - num3 * 256 / 6) * 6) / 255.0);
			if (num4 > 255)
			{
				num4 = 255;
			}
			int num5 = (int)(num - (num - (double)num2) * (double)((h - num3 * 256 / 6) * 6) / 255.0);
			if (num5 < 0)
			{
				num5 = 0;
			}
			int num6;
			int num7;
			int num8;
			switch (num3)
			{
			case 0:
				num6 = (int)num;
				num7 = num4;
				num8 = num2;
				break;
			case 1:
				num6 = num5;
				num7 = (int)num;
				num8 = num2;
				break;
			case 2:
				num6 = num2;
				num7 = (int)num;
				num8 = num4;
				break;
			case 3:
				num6 = num2;
				num7 = num5;
				num8 = (int)num;
				break;
			case 4:
				num6 = num4;
				num7 = num2;
				num8 = (int)num;
				break;
			case 5:
				num6 = (int)num;
				num7 = num2;
				num8 = num5;
				break;
			default:
				num6 = (int)num;
				num7 = num4;
				num8 = num2;
				break;
			}
			return (ulong)(0xffffffffff000000) | (ulong)((ulong)((long)num6) << 16) | (ulong)((ulong)((long)num7) << 8) | (ulong)((long)num8);
		}

		public ulong RGBToHSL(int r, int g, int b)
		{
			int num = Math.Max(r, Math.Max(g, b));
			int num2 = Math.Min(r, Math.Min(g, b));
			int num3 = 0;
			int num4 = 0;
			int num5 = (num2 + num) / 2;
			int num6 = num - num2;
			if (num6 != 0)
			{
				num4 = num6 * 256 / ((num5 <= 128) ? (num2 + num) : (512 - num - num2));
				if (r == num)
				{
					num3 = ((g == num2) ? (1280 + (num - b) * 256 / num6) : (256 - (num - g) * 256 / num6));
				}
				else if (g == num)
				{
					num3 = ((b == num2) ? (256 + (num - r) * 256 / num6) : (768 - (num - b) * 256 / num6));
				}
				else
				{
					num3 = ((r == num2) ? (768 + (num - g) * 256 / num6) : (1280 - (num - r) * 256 / num6));
				}
				num3 /= 6;
			}
			return (ulong)(0xffffffffff000000) | (ulong)((long)num3) | (ulong)((long)((long)num4 << 8)) | (ulong)((long)((long)num5 << 16));
		}

		public void HSLToRGB(ulong[] theSource, ulong[] theDest, int theSize)
		{
			for (int i = 0; i < theSize; i++)
			{
				ulong num = theSource[i];
				theDest[i] = (num & (ulong)(0xffffffffff000000)) | (this.HSLToRGB((int)(num & 255UL), (int)(num >> 8) & 255, (int)(num >> 16) & 255) & 16777215UL);
			}
		}

		public void RGBToHSL(ulong[] theSource, ulong[] theDest, int theSize)
		{
			for (int i = 0; i < theSize; i++)
			{
				ulong num = theSource[i];
				theDest[i] = (num & (ulong)(0xffffffffff000000)) | (this.RGBToHSL((int)((num >> 16) & 255UL), (int)(num >> 8) & 255, (int)(num & 255UL)) & 16777215UL);
			}
		}

		public virtual void PlaySample(int theSoundNum)
		{
			if (this.mSoundManager == null)
			{
				return;
			}
			SoundInstance soundInstance = this.mSoundManager.GetSoundInstance(theSoundNum);
			if (soundInstance != null)
			{
				soundInstance.Play(false, true);
			}
		}

		public virtual void PlaySample(int theSoundNum, int thePan)
		{
			if (this.mSoundManager == null)
			{
				return;
			}
			SoundInstance soundInstance = this.mSoundManager.GetSoundInstance(theSoundNum);
			if (soundInstance != null)
			{
				soundInstance.SetPan(thePan);
				soundInstance.Play(false, true);
			}
		}

		public bool IsMuted()
		{
			return this.mMuteCount > 0;
		}

		public void Mute(bool autoMute)
		{
			this.mMuteCount++;
			if (autoMute)
			{
				this.mAutoMuteCount++;
			}
			this.SetMusicVolume(this.mMusicVolume);
			this.SetSfxVolume(this.mSfxVolume);
		}

		public void Unmute(bool autoMute)
		{
			if (this.mMuteCount > 0)
			{
				this.mMuteCount--;
				if (autoMute)
				{
					this.mAutoMuteCount--;
				}
			}
			this.SetMusicVolume(this.mMusicVolume);
			this.SetSfxVolume(this.mSfxVolume);
		}

		public virtual double GetMusicVolume()
		{
			if (this.mMusicInterface.isPlayingUserMusic())
			{
				return 0.0;
			}
			return this.mMusicVolume;
		}

		public virtual void SetMusicVolume(double theVolume)
		{
			this.mMusicVolume = theVolume;
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.SetVolume((this.mMuteCount > 0) ? 0.0 : this.mMusicVolume);
			}
		}

		public virtual double GetSfxVolume()
		{
			return this.mSfxVolume;
		}

		public virtual void SetSfxVolume(double theVolume)
		{
			if (this.mSoundManager != null)
			{
				this.mSoundManager.SetMasterVolume(theVolume);
			}
			this.mSfxVolume = theVolume;
		}

		public virtual double GetMasterVolume()
		{
			if (this.mSoundManager != null)
			{
				return this.mSoundManager.GetMasterVolume();
			}
			return 0.0;
		}

		public virtual void SetMasterVolume(double theMasterVolume)
		{
			if (this.mSoundManager != null)
			{
				this.mSoundManager.SetMasterVolume(theMasterVolume);
			}
		}

		public virtual bool Is3DAccelerated()
		{
			return this.mAppDriver.Is3DAccelerated();
		}

		public virtual bool Is3DAccelerationSupported()
		{
			return this.mAppDriver.Is3DAccelerationSupported();
		}

		public virtual bool Is3DAccelerationRecommended()
		{
			return this.mAppDriver.Is3DAccelerationRecommended();
		}

		public virtual void Set3DAcclerated(bool is3D, bool reinit)
		{
			this.mAppDriver.Set3DAcclerated(is3D, reinit);
		}

		public virtual void LowMemoryWarning()
		{
		}

		public virtual bool InitFileDriver()
		{
			if (GlobalMembers.gFileDriver == null)
			{
				GlobalMembers.gFileDriver = new XNAFileDriver();
			}
			this.mFileDriver = GlobalMembers.gFileDriver;
			return true;
		}

		public virtual void ButtonPress(int theId)
		{
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public virtual void ButtonDepress(int theId)
		{
		}

		public virtual void ButtonDownTick(int theId)
		{
		}

		public virtual void ButtonMouseEnter(int theId)
		{
		}

		public virtual void ButtonMouseLeave(int theId)
		{
		}

		public virtual void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public void SetFirstLaunchStatus(bool theStatus)
		{
			this.mFirstLaunch = theStatus;
		}

		public bool GetFirstLaunchStatus()
		{
			return this.mFirstLaunch;
		}

		public void SetAppUpdateStatus(bool theStatus)
		{
			this.mAppUpdated = theStatus;
		}

		public bool GetAppUpdateStatus()
		{
			return this.mAppUpdated;
		}

		protected virtual PIEffect GetPIEffectByName(string name)
		{
			return this.mResourceManager.LoadPIEffect(name);
		}

		protected virtual Font GetFontByName(string name)
		{
			return this.mResourceManager.LoadFont(name);
		}

		protected virtual int GetSoundIDByName(string name)
		{
			return this.mResourceManager.LoadSound(name);
		}

		protected virtual PopAnim GetPopAnimByName(string name)
		{
			return this.mResourceManager.LoadPopAnim(name);
		}

		public const int DEMO_FILE_ID = 1119809400;

		public const int DEMO_VERSION = 2;

		public IAppDriver mAppDriver;

		public IAudioDriver mAudioDriver;

		public IGraphicsDriver mGraphicsDriver;

		public IFileDriver mFileDriver;

		public IGamepadDriver mGamepadDriver;

		public IResStreamsDriver mResStreamsDriver;

		public IProfileDriver mProfileDriver;

		public ISaveGameDriver mSaveGameDriver;

		public IHttpDriver mHttpDriver;

		public ILeaderboardDriver mLeaderboardDriver;

		public IAchievementDriver mAchievementDriver;

		public uint mRandSeed;

		public string mCompanyName;

		public string mFullCompanyName;

		public string mProdName;

		public string mRegKey;

		public string mChangeDirTo;

		public int mRelaxUpdateBacklogCount;

		public int mMaxUpdateBacklog;

		public bool mPauseWhenMoving;

		public int mPreferredX;

		public int mPreferredY;

		public int mPreferredWidth;

		public int mPreferredHeight;

		public int mWidth;

		public int mHeight;

		public int mFullscreenBits;

		public double mMusicVolume;

		public double mSfxVolume;

		public double mDemoMusicVolume;

		public double mDemoSfxVolume;

		public bool mNoSoundNeeded;

		public bool mWantFMod;

		public bool mCmdLineParsed;

		public bool mSkipSignatureChecks;

		public bool mStandardWordWrap;

		public bool mbAllowExtendedChars;

		public bool mOnlyAllowOneCopyToRun;

		public uint mNotifyGameMessage;

		public CritSect mCritSect = default(CritSect);

		public CritSect mGetImageCritSect = default(CritSect);

		public int mBetaValidate;

		public byte[] mAdd8BitMaxTable = new byte[512];

		public WidgetManager mWidgetManager;

		public Dictionary<int, Dialog> mDialogMap = new Dictionary<int, Dialog>();

		public LinkedList<Dialog> mDialogList = new LinkedList<Dialog>();

		public uint mPrimaryThreadId;

		public bool mSEHOccured;

		public bool mShutdown;

		public bool mExitToTop;

		public bool mIsWindowed;

		public bool mIsPhysWindowed;

		public bool mFullScreenWindow;

		public bool mForceFullscreen;

		public bool mForceWindowed;

		public bool mInitialized;

		public bool mProcessInTimer;

		public int mTimeLoaded;

		public bool mIsScreenSaver;

		public bool mAllowMonitorPowersave;

		public bool mWantsDialogCompatibility;

		public bool mNoDefer;

		public bool mFullScreenPageFlip;

		public bool mTabletPC;

		public MusicInterface mMusicInterface;

		public bool mReadFromRegistry;

		public string mRegisterLink;

		public string mProductVersion;

		public Image[] mCursorImages = new Image[13];

		public bool mIsOpeningURL;

		public bool mShutdownOnURLOpen;

		public string mOpeningURL;

		public uint mOpeningURLTime;

		public uint mLastTimerTime;

		public uint mLastBigDelayTime;

		public double mUnmutedMusicVolume;

		public double mUnmutedSfxVolume;

		public int mMuteCount;

		public int mAutoMuteCount;

		public bool mDemoMute;

		public bool mMuteOnLostFocus;

		public List<MemoryImage> mMemoryImageSet = new List<MemoryImage>();

		public List<ImageFont> mImageFontSet = new List<ImageFont>();

		public List<PIEffect> mPIEffectSet = new List<PIEffect>();

		public List<PopAnim> mPopAnimSet = new List<PopAnim>();

		public Dictionary<KeyValuePair<string, string>, SharedImage> mSharedImageMap = new Dictionary<KeyValuePair<string, string>, SharedImage>();

		public CritSect mImageSetCritSect = default(CritSect);

		public bool mCleanupSharedImages;

		public int mNonDrawCount;

		public float mFrameTime;

		public bool mIsDrawing;

		public bool mLastDrawWasEmpty;

		public bool mHasPendingDraw;

		public double mPendingUpdatesAcc;

		public double mUpdateFTimeAcc;

		public int mLastTimeCheck;

		public int mLastTime;

		public int mLastUserInputTick;

		public int mSleepCount;

		public int mDrawCount;

		public int mUpdateCount;

		public int mUpdateAppState;

		public int mUpdateAppDepth;

		public int mMaxNonDrawCount;

		public double mUpdateMultiplier;

		public bool mPaused;

		public int mFastForwardToUpdateNum;

		public bool mFastForwardToMarker;

		public bool mFastForwardStep;

		public int mLastDrawTick;

		public int mNextDrawTick;

		public int mStepMode;

		public int mCursorNum;

		public SoundManager mSoundManager;

		public LinkedList<SexyAppBase.WidgetSafeDeleteInfo> mSafeDeleteList = new LinkedList<SexyAppBase.WidgetSafeDeleteInfo>();

		public bool mMouseIn;

		public bool mRunning;

		public bool mActive;

		public bool mMinimized;

		public bool mPhysMinimized;

		public bool mIsDisabled;

		public int mDrawTime;

		public int mFPSStartTick;

		public int mFPSFlipCount;

		public int mFPSDirtyCount;

		public int mFPSTime;

		public int mFPSCount;

		public bool mShowFPS;

		public int mShowFPSMode;

		public double mVFPSUpdateTimes;

		public int mVFPSUpdateCount;

		public double mVFPSDrawTimes;

		public int mVFPSDrawCount;

		public float mCurVFPS;

		public int mScreenBltTime;

		public bool mAutoStartLoadingThread;

		public bool mLoadingThreadStarted;

		public bool mLoadingThreadCompleted;

		public bool mLoaded;

		public bool mReloadingResources;

		public float mReloadPct;

		public string mReloadText;

		public string mReloadSubText;

		public bool mYieldMainThread;

		public bool mLoadingFailed;

		public bool mCursorThreadRunning;

		public bool mSysCursor;

		public bool mCustomCursorsEnabled;

		public bool mCustomCursorDirty;

		public bool mLastShutdownWasGraceful;

		public bool mIsWideWindow;

		public bool mWriteToSexyCache;

		public bool mSexyCacheBuffers;

		public bool mWriteFontCacheDir;

		public int mNumLoadingThreadTasks;

		public int mCompletedLoadingThreadTasks;

		public bool mDebugKeysEnabled;

		public bool mEnableMaximizeButton;

		public bool mCtrlDown;

		public bool mAltDown;

		public bool mAllowAltEnter;

		public int mSyncRefreshRate;

		public bool mVSyncUpdates;

		public bool mNoVSync;

		public bool mVSyncBroken;

		public int mVSyncBrokenCount;

		public long mVSyncBrokenTestStartTick;

		public long mVSyncBrokenTestUpdates;

		public bool mWaitForVSync;

		public bool mSoftVSyncWait;

		public bool mAutoEnable3D;

		public bool mTest3D;

		public bool mNoD3D9;

		public uint mMinVidMemory3D;

		public uint mRecommendedVidMemory3D;

		public bool mWidescreenAware;

		public bool mWidescreenTranslate;

		public Rect mScreenBounds = default(Rect);

		public bool mEnableWindowAspect;

		public bool mAllowWindowResize;

		public int mOrigScreenWidth;

		public int mOrigScreenHeight;

		public bool mIsSizeCursor;

		public bool mFirstLaunch;

		public bool mAppUpdated;

		public Dictionary<string, string> mStringProperties = new Dictionary<string, string>();

		public Dictionary<string, bool> mBoolProperties = new Dictionary<string, bool>();

		public Dictionary<string, int> mIntProperties = new Dictionary<string, int>();

		public Dictionary<string, double> mDoubleProperties = new Dictionary<string, double>();

		public Dictionary<string, List<string>> mStringVectorProperties = new Dictionary<string, List<string>>();

		public ResourceManager mResourceManager;

		public EShowCompatInfoMode mShowCompatInfoMode;

		public bool mShowWidgetInspector;

		public bool mWidgetInspectorPickMode;

		public bool mWidgetInspectorLeftAnchor;

		public WidgetContainer mWidgetInspectorPickWidget;

		public WidgetContainer mWidgetInspectorCurWidget;

		public int mWidgetInspectorScrollOffset;

		public FPoint mWidgetInspectorClickPos = new FPoint();

		public ResStreamsManager mResStreamsManager;

		public ProfileManager mProfileManager;

		public LeaderboardManager mLeaderboardManager;

		public bool mAllowSwapScreenImage;

		public static bool sAttemptingNonRecommended3D;

		public int mGamepadLocked;

		public bool mHasFocus;

		private List<KeyValuePair<string, string>> mRemoveList = new List<KeyValuePair<string, string>>();

		public class Touch
		{
			public void SetTouchInfo(SexyPoint loc, _TouchPhase _phase, double _timestamp)
			{
				this.previousLocation = this.location;
				this.location = loc;
				this.phase = _phase;
				this.timestamp = _timestamp / 1000.0;
			}

			public IntPtr ident;

			public IntPtr _event;

			public SexyPoint location = new SexyPoint();

			public SexyPoint previousLocation = new SexyPoint();

			public int tapCount;

			public double timestamp;

			public _TouchPhase phase;
		}

		public class WidgetSafeDeleteInfo
		{
			public int mUpdateAppDepth;

			public Widget mWidget;
		}
	}
}
