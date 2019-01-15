using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Main.HandleClient
{
    public class HandleLogin
    {
        public static Response Login(TcpClient client, string Username, string Password, string HWID)
        {
            //Split Response into 3 indexes, Login Message, User ID, Avatar

            int UserID = 0;
            string AvatarURL = "";
            string Response = new WebClient().DownloadString("http://api.maverickcheats.eu/community/maverickcheats/login.php?Username=" + HttpUtility.UrlEncode(Username) + "&Password=" + HttpUtility.UrlEncode(Password) + "&HWID=" + HttpUtility.UrlEncode(HWID));

            if (Response.Contains("-"))
            {
                if (Response.Split('-').Length >= 2)
                {
                    if (Response.Split('-')[0] == "Login Found")
                    {
                        if (Response.Split('-')[1] != "")
                        {
                            UserID = int.TryParse(Response.Split('-')[1], out _) ? int.Parse(Response.Split('-')[1]) : 0;

                            if (Response.Split('-')[2] != "")
                                AvatarURL = Response.Split('-')[2];

                            return new Response("Login", Token.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), new Member(UserID, Username, AvatarURL)));
                        }
                        else
                        {
                            return new Response("Login", "Login Failed - UserID Query Failed", true);
                        }
                    }
                    else
                    {
                        return new Response("Login", "Login Failed - Login not Found", true);
                    }
                }
                else
                {
                    return new Response("Login", "Internal Error - No Data Provided", true);
                }
            }
            else
            {
                return new Response("Login", "Internal Error - No Data Provided", true);
            }
        }
    }
}
