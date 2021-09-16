using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.NetCore;
using Newtonsoft.Json;
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
            
        }

        public delegate void TextAddDelegate(string text);
        public TextAddDelegate textAddDelegate;

        private void TextAdd(string s) 
        {
            textOnBoard += s; FlushBoard();
        }

        private void WSConnect(string addr)
        {
            ws = new WebSocket(addr);
            ws.OnMessage += (sender, e) =>
            {
                TextAddDelegate textAddDelegate = new TextAddDelegate(TextAdd);
                try
                {
                    Dictionary<string, object> jo = JsonConvert.DeserializeObject<Dictionary<string, object>>(e.Data);
                    switch (jo["type"])
                    {
                        case "output":
                            pyOutput.Dispatcher.Invoke(textAddDelegate,System.Windows.Threading.DispatcherPriority.Render, jo["msg"]);
                            
                            break;
                        case "execution":
                            pyOutput.Dispatcher.Invoke(textAddDelegate, System.Windows.Threading.DispatcherPriority.Render, 
                                (jo["result"] != null ? jo["result"].ToString() + "\n" : "") + ">>> " );
                            break;
                    }
                }
                catch (Exception ex) { }


            };
            ws.OnClose += (sender, e) =>
            {
                TextAddDelegate textAddDelegate = new TextAddDelegate(TextAdd);
                pyOutput.Dispatcher.Invoke(textAddDelegate, System.Windows.Threading.DispatcherPriority.Render, "<Connection Closed>\n");
                connected = false;
            };
            ws.OnError+= (sender, e) =>
            {
                TextAddDelegate textAddDelegate = new TextAddDelegate(TextAdd);
                pyOutput.Dispatcher.Invoke(textAddDelegate, System.Windows.Threading.DispatcherPriority.Render, $"<Error>:{e}\n");
            };


            ws.Connect();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textOnBoard += pyCommandInput.Text + "\n";
            ws.SendAsync(pyCommandInput.Text, e => { });
            FlushBoard();
            pyCommandInput.Text = "";
        }

        private WebSocket ws;

        private void pyCommandInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            FlushBoard();
        }

        public void FlushBoard() 
        {
            pyOutput.Text = textOnBoard + pyCommandInput.Text;
            wsConnBtn.Content = !connected ? "Connect" : "Disconnect";
            if (pyCommandSendBtn != null) { pyCommandSendBtn.IsEnabled = connected; }
        }

        private string textOnBoard = ">>> ";

        private void wsConnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!connected) 
            {
                WSConnect(wsAddrInput.Text);
                TextAdd("<Connected>\n");
            }
            else 
            {
                ws.Close();
                TextAdd("<Disconnected>\n");
            }
            
            connected = !connected;
            FlushBoard();
        }
        private bool connected=false;

    }
}
