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
            Task t = new Task(() => {
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
            mTextJoinNeeded = true;
            FlushBoard(true);
        }

        public delegate void CaptionAddDelegate(string caption);
        public CaptionAddDelegate captionAddDelegate;
        private void CaptionAdd(string newCaption)
        {
            pyCaption.Text = newCaption;
            FlushBoard(false);
        }

        private void WSConnect(string addr)
        {
            ws = new WebSocket(addr);
            ws.OnMessage += (sender, e) =>
            {
                TextAddDelegate textAddDelegate = new TextAddDelegate(TextAdd);
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
                            ) + ">>> ", true, executionEvent.timestamp);
                            ;
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
                CaptionAddDelegate captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render, "Connection Closed");
                connected = false;
            };
            ws.OnError += (sender, e) =>
             {
                 CaptionAddDelegate captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                 pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render, $"Error:{e}");
             };

            connected = true;
            ws.Connect();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextAdd(pyCommandInput.Text + "\n", true, DateTime.UtcNow.ToFileTime());
            ws.SendAsync(pyCommandInput.Text, e => { });
            pyCommandInput.Text = "";
        }

        private WebSocket ws;

        private void pyCommandInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            FlushBoard(false);
        }

        public void FlushBoard(bool isScrollNeeded)
        {
            pyOutput.Text = TextJoin(pyCommandInput.Text);
            wsConnBtn.Content = !connected ? "Connect" : "Disconnect";
            if (pyCommandSendBtn != null) { pyCommandSendBtn.IsEnabled = connected; }
            if (isScrollNeeded) scrollText.ScrollToEnd();
        }

        private string TextJoin(string commandCurrent) 
        {
            if (!mTextJoinNeeded) 
            {
                return mTextOutputHarden + (mTextJoinCache + commandCurrent);
            }
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
            mTextJoinCache = result;
            mTextJoinNeeded = false;
            return mTextOutputHarden + (mTextJoinCache + commandCurrent);
        }

        private void wsConnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!connected)
            {
                string addr = wsAddrInput.Text;
                CaptionAdd("Connecting...");
                Task task = new Task(() => { 
                    WSConnect(addr);
                    CaptionAddDelegate captionAddDelegate = new CaptionAddDelegate(CaptionAdd);
                    pyCaption.Dispatcher.Invoke(captionAddDelegate, System.Windows.Threading.DispatcherPriority.Render,
                        connected ? "Connected" : "Failed to Connect"
                    );
                }
                );
                task.Start();
                //WSConnect();
                
            }
            else
            {
                connected = false;
                ws.Close();
                CaptionAdd("Not Connected");
            }

            //connected = !connected;
            
        }


        private bool connected = false;

        private void pyCommandInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                pyCommandSendBtn.IsDefault = true;
            }
        }

        private void pyCommandInput_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.LeftCtrl && Keyboard.IsKeyUp(Key.RightCtrl)) || (e.Key == Key.RightCtrl && Keyboard.IsKeyUp(Key.LeftCtrl)))
            {
                pyCommandSendBtn.IsDefault= false;
            }
        }

        internal class TextQueueList : List<KeyValuePair<long, string>> 
        {
            public void AddwithTime(KeyValuePair<long, string> item) 
            {
                long k = item.Key;
                if (mTextOutputPool.Count == 0 || k >= mTextOutputPool[^1].Key) 
                {
                    mTextOutputPool.Add(item);
                    return;
                }
                if (k <= mTextOutputPool[0].Key) 
                {
                    mTextOutputPool.Insert(0, item);
                    return;
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
                        mTextOutputPool.Insert(i, item);
                        return;
                    }
                    else if (cmpresult < 0)
                    {
                        if (b <= 1)
                        {
                            mTextOutputPool.Insert(i+1, item);
                            return;
                        }
                        mi = i;
                    }
                    else if (cmpresult > 0)
                    {
                        if (a <= 1)
                        {
                            mTextOutputPool.Insert(i, item);
                            return;
                        }
                        ma = i;
                    }
                }
                
            }
        }

        private static TextQueueList mTextOutputPool = new TextQueueList();
        private StringBuilder mTextOutputHarden = new StringBuilder(">>> ");
        private long mTextOutputPoolTimeout = 10 * 10000000;
        private int mTextOutputPoolMaxSize = 2048;
        private bool mTextJoinNeeded = true;
        private StringBuilder mTextJoinCache;

    }
}
