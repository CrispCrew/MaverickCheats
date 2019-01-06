using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Network Types - ONLY FOR SERVER COMMUNICATION, DO NOT SEND SENSITIVE DATA
/// </summary>
namespace NetworkTypes
{
    [Serializable]
    public class Request
    {
        public string Command;
        public string SubCommand;
        public Token Token = null;
        public object Object = 0;

        public Request()
        {

        }

        public Request(string Command, Token Token = null, object Object = null)
        {
            this.Command = Command;
            this.SubCommand = "";
            this.Token = Token;
            this.Object = Object != null ? Object : 0;
        }

        public Request(string Command, string SubCommand, Token Token = null, object Object = null)
        {
            this.Command = Command;
            this.SubCommand = SubCommand;
            this.Token = Token;
            this.Object = Object != null ? Object : 0;
        }
    }

    [Serializable]
    public class Response
    {
        public string Message;
        public object Object = 0;

        public Response()
        {

        }

        public Response(string Message, object Object = null)
        {
            this.Message = Message;
            this.Object = Object != null ? Object : 0;
        }
    }

    [Serializable]
    public class Login
    {
        public string Username;
        public string Password;
        public string HWID;

        public Login()
        {

        }

        public Login(string Username, string Password, string HWID = "")
        {
            this.Username = Username;
            this.Password = Password;
            this.HWID = HWID;
        }
    }

    [Serializable]
    public class OAuth
    {
        public string PrivateKey;
        public string HWID;

        public OAuth()
        {

        }

        public OAuth(string PrivateKey, string HWID)
        {
            this.PrivateKey = PrivateKey;
            this.HWID = HWID;
        }
    }

    /// <summary>
    /// Token - Authentication (Recieve/Send)
    /// </summary>
    [Serializable]
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
    }

    /// <summary>
    /// Product (Recieve/Send)
    /// </summary>
    [Serializable]
    public class Product
    {
        public int Id; //UID
        public int Group; //UID
        public string Name; //Product Name
        public string File; //Product Media
        public string ProcessName; //Product ProcessName
        public int Status; //Product Status
        public int Version; //Product Version
        public int Free; //Product Free [0=no][1=yes]
        public long AutoLaunchMem;
        public bool Internal;

        public byte[] Image;

        public Product()
        {

        }

        public Product(int Id, int Group, string Name, string File, string ProcessName, int Status, int Version, int Free, bool Internal)
        {
            this.Id = Id;
            this.Group = Group;
            this.Name = Name;
            this.File = File;
            this.ProcessName = ProcessName;
            this.Status = Status;
            this.Version = Version;
            this.Free = Free;
            this.Internal = Internal;

            this.Image = TryReadProductImage();
        }

        //Get Rid of
        public Product SetFromSQL(MySqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Group = reader.GetInt32(1);
            Name = reader.GetString(2);
            File = reader.GetString(3);
            ProcessName = reader.GetString(4);
            Status = reader.GetInt32(5);
            Version = reader.GetInt32(6);
            Free = reader.GetInt32(7);
            AutoLaunchMem = reader.GetInt64(8);
            Internal = (reader.GetInt32(9) == 1) ? true : false;

            this.Image = TryReadProductImage();

            return this;
        }

        //Get rid of
        private byte[] TryReadProductImage()
        {
            List<byte> bytes = new List<byte>(new byte[] { });

            try
            {
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\Images\\" + Name + ".png"))
                    using (FileStream fileStream = System.IO.File.Open(Environment.CurrentDirectory + "\\Images\\" + Name + ".png", FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        byte[] buffer = new byte[(int)fileStream.Length];

                        int length = (int)fileStream.Length;
                        int count = 0;
                        int read = 0;

                        Console.WriteLine("File Length: " + length);
                        Console.WriteLine("Read: " + count);

                        // read until Read method returns 0 (end of the stream has been reached)
                        while ((count = fileStream.Read(buffer, count, length - count)) != 0)
                        {
                            bytes.AddRange(buffer);

                            Console.WriteLine("File Length: " + length);
                            Console.WriteLine("Read: " + count);

                            read += count;
                        }
                    }
                else
                {
                    Console.WriteLine("Server cant find Product Image -> " + Environment.CurrentDirectory + "\\Images\\" + Name + ".png");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Product: " + Name + " Image Size: " + bytes.Count);

            return bytes.ToArray();
        }
    }

    /*
    [Serializable]
    public class News
    {
        public int ProductId;
        public string NewsFeed;
        public DateTime PostDate;

        public News()
        {

        }

        public News(MySqlDataReader reader)
        {
            ProductId = reader.GetInt32(1);
            NewsFeed = reader.GetString(2);
            PostDate = reader.GetDateTime(3);
        }

        public News(DateTime postdate, string newsfeed, int productid)
        {
            this.ProductId = productid;
            this.NewsFeed = newsfeed;
            this.PostDate = postdate;
        }
    }
    */

    /// <summary>
    /// Notification Structre - Global Notification Stats
    /// </summary>
    public class Notification
    {
        public int ID;
        public string Title;
        public string Message;
        public DateTime CreationDate;

        public Notification(int ID, string Title, string Message, DateTime CreationDate)
        {
            this.ID = ID;
            this.Title = Title;
            this.Message = Message;
            this.CreationDate = CreationDate;
        }

        //Get rid of
        public Notification(MySqlDataReader reader)
        {
            //Get Notification Base Stats
            ID = reader.GetInt32(0);
            Title = reader.GetString(1);
            Message = reader.GetString(2);
            CreationDate = reader.GetDateTime(3);
        }
    }
}
