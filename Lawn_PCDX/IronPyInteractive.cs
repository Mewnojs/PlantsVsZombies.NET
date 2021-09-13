using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using WebSocketSharp;
using WebSocketSharp.Server;
using Sexy;

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
                    Send("Executed: " + msg?.ToString());
                }
                catch (Exception ex)
                {
                    Send("Error: " + ex?.Message);
                }
            }
        }

        public static void Serve()
        {
            mPyEnj = Python.CreateEngine();
            mPyScope = mPyEnj.CreateScope();
            mPyScope.SetVariable("P", GlobalStaticVars.gLawnApp);
            mWS = new WebSocketServer(8800);
            Console.WriteLine("WS server started.");
            mWS.AddWebSocketService<PyHub>("/Py");
            mWS.Start();
        }

        public static void Stop()
        {
            mWS.Stop();
        }

        private static ScriptEngine mPyEnj;
        private static ScriptScope mPyScope;
        private static WebSocketServer mWS;
    }
}
