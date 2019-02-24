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
using System.Web;

namespace Main
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            //Listener Events for Unhandled Exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, arg) => HandleUnhandledException(arg.ExceptionObject as Exception);
        }

        private void HandleUnhandledException(Exception ex)
        {
            this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText(ex.ToString() + Environment.NewLine); this.textBox1.Refresh(); });
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

                List<HTTP_Connection> HTTP_Connections_Temp;

                lock (Cache.HTTP_Connections)
                {
                    HTTP_Connections_Temp = new List<HTTP_Connection>(Cache.HTTP_Connections);

                    foreach (HTTP_Connection connection in HTTP_Connections_Temp.Where(Connection => Connection == null || Connection.HttpListenerContext == null || Connection.Exited || Connection.LastRequestDate.AddMinutes(5) < DateTime.Now))
                    {
                        this.BeginInvoke((MethodInvoker)delegate { textBox2.AppendText("Connection: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine); this.textBox2.Refresh(); });

                        try
                        {
                            if (connection.Thread != null)
                            {
                                connection.Thread.Abort();

                                connection.Thread = null;
                            }

                            if (connection.HttpListenerContext != null)
                            {
                                connection.HttpListenerContext.Response.Close();

                                connection.HttpListenerContext = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke((MethodInvoker)delegate { textBox2.AppendText("Disposing HTTP Socket Failed: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine); this.textBox2.Refresh(); });
                            this.BeginInvoke((MethodInvoker)delegate { textBox2.AppendText("Error: " + ex.ToString() + Environment.NewLine); this.textBox2.Refresh(); });
                        }

                        Cache.HTTP_Connections.Remove(connection);
                    }
                }

                List<TCP_Connection> TCP_Connections_Temp;

                lock (Cache.TCP_Connections)
                {
                    TCP_Connections_Temp = new List<TCP_Connection>(Cache.TCP_Connections);

                    foreach (TCP_Connection connection in TCP_Connections_Temp.Where(Connection => Connection == null || Connection.TcpClient == null || !Connection.TcpClient.Connected || Connection.LastRequestDate.AddMinutes(5) < DateTime.Now))
                    {
                        this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText("Connection: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine); this.textBox1.Refresh(); });

                        try
                        {
                            if (connection.Thread != null)
                            {
                                connection.Thread.Abort();

                                connection.Thread = null;
                            }

                            if (connection.TcpClient != null)
                            {
                                connection.TcpClient.Close();

                                connection.TcpClient = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText("Disposing Socket Failed: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine); this.textBox1.Refresh(); });
                            this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText("Error: " + ex.ToString() + Environment.NewLine); this.textBox1.Refresh(); });
                        }

                        Cache.TCP_Connections.Remove(connection);
                    }
                }

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

                List<OAuth> OAuth_Temp;

                lock (Cache.OAuths)
                {
                    OAuth_Temp = new List<OAuth>(Cache.OAuths);

                    foreach (OAuth OAuth in OAuth_Temp.Where(OAuth => OAuth.CreationDate.AddMinutes(5) < DateTime.Now))
                    {
                        Console.WriteLine("OAUth: " + OAuth.Member.UserID + ", " + OAuth.PrivateKey + " is too old. {" + OAuth.CreationDate.ToString() + "}");

                        Cache.OAuths.Remove(OAuth);
                    }
                }

                this.BeginInvoke((MethodInvoker)delegate 
                {
                    VersionLabel.Text = "Version: " + Cache.Version;
                    TCPInstancesLabel.Text = "TCP Instances: " + Cache.TCP_Connections.Count;
                    HTTPInstancesLabel.Text = "HTTP Instances: " + Cache.HTTP_Connections.Count;
                    OAuthInstancesLabel.Text = "OAuth Instances: " + Cache.OAuths.Count;
                    AuthTokenInstancesLabel.Text = "AuthToken Instances: " + Cache.AuthTokens.Count;
                    ProductInstancesLabel.Text = "Number of Products in Cache: " + Cache.Products.Count;

                    this.Refresh();
                });

                Thread.Sleep(10000);
            }
        }

        public void HTTPServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();

            while (true)
            {
                Console.WriteLine("Opened HTTP Socket");

                HTTP_Connection connection = new HTTP_Connection(listener.GetContext());

                connection.Thread = new Thread(() => HTTP_Thread(connection));

                connection.Thread.Start();

                Cache.HTTP_Connections.Add(connection);

                this.BeginInvoke((MethodInvoker)delegate { textBox2.AppendText("Starting HTTP Connection - " + connection.IP + Environment.NewLine); this.textBox2.Refresh(); });

                Thread.Sleep(250);
            }
        }

        private void HTTP_Thread(HTTP_Connection connection)
        {
            byte[] Bytes = new byte[] { };

            HttpListenerContext context = connection.HttpListenerContext;

            try
            {
                connection.LastRequestDate = DateTime.Now;

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
                context.Response.Close();
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)delegate { HTTPExceptions.AppendText("Exception for HTTP Connection: " + connection.IP + Environment.NewLine + ex.ToString()); this.HTTPExceptions.Refresh(); });
            }

            this.BeginInvoke((MethodInvoker)delegate { textBox2.AppendText("Exited HTTP Thread - " + connection.IP + Environment.NewLine); this.textBox2.Refresh(); });

            connection.Exited = true;
        }

        public void TCPServer()
        {
            TcpListener server = new TcpListener(6060);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Opened Socket");

                TCP_Connection connection = new TCP_Connection(client);

                connection.Thread = new Thread(() => TCP_Thread(connection));

                connection.Thread.Start();

                Cache.TCP_Connections.Add(connection);

                this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText("Starting Connection - " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + Environment.NewLine); this.textBox1.Refresh(); });

                Thread.Sleep(250);
            }
        }

        public void TCP_Thread(TCP_Connection connection)
        {
            TcpClient client = connection.TcpClient;

            while (true)
            {
                if (client == null)
                    goto Close;
                else if (!client.Connected)
                    goto Close;

                NetworkStream strm = client.GetStream();

                if (strm == null)
                    goto Close;
                else if (!strm.DataAvailable)
                    goto Sleep;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                try
                {
                    IFormatter formatter = new BinaryFormatter();

                    Console.WriteLine("Request Init");

                    Request r = (Request)formatter.Deserialize(strm); // you have to cast the deserialized object 

                    Console.WriteLine("Recieved and Deserialized Request - " + stopwatch.Elapsed.TotalMilliseconds);

                    connection.LastRequestDate = DateTime.Now;

                    //Response
                    Console.WriteLine("Recieved: " + r.Command);

                    if (r.Command == "Version")
                    {
                        Response response = new Response("Version");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        response.Object = Cache.Version;

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Updater")
                    {
                        Response response = new Response("Updater", new MemoryStream());

                        //Upload File
                        Console.WriteLine("Reading File");

                        try
                        {
                            response.Object = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + "Updater.zip"));
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + ex.ToString()); this.TCPExceptions.Refresh(); });

                            Console.WriteLine(ex.ToString());
                        }

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

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
                            response.Object = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + "Update.zip"));
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + ex.ToString()); this.TCPExceptions.Refresh(); });

                            Console.WriteLine(ex.ToString());
                        }

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Login")
                    {
                        Response response = new Response("Login");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        Login login = (Login)r.Object;

                        Console.WriteLine("Converted Login - " + stopwatch.Elapsed.TotalMilliseconds);

                        int UserID = 0;
                        string AvatarURL = "";
                        string Response = new WebClient().DownloadString("http://api.maverickcheats.eu/community/maverickcheats/login.php?Username=" + HttpUtility.UrlEncode(login.Username) + "&Password=" + HttpUtility.UrlEncode(login.Password) + "&HWID=" + HttpUtility.UrlEncode(login.HWID));

                        if (Response.Contains("%delimiter%"))
                        {
                            if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None).Length >= 2)
                            {
                                if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[0] == "Login Found")
                                {
                                    if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1] != "")
                                    {
                                        UserID = int.TryParse(Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1], out _) ? int.Parse(Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1]) : 0;

                                        if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[2] != "")
                                            AvatarURL = Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[2];

                                        response = new Response("Login", Token.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), new Member(UserID, login.Username, AvatarURL)));
                                    }
                                    else
                                    {
                                        response = new Response("Login", "Login Failed - UserID Query Failed", true);
                                    }
                                }
                                else
                                {
                                    response = new Response("Login", "Login Failed - Login not Found", true);
                                }
                            }
                            else
                            {
                                response = new Response("Login", "Internal Error - No Data Provided", true);
                            }
                        }
                        else
                        {
                            response = new Response("Login", "Error - " + Response, true);
                        }

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Response - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "OAuth")
                    {
                        Response response = new Response("OAuth");

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

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Response - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Products")
                    {
                        Response response = new Response("Products");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            Console.WriteLine("Querying Data - " + stopwatch.Elapsed.TotalMilliseconds);

                            response.Object = Connect.QueryUserProducts(AuthToken.Member.UserID);

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }
                        else
                        {
                            response = new Response("Products", "Not Authenticated", true);

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                        }

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);
                    }
                    else if (r.Command == "Download")
                    {
                        Response response = new Response("Download", new MemoryStream());

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

                                response.Object = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + product.File));
                            }
                            else
                            {
                                response = new Response("Download", "Product Un-Owned", true);
                            }
                        }
                        else
                        {
                            response = new Response("Download", "Not Authenticated", true);
                        }

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);
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

                                    response = new Response("Authenticate", "Authenticated");
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

                        if (response.Error)
                            this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Log")
                    {
                        Response response = new Response("Log");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        //Get Logs, Write Logs

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else
                    {
                        this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + "Invalid Command - " + r.Command); this.TCPExceptions.Refresh(); });

                        formatter.Serialize(strm, new Response(r.Command, "Invalid Command", true));
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke((MethodInvoker)delegate { TCPExceptions.AppendText("Exception for TCP Connection: " + connection.IP + Environment.NewLine + ex.ToString() + Environment.NewLine); this.TCPExceptions.Refresh(); });

                    break;
                }

                Sleep:
                Thread.Sleep(100);
            }

            Close:
            this.BeginInvoke((MethodInvoker)delegate { textBox1.AppendText("Closing Connection - " + connection.IP + Environment.NewLine); this.textBox1.Refresh(); });

            Console.Write("Exited Thread " + Thread.CurrentThread.ManagedThreadId);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            VersionLabel.Text = "Version: " + Cache.Version;
            TCPInstancesLabel.Text = "TCP Instances: " + Cache.TCP_Connections.Count;
            HTTPInstancesLabel.Text = "HTTP Instances: " + Cache.HTTP_Connections.Count;
            OAuthInstancesLabel.Text = "OAuth Instances: " + Cache.OAuths.Count;
            AuthTokenInstancesLabel.Text = "AuthToken Instances: " + Cache.AuthTokens.Count;
            ProductInstancesLabel.Text = "Number of Products in Cache: " + Cache.Products.Count;

            this.Refresh();
        }
    }
}
