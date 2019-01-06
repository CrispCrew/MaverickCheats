using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class OAuth
    {
        public int UserID;
        public string Username;
        public string PrivateKey;
        public string HWID;

        public DateTime CreationDate;

        public OAuth(int UserID, string Username, string PrivateKey, string HWID)
        {
            this.UserID = UserID;
            this.Username = Username;
            this.PrivateKey = PrivateKey;
            this.PrivateKey = HWID;

            this.CreationDate = DateTime.UtcNow;
        }
    }

    public class Token
    {
        public string IP;
        public int ID;
        public string Username;
        public string AuthToken;
        public string LastDevice;
        public DateTime LastRequest;
        public DateTime CreationDate;

        public Token()
        {

        }

        public Token(string IP, int ID, string Username, string AuthToken)
        {
            this.IP = IP;
            this.ID = ID;
            this.Username = Username;
            this.AuthToken = AuthToken;
            this.LastRequest = DateTime.Now;
            this.CreationDate = DateTime.Now;
        }

        public Token(string IP, int ID, string Username, string LastDevice, string AuthToken)
        {
            this.IP = IP;
            this.ID = ID;
            this.Username = Username;
            this.AuthToken = AuthToken;
            this.LastDevice = LastDevice == null ? "" : LastDevice;
            this.LastRequest = DateTime.Now;
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Reutnr username associated with the token
        /// </summary>
        /// <param name="Roken"></param>
        /// <returns></returns>
        public Token GetTokenByIP(string IP)
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
        public Token GetTokenByToken(string Token)
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
        public NetworkTypes.Token GenerateToken(string IP, int ID, string Username)
        {
            //Check if the IP is Null or Invalid
            Token Token = new Token(IP, ID, Username, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

            //While the token we generated is already used, try and regen a new one
            while (Cache.AuthTokens.Any(token => token.AuthToken == Token.AuthToken))
            {
                Token = new Token(IP, ID, Username, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            }

            lock (Cache.AuthTokens)
            {
                //Add token to the memory
                Cache.AuthTokens.Add(Token);
            }

            return new NetworkTypes.Token(IP, ID, Username, Token.AuthToken);
        }
    }
}
