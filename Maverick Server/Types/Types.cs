using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
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

    public class Token
    {
        public string IP;
        public Member Member;
        public string AuthToken;
        public int RunningProduct = 0;
        public DateTime LastRequest;
        public DateTime CreationDate;

        public Token()
        {

        }

        public Token(string IP, Member Member, string AuthToken)
        {
            this.IP = IP;
            this.Member = Member;
            this.AuthToken = AuthToken;
            this.LastRequest = DateTime.Now;
            this.CreationDate = DateTime.Now;
        }

        public Token(string IP, Member Member, string AuthToken, int RunningProduct = 0)
        {
            this.IP = IP;
            this.Member = Member;
            this.AuthToken = AuthToken;
            this.RunningProduct = RunningProduct;
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
            Token Token = new Token(IP, Member, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

            //While the token we generated is already used, try and regen a new one
            while (Cache.AuthTokens.Any(token => token.AuthToken == Token.AuthToken))
            {
                Token = new Token(IP, Member, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            }

            lock (Cache.AuthTokens)
            {
                //Add token to the memory
                Cache.AuthTokens.Add(Token);
            }

            return new NetworkTypes.Token(new NetworkTypes.Member(Member.Username, Member.AvatarImage), Token.AuthToken);
        }
    }

    public class Member
    {
        public int UserID;
        public string Username;

        public string AvatarURL;

        public Image AvatarImage = null;

        public Member(int UserID, string Username, string AvatarURL)
        {
            this.UserID = UserID;
            this.Username = Username;

            this.AvatarURL = AvatarURL;

            try
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\Members\\" + Path.GetFileName(AvatarURL)))
                {
                    byte[] bytes = new WebClient().DownloadData(AvatarURL);

                    File.WriteAllBytes(Environment.CurrentDirectory + "\\Members\\" + Path.GetFileName(AvatarURL), bytes);

                    this.AvatarImage = Image.FromStream(new MemoryStream(new WebClient().DownloadData(AvatarURL)));
                }
                else
                {
                    this.AvatarImage = Image.FromStream(new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\Members\\" + Path.GetFileName(AvatarURL))));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
