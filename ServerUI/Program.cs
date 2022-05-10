using MessangerLibrary;
using ServerBLL.Logic;
using ServerBLL.Modeles;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerUI
{
    class Program
    {
        #region Server data
        // List clients
        static private List<ClientEntity> _clients = new List<ClientEntity>();
        // Server data
        static private Socket _serverSocket;
        // Data stream
        private static byte[] _dataStream = new byte[1024];
        public static ManualResetEvent Manual = new ManualResetEvent(false);
        #endregion

        static void Main(string[] args)
        {
            //data auntification
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, 8000);
            _serverSocket.Bind(endPoint);

            //console
            StartUpConsole();
            ConsoleMessage(true, ConsoleColor.Green, "THE SERVER HAS STARTED");

            // work program
            while (true)
            {
                Manual.Reset();
                IPEndPoint client = new IPEndPoint(ip, 0);
                EndPoint epSender = (EndPoint)client;
                _serverSocket.BeginReceiveFrom(_dataStream, 0, _dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(ReceiveMessage), endPoint);
                Manual.WaitOne();
            }
        }

        private static void ReceiveMessage(IAsyncResult asyncResult)
        {
            try
            {
                Manual.Set();
                IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)client;
                _serverSocket.EndReceiveFrom(asyncResult, ref epSender);
                Message clientMessage = MessageStream.ReadMessage(_dataStream);
                Message serverMessage = MessageStream.CreateMessage(clientMessage.TypeMessage, clientMessage.Username, null);

                switch (clientMessage.TypeMessage)
                {
                    case StatusMessage.LogIn:
                        {
                            
                        }
                        break;
                    case StatusMessage.SigIn:
                        {

                        }
                        break;
                    case StatusMessage.GenereteNewPassword:
                        {

                        }
                        break;
                    case StatusMessage.Joined: //Client log In
                        {
                        }
                        break;
                    case StatusMessage.Message: // Client send message
                        {
                        }
                        break;
                    case StatusMessage.Detached: //Client log out
                        {
                        }
                        break;
                }

                byte[] byteMessage = MessageStream.WriteMessage(serverMessage);

                foreach (var fclient in _clients)
                {
                    if (fclient.EndPoint != epSender || serverMessage.TypeMessage != StatusMessage.Joined)
                    {
                        _serverSocket.BeginSendTo(byteMessage, 0, byteMessage.Length, SocketFlags.None, fclient.EndPoint, new AsyncCallback(SendMessage), fclient.EndPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleMessage(false, ConsoleColor.Red, $"Error server receive message: {ex.Message}");
            }
        }
        private static void SendMessage(IAsyncResult asyncResult)
        {
            try
            {
                _serverSocket.EndSend(asyncResult);
            }
            catch (Exception ex)
            {
                ConsoleMessage(false, ConsoleColor.Red, $"Error server send message: {ex.Message}");
            }
        }

        //UI methods
        private static void ConsoleMessage(bool isDesign, ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            if (isDesign == true) { Console.WriteLine($"---------- {message} ----------"); }
            else { Console.WriteLine(message); }
        }
        private static void StartUpConsole()
        {
            Console.Title = "Server";
            Console.CursorVisible = false;
        }
    }
}