﻿using System;
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
using Microsoft.Scripting.Utils;
using System.Text;
using System.Net.Sockets;

namespace LawnMod
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

            public static string ExecutionEventJSON(ExecutionEventResult restype, object res, string exceptionType = null)
            {
                return JsonConvert.SerializeObject(JObject.FromObject(new ExecutionEvent()
                {
                    statuscode = restype,
                    result = res,
                    errortype = exceptionType,
                    timestamp = DateTime.UtcNow.ToFileTime()
                }, mSerializer));
            }

            public static string OutputEventJSON(string bufferName, string msg)
            {
                return JsonConvert.SerializeObject(JObject.FromObject(
                    new OutputEvent() {
                        name = bufferName, 
                        msg = msg, 
                        timestamp = DateTime.UtcNow.ToFileTime() 
                    })
                );
            }

            public static void Initialize(bool runModules = true)
            {
                Preinitialize();
                ConfigureWorkDir();  
                ConfigureStdlib();
                //mPyEnj.SetSearchPaths(new List<string>());
                static object DebugExec(string code)
                {
                    Debug.OutputDebug($"[Python]{code}");
                    return mPyEnj.Execute(code);
                }
                //mPyScope.SetVariable("P", GlobalStaticVars.gLawnApp);
                DebugExec($"__import__('clr').AddReference('{Assembly.GetExecutingAssembly().GetName().Name}')");
                //DebugExec($"import y");

                if (!string.IsNullOrEmpty(mCustRoot))
                    RunAllCustModules(Path.Join(mCustRoot, "cust/mods"));
                else
                    RunAllCustModules();
                if (runModules)
                    RunAllModules();
            }

            private static string mCustRoot;

            public static string CustRoot
            {
                get { return mCustRoot; }
                set
                {
                    mCustRoot = value;;
                }
            }

            /// <summary>
            /// 创建ScriptRuntime，配置输入输出，然后从中得到ScriptEngine与ScriptScope
            /// </summary>
            private static void Preinitialize()
            {
                var aRuntime = Python.CreateRuntime(/*new Dictionary<string, object> { {"PrivateBinding", true } }*/);
                aRuntime.IO.SetOutput(mStdoutStream, mStdoutWriter);  // a workaround, see Issue #1625 in IronPython3 repo
                aRuntime.IO.SetErrorOutput(mStderrStream, mStderrWriter);
                aRuntime.IO.SetInput(mStdinStream, Encoding.Default);
                mPyEnj = Python.GetEngine(aRuntime);
                mPyScope = mPyEnj.CreateScope();
            }

            private static void ConfigureStdlib()
            {
                string libPath = Main.FetchIronPythonStdLib(mPyEnj.LanguageVersion);
                var searchPaths = mPyEnj.GetSearchPaths();
                searchPaths.Add(libPath);
                mPyEnj.SetSearchPaths(searchPaths);
            }

            private static void ConfigureWorkDir()
            {
                Main.IronPythonConfigureWorkDir();
            }

            private static void RunAllCustModules(string CustModDirName = "cust/mods") 
            {
                RunAllModules(CustModDirName, false);
            }

            private static void RunAllModules(string ModuleDirName = "mods", bool doCreate = true)
            {
                if (Directory.Exists(ModuleDirName))
                {
                    foreach (string path in Directory.EnumerateFiles(ModuleDirName))
                    {
                        Debug.Log(DebugType.Info, $"Loading <{path}>");
                        string aModName;
                        ScriptScope aModScope;
                        try
                        {
                            if (Path.GetExtension(path).ToLower() == ".py")
                            {
                                // mPyEnj.ExecuteFile(path); // use more proper mod loading strategy
                                aModName = Path.GetFileNameWithoutExtension(path);
                                dynamic tempScope = mPyEnj.CreateModule(aModName);
                                tempScope.__file__ = path;
                                tempScope.__name__ = aModName;
                                mPyEnj.CreateScriptSourceFromFile(path).Compile().Execute(tempScope);
                                aModScope = mPyEnj.Runtime.ImportModule(aModName);
                                Debug.Log(DebugType.Info, $"Successfully loaded \"{aModName}\" from <{path}>");
                            }
                            else if (Path.GetExtension(path).ToLower() == ".dll")
                            {
                                var assembly = Assembly.LoadFrom(path);

                                aModName = assembly.GetName().Name;//ManifestModule.ScopeName;
                                mPyEnj.Runtime.LoadAssembly(assembly);

                                //var aModule = mPyEnj.Runtime.GetBuiltinModule().GetVariable("__import__")(aModName);
                                aModScope = mPyEnj.Runtime.ImportModule(aModName);
                                Debug.Log(DebugType.Info, $"Successfully loaded precompiled \"{aModName}\" from <{path}>");
                            }
                            else 
                            {
                                Debug.Log(DebugType.Info, $"Skipped <{path}> as it's not a supported script.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Log(DebugType.Error, $"{ex.GetType()}: {ex.Message}");
                        }
                    }
                }
                else 
                {
                    if (doCreate)
                        Directory.CreateDirectory(ModuleDirName);
                }
            }


            private static ScriptEngine mPyEnj;
            private static ScriptScope mPyScope;
            private static readonly VirtualWriter mStdoutWriter = new VirtualWriter("stdout");
            private static readonly VirtualWriter mStderrWriter = new VirtualWriter("stderr");
            //private static readonly TextReader mStdinReader = new VirtualReader("stdin");
            private static readonly Stream mStdoutStream = new TextStream(mStdoutWriter);
            private static readonly Stream mStderrStream = new TextStream(mStderrWriter);
            private static readonly Stream mStdinStream = new TextStream(TextReader.Null, Encoding.Default);

            internal abstract class TextStreamBase : Stream
            {

                private readonly bool _buffered;

                protected TextStreamBase(bool buffered)
                {
                    _buffered = buffered;
                }

                public abstract Encoding Encoding { get; }
                public abstract TextReader Reader { get; }
                public abstract TextWriter Writer { get; }

                public sealed override bool CanSeek
                {
                    get { return false; }
                }

                public sealed override bool CanWrite
                {
                    get { return Writer != null; }
                }

                public sealed override bool CanRead
                {
                    get { return Reader != null; }
                }

                public sealed override void Flush()
                {
                    if (!CanWrite) throw new InvalidOperationException();
                    Writer.Flush();
                }

                public sealed override int Read(byte[] buffer, int offset, int count)
                {
                    if (!CanRead) throw new InvalidOperationException();
                    ContractUtils.RequiresArrayRange(buffer, offset, count, "offset", "count");

                    char[] charBuffer = new char[count];
                    int realCount = Reader.Read(charBuffer, 0, count);
                    return Encoding.GetBytes(charBuffer, 0, realCount, buffer, offset);
                }

                public sealed override void Write(byte[] buffer, int offset, int count)
                {
                    ContractUtils.RequiresArrayRange(buffer, offset, count, "offset", "count");
                    char[] charBuffer = Encoding.GetChars(buffer, offset, count);
                    Writer.Write(charBuffer, 0, charBuffer.Length);
                    if (!_buffered) Writer.Flush();
                }
                public sealed override long Length
                {
                    get
                    {
                        throw new InvalidOperationException();
                    }
                }

                public sealed override long Position
                {
                    get
                    {
                        throw new InvalidOperationException();
                    }
                    set
                    {
                        throw new InvalidOperationException();
                    }
                }

                public sealed override long Seek(long offset, SeekOrigin origin)
                {
                    throw new InvalidOperationException();
                }

                public sealed override void SetLength(long value)
                {
                    throw new InvalidOperationException();
                }
            }

            internal sealed class TextStream : TextStreamBase
            {

                private readonly TextReader _reader;
                private readonly TextWriter _writer;
                private readonly Encoding _encoding;

                public override Encoding Encoding
                {
                    get { return _encoding; }
                }

                public override TextReader Reader
                {
                    get { return _reader; }
                }

                public override TextWriter Writer
                {
                    get { return _writer; }
                }

                internal TextStream(TextReader reader, Encoding encoding)
                    : base(true)
                {
                    ContractUtils.RequiresNotNull(reader, nameof(reader));
                    ContractUtils.RequiresNotNull(encoding, nameof(encoding));

                    _reader = reader;
                    _encoding = encoding;
                }

                internal TextStream(TextWriter writer)
                    : this(writer, writer.Encoding, true)
                {
                }

                internal TextStream(TextWriter writer, Encoding encoding, bool buffered)
                    : base(buffered)
                {
                    ContractUtils.RequiresNotNull(writer, nameof(writer));
                    ContractUtils.RequiresNotNull(encoding, nameof(encoding));

                    _writer = writer;
                    _encoding = encoding;
                }
            }

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


        private static int FindAvailablePort() 
        {
            int port = defaultPort;
            while (true)
            {
                try
                {
                    TcpListener l = new TcpListener(IPAddress.Any, port);
                    l.Start();
                    l.Stop();
                    return port;
                }
                catch (SocketException)
                {
                    port++;
                }
            }

        }

        public static int Serve()
        {
            return Serve(FindAvailablePort());
        }

        public static int Serve(int port)
        {
            mWS = new WebSocketServer(IPAddress.Any, port);
            mWS.AddWebSocketService<PyHub>("/Py");
            PyHub.Initialize();
            mWS.Start();
            return mWS.Port;
        }

        public static void Stop()
        {
            //mWS.Stop();
        }

        private static WebSocketServer mWS;
        private static readonly JsonSerializer mSerializer = new JsonSerializer()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };

        public static int defaultPort = 8080;
    }
}
