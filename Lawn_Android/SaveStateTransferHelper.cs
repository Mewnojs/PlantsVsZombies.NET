using Android.App;
using Android.Content;
using Sexy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lawn_Android
{
    internal static class SaveStateTransferHelper
    {
        private static bool DetectIsolatedStorageRemnants()
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            if (isolatedStorageFile.DirectoryExists(GlobalStaticVars.GetDocumentsDir()))
                return true;
            return false;
        }

        private static readonly string _pathToIsolatedStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".config/.isolated-storage/");

        private static bool DetectIsolatedStorageRemnantsNetStandard()
        {
            if (Directory.Exists(Path.Combine(_pathToIsolatedStorage, GlobalStaticVars.GetDocumentsDir())))
                return true;
            return false;
        }

        public static OldSaveStateType DetectOldSaveStates() =>
            (DetectIsolatedStorageRemnants() ? OldSaveStateType.Isolated : OldSaveStateType.None)
          | (DetectIsolatedStorageRemnantsNetStandard() ? OldSaveStateType.IsolatedNetStandard : OldSaveStateType.None);

        public static void OnTransfer(PvZActivity activity, OldSaveStateType type)
        {
            List<Tuple<string, OldSaveStateType>> saveStateLocations = new();
            if ((type & OldSaveStateType.Isolated) == OldSaveStateType.Isolated)
            {
                IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
                saveStateLocations.Add(new Tuple<string, OldSaveStateType>(Path.Combine(isolatedStorageFile.GetType().GetProperty("RootDirectory", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(isolatedStorageFile).ToString(), GlobalStaticVars.GetDocumentsDir()), OldSaveStateType.Isolated));
            }
            if ((type & OldSaveStateType.IsolatedNetStandard) == OldSaveStateType.IsolatedNetStandard)
            {
                saveStateLocations.Add(new Tuple<string, OldSaveStateType>(Path.Combine(_pathToIsolatedStorage, GlobalStaticVars.GetDocumentsDir()), OldSaveStateType.IsolatedNetStandard));
            }
            var transferCompleted = new List<bool>();
            while (transferCompleted.Count == 0)
            {
                var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                activity.RunOnUiThread(() =>
                {
                    var aDialog = new AlertDialog.Builder(activity);
                    aDialog.SetTitle("检测到旧存档！");
                    var msg = $"检测到以下位置的旧存档，是否要覆盖新存档？\n{(saveStateLocations.Count >= 1 ? "存档一: " + saveStateLocations[0].Item1 : "")}\n{(saveStateLocations.Count >= 2 ? "存档二: " + saveStateLocations[1].Item1 : "")}\n注意：选择一项存档后，选择的存档会覆盖到新位置，其他存档将被清空！\n若不选择，则旧存档均会清空！";
                    aDialog.SetMessage(msg);
                    aDialog.SetCancelable(false);
                    aDialog.SetNegativeButton("使用存档一", (object sender, DialogClickEventArgs e) =>
                    {
                        DoConfirmTranferDialog(activity, type, saveStateLocations[0].Item2, 0, transferCompleted, waitHandle);
                    });
                    if (saveStateLocations.Count >= 2)
                    {
                        aDialog.SetPositiveButton("使用存档二", (object sender, DialogClickEventArgs e) =>
                        {
                            DoConfirmTranferDialog(activity, type, saveStateLocations[1].Item2, 1, transferCompleted, waitHandle);
                        });
                    }
                    aDialog.SetNeutralButton("删除以上所有", (object sender, DialogClickEventArgs e) =>
                    {
                        DoConfirmTranferDialog(activity, type, OldSaveStateType.None, 2, transferCompleted, waitHandle);
                    });

                    aDialog.Show();
                });
                waitHandle.WaitOne();
            }
            activity.RunOnUiThread(() =>
            {
                var finishedDialog = new AlertDialog.Builder(activity);
                finishedDialog.SetTitle("迁移已完成");
                finishedDialog.SetMessage("存档迁移完成，重新进入游戏即可继续游玩");
                finishedDialog.SetPositiveButton("好", (o, e) => { Environment.Exit(0); });
                finishedDialog.Show();
            });

            void DoConfirmTranferDialog(PvZActivity activity, OldSaveStateType type, OldSaveStateType selection, int selectedNumber, List<bool> result, EventWaitHandle waitHandle)
            {
                string[] operation = new string[3] { "使用存档一覆盖新存档", "使用存档二覆盖新存档", "使用新存档，删除所有旧存档" };
                var aDialog = new AlertDialog.Builder(activity);
                aDialog.SetTitle("确认操作");
                aDialog.SetMessage($"你选择了 {operation[selectedNumber]} \n未选择的存档将会永久删除(消失)！\n确定要这么做吗？");
                aDialog.SetCancelable(false);
                aDialog.SetNegativeButton("取消", (object sender, DialogClickEventArgs e) => { waitHandle.Set(); });
                aDialog.SetPositiveButton("确定", (object sender, DialogClickEventArgs e) =>
                {
                    var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
                    var docDir = GlobalStaticVars.GetDocumentsDir();
                    if (selection == OldSaveStateType.None)
                    {
                        if (isoStore.DirectoryExists(docDir))
                            DeleteIsolatedStorageDirRecursively(docDir, isoStore);
                        if (Directory.Exists(Path.Combine(_pathToIsolatedStorage, docDir)))
                            Directory.Delete(Path.Combine(_pathToIsolatedStorage, docDir), true);
                    }
                    else if (selection == OldSaveStateType.Isolated)
                    {
                        string destDir = Path.Combine(GlobalStaticVars.gSexyAppBase.applicationStoragePath, docDir);
                        if (Directory.Exists(destDir))
                            Directory.Delete(destDir, true);

                        CopyIsolatedStorageDirToDirRecursively(docDir, isoStore, destDir);
                        DeleteIsolatedStorageDirRecursively(docDir, isoStore);

                        if (Directory.Exists(Path.Combine(_pathToIsolatedStorage, docDir)))
                            Directory.Delete(Path.Combine(_pathToIsolatedStorage, docDir), true);
                    }
                    else if (selection == OldSaveStateType.IsolatedNetStandard)
                    {
                        string destDir = Path.Combine(GlobalStaticVars.gSexyAppBase.applicationStoragePath, docDir);
                        if (Directory.Exists(destDir))
                            Directory.Delete(destDir, true);

                        CopyPlainDirToDirRecursively(Path.Combine(_pathToIsolatedStorage, docDir), destDir);

                        if (isoStore.DirectoryExists(docDir))
                            DeleteIsolatedStorageDirRecursively(docDir, isoStore);

                        if (Directory.Exists(Path.Combine(_pathToIsolatedStorage, docDir)))
                            Directory.Delete(Path.Combine(_pathToIsolatedStorage, docDir), true);
                    }
                    result.Add(true);
                    waitHandle.Set();
                });
                aDialog.Show();
            }

            void DeleteIsolatedStorageDirRecursively(string path, IsolatedStorageFile isoStore)
            {
                foreach (var file in isoStore.GetFileNames(Path.Combine(path, "*")))
                {
                    var fullpath = Path.Combine(path, file);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: deleting file {fullpath} from IsoStore");
                    isoStore.DeleteFile(fullpath);
                }
                foreach (var dir in isoStore.GetDirectoryNames(Path.Combine(path, "*")))
                {
                    var fullpath = Path.Combine(path, dir);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: deleting directory {fullpath} from IsoStore");
                    DeleteIsolatedStorageDirRecursively(fullpath, isoStore);
                }
                isoStore.DeleteDirectory(path);
            }

            void CopyIsolatedStorageDirToDirRecursively(string sourcePath, IsolatedStorageFile sourceIsoStore, string destPath)
            {
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                foreach (string file in sourceIsoStore.GetFileNames(Path.Combine(sourcePath, "*")))
                {
                    var fullpathSource = Path.Combine(sourcePath, file);
                    var fullpathDest = Path.Combine(destPath, file);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: copying file from {fullpathSource} to {fullpathDest}");
                    var fs = sourceIsoStore.OpenFile(fullpathSource, FileMode.Open);
                    var buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    var destFs = File.OpenWrite(fullpathDest);
                    destFs.Write(buffer, 0, buffer.Length);
                    destFs.Close();
                }
                foreach (string directory in sourceIsoStore.GetDirectoryNames(Path.Combine(sourcePath, "*")))
                {
                    var fullpathSource = Path.Combine(sourcePath, directory);
                    var fullpathDest = Path.Combine(destPath, directory);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: copying file from {fullpathSource} to {fullpathDest}");
                    CopyIsolatedStorageDirToDirRecursively(fullpathSource, sourceIsoStore, fullpathDest);
                }
            }

            void CopyPlainDirToDirRecursively(string sourcePath, string destPath)
            {
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    var fileStripped = Path.GetFileName(file);
                    var fullpathSource = Path.Combine(sourcePath, fileStripped);
                    var fullpathDest = Path.Combine(destPath, fileStripped);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: copying file from {fullpathSource} to {fullpathDest}");
                    var fs = File.OpenRead(fullpathSource);
                    var buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    var destFs = File.OpenWrite(fullpathDest);
                    destFs.Write(buffer, 0, buffer.Length);
                    destFs.Close();
                }
                foreach (string directory in Directory.GetDirectories(sourcePath))
                {
                    var dirStripped = Path.GetFileName(directory);
                    var fullpathSource = Path.Combine(sourcePath, dirStripped);
                    var fullpathDest = Path.Combine(destPath, dirStripped);
                    Debug.Log(DebugType.Info, $"SaveStateTransfer: copying file from {fullpathSource} to {fullpathDest}");
                    CopyPlainDirToDirRecursively(fullpathSource, fullpathDest);
                }
            }
        }

        public enum OldSaveStateType
        {
            None = 0x0,
            Isolated = 0x1,
            IsolatedNetStandard = 0x2,
        }
    }
}
