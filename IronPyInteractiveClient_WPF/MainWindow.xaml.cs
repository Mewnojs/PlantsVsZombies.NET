using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.NetCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static IronPyInteractiveDef_Shared.WSEvents;
using System.Threading;

namespace IronPyInteractiveClient_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task t = new Task(() =>
            {
                for (; ; )
                {
                    Thread.Sleep(10000);
                    GC.Collect();
                }
            });
            t.Start();
        }

        public delegate void TextAddDelegate(string text, bool isScrollNeeded, long timestamp);
        public TextAddDelegate textAddDelegate;

        private void TextAdd(string s, bool isScrollNeeded, long timestamp)
        {
            mTextOutputPool.AddwithTime(new KeyValuePair<long, string>(timestamp, s));
            mIsTextFlushNeeded = true;
            FlushBoard(true);
        }

        public delegate void CaptionAddDelegate(string caption, bool isFlushNeeded);
        public CaptionAddDelegate captionAddDelegate;
        private void CaptionAdd(string newCaption, bool isFlushNeeded)
        {
            pyCaption.Text = newCaption;
            if (isFlushNeeded) { FlushBoard(false); }
        }

        public delegate void ExecutionCompletedDelegate(object exception);
        public ExecutionCompletedDelegate executionCompletedDelegate;
        private void ExecutionCompleted(object exception)
        {
            pyPromptSign.Text = mPromptStr_Rest;
            mTextOutputHiddenSuffix += ">>> ";
        }

        private void WSConnect(string addr)
        {
            ws = new WebSocket(addr);
            ws.OnMessage += (sender, e) =>
            {
                executionCompletedDelegate = new ExecutionCompletedDelegate(ExecutionCompleted);
                textAddDelegate = new TextAddDelegate(TextAdd);
                try
                {
                    JObject jo = JObject.Parse(e.Data);
                    WSEvent wsEvent = jo.ToObject<WSEvent>();
                    switch (wsEvent.eventtype)
                    {
                        case "output":
                            OutputEvent outputEvent = jo.ToObject<OutputEvent>();
                            pyOutput.Dispatcher.Invoke(textAddDelegate, System.Windows.Threading.DispatcherPriority.Render,
                                outputEvent.msg, true, outputEvent.timestamp);
                            break;
                        case "execution":
                            ExecutionEvent executionEvent = jo.ToObject<ExecutionEvent>();
                            pyOutput.Dispatcher.Invoke(textAddDelegate, System.Windows.Threading.DispatcherPriority.Render,
                            (
                                executionEvent.statuscode == ExecutionEventResult.error
                                ? $"{executionEvent.errortype}: {executionEvent.result}\n"
                                : (executionEvent.result != null ? $"{executionEvent.result}\n" : "")
                            )/* + ">>> "*/, true, executionEvent.timestamp);
                            ;
                            pyOutput.Dispatcher.Invoke(executionCompletedDelegate, System.Windows.Threading.DispatcherPriority.Render,
                                executionEvent.errortype);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.GetType()}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            ws.OnClose += (sender, e) =>
            {
                captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                mWSConnected = false;
                pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render, "Connection Closed", true);
            };
            ws.OnError += (sender, e) =>
             {
                 captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                 pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render, $"Error:{e}", true);
             };

            mWSConnected = true;
            ws.Connect();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextAdd(mTextOutputHiddenSuffix + pyCommandInput.Text + "\n", true, DateTime.UtcNow.ToFileTime());
            mTextOutputHiddenSuffix = "";
            pyPromptSign.Text = mPromptStr_Working;
            ws.SendAsync(pyCommandInput.Text, e => { });
            pyCommandInput.Text = "";
        }

        private WebSocket ws;

        private void pyCommandInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //FlushBoard(false);
        }

        public void FlushBoard(bool isScrollNeeded)
        {
            pyOutput.Text = TextJoin();
            wsConnBtn.Content = !mWSConnected ? "Connect" : "Disconnect";
            if (!mWSConnected)
            {
                pyPromptSign.Text = mPromptStr_NotAvailable;
            }
            if (pyCommandSendBtn != null) { pyCommandSendBtn.IsEnabled = mWSConnected; }
            if (isScrollNeeded) scrollText.ScrollToEnd();
        }

        private ref string TextJoin()
        {
            if (!mIsTextFlushNeeded)
            {
                return ref mTextOutputHardenString;
            }
            mTextOutputHarden.Append(mTextOutputHardenLineCharCache);
            mTextOutputHarden.Remove(mTextJoinCacheIndex, mTextOutputHarden.Length - mTextJoinCacheIndex);
            StringBuilder result = new StringBuilder();
            long time = DateTime.UtcNow.ToFileTime();
            int countstobedeleted = 0;
            for (int i = 0; i < mTextOutputPool.Count; i++)
            {
                if (time - mTextOutputPool[i].Key >= mTextOutputPoolTimeout || mTextOutputPool.Count - i > mTextOutputPoolMaxSize)
                {
                    mTextOutputHarden.Append(mTextOutputPool[i].Value);
                    countstobedeleted = i + 1;
                }
                else
                {
                    result.Append(mTextOutputPool[i].Value);
                }
            }
            if (countstobedeleted != 0)
            {
                mTextOutputPool.RemoveRange(0, countstobedeleted);
            }
            mTextJoinCacheIndex = mTextOutputHarden.Length;
            mTextOutputHarden.Append(result);
            if (mTextOutputHarden.Length >= 1 && mTextOutputHarden[^1] == '\n')
            {
                mTextOutputHarden.Remove(mTextOutputHarden.Length - 1, 1);
                mTextOutputHardenLineCharCache += '\n';
                if (mTextOutputHarden.Length >= 1 && mTextOutputHarden[^1] == '\r')
                {
                    mTextOutputHarden.Remove(mTextOutputHarden.Length - 1, 1);
                    mTextOutputHardenLineCharCache += '\r';
                }
            }
            mIsTextFlushNeeded = false;
            mTextOutputHardenString = mTextOutputHarden.ToString();
            return ref mTextOutputHardenString;
        }

        private void wsConnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!mWSConnected)
            {
                string addr = wsAddrInput.Text;
                CaptionAdd("Connecting...", true);
                Task task = new Task(() =>
                {
                    WSConnect(addr);
                    captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                    pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render,
                        mWSConnected ? "Connected" : "Failed to Connect", true
                    );
                    if (mWSConnected) GetLogoDisplay();
                });
                task.Start();
            }
            else
            {
                mWSConnected = false;
                ws.Close();
                CaptionAdd("Not Connected", true);
            }
        }

        private bool mWSConnected = false;

        private void pyCommandInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                pyCommandSendBtn.IsDefault = true;
        }

        private void pyCommandInput_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.LeftCtrl && Keyboard.IsKeyUp(Key.RightCtrl)) ||
            (e.Key == Key.RightCtrl && Keyboard.IsKeyUp(Key.LeftCtrl)))
                pyCommandSendBtn.IsDefault = false;
        }

        internal class TextQueueList : List<KeyValuePair<long, string>>
        {
            public void AddwithTime(KeyValuePair<long, string> item)
            {
                long k = item.Key;
                if (mTextOutputPool.Count == 0 || k >= mTextOutputPool[^1].Key)
                {
                    mTextOutputPool.Add(item);return;
                }
                if (k <= mTextOutputPool[0].Key)
                {
                    mTextOutputPool.Insert(0, item);return;
                }
                int mi = 0, ma = mTextOutputPool.Count, a = 0, b = 0, i = 0;
                int cmpresult;
                for (; ; )
                {
                    a = (ma - mi) / 2;
                    b = ma - mi - a;
                    i = mi + a;
                    cmpresult = mTextOutputPool[i].Key.CompareTo(k);
                    if (cmpresult == 0)
                    {
                        mTextOutputPool.Insert(i, item);return;
                    }
                    else if (cmpresult < 0)
                    {
                        if (b <= 1)
                        {
                            mTextOutputPool.Insert(i + 1, item);return;
                        }
                        mi = i;
                    }
                    else if (cmpresult > 0)
                    {
                        if (a <= 1)
                        {
                            mTextOutputPool.Insert(i, item);return;
                        }
                        ma = i;
                    }
                }

            }
        }

        private void GetLogoDisplay()
        {
            ws.SendAsync(
                "__import__('sys').stdout.write('IronPython '+__import__('sys').version+'\\nType \"help\", \"copyright\", " +
                "\"credits\" or \"license\" for more information.\\n');", (x) => { });
        }

        private static TextQueueList mTextOutputPool = new TextQueueList();
        private StringBuilder mTextOutputHarden = new StringBuilder("");
        private string mTextOutputHardenLineCharCache = "";
        private string mTextOutputHardenString;
        private string mTextOutputHiddenSuffix = "";
        private long mTextOutputPoolTimeout = 10 * 10000000;
        private int mTextOutputPoolMaxSize = 2048;
        private bool mIsTextFlushNeeded = true;
        private int mTextJoinCacheIndex;
        private readonly string mPromptStr_Rest = ">>>";
        private readonly string mPromptStr_Working = "<...>";
        private readonly string mPromptStr_NotAvailable = "   ";

        private void Btn_clearScreen_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            mIsTextFlushNeeded = true;
            FlushBoard(true);
            string s = pyCaption.Text;
            CaptionAdd("Cleared!", false);
            Task.Run(async delegate
            {
                await Task.Delay(1500);
                captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render,
                    s, false);
            });
        }

        private void Reset()
        {
            mTextOutputHarden.Clear();
            mTextOutputPool.Clear();
            mTextOutputHardenLineCharCache = "";
            mTextJoinCacheIndex = 0;
            mTextOutputHiddenSuffix = "";
        }
    }
}
