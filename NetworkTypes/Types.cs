using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

/// <summary>
/// Network Types - ONLY FOR SERVER COMMUNICATION, DO NOT SEND SENSITIVE DATA
/// </summary>
namespace NetworkTypes
{
    [Serializable]
    public class Request
    {
        public string Command;
        public Token Token = null;
        public object Object = 0;

        public Request()
        {

        }

        public Request(string Command, object Object = null)
        {
            this.Command = Command;
            this.Object = Object != null ? Object : 0;
        }

        public Request(string Command, Token Token, object Object = null)
        {
            this.Command = Command;
            this.Token = Token;
            this.Object = Object != null ? Object : 0;
        }
    }

    [Serializable]
    public class Response
    {
        public string Message;
        public object Object = null;
        public bool Error = false;

        public Response(string Message, object Object = null, bool Error = false)
        {
            this.Message = Message;
            this.Object = Object;
            this.Error = Error;
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
        public Member Member = null;

        public string AuthToken;

        public GameAccountInfo GameAccountInfo = null;

        public Token(string AuthToken)
        {
            this.AuthToken = AuthToken;
        }

        public Token(Member Member, string AuthToken)
        {
            this.Member = Member;
            this.AuthToken = AuthToken;
        }

        public Token(Member Member, string AuthToken, GameAccountInfo GameAccountInfo)
        {
            this.Member = Member;
            this.AuthToken = AuthToken;
            this.GameAccountInfo = GameAccountInfo;
        }
    }

    [Serializable]
    public class Member
    {
        public int UserID;

        public string Username;

        public Image Avatar;

        public Member(int UserID, string Username, Image Avatar)
        {
            this.UserID = UserID;

            this.Username = Username;

            this.Avatar = Avatar;
        }
    }

    /// <summary>
    /// Token - Authentication (Recieve/Send)
    /// </summary>
    [Serializable]
    public class GameAccountInfo
    {
        public string AccountID = "";

        public string AccountName = "";

        public GameAccountInfo()
        {
            AccountID = "";
            AccountName = "";
        }

        public GameAccountInfo(string AccountID, string AccountName)
        {
            this.AccountID = AccountID;
            this.AccountName = AccountName;
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

        //Get rid of
        private byte[] TryReadProductImage()
        {
            List<byte> bytes = new List<byte>();

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

        public Notification()
        {

        }
    }
}
