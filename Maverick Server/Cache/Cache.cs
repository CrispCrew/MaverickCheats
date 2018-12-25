using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Cache
    {
        /// <summary>
        /// OAUth Instances
        /// </summary>
        //public static List<OAuth> OAuth = new List<OAuth>();

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
        public static List<News> Newsfeed = new List<News>();

        /// <summary>
        /// Notification Cache
        /// </summary>
        public static List<Notification> Notifications = new List<Notification>();

        /// <summary>
        /// OAuth Cache
        /// </summary>
        //public static List<OAuth> OAuth = new List<OAuth>();
    }
}
