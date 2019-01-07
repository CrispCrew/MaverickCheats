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
                byte[] Bytes = new byte[] { };

                HttpListenerContext context = listener.GetContext();

                try
                {
                    Functions.Request request = new Functions.Request(context.Request.Url.Query);

                    if (request.Contains("Request"))
                    {
                        if (request.Get("Request") == "Authenticate")
                        {
                            if (request.Contains("Token"))
                            {
                                if (Cache.AuthTokens.Any(token => token.AuthToken == request.Get("Token") && token.IP == context.Request.RemoteEndPoint.Address.ToString()))
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Authenticated");
                                }
                                else
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Not Authenticated - " + request.Get("Token") + "," + context.Request.RemoteEndPoint.Address.ToString());
                                }
                            }
                            else
                            {
                                Bytes = Encoding.UTF8.GetBytes("No Token Provided - " + request.Get("Token"));
                            }
                        }
                        else if (request.Get("Request") == "OAuth")
                        {
                            if (request.Contains("UserID"))
                            {
                                int UserID = Convert.ToInt32(request.Get("UserID"));

                                if (request.Contains("Username"))
                                {
                                    string Username = request.Get("Username");

                                    if (request.Contains("PrivateKey"))
                                    {
                                        string PrivateKey = request.Get("PrivateKey");

                                        if (request.Contains("HWID"))
                                        {
                                            string HWID = request.Get("HWID");

                                            lock (Cache.OAuths)
                                            {
                                                if (!Cache.OAuths.Any(oauth => oauth.UserID == UserID && oauth.PrivateKey == PrivateKey && oauth.HWID == HWID))
                                                    Cache.OAuths.Add(new OAuth(UserID, Username, PrivateKey, HWID));

                                                Bytes = Encoding.UTF8.GetBytes("Login Found");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Missing Argument - UserID");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Missing Argument - UserID");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Missing Argument - UserID");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Missing Argument - UserID");
                            }
                        }
                        else if (request.Get("Request") == "Products")
                        {
                            string Output = "";

                            foreach (Product product in Cache.Products)
                            {
                                Output += (product.Id == 1 ? "" + product.Id : "%split%" + product.Id) + "%delimiter%" + product.Name + "%delimiter%" + product.File + "%delimiter%" + product.ProcessName + "%delimiter%" + product.Status + "%delimiter%" + product.Version + "%delimiter%" + product.Free + "%delimiter%" + product.AutoLaunchMem + "%delimiter%" + product.Internal;
                            }

                            Bytes = Encoding.UTF8.GetBytes(Output);
                        }
                        else if (request.Get("Request") == "OnlineCounts")
                        {
                            string Output = "";

                            foreach (Product product in Cache.Products)
                            {
                                Output += (product.Id == 1 ? "" + product.Name : "%split%" + product.Name) + "%delimiter%" + Cache.AuthTokens.Count(token => token.LastDevice == product.Name).ToString();
                            }

                            Bytes = Encoding.UTF8.GetBytes(Output);
                        }
                        else
                        {
                            Bytes = Encoding.UTF8.GetBytes("Invalid Request - " + request.Data);
                        }
                    }
                    else
                    {
                        Bytes = Encoding.UTF8.GetBytes("No Request Provided - " + request.Data);
                    }

                    //
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = Bytes.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));

                    context.Response.OutputStream.Write(Bytes, 0, Bytes.Length);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    Bytes = Encoding.UTF8.GetBytes(ex.ToString());

                    //output Error
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = Bytes.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));

                    context.Response.OutputStream.Write(Bytes, 0, Bytes.Length);

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.OutputStream.Flush();
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
                        Response response = new Response("Update", new MemoryStream());

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
                    else if (r.Command == "OAuth")
                    {
                        Response response = new Response();

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.OAuth login = (NetworkTypes.OAuth)r.Object;

                        Console.WriteLine("Converted OAuth - " + stopwatch.Elapsed.TotalMilliseconds);

                        lock (Cache.OAuths)
                        {
                            if (Cache.OAuths.Any(oauth => oauth.PrivateKey == login.PrivateKey))
                            {
                                OAuth OAuths = Cache.OAuths.Find(oauth => oauth.PrivateKey == login.PrivateKey);

                                if (OAuths != null)
                                {
                                    response = new Response("Login Found", new Token().GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), OAuths.UserID, OAuths.Username));

                                    Cache.OAuths.Remove(OAuths);
                                }
                                else
                                {
                                    response = new Response("OAuth Not Found - NULL");
                                }
                            }
                            else
                            {
                                response = new Response("OAuth Not Found");
                            }
                        }

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Response - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Products")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

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

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

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
                    else if (r.Command == "Download")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;
                        Product product = (Product)r.Object;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Connect connect = new Connect();

                        Console.WriteLine("Connected to Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        List<Product> products = connect.QueryUserProducts(token.ID);

                        Console.WriteLine("Queried Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        connect.Close();

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        if (products.Any(product_search => product_search.Id == product.Id))
                        {
                            //Upload File
                            Console.WriteLine("Reading File");

                            MemoryStream stream = new MemoryStream();
                            using (Stream source = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + product.File))
                            {
                                int bytesRead = 0;
                                byte[] buffer = new byte[2048];
                                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                    stream.Write(buffer, 0, bytesRead);
                            }

                            formatter.Serialize(strm, new Response("Download", stream));
                        }
                        else
                        {
                            formatter.Serialize(strm, new Response("Download", "Not Owned"));
                        }

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
