using Main;
using MySql.Data.MySqlClient;
using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace MaverickServer.Database
{
    public class Connect
    {
        private MySqlConnection connection;
        private string Server;
        private string Database;
        private string Username;
        private string Password;

        public Connect()
        {
            Server = "127.0.0.1";
            Database = "auth";
            Username = "auth";
            Password = "password";

            connection = new MySqlConnection("SERVER=" + Server + ";" + "DATABASE=" + Database + ";" + "UID=" + Username + ";" + "PASSWORD=" + Password + ";");

            connection.Open();
        }

        public Connect(string Server, string Database, string Username, string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.Username = Username;
            this.Password = Password;

            connection = new MySqlConnection("SERVER=" + Server + ";" + "DATABASE=" + Database + ";" + "UID=" + Username + ";" + "PASSWORD=" + Password + ";");

            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public string Version()
        {
            string Version = "";

            //Handle Manual Products
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM version", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Debug.WriteLine("Reading Version");
                        Debug.WriteLine("Reading Version");

                        Version = ((!reader.IsDBNull(0)) ? reader.GetString(0) : "0.00");
                    }
                }
            }

            return Version;
        }

        #region Queries
        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<Product> QueryProducts()
        {
            List<Product> temp = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("Reading Products");

                        //Get Product Data from SQL Reader
                        temp.Add(new Product().SetFromSQL(reader));
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<Product> QueryUserProducts(int ID)
        {
            List<Product> temp = new List<Product>();

            //If IsAdmin (Forum Permission Check?) , give all products
            if (ID == 1)
                return Cache.Products;

            //Add Free Products
            temp.AddRange(Cache.Products.Where(product => product.Free == 1));

            //API Request for Paid Products
            string Products = new WebClient().DownloadString("http://api.maverickcheats.eu/community/maverickcheats/productcheck.php?UserID=" + ID);

            //No Paid Products
            if (Products == "")
                return temp.OrderBy(id => id).ToList();

            //Paid Product Found - Add them
            foreach (string Product in Products.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                try
                {
                    int ProductID = Convert.ToInt32(Product);

                    if (Cache.Products.Any(product => product.Group == ProductID))
                        temp.AddRange(Cache.Products.Where(product => product.Group == ProductID));
                }
                catch
                {
                    //Not an INT - Ignore
                }

            return temp.OrderBy(id => id).ToList();
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<News> QueryNewsfeed()
        {
            List<News> temp = new List<News>();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Newsfeed", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("Reading Products");

                        News news = new News(reader);

                        temp.Add(news);
                    }
                }
            }

            return temp;
        }
        #endregion

        #region Notification
        public List<Notification> QueryNotifications()
        {
            List<Notification> temp = new List<Notification>();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Notifications", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("Reading Products");

                        Notification notification = new Notification(reader);

                        temp.Add(notification);
                    }
                }
            }

            return temp;
        }

        public bool ReadNotification(int UserID, int NotificationID, ref DateTime ReadDate)
        {
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM notification_read WHERE UserID=@UserID AND NotificationID=@NotificationID", connection);
            com.Parameters.AddWithValue("@UserID", UserID);
            com.Parameters.AddWithValue("@NotificationID", NotificationID);

            if (Convert.ToInt32(com.ExecuteScalar()) > 0)
            {
                com = new MySqlCommand("SELECT DateTime FROM notification_read WHERE UserID=@UserID AND NotificationID=@NotificationID", connection);
                com.Parameters.AddWithValue("@UserID", UserID);
                com.Parameters.AddWithValue("@NotificationID", NotificationID);

                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ReadDate = reader.GetDateTime(0);
                    }
                }

                return true;
            }
            else
            {
                ReadDate = DateTime.Now;

                return false;
            }
        }

        public bool UpdateNotificationStatus(int UserID, int NotificationID)
        {
            //TODO: Implement LicenseKeys Fully
            MySqlCommand com = new MySqlCommand("SELECT Count(*) FROM notification_read WHERE UserID=@UserID AND NotificationID=@NotificationID", connection);
            com.Parameters.AddWithValue("@UserID", UserID);
            com.Parameters.AddWithValue("@NotificationID", NotificationID);

            if (Convert.ToInt32(com.ExecuteScalar()) <= 0)
            {
                com = new MySqlCommand("INSERT INTO notification_read (UserID, NotificationID) VALUES (@UserID, @NotificationID)", connection);
                com.Parameters.AddWithValue("@UserID", UserID);
                com.Parameters.AddWithValue("@NotificationID", NotificationID);
                com.ExecuteNonQuery();
                com.Dispose();

                return true;
            }
            else
            {
                //Set Notification as UnRead?????
                com = new MySqlCommand("DELETE FROM notification_read WHERE UserID=@UserID AND NotificationID=@NotificationID", connection);
                com.Parameters.AddWithValue("@UserID", UserID);
                com.Parameters.AddWithValue("@NotificationID", NotificationID);
                com.ExecuteNonQuery();
                com.Dispose();

                return false;
            }
        }
        #endregion

        #region Logs
        //Insert Log
        public void InsertLog(int UserID, string LogProcess, string LogType, string LogID, string LogMessage)
        {
            //Create an entry
            MySqlCommand com = new MySqlCommand("INSERT INTO logs (UserID, LogProcess, LogType, LogID, LogMessage) VALUES (@UserID, @LogProcess, @LogType, @LogID, @LogMessage)", connection);
            com.Parameters.AddWithValue("@UserID", UserID);
            com.Parameters.AddWithValue("@LogProcess", LogProcess);
            com.Parameters.AddWithValue("@LogType", LogType);
            com.Parameters.AddWithValue("@LogID", LogID);
            com.Parameters.AddWithValue("@LogMessage", LogMessage);
            com.ExecuteNonQuery();
        }

        //Insert Violation
        public void InsertViolation(string IP, string Username, string Type, string Violation, string ProcessName, string ProcessPath, string RawLog)
        {
            //Create an entry
            MySqlCommand com = new MySqlCommand("INSERT INTO violations (IP, Username, Type, Violation, ProcessName, ProcessPath, RawLog) VALUES (@IP, @Username, @Type, @Violation, @ProcessName, @ProcessPath, @RawLog)", connection);
            com.Parameters.AddWithValue("@IP", IP);
            com.Parameters.AddWithValue("@Username", Username);
            com.Parameters.AddWithValue("@Type", Type);
            com.Parameters.AddWithValue("@Violation", Violation);
            com.Parameters.AddWithValue("@ProcessName", ProcessName);
            com.Parameters.AddWithValue("@ProcessPath", ProcessPath);
            com.Parameters.AddWithValue("@RawLog", RawLog);
            com.ExecuteNonQuery();
        }
        #endregion

        #region Tokens
        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public bool CacheTokens(List<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                MySqlCommand com = new MySqlCommand("INSERT INTO tokens (UserID, IP, Username, AuthToken, LastDevice) VALUES (@UserID, @IP, @Username, @AuthToken, @LastDevice)", connection);
                com.Parameters.AddWithValue("@UserID", token.ID);
                com.Parameters.AddWithValue("@IP", token.IP);
                com.Parameters.AddWithValue("@Username", token.Username);
                com.Parameters.AddWithValue("@AuthToken", token.AuthToken);
                com.Parameters.AddWithValue("@LastDevice", ((token.LastDevice == null) ? "" : token.LastDevice));
                com.ExecuteNonQuery();
                com.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Returns list of all downloadable products and their status's
        /// </summary>
        /// <returns></returns>
        public List<Token> LoadTokens()
        {
            List<Token> tokens = new List<Token>();

            //Handle Manual Products
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM tokens", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tokens.Add(new Token(reader.GetString(2), reader.GetInt32(1), reader.GetString(3), reader.GetString(5), reader.GetString(4)));
                    }
                }
            }

            return tokens;
        }
        #endregion
    }
}
