using NetworkTypes;

using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using MaverickServer.Database;
using Main.HandleClient;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using MaverickServer.HandleClients.Tokens;

namespace Main
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            new Thread(() => HTTPServer()).Start();

            new Thread(() => TCPServer()).Start();
        }

        public void HTTPServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
        
            //New Ready
            while (true)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();

                    string API = context.Request.Url.Query;

                    string Response = "";

                    foreach (Product product in Cache.Products)
                    {
                        Response += product.Id + "," + product.Name + "," + Cache.AuthTokens.FindAll(token => token.LastDevice == product.Name).Count + ";";
                    }

                    Response += "%split%" + Cache.AuthTokens.Count;

                    byte[] bytes = Encoding.UTF8.GetBytes(Response);

                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = bytes.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));

                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch (Exception ex)
                {

                }
            }

        }

        public void TCPServer()
        {
            TcpListener server = new TcpListener(6060);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Opened Socket");

                new Thread(() => ClientThread(client)).Start();

                Thread.Sleep(1000);
            }
        }

        public void ClientThread(TcpClient client)
        {
            //Initial Request
            NetworkStream strm = client.GetStream();

            while (true)
            {
                if (!strm.DataAvailable)
                    goto Sleep;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                try
                {
                    IFormatter formatter = new BinaryFormatter();

                    Console.WriteLine("Request Init");

                    Request r = (Request)formatter.Deserialize(strm); // you have to cast the deserialized object 

                    Console.WriteLine("Recieved and Deserialized Request - " + stopwatch.Elapsed.TotalMilliseconds);

                    //Response
                    Console.WriteLine("Recieved: " + r.Command);

                    if (r.Command == "Version")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        //HandleVersion
                        Connect connect = new Connect();

                        Console.WriteLine("Connected to Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        string Version = connect.Version();

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        connect.Close();

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, new Response("Version", Version));

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Updater")
                    {
                        Response response = new Response("Updater", new MemoryStream());

                        //Upload File
                        Console.WriteLine("Reading File");

                        using (Stream source = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + "Updater.zip"))
                        {
                            int bytesRead = 0;
                            byte[] buffer = new byte[2048];
                            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                ((MemoryStream)response.Object).Write(buffer, 0, bytesRead);
                        }

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Update")
                    {
                        Response response = new Response("Updater", new MemoryStream());

                        //Upload File
                        Console.WriteLine("Reading File");

                        using (Stream source = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + "Update.zip"))
                        {
                            int bytesRead = 0;
                            byte[] buffer = new byte[2048];
                            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                ((MemoryStream)response.Object).Write(buffer, 0, bytesRead);
                        }

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Login")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        Login login = (Login)r.Object;

                        Console.WriteLine("Converted Login - " + stopwatch.Elapsed.TotalMilliseconds);

                        Response response = HandleLogin.Login(client, login.Username, login.Password, login.HWID);

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Response - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    /*
                    else if (r.Command == "OAuth_Finish")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        OAuth login = (OAuth)r.Object;

                        Console.WriteLine("Converted OAuth - " + stopwatch.Elapsed.TotalMilliseconds);

                        lock (Cache.OAuth)
                        {
                            if (Cache.OAuth.Any(oauth => oauth.PrivateKey == login.PrivateKey))
                            {
                                OAuth = Cache.OAuth.Find(oauth => oauth.PrivateKey == login.PrivateKey);

                                if (OAuth != null)
                                {
                                    ServerResponse = "Login Found" + "-" + Tokens.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), login.UserID, OAuth.Username);

                                    Cache.OAuth.Remove(OAuth);
                                }
                                else
                                {
                                    ServerResponse = "OAuth Null";
                                }
                            }
                            else
                            {
                                ServerResponse = "OAuth Not Found";
                            }
                        }

                        Response response = HandleLogin.Login(client, login.Username, login.Password, login.HWID);

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Response - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    */
                    else if (r.Command == "Products")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token token = (Token)r.Object;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Connect connect = new Connect();

                        Console.WriteLine("Connected to Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        List<Product> products = connect.QueryUserProducts(token.ID);

                        Console.WriteLine("Queried Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        connect.Close();

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, new Response("Products", products));

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Products")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token token = (Token)r.Object;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Connect connect = new Connect();

                        Console.WriteLine("Connected to Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        List<Product> products = connect.QueryUserProducts(token.ID);

                        Console.WriteLine("Queried Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        connect.Close();

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, new Response("Products", products));

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else
                    {
                        formatter.Serialize(strm, new Response(r.Command, " is an Invalid Command"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    
                    break;
                }


                Sleep:
                Thread.Sleep(100);
            }

            client.Close();

            Console.Write("Exited Thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
