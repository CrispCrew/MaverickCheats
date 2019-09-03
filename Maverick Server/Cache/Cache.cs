using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Cache
    {
        /// <summary>
        /// Client Version
        /// </summary>
        public static string Version = "";

        public static List<RateLimit> RateLimits = new List<RateLimit>();

        /// <summary>
        /// TCP Instances
        /// </summary>
        public static List<TCP_Connection> TCP_Connections = new List<TCP_Connection>();

        /// <summary>
        /// HTTP Instances
        /// </summary>
        public static List<HTTP_Connection> HTTP_Connections = new List<HTTP_Connection>();

        /// <summary>
        /// OAUth Instances
        /// </summary>
        public static List<OAuth> OAuths = new List<OAuth>();

        /// <summary>
        /// Login Tokens
        /// </summary>
        public static List<Token> AuthTokens = new List<Token>();

        /// <summary>
        /// Product Cache
        /// </summary>
        public static List<Product> Products = new List<Product>();

        /// <summary>
        /// Newsfeed Cache
        /// </summary>
        //public static List<News> Newsfeed = new List<News>();

        /// <summary>
        /// Notification Cache
        /// </summary>
        public static List<Notification> Notifications = new List<Notification>();
    }
}
