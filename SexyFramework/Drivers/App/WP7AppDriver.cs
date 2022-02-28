using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Sexy.Drivers.Graphics;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Drivers.App
{
	public class WP7AppDriver : IAppDriver
	{
		public static WP7AppDriver CreateAppDriver(SexyAppBase App)
		{
			if (WP7AppDriver.sWP7AppDriverInstance == null)
			{
				WP7AppDriver.sWP7AppDriverInstance = new WP7AppDriver(App);
			}
			return WP7AppDriver.sWP7AppDriverInstance;
		}

		public WP7AppDriver(SexyAppBase appBase)
		{
			this.mApp = appBase;
		}

		public override void Dispose()
		{
			this.Shutdown();
		}

		public override bool InitAppDriver()
		{
			this.mApp.mNotifyGameMessage = 0U;
			this.mApp.mOnlyAllowOneCopyToRun = true;
			this.mApp.mNoDefer = false;
			this.mApp.mFullScreenPageFlip = true;
			this.mApp.mTimeLoaded = this.GetTickCount();
			this.mApp.mSEHOccured = false;
			this.mApp.mProdName = "Product";
			this.mApp.mShutdown = false;
			this.mApp.mExitToTop = false;
			this.mApp.mWidth = 1066;
			this.mApp.mHeight = 640;
			this.mApp.mFullscreenBits = 16;
			this.mApp.mIsWindowed = true;
			this.mApp.mIsPhysWindowed = true;
			this.mApp.mFullScreenWindow = false;
			this.mApp.mPreferredX = -1;
			this.mApp.mPreferredY = -1;
			this.mApp.mPreferredWidth = -1;
			this.mApp.mPreferredHeight = -1;
			this.mApp.mIsScreenSaver = false;
			this.mApp.mAllowMonitorPowersave = true;
			this.mApp.mWantsDialogCompatibility = false;
			this.mApp.mFrameTime = 10f;
			this.mApp.mNonDrawCount = 0;
			this.mApp.mDrawCount = 0;
			this.mApp.mSleepCount = 0;
			this.mApp.mUpdateCount = 0;
			this.mApp.mUpdateAppState = 0;
			this.mApp.mUpdateAppDepth = 0;
			this.mApp.mPendingUpdatesAcc = 0.0;
			this.mApp.mUpdateFTimeAcc = 0.0;
			this.mApp.mHasPendingDraw = true;
			this.mApp.mIsDrawing = false;
			this.mApp.mLastDrawWasEmpty = false;
			this.mApp.mLastTimeCheck = 0;
			this.mApp.mUpdateMultiplier = 1.0;
			this.mApp.mMaxNonDrawCount = 10;
			this.mApp.mPaused = false;
			this.mApp.mFastForwardToUpdateNum = 0;
			this.mApp.mFastForwardToMarker = false;
			this.mApp.mFastForwardStep = false;
			this.mApp.mCursorNum = 13;
			this.mApp.mMouseIn = false;
			this.mApp.mRunning = false;
			this.mApp.mActive = true;
			this.mApp.mProcessInTimer = false;
			this.mApp.mMinimized = false;
			this.mApp.mPhysMinimized = false;
			this.mApp.mIsDisabled = false;
			this.mApp.mLoaded = false;
			this.mApp.mReloadingResources = false;
			this.mApp.mReloadPct = 0f;
			this.mApp.mYieldMainThread = false;
			this.mApp.mLoadingFailed = false;
			this.mApp.mLoadingThreadStarted = false;
			this.mApp.mAutoStartLoadingThread = true;
			this.mApp.mLoadingThreadCompleted = false;
			this.mApp.mCursorThreadRunning = false;
			this.mApp.mNumLoadingThreadTasks = 0;
			this.mApp.mCompletedLoadingThreadTasks = 0;
			this.mApp.mLastDrawTick = this.timeGetTime();
			this.mApp.mNextDrawTick = this.timeGetTime();
			this.mApp.mSysCursor = true;
			this.mApp.mForceFullscreen = false;
			this.mApp.mForceWindowed = false;
			this.mApp.mHasFocus = true;
			this.mApp.mIsOpeningURL = false;
			this.mApp.mInitialized = false;
			this.mApp.mLastShutdownWasGraceful = true;
			this.mApp.mReadFromRegistry = false;
			this.mApp.mCmdLineParsed = false;
			this.mApp.mSkipSignatureChecks = false;
			this.mApp.mCtrlDown = false;
			this.mApp.mAltDown = false;
			this.mApp.mAllowAltEnter = true;
			this.mApp.mStepMode = 1;
			this.mApp.mCleanupSharedImages = false;
			this.mApp.mStandardWordWrap = true;
			this.mApp.mbAllowExtendedChars = true;
			this.mApp.mEnableMaximizeButton = false;
			this.mApp.mWriteToSexyCache = true;
			this.mApp.mSexyCacheBuffers = false;
			this.mApp.mWriteFontCacheDir = true;
			this.mApp.mMusicVolume = 0.85;
			this.mApp.mSfxVolume = 0.85;
			this.mApp.mMuteCount = 0;
			this.mApp.mAutoMuteCount = 0;
			this.mApp.mDemoMute = false;
			this.mApp.mMuteOnLostFocus = true;
			this.mApp.mFPSTime = 0;
			this.mApp.mFPSStartTick = this.GetTickCount();
			this.mApp.mFPSFlipCount = 0;
			this.mApp.mFPSCount = 0;
			this.mApp.mFPSDirtyCount = 0;
			this.mApp.mShowFPS = false;
			this.mApp.mShowFPSMode = 0;
			this.mApp.mVFPSUpdateTimes = 0.0;
			this.mApp.mVFPSUpdateCount = 0;
			this.mApp.mVFPSDrawTimes = 0.0;
			this.mApp.mVFPSDrawCount = 0;
			this.mApp.mCurVFPS = 0f;
			this.mApp.mDrawTime = 0;
			this.mApp.mScreenBltTime = 0;
			this.mApp.mDebugKeysEnabled = false;
			this.mApp.mNoSoundNeeded = false;
			this.mApp.mWantFMod = false;
			this.mApp.mSyncRefreshRate = 100;
			this.mApp.mVSyncUpdates = false;
			this.mApp.mNoVSync = true;
			this.mApp.mVSyncBroken = false;
			this.mApp.mVSyncBrokenCount = 0;
			this.mApp.mVSyncBrokenTestStartTick = 0L;
			this.mApp.mVSyncBrokenTestUpdates = 0L;
			this.mApp.mWaitForVSync = false;
			this.mApp.mSoftVSyncWait = true;
			this.mApp.mAutoEnable3D = false;
			this.mApp.mTest3D = false;
			this.mApp.mNoD3D9 = false;
			this.mApp.mMinVidMemory3D = 6U;
			this.mApp.mRecommendedVidMemory3D = 14U;
			this.mApp.mRelaxUpdateBacklogCount = 0;
			this.mApp.mWidescreenAware = false;
			this.mApp.mWidescreenTranslate = true;
			this.mApp.mEnableWindowAspect = false;
			this.mApp.mIsWideWindow = false;
			this.mApp.mOrigScreenWidth = 800;
			this.mApp.mOrigScreenHeight = 400;
			this.mApp.mIsSizeCursor = false;
			for (int i = 0; i < 13; i++)
			{
				this.mApp.mCursorImages[i] = null;
			}
			for (int i = 0; i < 256; i++)
			{
				this.mApp.mAdd8BitMaxTable[i] = (byte)i;
			}
			for (int i = 256; i < 512; i++)
			{
				this.mApp.mAdd8BitMaxTable[i] = byte.MaxValue;
			}
			this.mApp.mPrimaryThreadId = 0U;
			this.mApp.mShowWidgetInspector = false;
			this.mApp.mWidgetInspectorCurWidget = null;
			this.mApp.mWidgetInspectorScrollOffset = 0;
			this.mApp.mWidgetInspectorPickWidget = null;
			this.mApp.mWidgetInspectorPickMode = false;
			this.mApp.mWidgetInspectorLeftAnchor = false;
			GlobalMembers.gIs3D = true;
			return true;
		}

		public override void Start()
		{
			if (this.mApp.mShutdown)
			{
				return;
			}
			if (this.mApp.mAutoStartLoadingThread)
			{
				this.StartLoadingThread();
			}
		}

		public override void Init()
		{
			if (this.mApp.mShutdown)
			{
				return;
			}
			this.mApp.mFileDriver.InitFileDriver(this.mApp);
			this.mApp.mFileDriver.InitSaveDataFolder();
			this.mApp.mRandSeed = (uint)this.GetTickCount();
			this.mXNAGraphicsDriver.Init();
			this.mApp.mSoundManager = this.mApp.mAudioDriver.CreateSoundManager();
			this.mApp.mMusicInterface = new SoundEffectMusicInterface();
			this.mApp.SetMusicVolume(this.mApp.mMusicVolume);
			this.IsScreenSaver();
			this.mApp.mScreenBounds.mWidth = this.mApp.mWidth;
			this.mApp.mScreenBounds.mHeight = this.mApp.mHeight;
			this.mApp.mWidgetManager.Resize(this.mApp.mScreenBounds, this.mApp.mScreenBounds);
			this.mApp.mWidgetManager.mImage = null;
			this.mApp.mWidgetManager.MarkAllDirty();
			this.mApp.mInitialized = true;
		}

		public override bool UpdateAppStep(ref bool updated)
		{
			updated = false;
			if (this.mApp.mExitToTop)
			{
				return false;
			}
			this.mApp.mUpdateAppState = 1;
			this.mApp.mUpdateAppDepth++;
			if (this.mApp.mStepMode != 0)
			{
				this.DoUpdateFrames();
				this.DoUpdateFrames();
				this.DoUpdateFramesF(2f);
				updated = true;
			}
			else
			{
				int mUpdateCount = this.mApp.mUpdateCount;
				this.DoUpdateFrames();
				this.DoUpdateFrames();
				this.DoUpdateFramesF(2f);
				updated = this.mApp.mUpdateCount != mUpdateCount;
				updated = true;
			}
			this.mApp.mUpdateAppDepth--;
			this.mApp.ProcessSafeDeleteList();
			return true;
		}

		public override void ClearUpdateBacklog(bool relaxForASecond)
		{
			this.mApp.mLastTimeCheck = this.timeGetTime();
			this.mApp.mUpdateFTimeAcc = 0.0;
			if (relaxForASecond)
			{
				this.mApp.mRelaxUpdateBacklogCount = 1000;
			}
		}

		public override void Shutdown()
		{
			this.mWP7Game.Exit();
		}

		public override void DoExit(int theCode)
		{
		}

		public override void Remove3DData(MemoryImage theMemoryImage)
		{
		}

		public override void BeginPopup()
		{
		}

		public override void EndPopup()
		{
		}

		public override int MsgBox(string theText, string theTitle, int theFlags)
		{
			return 0;
		}

		public override void Popup(string theString)
		{
		}

		public override bool OpenURL(string theURL, bool shutdownOnOpen)
		{
			return true;
		}

		public override string GetGameSEHInfo()
		{
			return "";
		}

		public override void SEHOccured()
		{
		}

		public override void GetSEHWebParams(DefinesMap theDefinesMap)
		{
		}

		public override void DoParseCmdLine()
		{
		}

		public override void ParseCmdLine(string theCmdLine)
		{
		}

		public override void HandleCmdLineParam(string theParamName, string theParamValue)
		{
		}

		public override void StartLoadingThread()
		{
		}

		public override double GetLoadingThreadProgress()
		{
			return 1.0;
		}

		public override void CopyToClipboard(string theString)
		{
		}

		public override string GetClipboard()
		{
			return "";
		}

		public override void SetCursor(int theCursorNum)
		{
		}

		public override int GetCursor()
		{
			return 0;
		}

		public override void EnableCustomCursors(bool enabled)
		{
		}

		public override void SetCursorImage(int theCursorNum, Image theImage)
		{
		}

		public override void SwitchScreenMode()
		{
		}

		public override void SwitchScreenMode(bool wantWindowed)
		{
		}

		public override void SwitchScreenMode(bool wantWindowed, bool is3d, bool force)
		{
		}

		public override bool KeyDown(int theKey)
		{
			return false;
		}

		public override bool DebugKeyDown(int theKey)
		{
			return false;
		}

		public override bool DebugKeyDownAsync(int theKey, bool ctrlDown, bool altDown)
		{
			return false;
		}

		public override bool Is3DAccelerated()
		{
			return true;
		}

		public override bool Is3DAccelerationSupported()
		{
			return true;
		}

		public override bool Is3DAccelerationRecommended()
		{
			return true;
		}

		public override void Set3DAcclerated(bool is3D, bool reinit)
		{
		}

		public override bool IsUIOrientationAllowed(UI_ORIENTATION theOrientation)
		{
			return false;
		}

		public override UI_ORIENTATION GetUIOrientation()
		{
			return UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_RIGHT;
		}

		public override bool IsSystemUIShowing()
		{
			return false;
		}

		public override void ShowKeyboard()
		{
		}

		public override void HideKeyboard()
		{
		}

		public override bool CheckSignature(SexyBuffer theBuffer, string theFileName)
		{
			return true;
		}

		public override bool ReloadAllResources()
		{
			return true;
		}

		public override bool ConfigGetSubKeys(string theKeyName, List<string> theSubKeys)
		{
			return true;
		}

		public override bool ConfigReadString(string theValueName, ref string theString)
		{
			return true;
		}

		public override bool ConfigReadInteger(string theValueName, ref int theValue)
		{
			return true;
		}

		public override bool ConfigReadBoolean(string theValueName, ref bool theValue)
		{
			return true;
		}

		public override bool ConfigReadData(string theValueName, ref byte[] theValue, ref ulong theLength)
		{
			return true;
		}

		public override bool ConfigWriteString(string theValueName, string theString)
		{
			return true;
		}

		public override bool ConfigWriteInteger(string theValueName, int theValue)
		{
			return true;
		}

		public override bool ConfigWriteBoolean(string theValueName, bool theValue)
		{
			return true;
		}

		public override bool ConfigWriteData(string theValueName, byte[] theValue, ulong theLength)
		{
			return true;
		}

		public override bool ConfigEraseKey(string theKeyName)
		{
			return true;
		}

		public override void ConfigEraseValue(string theValueName)
		{
		}

		public override void ReadFromConfig()
		{
		}

		public override void WriteToConfig()
		{
		}

		public override bool WriteBufferToFile(string theFileName, SexyBuffer theBuffer)
		{
			return true;
		}

		public override bool ReadBufferFromFile(string theFileName, SexyBuffer theBuffer, bool dontWriteToDemo)
		{
			return true;
		}

		public override bool WriteBytesToFile(string theFileName, object theData, ulong theDataLen)
		{
			return true;
		}

		public override DeviceImage GetOptimizedImage(string theFileName, bool commitBits, bool allowTriReps)
		{
			return ((XNAGraphicsDriver)this.mApp.mGraphicsDriver).GetOptimizedImage(theFileName, commitBits, allowTriReps);
		}

		public override DeviceImage GetOptimizedImage(Stream stream, bool commitBits, bool allowTriReps)
		{
			return ((XNAGraphicsDriver)this.mApp.mGraphicsDriver).GetOptimizedImage(stream, commitBits, allowTriReps);
		}

		public override DeviceImage GetOptimizedImageFromData(string theFileName, bool commitBits, bool allowTriReps, int width, int height)
		{
			return ((XNAGraphicsDriver)this.mApp.mGraphicsDriver).GetOptimizedImageFromData(theFileName, commitBits, allowTriReps, width, height);
		}

		public override object GetOptimizedRenderData(string theFileName)
		{
			return ((XNAGraphicsDriver)this.mApp.mGraphicsDriver).GetOptimizedRenderData(theFileName);
		}

		public override bool ShouldPauseUpdates()
		{
			return false;
		}

		public override void Draw()
		{
			this.mXNAGraphicsDriver.ClearColorBuffer(SexyColor.Black);
			this.DrawDirtyStuff();
		}

		public override int GetAppTime()
		{
			return (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
		}

		public override Localization.LanguageType GetAppLanguage()
		{
			Localization.LanguageType result = Localization.LanguageType.Language_EN;
			string twoLetterISOLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			string name = CultureInfo.CurrentCulture.Name;
			bool flag = false;
			string[] array = new string[] { "es-CO", "pt-BR", "zh-TW" };
			Localization.LanguageType[] array2 = new Localization.LanguageType[]
			{
				Localization.LanguageType.Language_SPC,
				Localization.LanguageType.Language_PGB,
				Localization.LanguageType.Language_CHT
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (name.Equals(array[i]))
				{
					result = array2[i];
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				string[] array3 = new string[] { "en", "fr", "it", "de", "es", "zh", "ru", "pl", "pt" };
				for (int j = 0; j < array3.Length; j++)
				{
					if (twoLetterISOLanguageName.Equals(array3[j]))
					{
						result = (Localization.LanguageType)j;
						break;
					}
				}
			}
			return result;
		}

		public void InitXNADriver(Game game)
		{
			this.mWP7Game = game;
			this.mXNAGraphicsDriver = new XNAGraphicsDriver(this.mWP7Game, this.mApp);
			this.mContentManager = this.mWP7Game.Content;
			this.mGameTime = new GameTime();
			this.mApp.mGraphicsDriver = this.mXNAGraphicsDriver;
		}

		public int timeGetTime()
		{
			if (this.mGameTime != null)
			{
				return this.mGameTime.TotalGameTime.Milliseconds;
			}
			return 0;
		}

		public int GetTickCount()
		{
			return 0;
		}

		public bool IsScreenSaver()
		{
			return this.mApp.mIsScreenSaver;
		}

		public bool AppCanRestore()
		{
			return !this.mApp.mIsDisabled;
		}

		public void ReloadAllResources_DrawStateUpdate(string theHeader, string theSubText, float thePct)
		{
			MemoryImage mImage = this.mApp.mWidgetManager.mImage;
		}

		public void ReloadAllResourcesProc()
		{
			this.mApp.mReloadingResources = false;
		}

		public void ReloadAllResourcesProcStub(IntPtr theArg)
		{
		}

		public string GetProductVersion(string thePath)
		{
			string fullName = Assembly.GetCallingAssembly().FullName;
			return "v" + fullName.Split(new char[] { '=' })[1].Split(new char[] { ',' })[0];
		}

		private bool Process()
		{
			if (this.mApp.mLoadingFailed)
			{
				this.mApp.Shutdown();
			}
			bool flag = this.mApp.mVSyncUpdates && !this.mApp.mLastDrawWasEmpty && !this.mApp.mVSyncBroken && (!this.mApp.mIsPhysWindowed || (this.mApp.mIsPhysWindowed && this.mApp.mWaitForVSync && !this.mApp.mSoftVSyncWait));
			double num;
			double num2;
			if (this.mApp.mVSyncUpdates)
			{
				num = 1000.0 / (double)this.mApp.mSyncRefreshRate / this.mApp.mUpdateMultiplier;
				num2 = (double)((float)(1000.0 / (double)(this.mApp.mFrameTime * (float)this.mApp.mSyncRefreshRate)));
			}
			else
			{
				num = (double)this.mApp.mFrameTime / this.mApp.mUpdateMultiplier;
				num2 = 1.0;
			}
			if (!this.mApp.mPaused && this.mApp.mUpdateMultiplier > 0.0)
			{
				int num3 = this.timeGetTime();
				int num4 = 0;
				if (!flag)
				{
					this.UpdateFTimeAcc();
				}
				bool flag2 = false;
				if (this.mApp.mUpdateAppState == 1)
				{
					if (++this.mApp.mNonDrawCount < (int)Math.Ceiling((double)this.mApp.mMaxNonDrawCount * this.mApp.mUpdateMultiplier) || !this.mApp.mLoaded)
					{
						bool flag3 = true;
						if (flag3)
						{
							if (this.mApp.mUpdateMultiplier == 1.0)
							{
								this.mApp.mVSyncBrokenTestUpdates += 1L;
								if ((float)this.mApp.mVSyncBrokenTestUpdates >= (1000f + this.mApp.mFrameTime - 1f) / this.mApp.mFrameTime)
								{
									if ((long)num3 - this.mApp.mVSyncBrokenTestStartTick <= 800L)
									{
										this.mApp.mVSyncBrokenCount++;
										if (this.mApp.mVSyncBrokenCount >= 3)
										{
											this.mApp.mVSyncBroken = true;
										}
									}
									else
									{
										this.mApp.mVSyncBrokenCount = 0;
									}
									this.mApp.mVSyncBrokenTestStartTick = (long)num3;
									this.mApp.mVSyncBrokenTestUpdates = 0L;
								}
							}
							bool flag4 = this.DoUpdateFrames();
							if (flag4)
							{
								this.mApp.mUpdateAppState = 2;
							}
							this.mApp.mHasPendingDraw = true;
							flag2 = true;
						}
					}
				}
				else if (this.mApp.mUpdateAppState == 2)
				{
					this.mApp.mUpdateAppState = 3;
					this.mApp.mPendingUpdatesAcc += num2;
					this.mApp.mPendingUpdatesAcc -= 1.0;
					while (this.mApp.mPendingUpdatesAcc >= 1.0)
					{
						this.mApp.mNonDrawCount++;
						bool flag5 = this.DoUpdateFrames();
						if (!flag5)
						{
							break;
						}
						this.mApp.mPendingUpdatesAcc -= 1.0;
					}
					this.DoUpdateFramesF((float)num2);
					if (flag)
					{
						this.mApp.mUpdateFTimeAcc = Math.Max(this.mApp.mUpdateFTimeAcc - num - 0.20000000298023224, 0.0);
					}
					else
					{
						this.mApp.mUpdateFTimeAcc -= num;
					}
					if (this.mApp.mRelaxUpdateBacklogCount > 0)
					{
						this.mApp.mUpdateFTimeAcc = 0.0;
					}
					flag2 = true;
				}
				if (!flag2)
				{
					this.mApp.mUpdateAppState = 3;
					this.mApp.mNonDrawCount = 0;
					if (this.mApp.mHasPendingDraw)
					{
						this.DrawDirtyStuff();
					}
					else
					{
						int num5 = (int)num - (int)this.mApp.mUpdateFTimeAcc;
						if (num5 > 0)
						{
							this.mApp.mSleepCount++;
							Thread.Sleep(num5);
							num4 += num5;
						}
					}
				}
				if (this.mApp.mYieldMainThread)
				{
					int num6 = this.timeGetTime();
					int num7 = num6 - num3 - num4;
					int num8 = Math.Min(250, num7 * 2 - num4);
					if (num8 >= 0)
					{
						Thread.Sleep(num8);
					}
				}
			}
			return true;
		}

		private void UpdateFTimeAcc()
		{
			int num = this.timeGetTime();
			if (this.mApp.mLastTimeCheck != 0)
			{
				int num2 = num - this.mApp.mLastTimeCheck;
				this.mApp.mUpdateFTimeAcc = Math.Min(this.mApp.mUpdateFTimeAcc + (double)num2, (double)((float)this.mApp.mMaxUpdateBacklog));
				if (this.mApp.mRelaxUpdateBacklogCount > 0)
				{
					this.mApp.mRelaxUpdateBacklogCount = Math.Max(this.mApp.mRelaxUpdateBacklogCount - num2, 0);
				}
			}
			this.mApp.mLastTimeCheck = num;
		}

		private void ReDraw()
		{
			this.mXNAGraphicsDriver.Redraw(Rect.ZERO_RECT);
		}

		private bool DrawDirtyStuff()
		{
			int num = this.timeGetTime();
			this.mApp.mIsDrawing = true;
			bool flag = this.mApp.mWidgetManager.DrawScreen();
			this.mApp.mIsDrawing = false;
			if ((flag || num - this.mApp.mLastDrawTick >= 1000 || this.mApp.mCustomCursorDirty) && num - this.mApp.mNextDrawTick >= 0)
			{
				this.mApp.mLastDrawWasEmpty = false;
				this.mApp.mDrawCount++;
				int num2 = this.timeGetTime();
				this.mApp.mFPSCount++;
				this.mApp.mFPSTime += num2 - num;
				this.mApp.mDrawTime += num2 - num;
				int num3 = this.timeGetTime();
				this.mApp.mLastDrawTick = num3;
				this.ReDraw();
				int num4 = this.timeGetTime();
				this.mApp.mScreenBltTime = num4 - num3;
				if (this.mApp.mLoadingThreadStarted && !this.mApp.mLoadingThreadCompleted)
				{
					int num5 = num4 - num;
					this.mApp.mNextDrawTick += 35 + Math.Max(num5, 15);
					if (num4 - this.mApp.mNextDrawTick >= 0)
					{
						this.mApp.mNextDrawTick = num4;
					}
				}
				else
				{
					this.mApp.mNextDrawTick = num4;
				}
				this.mApp.mHasPendingDraw = false;
				this.mApp.mCustomCursorDirty = false;
				return true;
			}
			this.mApp.mHasPendingDraw = false;
			this.mApp.mLastDrawWasEmpty = true;
			return false;
		}

		private void DoUpdateFramesF(float theFrac)
		{
			if (this.mApp.mVSyncUpdates && !this.mApp.mMinimized)
			{
				this.mApp.mWidgetManager.UpdateFrameF(theFrac);
			}
		}

		private bool DoUpdateFrames()
		{
			if (this.mApp.mLoadingThreadCompleted && !this.mApp.mLoaded)
			{
				this.mApp.mLoaded = true;
				this.mApp.mYieldMainThread = false;
				this.mApp.LoadingThreadCompleted();
			}
			this.mApp.UpdateFrames();
			return true;
		}

		public void SetOrientation(int Orientation)
		{
			this.mXNAGraphicsDriver.SetOrientation(Orientation);
		}

		public static WP7AppDriver sWP7AppDriverInstance;

		private SexyAppBase mApp;

		private Game mWP7Game;

		private GameTime mGameTime;

		public ContentManager mContentManager;

		private XNAGraphicsDriver mXNAGraphicsDriver;
	}
}
