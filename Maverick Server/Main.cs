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
using System.Security.Cryptography;

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

            new Thread(() => CacheThread()).Start();
        }

        #region Encryption
        public byte[] Encrypt(byte[] Bytes)
        {
            List<byte> temp_bytes = new List<byte>();

            int i = 0;
            foreach (byte encrypt in Bytes)
            {
                //End
                if (i + 1 < Bytes.Length)
                    temp_bytes.Add((byte)(encrypt ^ Bytes[i + 1]));
                else
                {
                    temp_bytes.Add((byte)(encrypt ^ Bytes[0]));
                    temp_bytes.Add(Bytes[i]);
                }

                i++;
            }

            return temp_bytes.ToArray();
        }
        #endregion

        public void CacheThread()
        {
            while (true)
            {
                //ReCache Server Data
                Connect connect = new Connect();

                Cache.Version = connect.Version();

                Cache.Products = connect.QueryProducts();

                connect.Close();

                //Remove Tokens
                List<Token> AuthTokens_Temp;

                lock (Cache.AuthTokens)
                {
                    AuthTokens_Temp = new List<Token>(Cache.AuthTokens);

                    foreach (Token AuthToken in AuthTokens_Temp.Where(Token => Token.LastRequest.AddMinutes(5) < DateTime.Now))
                    {
                        Console.WriteLine("Token: " + AuthToken.AuthToken + " is too old. {" + AuthToken.LastRequest.ToString() + "}");

                        Cache.AuthTokens.Remove(AuthToken);
                    }
                }

                Thread.Sleep(30000);
            }
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

                    Console.WriteLine("Request URL: " + request.Data);

                    if (context.Request.UserAgent == "Auth")
                    {
                        context.Response.ContentType = "application/octet-stream";

                        if (request.Contains("Request"))
                        {
                            if (request.Get("Request") == "Authenticate")
                            {
                                if (request.Contains("Token"))
                                {
                                    Token token = Token.GetTokenByToken(request.Get("Token"));

                                    if (token != null)
                                    {
                                        if (request.Contains("ProductID"))
                                        {
                                            int ProductID = Convert.ToInt32(request.Get("ProductID"));

                                            token.RunningProduct = ProductID;

                                            Console.WriteLine("RunningProduct Set");
                                        }

                                        if (token.IP == context.Request.RemoteEndPoint.Address.ToString())
                                        {
                                            token.LastRequest = DateTime.Now;

                                            Console.WriteLine("Updated Token Expiry");

                                            Bytes = Encoding.UTF8.GetBytes("Authenticated");

                                            Console.WriteLine("Authenticated");
                                        }
                                        else
                                        {
                                            Bytes = Encoding.UTF8.GetBytes("IP Not Authenticated - " + request.Get("Token") + ", " + context.Request.RemoteEndPoint.Address.ToString());

                                            Console.WriteLine("Not Authenticated");
                                        }
                                    }
                                    else
                                    {
                                        Bytes = Encoding.UTF8.GetBytes("Token Not Authenticated - " + request.Get("Token") + ", " + context.Request.RemoteEndPoint.Address.ToString());

                                        Console.WriteLine("Not Authenticated");
                                    }
                                }
                                else if (Cache.AuthTokens.Any(token => token.IP == context.Request.RemoteEndPoint.Address.ToString()))
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Authenticated");

                                    Console.WriteLine("Authenticated");
                                }
                                else
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Not Authenticated - " + context.Request.RemoteEndPoint.Address.ToString());

                                    Console.WriteLine("Not Authenticated");
                                }
                            }
                            else if (request.Get("Request") == "Download")
                            {
                                if (request.Contains("Token"))
                                {
                                    if (Cache.AuthTokens.Any(token => token.AuthToken == request.Get("Token") && token.IP == context.Request.RemoteEndPoint.Address.ToString()))
                                    {
                                        if (request.Contains("ProductID"))
                                        {
                                            int ProductID = Convert.ToInt32(request.Get("ProductID"));

                                            List<Product> products = Connect.QueryUserProducts(Cache.AuthTokens.Find(token => token.AuthToken == request.Get("Token")).Member.UserID);

                                            if (products.Any(product => product.Id == ProductID))
                                            {
                                                Bytes = File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\DLLs\\" + Cache.Products.Find(product => product.Id == ProductID).Name + ".dll");

                                                Console.WriteLine("Product Downloaded");
                                            }
                                            else
                                            {
                                                Bytes = Encoding.UTF8.GetBytes("Product Un-Owned");

                                                Console.WriteLine("Product Un-Owned");
                                            }
                                        }
                                        else
                                        {
                                            Bytes = Encoding.UTF8.GetBytes("No Product ID Provided - " + request.Data);
                                        }
                                    }
                                    else
                                    {
                                        Bytes = Encoding.UTF8.GetBytes("Not Authenticated - Token Not Found");
                                    }
                                }
                                else
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Not Authenticated - No Token");
                                }
                            }
                            else
                            {
                                Bytes = Encoding.UTF8.GetBytes("No Request Provided - " + request.Data);
                            }
                        }

                        Bytes = Encrypt(Bytes);
                    }
                    else
                    {
                        context.Response.ContentType = "text/plain";

                        if (request.Contains("Request"))
                        {
                            if (request.Get("Request") == "OAuth")
                            {
                                if (request.Contains("UserID"))
                                {
                                    int UserID = Convert.ToInt32(request.Get("UserID"));

                                    if (request.Contains("Username"))
                                    {
                                        string Username = request.Get("Username");

                                        if (request.Contains("Avatar"))
                                        {
                                            string Avatar = request.Get("Avatar");

                                            if (request.Contains("PrivateKey"))
                                            {
                                                string PrivateKey = request.Get("PrivateKey");

                                                if (request.Contains("HWID"))
                                                {
                                                    string HWID = request.Get("HWID");

                                                    lock (Cache.OAuths)
                                                    {
                                                        if (!Cache.OAuths.Any(oauth => oauth.Member.UserID == UserID && oauth.PrivateKey == PrivateKey && oauth.HWID == HWID))
                                                            Cache.OAuths.Add(new OAuth(new Member(UserID, Username, Avatar), PrivateKey, HWID));

                                                        Bytes = Encoding.UTF8.GetBytes("Login Found");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Missing Argument - HWID");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Missing Argument - PrivateKey");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Missing Argument - Avatar");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Missing Argument - Username");
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
                                    Output += (product.Id == 1 ? "" + product.Name : "%split%" + product.Name) + "%delimiter%" + Cache.AuthTokens.Count(token => token.RunningProduct == product.Id).ToString();
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
                    }

                    //application/octet-stream
                    context.Response.ContentLength64 = Bytes.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AppendHeader("Cache-Control", "no-cache");

                    context.Response.OutputStream.Write(Bytes, 0, Bytes.Length);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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

                Thread.Sleep(250);
            }
        }

        public void ClientThread(TcpClient client)
        {
            while (true)
            {
                NetworkStream strm = client.GetStream();

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

                        string Version = Cache.Version;

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        formatter.Serialize(strm, new Response("Version", Version));

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Updater")
                    {
                        Response response = new Response("Updater", new MemoryStream());

                        //Upload File
                        Console.WriteLine("Reading File");

                        try
                        {
                            using (Stream source = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + "Updater.zip"))
                            {
                                int bytesRead = 0;
                                byte[] buffer = new byte[2048];
                                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                    ((MemoryStream)response.Object).Write(buffer, 0, bytesRead);
                            }
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            Console.WriteLine(ex.ToString());
                        }

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Update")
                    {
                        Response response = new Response("Update", new MemoryStream());

                        //Upload File
                        Console.WriteLine("Reading File");

                        try
                        {
                            using (Stream source = File.OpenRead(Environment.CurrentDirectory + "\\Products\\" + "Update.zip"))
                            {
                                int bytesRead = 0;
                                byte[] buffer = new byte[2048];
                                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                    ((MemoryStream)response.Object).Write(buffer, 0, bytesRead);
                            }
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            Console.WriteLine(ex.ToString());
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
                                    response = new Response("Login", Token.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), new Member(OAuths.Member.UserID, OAuths.Member.Username, OAuths.Member.AvatarURL)));
                                
                                    Cache.OAuths.Remove(OAuths);
                                }
                                else
                                {
                                    response = new Response("OAuth", "Not Found");
                                }
                            }
                            else
                            {
                                response = new Response("OAuth", "Not Authenticated");
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

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            Console.WriteLine("Querying Data - " + stopwatch.Elapsed.TotalMilliseconds);

                            List<Product> products = Connect.QueryUserProducts(AuthToken.Member.UserID);

                            formatter.Serialize(strm, new Response("Products", products));

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }
                        else
                        {
                            formatter.Serialize(strm, new Response("Products", "Not Authenticated", true));

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }
                    }
                    else if (r.Command == "Download")
                    {
                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;
                        Product product = (Product)r.Object;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            Console.WriteLine("Queried Data - " + stopwatch.Elapsed.TotalMilliseconds);

                            List<Product> products = Connect.QueryUserProducts(AuthToken.Member.UserID);

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

                                Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                            }
                            else
                            {
                                formatter.Serialize(strm, new Response("Download", "Product Un-Owned", true));
                            }

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }
                        else
                        {
                            formatter.Serialize(strm, new Response("Download", "Not Authenticated", true));

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }
                    }
                    else if (r.Command == "Authenticate")
                    {
                        Response response = new Response("Authenticate");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            if ((int)r.Object != 0)
                            {
                                AuthToken.RunningProduct = (int)r.Object;

                                Console.WriteLine("Queried Data - " + stopwatch.Elapsed.TotalMilliseconds);

                                if (Connect.QueryUserProducts(AuthToken.Member.UserID).Any(product => product.Id == AuthToken.RunningProduct))
                                {
                                    AuthToken.LastRequest = DateTime.Now;

                                    response = new Response("Authenticate", "Authenticated", false);
                                }
                                else
                                {
                                    response = new Response("Authenticate", "Product Un-Owned", true);
                                }

                                Console.WriteLine("Queried Database - " + stopwatch.Elapsed.TotalMilliseconds);

                                Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);
                            }
                            else
                            {
                                response = new Response("Authenticate", "Product ID not Provided", true);
                            }
                        }
                        else
                        {
                            response = new Response("Authenticate", "Not Authenticated", true);
                        }

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else
                    {
                        formatter.Serialize(strm, new Response(r.Command, "Invalid Command", true));
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
