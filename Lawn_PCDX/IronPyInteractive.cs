using System;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using WebSocketSharp;
using WebSocketSharp.Server;
using Sexy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Lawn
{
    class IronPyInteractive
    {
        public class PyHub : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                ScriptSource src = mPyEnj.CreateScriptSourceFromString(e.Data);

                try
                {
                    object msg = src.Execute<object>(mPyScope);
                    Send(ExecutionEventJSON(ExecutionEventResult.executed, msg));
                }
                catch (Exception ex)
                {
                    Send(ExecutionEventJSON(ExecutionEventResult.error, ex/*ex?.Message*/));
                }
            }

            protected override void OnOpen() 
            {
                mPyEnj = Python.CreateEngine();
                mPyScope = mPyEnj.CreateScope();
                mPyScope.SetVariable("P", GlobalStaticVars.gLawnApp);
                mStdoutWriter = new VirtualWriter();
                mStderrWriter = new VirtualWriter();
                mStdoutWriter.FlushEvent += OnWriterFlush;
                mStderrWriter.FlushEvent += OnWriterFlush;
                mPyEnj.Runtime.IO.SetOutput(mStdoutStream, mStdoutWriter);
                mPyEnj.Runtime.IO.SetErrorOutput(mStderrStream, mStderrWriter);
                
            }

            private void OnWriterFlush(VirtualWriter sender) 
            {
                Send(OutputEventJSON(sender.name, sender.ToString()));
                sender.GetStringBuilder().Clear();
            }

            protected override void OnClose(CloseEventArgs e)
            {
                mStdoutWriter = null;
                mStderrWriter = null;
            }

            public string ExecutionEventJSON(ExecutionEventResult restype, object res)
            {
                return JsonConvert.SerializeObject(JObject.FromObject(new
                {
                    type = "execution", statuscode = restype, result = res
                }));
            }

            public string OutputEventJSON(string bufferName, string msg)
            {
                return JsonConvert.SerializeObject(
                    new Dictionary<string, object>
                        { {"type", "output" }, {"name", bufferName}, {"msg", msg } }
                );
            }

            public enum ExecutionEventResult 
            {
                error = -1,
                executed = 0
            }

            private static ScriptEngine mPyEnj;
            private static ScriptScope mPyScope;
            private static MemoryStream mStdoutStream = new MemoryStream();
            private static MemoryStream mStderrStream = new MemoryStream();
            private static VirtualWriter mStdoutWriter;
            private static VirtualWriter mStderrWriter;

            internal class VirtualWriter : StringWriter 
            {
                public override void Flush()
                {
                    base.Flush();
                    FlushEvent.Invoke(this);
                }

                public delegate void FlushEventHandler(VirtualWriter sender);
                public event FlushEventHandler FlushEvent;
                public string name;
            }
        }

        public static void Serve()
        {
            mWS = new WebSocketServer(8800);
            Console.WriteLine("WS server started.");
            mWS.AddWebSocketService<PyHub>("/Py");
            mWS.Start();
        }

        public static void Stop()
        {
            mWS.Stop();
        }

        private static WebSocketServer mWS;
    }
}
