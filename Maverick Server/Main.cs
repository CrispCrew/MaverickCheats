using NetworkTypes;

using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using MaverickServer.Database;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Web;

namespace Main
{
    public partial class Main : Form
    {
        //Requests
        public static List<string> RequestLogEnteries = new List<string>();
        public static List<string> RateLimitingLogEnteries = new List<string>();
        //TCP
        public static List<string> TCPLogEnteries= new List<string>();
        public static List<string> TCPExceptionLogEnteries = new List<string>();
        //HTTP
        public static List<string> HTTPLogEnteries = new List<string>();
        public static List<string> HTTPExceptionLogEnteries = new List<string>();

        public Main()
        {
            InitializeComponent();

            //Listener Events for Unhandled Exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, arg) => HandleUnhandledException(arg.ExceptionObject as Exception);
        }

        private void HandleUnhandledException(Exception ex)
        {
            AppendRequestLogs(ex.ToString());
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.CurrentDirectory + "\\" + "Tokens.data"))
                    using (FileStream FileStream = new FileStream(Environment.CurrentDirectory + "\\" + "Tokens.data", FileMode.Open, FileAccess.Read))
                        lock (Cache.AuthTokens)
                            Cache.AuthTokens = (List<Token>)new BinaryFormatter().Deserialize(FileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Loading Tokens Failed" + Environment.NewLine + ex.ToString());
            }

            new Thread(() => LogThread()).Start();

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

            Bytes = temp_bytes.ToArray();

            temp_bytes.Clear();

            return Bytes;
        }
        #endregion

        public void LogThread()
        {
            while (true)
            {
                string RequestLogText = "";
                string RateLimitingLogText = "";
                string TCPLogText = "";
                string TCPExceptionLogText = "";
                string HTTPLogText = "";
                string HTTPExceptionLogText = "";

                #region RequestLogs
                lock (RequestLogEnteries)
                {
                    foreach (string RequestLog in RequestLogEnteries)
                        RequestLogText += RequestLog;
                }
                #endregion

                #region RateLimitingLogs
                lock (RateLimitingLogEnteries)
                {
                    foreach (string RateLimitingLog in RateLimitingLogEnteries)
                        RateLimitingLogText += RateLimitingLog;
                }
                #endregion

                #region TCPLogEnteries
                lock (TCPLogEnteries)
                {
                    foreach (string TCPLog in TCPLogEnteries)
                        TCPLogText += TCPLog;
                }
                #endregion

                #region TCPExceptionLogEnteries
                lock (TCPExceptionLogEnteries)
                {
                    foreach (string TCPExceptionLog in TCPExceptionLogEnteries)
                        TCPExceptionLogText += TCPExceptionLog;
                }
                #endregion

                #region HTTPLogEnteries
                lock (HTTPLogEnteries)
                {
                    foreach (string HTTPLog in HTTPLogEnteries)
                        HTTPLogText += HTTPLog;
                }
                #endregion

                #region HTTPExceptionLogEnteries
                lock (HTTPExceptionLogEnteries)
                {
                    foreach (string HTTPExceptionLog in HTTPExceptionLogEnteries)
                        HTTPExceptionLogText += HTTPExceptionLog;
                }
                #endregion

                this.Invoke((MethodInvoker)delegate {
                    //RequestLogText
                    if ((RequestLogs.TextLength + RequestLogText.Length) > RequestLogs.MaxLength)
                        RequestLogs.Text = (RequestLogs.Text + RequestLogText).Substring((RequestLogs.TextLength + RequestLogText.Length) - RequestLogs.MaxLength);
                    else
                        RequestLogs.Text += RequestLogText;

                    RequestLogs.SelectionStart = RequestLogs.TextLength;
                    RequestLogs.ScrollToCaret();

                    //RateLimitingLogText
                    if ((RateLimitingLogs.TextLength + RateLimitingLogText.Length) > RateLimitingLogs.MaxLength)
                        RateLimitingLogs.Text = (RateLimitingLogs.Text + RateLimitingLogText).Substring(RateLimitingLogs.TextLength + RateLimitingLogText.Length - RateLimitingLogs.MaxLength);
                    else
                        RateLimitingLogs.Text += RateLimitingLogText;

                    RateLimitingLogs.SelectionStart = RateLimitingLogs.TextLength;
                    RateLimitingLogs.ScrollToCaret();

                    //TCPLogText
                    if ((TCPLogs.TextLength + TCPLogText.Length) > TCPLogs.MaxLength)
                        TCPLogs.Text = (TCPLogs.Text + TCPLogText).Substring(TCPLogs.TextLength + TCPLogText.Length - TCPLogs.MaxLength);
                    else
                        TCPLogs.Text += TCPLogText;

                    TCPLogs.SelectionStart = TCPLogs.TextLength;
                    TCPLogs.ScrollToCaret();

                    //TCPExceptionLogText
                    if ((TCPExceptions.TextLength + TCPExceptionLogText.Length) > TCPExceptions.MaxLength)
                        TCPExceptions.Text = (TCPExceptions.Text + TCPExceptionLogText).Substring(TCPExceptions.TextLength + TCPExceptionLogText.Length - TCPExceptions.MaxLength);
                    else
                        TCPExceptions.Text += TCPExceptionLogText;

                    TCPExceptions.SelectionStart = TCPExceptions.TextLength;
                    TCPExceptions.ScrollToCaret();

                    //HTTPLogText
                    if ((HTTPLogs.TextLength + HTTPLogText.Length) > HTTPLogs.MaxLength)
                        HTTPLogs.Text = (HTTPLogs.Text + HTTPLogText).Substring(HTTPLogs.TextLength + HTTPLogText.Length - HTTPLogs.MaxLength);
                    else
                        HTTPLogs.Text += HTTPLogText;

                    HTTPLogs.SelectionStart = HTTPLogs.TextLength;
                    HTTPLogs.ScrollToCaret();

                    //HTTPExceptionLogText
                    if ((HTTPExceptions.TextLength + HTTPExceptionLogText.Length) > HTTPExceptions.MaxLength)
                        HTTPExceptions.Text = (HTTPExceptions.Text + HTTPExceptionLogText).Substring(HTTPExceptions.TextLength + HTTPExceptionLogText.Length - HTTPExceptions.MaxLength);
                    else
                        HTTPExceptions.Text += HTTPExceptionLogText;

                    HTTPExceptions.SelectionStart = HTTPExceptions.TextLength;
                    HTTPExceptions.ScrollToCaret();
                });

                //Requests
                RequestLogEnteries.Clear();
                RateLimitingLogEnteries.Clear();
                //TCP
                TCPLogEnteries.Clear();
                TCPExceptionLogEnteries.Clear();
                //HTTP
                HTTPLogEnteries.Clear();
                HTTPExceptionLogEnteries.Clear();

                Thread.Sleep(1000);
            }
        }

        public void HTTPServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();

            while (true)
            {
                HttpListenerContext HttpListenerContext = listener.GetContext();

                Console.WriteLine("Opened HTTP Socket");

                HTTP_Connection connection = new HTTP_Connection(HttpListenerContext);

                connection.Thread = new Thread(() => HTTP_Thread(connection));

                connection.Thread.Start();

                Cache.HTTP_Connections.Add(connection);

                AppendHTTPLogs("Starting HTTP Connection - " + connection.IP + Environment.NewLine);

                Thread.Sleep(100);
            }
        }

        private void HTTP_Thread(HTTP_Connection connection)
        {
            byte[] Bytes = new byte[] { };

            HttpListenerContext context = connection.HttpListenerContext;

            try
            {
                if (connection.IP != "159.203.7.208")
                {
                    if (Cache.RateLimits.Any(RateLimits => RateLimits.IP == connection.IP))
                    {
                        RateLimit RateLimit = Cache.RateLimits.FirstOrDefault(ratelimits => ratelimits.IP == connection.IP);
                        RateLimit.APIRequest();

                        if (RateLimit.IsLimited())
                        {
                            AppendHTTPLogs("RateLimiting HTTP Connection - " + connection.IP + " due to {Connections: + " + RateLimit.Connections() + ", Requests: " + RateLimit.Requests() + "}" + Environment.NewLine);

                            context.Response.ContentType = "text/plain";

                            Bytes = Encoding.UTF8.GetBytes("RateLimited: (UTC Time): " + RateLimit.RateLimitedUntil.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"));

                            goto RequestEnd;
                        }
                    }
                    else
                    {
                        Cache.RateLimits.Add(new RateLimit(connection.IP));
                    }
                }

                connection.LastRequestDate = DateTime.Now;

                Functions.Request request = new Functions.Request(context.Request.Url.Query);

                string IP = context.Request.RemoteEndPoint.Address.IsIPv4MappedToIPv6 ? context.Request.RemoteEndPoint.Address.MapToIPv4().ToString() : context.Request.RemoteEndPoint.Address.ToString();

                Console.WriteLine("Request URL: " + request.Data + " Request IP: " + IP + " Request IP was IPv6: " + context.Request.RemoteEndPoint.Address.IsIPv4MappedToIPv6.ToString());

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

                                    if (token.IP == IP)
                                    {
                                        token.LastRequest = DateTime.Now;

                                        Console.WriteLine("Updated Token Expiry");

                                        Bytes = Encoding.UTF8.GetBytes("Authenticated");

                                        Console.WriteLine("Authenticated");
                                    }
                                    else
                                    {
                                        Bytes = Encoding.UTF8.GetBytes("IP Not Authenticated - " + request.Get("Token") + ", " + IP);

                                        Console.WriteLine("Not Authenticated");
                                    }
                                }
                                else
                                {
                                    Bytes = Encoding.UTF8.GetBytes("Token Not Authenticated - " + request.Get("Token") + ", " + IP);

                                    Console.WriteLine("Not Authenticated");
                                }
                            }
                            else if (Cache.AuthTokens.Any(token => token.IP == IP))
                            {
                                Bytes = Encoding.UTF8.GetBytes("Authenticated");

                                Console.WriteLine("Authenticated");
                            }
                            else
                            {
                                Bytes = Encoding.UTF8.GetBytes("Not Authenticated - " + IP);

                                Console.WriteLine("Not Authenticated");
                            }
                        }
                        else if (request.Get("Request") == "Download")
                        {
                            if (request.Contains("Token"))
                            {
                                if (Cache.AuthTokens.Any(token => token.AuthToken == request.Get("Token") && token.IP == IP))
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

                RequestEnd:
                //application/octet-stream
                context.Response.ContentLength64 = Bytes.Length;
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.AppendHeader("Cache-Control", "no-cache");

                context.Response.OutputStream.Write(Bytes, 0, Bytes.Length);
                context.Response.OutputStream.Flush();
                context.Response.OutputStream.Close();

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.Close();
            }
            catch (Exception ex)
            {
                AppendHTTPExceptions("Exception for HTTP Connection: " + connection.IP + Environment.NewLine + ex.ToString());
            }

            AppendHTTPLogs("Exited HTTP Thread - " + connection.IP + Environment.NewLine);

            Bytes = null;

            connection.Close = true;
        }

        public void TCPServer()
        {
            TcpListener server = new TcpListener(6060);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                string IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

                Console.WriteLine("Opened Socket");

                TCP_Connection connection = new TCP_Connection(client);

                connection.Thread = new Thread(() => TCP_Thread(connection));

                connection.Thread.Start();

                Cache.TCP_Connections.Add(connection);

                AppendTCPLogs("Starting Connection - " + connection.IP + Environment.NewLine);

                Thread.Sleep(100);
            }
        }

        public void TCP_Thread(TCP_Connection connection)
        {
            TcpClient client = connection.TcpClient;

            while (true)
            {
                if (client == null)
                    break;
                else if (!client.Connected)
                    break;

                NetworkStream strm = client.GetStream();

                if (strm == null)
                    break;
                else if (!strm.DataAvailable)
                    goto Sleep;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                try
                {
                    AppendRequestLogs(Environment.NewLine + "BinaryFormatter Init Started: " + connection.IP + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    IFormatter formatter = new BinaryFormatter();

                    Console.WriteLine("Request Init");

                    AppendRequestLogs(Environment.NewLine + "Request Init Started: " + connection.IP + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    Request r = (Request)formatter.Deserialize(strm); // you have to cast the deserialized object 

                    Console.WriteLine("Recieved and Deserialized Request - " + stopwatch.Elapsed.TotalMilliseconds);

                    AppendRequestLogs(Environment.NewLine + "Request Init Ended: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((r.Object != null && r.Object is string) ? (string)r.Object : "r.Object is NULL or not a String") + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    connection.LastRequestDate = DateTime.Now;

                    AppendRequestLogs(Environment.NewLine + "RateLimiting Check Started: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((r.Object != null && r.Object is string) ? (string)r.Object : "r.Object is NULL or not a String") + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    //RateLimit
                    if (Cache.RateLimits.Any(ratelimits => ratelimits.IP == connection.IP))
                    {
                        RateLimit RateLimit = Cache.RateLimits.FirstOrDefault(ratelimits => ratelimits.IP == connection.IP);
                        RateLimit.APIRequest();

                        if (RateLimit.IsLimited())
                        {
                            AppendTCPLogs("RateLimiting TCP Requests - " + connection.IP + " due to {Connections: + " + RateLimit.Connections() + ", Requests: " + RateLimit.Requests() + "}" + Environment.NewLine);

                            Response response = new Response("RateLimited", RateLimit.RateLimitedUntil, true);

                            Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                            if (response.Error)
                                AppendTCPLogs("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                            formatter.Serialize(strm, response);

                            Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);

                            break;
                        }
                    }
                    else
                    {
                        Cache.RateLimits.Add(new RateLimit(connection.IP));
                    }

                    AppendRequestLogs(Environment.NewLine + "RateLimiting Check Ended: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((r.Object != null && r.Object is string) ? (string)r.Object : "r.Object is NULL or not a String") + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    //Response
                    Console.WriteLine("Recieved: " + r.Command);

                    AppendRequestLogs(Environment.NewLine + "Request Started: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((r.Object != null && r.Object is string) ? (string)r.Object : "r.Object is NULL or not a String") + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);

                    if (r.Command == "Version")
                    {
                        Response response = new Response("Version");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        response.Object = Cache.Version;

                        Console.WriteLine("Closed Database Database - " + stopwatch.Elapsed.TotalMilliseconds);

                        if (response.Error)
                            AppendTCPLogs("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "Updater")
                    {
                        MemoryStream MemoryStream = new MemoryStream();

                        Response response = new Response("Updater", MemoryStream);

                        //Upload File
                        Console.WriteLine("Reading File");

                        try
                        {
                            MemoryStream = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + "Updater.zip"));

                            response.Object = MemoryStream;
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            AppendTCPExceptions("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + ex.ToString());

                            Console.WriteLine(ex.ToString());
                        }

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);

                        response.Object = null;

                        MemoryStream.Dispose();
                    }
                    else if (r.Command == "Update")
                    {
                        MemoryStream MemoryStream = new MemoryStream();

                        Response response = new Response("Update", MemoryStream);

                        //Upload File
                        Console.WriteLine("Reading File");

                        try
                        {
                            MemoryStream = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + "Update.zip"));

                            response.Object = MemoryStream;
                        }
                        catch (Exception ex)
                        {
                            response = new Response("Update", "Server Error", true);

                            AppendTCPExceptions("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + ex.ToString());

                            Console.WriteLine(ex.ToString());
                        }

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);

                        response.Object = null;

                        MemoryStream.Dispose();
                    }
                    else if (r.Command == "Login")
                    {
                        Response response = new Response("Login");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        Login login = (Login)r.Object;

                        Console.WriteLine("Converted Login - " + stopwatch.Elapsed.TotalMilliseconds);

                        int UserID = 0;
                        string AvatarURL = "";
                        string Response = new WebClient().DownloadString("http://api.maverickCheats.net/community/maverickcheats/login.php?Username=" + HttpUtility.UrlEncode(login.Username) + "&Password=" + HttpUtility.UrlEncode(login.Password) + "&HWID=" + HttpUtility.UrlEncode(login.HWID));

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

                                        response = new Response("Login", Token.GenerateToken(connection.IP, new Member(UserID, login.Username, AvatarURL)));
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
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

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
                                    response = new Response("Login", Token.GenerateToken(connection.IP, new Member(OAuths.Member.UserID, OAuths.Member.Username, OAuths.Member.AvatarURL)));
                                
                                    Cache.OAuths.Remove(OAuths);
                                }
                                else
                                {
                                    response = new Response("OAuth", "Not Found", true);
                                }
                            }
                            else
                            {
                                response = new Response("OAuth", "Not Authenticated", true);
                            }
                        }

                        Console.WriteLine("Queried Response - " + stopwatch.Elapsed.TotalMilliseconds);

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

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
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);
                    }
                    else if (r.Command == "Download")
                    {
                        MemoryStream MemoryStream = new MemoryStream();

                        Response response = new Response("Download", MemoryStream);

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

                                MemoryStream = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Products\\" + product.File));

                                response.Object = MemoryStream;
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
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);

                        response.Object = null;

                        MemoryStream.Dispose();
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
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "SetAccountID")
                    {
                        Response response = new Response("SetAccountID");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            if (r.Object != null)
                            {
                                if (r.Object is GameAccountInfo)
                                {
                                    if (Cache.AuthTokens.Any(Token => Token.AuthToken == AuthToken.AuthToken))
                                    {
                                        Cache.AuthTokens.FirstOrDefault(Token => Token.AuthToken == AuthToken.AuthToken).GameAccountInfo = (GameAccountInfo)r.Object;

                                        response = new Response("SetAccountID", r.Object);

                                        AppendTCPLogs("Set Account ID(" + AuthToken.Member.Username + "): " + ((GameAccountInfo)response.Object).AccountID + " -> " + ((GameAccountInfo)response.Object).AccountName + Environment.NewLine);
                                    }
                                    else
                                    {
                                        response = new Response("SetAccountID", "Token not Found", true);
                                    }
                                }
                                else
                                {
                                    response = new Response("SetAccountID", "GameIno was Invalid", true);
                                }
                            }
                            else
                            {
                                response = new Response("SetAccountID", "No Name was Passed to the Server", true);
                            }
                        }
                        else
                        {
                            response = new Response("SetAccountID", "Not Authenticated", true);
                        }

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    else if (r.Command == "CheckAccountID")
                    {
                        Response response = new Response("CheckAccountID");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            if (AuthToken.Member.UserID == 1 || AuthToken.Member.UserID == 122862 || AuthToken.Member.UserID == 122893)
                            {
                                if (r.Object != null)
                                {
                                    if (r.Object is List<string>)
                                    {
                                        List<NetworkTypes.Token> FoundAccounts = new List<NetworkTypes.Token>();

                                        foreach (string AccountID in (List<string>)r.Object)
                                        {
                                            if (Cache.AuthTokens.Any(Token => Token.GameAccountInfo != null && Token.GameAccountInfo.AccountID == AccountID))
                                            {
                                                Token LocalToken = Cache.AuthTokens.FirstOrDefault(Token => Token.GameAccountInfo != null && Token.GameAccountInfo.AccountID == AccountID);

                                                FoundAccounts.Add(new NetworkTypes.Token(new NetworkTypes.Member(LocalToken.Member.UserID, LocalToken.Member.Username, LocalToken.Member.AvatarImage), "", new GameAccountInfo(LocalToken.GameAccountInfo.AccountID, LocalToken.GameAccountInfo.AccountName)));

                                                AppendTCPLogs("Found Account ID (" + LocalToken.GameAccountInfo.AccountID + "/" + LocalToken.GameAccountInfo.AccountName + "): " + LocalToken.Member.Username + Environment.NewLine);
                                            }
                                        }

                                        //Check List and Send List
                                        if (FoundAccounts.Count > 0)
                                        {
                                            response = new Response("CheckAccountID", FoundAccounts);
                                        }
                                        else
                                        {
                                            response = new Response("CheckAccountID", "Game IDs not Found", true);
                                        }
                                    }
                                    else
                                    {
                                        response = new Response("CheckAccountID", "No List was Passed to the Server", true);
                                    }
                                }
                                else
                                {
                                    response = new Response("CheckAccountID", "No Name was Passed to the Server", true);
                                }
                            }
                            else
                            {
                                response = new Response("CheckAccountID", "No Permission", true);
                            }
                        }
                        else
                        {
                            response = new Response("CheckAccountID", "Not Authenticated", true);
                        }

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    /*
                    else if (r.Command == "Notifications")
                    {
                        Response response = new Response("Notifications");

                        Console.WriteLine("Processed Command - " + stopwatch.Elapsed.TotalMilliseconds);

                        NetworkTypes.Token token = (NetworkTypes.Token)r.Token;

                        Console.WriteLine("Converted Token - " + stopwatch.Elapsed.TotalMilliseconds);

                        Token AuthToken = Token.GetTokenByToken(token.AuthToken);

                        if (AuthToken != null)
                        {
                            Connect connect = new Connect("174.138.113.192", "community", "community", "L3UeZW0$9Fa5d$R&vfo$@BrCAoLoI32V");
                        }
                        else
                        {
                            response = new Response("Authenticate", "Not Authenticated", true);
                        }

                        if (response.Error)
                            AppendTCPExceptions("Error for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((response != null && response.Object is string) ? (string)response.Object : "r.Object is NULL or not a String") + Environment.NewLine);

                        formatter.Serialize(strm, response);

                        Console.WriteLine("Sent Request - " + stopwatch.Elapsed.TotalMilliseconds);
                    }
                    */
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
                        AppendTCPExceptions("Exception for TCP Connection: " + connection.IP + Environment.NewLine + "Command: " + r.Command + Environment.NewLine + "Invalid Command - " + r.Command);

                        formatter.Serialize(strm, new Response(r.Command, "Invalid Command", true));
                    }

                    AppendRequestLogs(Environment.NewLine + "Request Complete: " + connection.IP + Environment.NewLine + "Command: " + r.Command + ", " + ((r.Object != null && r.Object is string) ? (string)r.Object : "r.Object is NULL or not a String") + Environment.NewLine + "Time: " + stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    AppendTCPExceptions("Exception for TCP Connection: " + connection.IP + Environment.NewLine + ex.ToString() + Environment.NewLine);

                    break;
                }

                strm.Flush();

                Sleep:
                Thread.Sleep(25);
            }

            //Break
            AppendTCPLogs("Closing Connection - " + connection.IP + Environment.NewLine);

            Console.Write("Exited Thread " + Thread.CurrentThread.ManagedThreadId);

            client.Close();

            connection.Close = true;
        }

        public void CacheThread()
        {
            while (true)
            {
                //ReCache Server Data
                Connect connect = new Connect();

                Cache.Version = connect.Version();

                Cache.Products = connect.QueryProducts();

                connect.Close();

                //RateLimiter
                //Connections

                List<RateLimit> RateLimits_Temp;

                lock (Cache.RateLimits)
                {
                    RateLimits_Temp = new List<RateLimit>(Cache.RateLimits);

                    foreach (RateLimit RateLimit in RateLimits_Temp)
                    {
                        if (RateLimit.CleanUp())
                        {
                            Cache.RateLimits.Remove(RateLimit);

                            AppendRateLimitingLogs("Rate Limit: " + RateLimit.IP + " is too old. {" + RateLimit.LastRequestDate.ToString() + "}" + Environment.NewLine);
                        }
                    }
                }

                List<HTTP_Connection> HTTP_Connections_Temp;

                lock (Cache.HTTP_Connections)
                {
                    HTTP_Connections_Temp = new List<HTTP_Connection>(Cache.HTTP_Connections);

                    foreach (HTTP_Connection connection in HTTP_Connections_Temp.Where(Connection => Connection == null || Connection.Close || Connection.HttpListenerContext == null || Connection.LastRequestDate.AddMinutes(1) < DateTime.Now))
                    {
                        AppendHTTPLogs("Connection: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine);

                        try
                        {
                            if (connection.Thread != null)
                            {
                                connection.Thread.Abort();

                                connection.Thread = null;
                            }

                            if (connection.HttpListenerContext != null)
                            {
                                if (connection.HttpListenerContext.Response != null)
                                    connection.HttpListenerContext.Response.Close();

                                connection.HttpListenerContext = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            AppendHTTPLogs("Disposing HTTP Socket Failed: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine);
                            AppendHTTPLogs("Error: " + ex.ToString() + Environment.NewLine);
                        }

                        Cache.HTTP_Connections.Remove(connection);
                    }
                }

                List<TCP_Connection> TCP_Connections_Temp;

                lock (Cache.TCP_Connections)
                {
                    TCP_Connections_Temp = new List<TCP_Connection>(Cache.TCP_Connections);

                    foreach (TCP_Connection connection in TCP_Connections_Temp.Where(Connection => Connection == null || Connection.Close || Connection.TcpClient == null || !Connection.TcpClient.Connected || Connection.LastRequestDate.AddMinutes(5) < DateTime.Now))
                    {
                        AppendTCPLogs("Connection: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine);

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
                            AppendTCPLogs("Disposing Socket Failed: " + connection.IP + " is too old. {" + connection.LastRequestDate.ToString() + "}" + Environment.NewLine);
                            AppendTCPLogs("Error: " + ex.ToString() + Environment.NewLine);
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
                        if (AuthToken != null)
                        {
                            if (AuthToken.Member != null)
                            {
                                AuthToken.Member.Dispose();
                            }
                        }

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

                        if (OAuth.Member != null)
                        {
                            OAuth.Member.Dispose();
                        }

                        Cache.OAuths.Remove(OAuth);
                    }
                }

                this.Invoke((MethodInvoker)delegate
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

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            VersionLabel.Text = "Version: " + Cache.Version;
            TCPInstancesLabel.Text = "TCP Instances: " + Cache.TCP_Connections.Count;
            HTTPInstancesLabel.Text = "HTTP Instances: " + Cache.HTTP_Connections.Count;
            OAuthInstancesLabel.Text = "OAuth Instances: " + Cache.OAuths.Count;
            AuthTokenInstancesLabel.Text = "AuthToken Instances: " + Cache.AuthTokens.Count;
            RateLimitInstancesLabel.Text = "RateLimiting Instances: " + Cache.RateLimits.Count;
            ProductInstancesLabel.Text = "Number of Products in Cache: " + Cache.Products.Count;

            this.Refresh();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Tokens.data
                using (FileStream FileStream = new FileStream(Environment.CurrentDirectory + "\\" + "Tokens.data", FileMode.Create, FileAccess.Write))
                    lock (Cache.AuthTokens)
                        new BinaryFormatter().Serialize(FileStream, Cache.AuthTokens);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Writing Tokens Failed" + Environment.NewLine + ex.ToString());
            }

            Environment.Exit(1);
        }

        public void AppendRequestLogs(string value)
        {
            lock (RequestLogEnteries)
                RequestLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendRequestLogs(value); });
            else
                RequestLogs.Text += value;
            */
        }

        public void AppendRateLimitingLogs(string value)
        {
            lock (RateLimitingLogEnteries)
                RateLimitingLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendRateLimitingLogs(value); });
            else
                RateLimitingLogs.Text += value;
            */
        }

        public void AppendTCPLogs(string value)
        {
            lock (TCPLogEnteries)
                TCPLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendTCPLogs(value); });
            else
                TCPLogs.Text += value;
            */
        }

        public void AppendHTTPLogs(string value)
        {
            lock (HTTPLogEnteries)
                HTTPLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendHTTPLogs(value); });
            else
                HTTPLogs.Text += value;
            */
        }

        public void AppendTCPExceptions(string value)
        {
            lock (TCPExceptionLogEnteries)
                TCPExceptionLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendTCPExceptions(value); });
            else
                TCPExceptions.Text += value;
            */
        }

        public void AppendHTTPExceptions(string value)
        {
            lock (HTTPExceptionLogEnteries)
                HTTPExceptionLogEnteries.Add(value);

            /*
            if (InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate () { AppendHTTPExceptions(value); });
            else
                HTTPExceptions.Text += value;
            */
        }

        private void RequestLogsButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(RequestLogs.Text);
        }

        private void RateLimitLogs_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(RateLimitingLogs.Text);
        }

        private void TCPLogsButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TCPLogs.Text);
        }

        private void TCPExceptionsButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TCPExceptions.Text);
        }
    }
}
