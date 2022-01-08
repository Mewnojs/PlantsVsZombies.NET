using System;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Sexy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using WebSocketSharp.NetCore.Server;
using WebSocketSharp.NetCore;
using static IronPyInteractiveDef_Shared.WSEvents;

namespace Lawn
{
    class IronPyInteractive
    {
        public class PyHub : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                ScriptSource src = mPyEnj.CreateScriptSourceFromString(e.Data);
                object msg = null;
                try
                {
                    msg = src.Execute<object>(mPyScope);
                }
                catch (Exception ex)
                {
                    SendAsync(ExecutionEventJSON(ExecutionEventResult.error, ex.Message, ex.GetType().ToString()), x => { });
                    return;
                }
                dynamic repr = mPyEnj.Runtime.GetBuiltinModule().GetVariable("repr");
                SendAsync(ExecutionEventJSON(ExecutionEventResult.executed, (msg != null) ? repr(msg) : null), new Action<bool>(delegate { }));// 
            }

            protected override void OnOpen()
            {
                mStdoutWriter.FlushEvent += OnWriterFlush;
                mStderrWriter.FlushEvent += OnWriterFlush;
            }

            private void OnWriterFlush(VirtualWriter sender)
            {
                SendAsync(OutputEventJSON(sender.name, sender.ToString()), x => { });

            }

            protected override void OnClose(CloseEventArgs e)
            {
                mStdoutWriter.FlushEvent -= OnWriterFlush;
                mStderrWriter.FlushEvent -= OnWriterFlush;
            }

            public string ExecutionEventJSON(ExecutionEventResult restype, object res, string exceptionType = null)
            {
                return JsonConvert.SerializeObject(JObject.FromObject(new ExecutionEvent()
                {
                    statuscode = restype,
                    result = res,
                    errortype = exceptionType,
                    timestamp = DateTime.UtcNow.ToFileTime()
                }, mSerializer));
            }

            public string OutputEventJSON(string bufferName, string msg)
            {
                return JsonConvert.SerializeObject(JObject.FromObject(
                    new OutputEvent() {
                        name = bufferName, 
                        msg = msg, 
                        timestamp = DateTime.UtcNow.ToFileTime() 
                    })
                );
            }

            public static void Initialize()
            {
                mPyEnj.SetSearchPaths(new List<string>());
                static object DebugExec(string code)
                {
                    Debug.OutputDebug($"[Python]{code}");
                    return mPyEnj.Execute(code);
                }
                //mPyScope.SetVariable("P", GlobalStaticVars.gLawnApp);
                DebugExec($"__import__('clr').AddReference('{Assembly.GetExecutingAssembly().GetName().Name}')");
                mPyEnj.Runtime.IO.SetOutput(mStdoutStream, mStdoutWriter);
                mPyEnj.Runtime.IO.SetErrorOutput(mStderrStream, mStderrWriter);
                ConfigureStdlib();
            }

            private static void ConfigureStdlib()
            {
                string libPath = Main.FetchIronPythonStdLib(mPyEnj.LanguageVersion);
                var searchPaths = mPyEnj.GetSearchPaths();
                searchPaths.Add(libPath);
                mPyEnj.SetSearchPaths(searchPaths);
            }

            private static readonly ScriptEngine mPyEnj = Python.CreateEngine();
            private static readonly ScriptScope mPyScope = mPyEnj.CreateScope();
            private static readonly MemoryStream mStdoutStream = new MemoryStream();
            private static readonly MemoryStream mStderrStream = new MemoryStream();
            private static readonly VirtualWriter mStdoutWriter = new VirtualWriter("stdout");
            private static readonly VirtualWriter mStderrWriter = new VirtualWriter("stderr");

            internal class VirtualWriter : StringWriter
            {
                public VirtualWriter(string Name) : base()
                {
                    name = Name;
                    return;
                }

                public override void Flush()
                {
                    base.Flush();
                    FlushEvent?.Invoke(this);
                    GetStringBuilder().Clear();
                }

                public delegate void FlushEventHandler(VirtualWriter sender);
                public event FlushEventHandler FlushEvent;
                public string name;
            }
        }




        public static void Serve()
        {
            int port = 8080;
            mWS = new WebSocketServer(IPAddress.Any, port);
            Console.WriteLine($"WS server started at: Port {port}");
            mWS.AddWebSocketService<PyHub>("/Py");
            PyHub.Initialize();
            mWS.Start();

        }

        public static void Stop()
        {
            //mWS.Stop();
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>


        private static WebSocketServer mWS;
        private static readonly JsonSerializer mSerializer = new JsonSerializer()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };
    }
}
