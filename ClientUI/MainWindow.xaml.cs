using MessangerLibrary.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace ClientUI
{
    public partial class MainWindow : Window
    {
        private Socket clientSocket;
        private EndPoint epServer;
        private int idUser;
        private byte[] dataStream = new byte[1024];

        // Display message delegate
        private delegate void DisplayMessageDelegate(string message);
        private DisplayMessageDelegate displayMessageDelegate = null;
        private string _oldMesage;

        public MainWindow()
        {
            InitializeComponent();
            StartUp();
        }

        private void StartUp()
        {
            try
            {
                Message message = MessageStream.CreateMessage(StatusMessage.Joined, null, null);
                byte[] messageByte = MessageStream.WriteMessage(message);

                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                IPEndPoint server = new IPEndPoint(serverIP, 2000);
                epServer = server;
                

                clientSocket.BeginSendTo(messageByte, 0, messageByte.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);


                this.dataStream = new byte[1024];


                byte[] bytes_ar = new byte[5024];
                clientSocket.Receive(bytes_ar);
                string serverString = Encoding.UTF8.GetString(bytes_ar);

                foreach (string item in serverString.Split("\n"))
                {
                    viewMessage.Items.Add(item);
                    viewMessage.ScrollIntoView(item);
                }

                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex.Message, "Error menu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //All labels
        private void LabelLichBlueToWhite(object sender, MouseEventArgs e) { ((Label)sender).Foreground = Brushes.White; }
        private void LabelWhiteToLichBlue(object sender, MouseEventArgs e) { ((Label)sender).Foreground = Brushes.LightBlue; }

        //Exit label
        private void Exit(object sender, MouseButtonEventArgs e) { Close(); }

        //Profile label
        private void Profile(object sender, MouseButtonEventArgs e)
        {

        }

        //Settings label
        private void Settings(object sender, MouseButtonEventArgs e)
        {

        }

        //TextBox Message changed
        private void MessageTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_messageText.Text.Length <= 512)
            {
                _oldMesage = _messageText.Text;
            }
            else
            {
                _messageText.Text = _oldMesage;
                MessageBox.Show("Max size message is 512 simbol!", "Info menu", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //generete listbox item
        static public ListBoxItem GetMessage(Message message)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = message.ToString();
            listBoxItem.FontSize = 20;

            return listBoxItem;
        }

        //send message
        private void SendClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(_messageText.Text))
                {
                    Message message = MessageStream.CreateMessage(StatusMessage.Message, null, _messageText.Text);
                    byte[] byteMessage = MessageStream.WriteMessage(message);
                    clientSocket.BeginSendTo(byteMessage, 0, byteMessage.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);
                    _messageText.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Error: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SendData(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error send data: " + ex.Message, "Error menu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                this.clientSocket.EndReceive(ar);
                Message message = MessageStream.ReadMessage(dataStream);

                if (message.Content != null) ;
                {
                    displayMessageDelegate.Invoke(message.Content);
                }

                this.dataStream = new byte[1024];

                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show("Error receive process: " + ex.Message, "Error menu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void DisplayMessage(string messge)
        {
            Dispatcher.Invoke(() => viewMessage.Items.Add(messge + Environment.NewLine));
            Dispatcher.Invoke(() => viewMessage.ScrollIntoView(messge + Environment.NewLine));
        }

        //send file
        private void FileClick(object sender, RoutedEventArgs e)
        {

        }

        //implement key
        private void MessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl)
            {
                SendClick(null, null);
            }
            if (e.Key == Key.Enter)
            {
                _messageText.Text += "\n";
                _messageText.SelectionStart = _messageText.Text.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.displayMessageDelegate = new DisplayMessageDelegate(this.DisplayMessage);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if (this.clientSocket != null)
                {
                    Message message = MessageStream.CreateMessage(StatusMessage.Detached, null, null);
                    byte[] byteMessage = MessageStream.WriteMessage(message);
                    clientSocket.SendTo(byteMessage, 0, byteMessage.Length, SocketFlags.None, epServer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconect Error: " + ex.Message, "Error menu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
