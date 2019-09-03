using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public class RateLimit
    {
        public string IP;

        public List<DateTime> ConnectionAttempts = new List<DateTime>();
        public List<DateTime> APIRequestAttempts = new List<DateTime>();

        public DateTime RateLimitedUntil = DateTime.MinValue;

        public DateTime LastRequestDate = DateTime.Now;

        public DateTime CreationDate = DateTime.Now;

        public RateLimit(string IP)
        {
            this.IP = IP;
        }

        public int Connections()
        {
            lock (ConnectionAttempts)
                return ConnectionAttempts.Count(date => date.AddMinutes(1) > DateTime.Now);
        }

        public int Requests()
        {
            lock (APIRequestAttempts)
                return APIRequestAttempts.Count(date => date.AddMinutes(1) > DateTime.Now);
        }

        public void SocketConnected()
        {
            ConnectionAttempts.Add(DateTime.Now);

            //Check
            if (ConnectionAttempts.Count(date => date.AddMinutes(1) > DateTime.Now) > 50)
                RateLimitedUntil = DateTime.Now.AddMinutes(5);

            LastRequestDate = DateTime.Now;
        }

        public void APIRequest()
        {
            APIRequestAttempts.Add(DateTime.Now);

            //Check
            if (APIRequestAttempts.Count(date => date.AddMinutes(1) > DateTime.Now) > 50)
                RateLimitedUntil = DateTime.Now.AddMinutes(5);

            LastRequestDate = DateTime.Now;
        }

        public bool CleanUp()
        {
            //If this RateLimit is Old with no new Requests, remove the RateLimit Entry
            if (LastRequestDate.AddMinutes(5) < DateTime.Now)
            {
                ConnectionAttempts.Clear();
                APIRequestAttempts.Clear();

                return true;
            }
            else
            {
                if (ConnectionAttempts.Count > 0)
                {
                    //Connections
                    List<DateTime> ConnectionAttempts_Temp = new List<DateTime>();

                    lock (ConnectionAttempts)
                    {
                        ConnectionAttempts_Temp = new List<DateTime>(ConnectionAttempts);

                        foreach (DateTime connection in ConnectionAttempts_Temp)
                        {
                            if (connection.AddMinutes(1) < DateTime.Now)
                            {
                                ConnectionAttempts.Remove(connection);
                            }
                        }
                    }
                }

                if (APIRequestAttempts.Count > 0)
                {
                    //Requests
                    List<DateTime> APIRequestAttempts_Temp = new List<DateTime>();

                    lock (APIRequestAttempts)
                    {
                        APIRequestAttempts_Temp = new List<DateTime>(APIRequestAttempts);

                        foreach (DateTime request in APIRequestAttempts_Temp)
                        {
                            if (request.AddMinutes(1) < DateTime.Now)
                            {
                                APIRequestAttempts.Remove(request);
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool IsLimited()
        {
            if (RateLimitedUntil > DateTime.Now)
                return true;

            return false;
        }
    }

    public class HTTP_Connection
    {
        public Thread Thread;

        public HttpListenerContext HttpListenerContext;

        public string IP;

        public DateTime CreationDate;
        public DateTime LastRequestDate;

        public bool Close = false;

        public HTTP_Connection(HttpListenerContext HttpListenerContext)
        {
            this.HttpListenerContext = HttpListenerContext;

            this.IP = HttpListenerContext.Request.RemoteEndPoint.Address.IsIPv4MappedToIPv6 ? HttpListenerContext.Request.RemoteEndPoint.Address.MapToIPv4().ToString() : HttpListenerContext.Request.RemoteEndPoint.Address.ToString();

            this.CreationDate = DateTime.Now;
            this.LastRequestDate = DateTime.Now;
        }

        public HTTP_Connection(Thread Thread, HttpListenerContext HttpListenerContext)
        {
            this.Thread = Thread;

            this.HttpListenerContext = HttpListenerContext;

            this.IP = HttpListenerContext.Request.RemoteEndPoint.Address.IsIPv4MappedToIPv6 ? HttpListenerContext.Request.RemoteEndPoint.Address.MapToIPv4().ToString() : HttpListenerContext.Request.RemoteEndPoint.Address.ToString();

            this.CreationDate = DateTime.Now;
            this.LastRequestDate = DateTime.Now;
        }
    }

    public class TCP_Connection
    {
        public Thread Thread;

        public TcpClient TcpClient;

        public string IP;

        public DateTime CreationDate;
        public DateTime LastRequestDate;

        public bool Close = false;

        public TCP_Connection(TcpClient TcpClient)
        {
            this.TcpClient = TcpClient;

            this.IP = ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();

            this.CreationDate = DateTime.Now;
            this.LastRequestDate = DateTime.Now;
        }

        public TCP_Connection(Thread Thread, TcpClient TcpClient)
        {
            this.Thread = Thread;

            this.TcpClient = TcpClient;

            this.IP = ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();

            this.CreationDate = DateTime.Now;
            this.LastRequestDate = DateTime.Now;
        }
    }

    public class OAuth
    {
        public Member Member;
        public string PrivateKey;
        public string HWID;

        public DateTime CreationDate;

        public OAuth(Member Member, string PrivateKey, string HWID)
        {
            this.Member = Member;
            this.PrivateKey = PrivateKey;
            this.HWID = HWID;

            this.CreationDate = DateTime.UtcNow;
        }
    }

    [Serializable]
    public class Token
    {
        public string IP;
        public Member Member;
        public string AuthToken;
        public int RunningProduct = 0;
        public GameAccountInfo GameAccountInfo;
        public DateTime LastRequest;
        public DateTime CreationDate;

        public Token()
        {

        }

        public Token(string IP, Member Member, string AuthToken, GameAccountInfo GameAccountInfo)
        {
            this.IP = IP;
            this.Member = Member;
            this.AuthToken = AuthToken;
            this.GameAccountInfo = GameAccountInfo;
            this.LastRequest = DateTime.Now;
            this.CreationDate = DateTime.Now;
        }

        public Token(string IP, Member Member, string AuthToken, GameAccountInfo GameAccountInfo, int RunningProduct = 0)
        {
            this.IP = IP;
            this.Member = Member;
            this.AuthToken = AuthToken;
            this.RunningProduct = RunningProduct;
            this.GameAccountInfo = GameAccountInfo;
            this.LastRequest = DateTime.Now;
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Reutnr username associated with the token
        /// </summary>
        /// <param name="Roken"></param>
        /// <returns></returns>
        public static Token GetTokenByIP(string IP)
        {
            List<Token> tokens = new List<Token>();

            lock (Cache.AuthTokens)
            {
                tokens = Cache.AuthTokens;
            }

            foreach (Token token in tokens)
            {
                if (token.IP == IP)
                {
                    return token;
                }
            }

            //Return false if not found
            return null;
        }

        /// <summary>
        /// Reutnr username associated with the token
        /// </summary>
        /// <param name="Roken"></param>
        /// <returns></returns>
        public static Token GetTokenByToken(string Token)
        {
            List<Token> tokens = new List<Token>();

            lock (Cache.AuthTokens)
            {
                tokens = Cache.AuthTokens;
            }

            foreach (Token token in tokens)
            {
                if (token.AuthToken == Token)
                {
                    return token;
                }
            }

            //Return false if not found
            return null;
        }

        /// <summary>
        /// Generates Random Token
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static NetworkTypes.Token GenerateToken(string IP, Member Member)
        {
            //Check if the IP is Null or Invalid
            Token Token = new Token(IP, Member, Convert.ToBase64String(Guid.NewGuid().ToByteArray()), new GameAccountInfo("New Token", "New Token"));

            //While the token we generated is already used, try and regen a new one
            while (Cache.AuthTokens.Any(token => token.AuthToken == Token.AuthToken))
            {
                Token = new Token(IP, Member, Convert.ToBase64String(Guid.NewGuid().ToByteArray()), new GameAccountInfo("New Token", "New Token"));
            }

            lock (Cache.AuthTokens)
            {
                //Add token to the memory
                Cache.AuthTokens.Add(Token);
            }

            return new NetworkTypes.Token(new NetworkTypes.Member(Member.UserID, Member.Username, Member.AvatarImage), Token.AuthToken);
        }
    }

    [Serializable]
    public class Member
    {
        public int UserID;
        public string Username;

        public string AvatarURL;

        public Image AvatarImage = null;

        public Member()
        {

        }

        public Member(int UserID, string Username, string AvatarURL)
        {
            this.UserID = UserID;
            this.Username = Username;

            this.AvatarURL = AvatarURL;

            try
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\"))
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\");

                if (!File.Exists(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\" + Path.GetFileName(AvatarURL)))
                {
                    byte[] bytes = new WebClient().DownloadData(AvatarURL);

                    File.WriteAllBytes(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\" + Path.GetFileName(AvatarURL), bytes);

                    this.AvatarImage = Image.FromFile(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\" + Path.GetFileName(AvatarURL));
                }
                else
                {
                    this.AvatarImage = Image.FromFile(Environment.CurrentDirectory + "\\Members\\" + UserID + "\\" + Path.GetFileName(AvatarURL));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Dispose()
        {
            if (AvatarImage != null)
                AvatarImage.Dispose();
        }
    }
}
