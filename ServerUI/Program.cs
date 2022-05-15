using MessangerLibrary.Chat;
using MessangerLibrary.Email;
using ServerBLL.Modeles;
using ServerBLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerUI
{
    class Program
    {
        #region Server data
        // List clients
        static private List<ClientEntity> _onlineUsers = new List<ClientEntity>();
        // Server data
        static private Socket _serverSocket;
        // Data stream
        static private byte[] _dataStream = new byte[1024];
        static public ManualResetEvent Manual = new ManualResetEvent(false);

        //Data services
        static private UserService userService = new UserService();
        static private MessageService messageService = new MessageService();

        #endregion

        static void Main(string[] args)
        {
            //creadet data auntification
            IPAddress ip = IPAddress.Parse("127.0.0.1"); // IP
            IPEndPoint endPoint = new IPEndPoint(ip, 2000); // PORT

            //data auntification


            //////////////////////////////////////////////////////        EXEPTION
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //////////////////////////////////////////////////////        EXEPTION

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
                bool isMessage = false;
                _serverSocket.EndReceiveFrom(asyncResult, ref epSender);
                Message clientMessage = MessageStream.ReadMessage(_dataStream);
                Message serverMessage = MessageStream.CreateMessage(clientMessage.TypeMessage, clientMessage.Username, clientMessage.Content);

                switch (clientMessage.TypeMessage)
                {
                    case StatusMessage.LogIn:
                        {
                            // get log data
                            List<string> logData = ParsingContent.ReadParser(clientMessage.Content);
                            //result
                            byte[] result;

                            //validation 
                            if (logData.Count == 2)
                            {
                                //if data log is corect
                                result = BitConverter.GetBytes(userService.IsLogin(logData[0], logData[1]));
                            }
                            else
                            {
                                //if not corect
                                result = BitConverter.GetBytes(-2);
                            }

                            //send result
                            _serverSocket.BeginSendTo(result, 0, result.Length, SocketFlags.None, epSender, new AsyncCallback(SendMessage), epSender);

                            //view in console
                            if(BitConverter.ToInt32(result) >= 0)
                            {
                                ConsoleMessage(true, ConsoleColor.Green, clientMessage.Username + ": sucesfule logined");
                            }
                            else
                            {
                                ConsoleMessage(true, ConsoleColor.Green, clientMessage.Username + ": not sucesfule logined");
                            }
                        }
                        break;
                    case StatusMessage.SigIn:
                        {
                            // get sig data
                            List<string> sigData = ParsingContent.ReadParser(clientMessage.Content);
                            //result
                            byte[] result;

                            //validation 
                            if (sigData.Count == 3)
                            {
                                //if data log is corect
                                result = BitConverter.GetBytes(userService.Add(new UserEntity() {UserName = sigData[0], Password = sigData[1], Email = sigData[3] }));
                            }
                            else
                            {
                                //if not corect
                                result = BitConverter.GetBytes(-2);
                            }

                            //send result
                            _serverSocket.BeginSendTo(result, 0, result.Length, SocketFlags.None, epSender, new AsyncCallback(SendMessage), epSender);

                            //view in console
                            if (BitConverter.ToInt32(result) >= 0)
                            {
                                ConsoleMessage(true, ConsoleColor.Green, clientMessage.Username + ": sucesfule sigined");
                            }
                            else
                            {
                                ConsoleMessage(true, ConsoleColor.Green, clientMessage.Username + ": not sucesfule sigined");
                            }
                        }
                        break;
                    case StatusMessage.GenereteNewPassword:
                        {
                            byte[] result;
                            int checkEmail = userService.CheckEmail(clientMessage.Content);

                            if (checkEmail >= 0)
                            {
                                UserEntity user = userService.GetById(checkEmail);
                                UserEntity tempUser = new UserEntity
                                {
                                    Id = user.Id,
                                    UserName = user.UserName,
                                    Password = userService.GenerateNewPasword(8),
                                    Email = user.Email
                                };

                                userService.Update(tempUser);

                                EmailSender sender = new EmailSender();
                                EmailMessage message = new EmailMessage(new List<string> { clientMessage.Content }, "New password", tempUser.Password);

                                result = Encoding.UTF8.GetBytes("New password in your email!");
                            }
                            else
                            {
                                result = Encoding.UTF8.GetBytes("Your email not found!");
                            }

                            //send result
                            _serverSocket.BeginSendTo(result, 0, result.Length, SocketFlags.None, epSender, new AsyncCallback(SendMessage), epSender);

                            //view in console
                            ConsoleMessage(true, ConsoleColor.Blue, clientMessage.Username + ": generete new password");
                        }
                        break;
                    case StatusMessage.Joined: //Client log In
                        {
                            isMessage = true;
                            ClientEntity NewClient = new ClientEntity() { EndPoint = client, UserName = clientMessage.Username };
                            _onlineUsers.Add(NewClient);

                            foreach(var oldMessage in messageService.GetAll())
                            {
                                byte[] result = Encoding.UTF8.GetBytes($"{oldMessage.UserName}: {oldMessage.Content}");
                                _serverSocket.BeginSendTo(result, 0, result.Length, SocketFlags.None, epSender, new AsyncCallback(SendMessage), epSender);
                            }

                            //view in console
                            ConsoleMessage(true, ConsoleColor.Green, clientMessage.Username + clientMessage.Content);
                        }
                        break;
                    case StatusMessage.Message: // Client send message
                        {
                            isMessage = true;
                            ConsoleMessage(true, ConsoleColor.Gray, clientMessage.Username + clientMessage.Content);
                        }
                        break;
                    case StatusMessage.Detached: //Client log out
                        {
                            foreach (ClientEntity c in _onlineUsers)
                            {
                                if (c.EndPoint.Equals(epSender))
                                {
                                    _onlineUsers.Remove(c);
                                    isMessage = true;
                                    ConsoleMessage(true, ConsoleColor.Red, clientMessage.Username + clientMessage.Content);
                                    break;
                                }
                            }
                        }
                        break;
                }

                if(isMessage == true)
                {
                    byte[] byteMessage = MessageStream.WriteMessage(serverMessage);


                    messageService.Add(new MessageEntity { Content = serverMessage.Content, UserName = serverMessage.Username });

                    foreach (var fclient in _onlineUsers)
                    {
                        if (fclient.EndPoint != epSender || serverMessage.TypeMessage != StatusMessage.Joined)
                        {
                            _serverSocket.BeginSendTo(byteMessage, 0, byteMessage.Length, SocketFlags.None, fclient.EndPoint, new AsyncCallback(SendMessage), fclient.EndPoint);
                        }
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