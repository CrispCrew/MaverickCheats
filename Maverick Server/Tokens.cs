using Main;
using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaverickServer.HandleClients.Tokens
{
    public class Tokens
    {
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
        public static Token GenerateToken(string IP, int ID, string Username)
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

            return Token;
        }
    }
}
