using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Tencent.Bugly.Crashreport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sexy;
using Throwable = Java.Lang.Throwable;
using Android.Content.Res;
using System.Reflection;

namespace Lawn_Android
{
    [Application]
    public partial class AndroidApplication : Application
    {
        //private const string BuglyId = "Your AppId on Bugly";

        public AndroidApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
#if DEBUG
            CrashReport.SetIsDevelopmentDevice(Context, true);
#endif
            static void loggerBugly(string s, DebugType msgtype) 
            {
                switch (msgtype)
                {
                case DebugType.Debug:
                    break;
                case DebugType.Info:
                    BuglyLog.I("Info", s);
                    break;
                case DebugType.Warn:
                    BuglyLog.W("Warn", s);
                    break;
                case DebugType.Error:
                    BuglyLog.E("Error", s);
                    break;
                case DebugType.Fatal:
                    BuglyLog.E("Fatal", s);
                    break;
                }
            }
            string buglyId = (string)(this.GetType().GetField("BuglyId", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(this) ?? "");
            CrashReport.InitCrashReport(ApplicationContext, buglyId, true);
            Sexy.Debug.Logger = loggerBugly;
            PvZActivity.ExceptionReporter = (Exception e) =>
            {
                CrashReport.PostCatchedException(Throwable.FromException(e));
            };
        }
    }
}