using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CTest.SS
{
    //
    public class S
    {
        public static int port = 12345;
        static Socket _serverSocket;

        public static bool isRunning;

        static S()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(1000);

            isRunning = true;

            Console.WriteLine("Serving on port" + port + "...");

            var thread = new Thread(OnStart);
            //IsBackground表示程序退出时自动终止线程，如果不设置，关闭窗体时，程序不会退出，线程会停留在OnStart方法中_serverSocket.Accept()
            //如果此时isRunning被贼值为false，线程不会即时终止，会在下一次端口接收到数据时终止，但终止前由于在处理数据包的方法AcceptSocket没有做处理，客户端得不到回应
            thread.IsBackground = true;
            thread.Start();
        }

        private static void OnStart()
        {
            while (isRunning)
            {
                try
                {
                    Console.WriteLine("1");
                    Socket socket = _serverSocket.Accept();
                    Console.WriteLine("2");
                    AcceptSocket(socket);
                    Task.Run(() =>
                    {
                        while (isRunning)
                        {
                            try
                            {
                                var data = new byte[4096];
                                Console.WriteLine("5");
                                
                                var len = socket.Receive(data);
                                Console.WriteLine("6");
                                Console.WriteLine("from " + socket.RemoteEndPoint.ToString() + " : " + Encoding.UTF8.GetString(data, 0, len));
                            }
                            catch (Exception e)
                            {
                                Stop(socket);
                                Console.WriteLine(e.Message + " onstart while task while");
                                break;
                            }
                        }
                    });
                }
                catch (Exception e) { Console.WriteLine(e.Message + " onstart while"); break; }
            }
        }

        public static Dictionary<string, Socket> clients = new Dictionary<string, Socket>();

        private static void AcceptSocket(Socket socket)
        {
            if (isRunning)
            {
                Console.WriteLine("3");
                clients.Add(socket.RemoteEndPoint.ToString(), socket);
                Console.WriteLine("4");
                DoMessage(socket);
            }
        }

        private static void DoMessage(Socket socket)
        {
            Console.WriteLine("A client connected.");
        }

        public static void Broadcast(string s)
        {
            var cl = new List<string>();
            foreach (var kvp in clients)
            {
                if (kvp.Value.Connected)
                {
                    kvp.Value.Send(Encoding.UTF8.GetBytes(s));
                }
                else
                {
                    cl.Add(kvp.Key);
                }
            }
            foreach (var k in cl)
            {
                clients.Remove(k);
            }
        }

        public static void Stop(Socket socket)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            //socket.Close();
        }

        public static void Clear()
        {
            isRunning = false;
            foreach (var kvp in clients)
            {
                kvp.Value.Send(Encoding.UTF8.GetBytes("!!q"));
                Stop(kvp.Value);
            }
            clients.Clear();
        }


        public static void cl()
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nE to Exit.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("L to List.Number Enter Session.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Number Enter Session.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("B to Broadcast.");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("C to Interrupt all.\n");
                    Console.ResetColor();
                    var command = Console.ReadLine().ToLower();
                    if (command == "c")
                    {
                        Clear();
                        Console.WriteLine("clear complete.");
                    }
                    else if (command == "e")
                    {
                        return;
                    }
                    else if (command == "b")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("input to send...");
                        Console.ResetColor();
                        var s = Console.ReadLine();
                        Broadcast(s);
                        while (isRunning)
                        {
                            if (clients.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("no client...");
                                Console.ResetColor();
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("E to Back,Otherwise keep send input.");
                            Console.ResetColor();
                            var c_or_s = Console.ReadLine().ToLower();
                            if (c_or_s == "e")
                            {
                                break;
                            }
                            Broadcast(c_or_s);
                        }
                    }
                    else if (command == "l")
                    {
                        var i = 0;
                        foreach (var kvp in clients)
                        {
                            i++;
                            Console.Write(i + ") " + kvp.Key);
                            if (i % 3 == 0)
                                Console.WriteLine();
                            else
                                Console.Write("\t");
                        }

                        Console.WriteLine();
                    }
                    else
                    {
                        var icommand = 0;
                        if (int.TryParse(command, out icommand))
                        {
                            try
                            {
                                var kvp = clients.ElementAt(icommand);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("input to send...");
                                Console.ResetColor();
                                var s = Console.ReadLine();
                                kvp.Value.Send(Encoding.UTF8.GetBytes(s));
                                while (isRunning)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("E to Back,Otherwise keep send input.");
                                    Console.ResetColor();
                                    var c_or_s = Console.ReadLine().ToLower();
                                    if (c_or_s == "e")
                                    {
                                        break;
                                    }
                                    kvp.Value.Send(Encoding.UTF8.GetBytes(c_or_s));
                                }
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error has occured,It probably caused by not exists the client");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Unrecognized.");
                            Console.ResetColor();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " cl outer");
                }
            }
        }

    }
}

namespace CTest.SC
{
    public class C
    {
        static bool keep = true;
        static Socket _socketClient;

        static C()
        {
            //Connect("172.16.10.44", 12345);
            Connect("127.0.0.1", 12345);

            Task.Run(() =>
            {
                ReceiveMsg();
            });
        }

        static void Init()
        {
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static void Connect(string ip, int port)
        {
            Init();
            _socketClient.Connect(ip, port);
        }

        private static void ReceiveMsg()
        {
            while (keep)
            {
                try
                {
                    var data = new byte[4096];
                    var len = _socketClient.Receive(data, 4096, SocketFlags.None);
                    var recv = Encoding.UTF8.GetString(data, 0, len);
                    Console.WriteLine("recv:" + recv);
                    if (recv == "!!q")
                    {
                        keep = false;
                        Close();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message+ " ReceiveMsg while");
                    break;
                }
            }
        }

        public static void Close()
        {
            try
            {
                _socketClient.Close();
                //_socketClient.Shutdown(SocketShutdown.Both);
            }
            catch { }
            //_socketClient.Close();
        }

        public static void Send(string s)
        {
            _socketClient.Send(Encoding.UTF8.GetBytes(s));
        }

        public static void ShowEndpoint()
        {
            Console.WriteLine(_socketClient.LocalEndPoint.ToString());
        }

        public static void cl()
        {
            while (keep)
            {
                try
                {
                    Console.WriteLine("\nS to Send.P to change endpoint re-conn.C to show local address.\n");
                    var command = Console.ReadLine().ToLower();
                    if (command == "s")
                    {
                        Console.WriteLine("input to send...");
                        var s = Console.ReadLine();
                        Send(s);
                        while (keep)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("E to Back,Otherwise keep send input.");
                            Console.ResetColor();
                            var c_or_s = Console.ReadLine().ToLower();
                            if (c_or_s == "e")
                            {
                                break;
                            }
                            Send(c_or_s);
                        }
                    }
                    else if (command == "p")
                    {
                        try
                        {
                            Console.WriteLine("Enter IPAddress");
                            var ip = Console.ReadLine();
                            Console.WriteLine("Enter Port");
                            var port = Console.ReadLine();
                            Close();
                            Connect(ip, int.Parse(port));
                        }
                        catch
                        {
                            Console.WriteLine("An error has occured,It probably caused by invalid ip address.");
                        }
                    }
                    else if (command == "c")
                    {
                        ShowEndpoint();
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized.");
                    }
                }
                catch { }
            }
        }


    }
}