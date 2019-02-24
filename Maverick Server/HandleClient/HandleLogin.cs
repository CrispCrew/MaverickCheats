using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Main.HandleClient
{
    public class HandleLogin
    {
        public static Response Login(TcpClient client, string Username, string Password, string HWID)
        {
            Response response = new Response("Login");

            int UserID = 0;
            string AvatarURL = "";
            string Response = new WebClient().DownloadString("http://api.maverickcheats.eu/community/maverickcheats/login.php?Username=" + HttpUtility.UrlEncode(Username) + "&Password=" + HttpUtility.UrlEncode(Password) + "&HWID=" + HttpUtility.UrlEncode(HWID));

            if (Response.Contains("%delimiter%"))
            {
                if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None).Length >= 2)
                {
                    if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[0] == "Login Found")
                    {
                        if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1] != "")
                        {
                            UserID = int.TryParse(Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1], out _) ? int.Parse(Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[1]) : 0;

                            if (Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[2] != "")
                                AvatarURL = Response.Split(new string[] { "%delimiter%" }, StringSplitOptions.None)[2];

                            response = new Response("Login", Token.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), new Member(UserID, Username, AvatarURL)));
                        }
                        else
                        {
                            response = new Response("Login", "Login Failed - UserID Query Failed", true);
                        }
                    }
                    else
                    {
                        response = new Response("Login", "Login Failed - Login not Found", true);
                    }
                }
                else
                {
                    response = new Response("Login", "Internal Error - No Data Provided", true);
                }
            }
            else
            {
                response = new Response("Login", "Error - " + Response, true);
            }

            Console.WriteLine("Response: " + Response + " , " + response.Message + "," + ((response.Object is string) ? response.Object : "response.Object") + "," + response.Error);

            return response;
        }
    }
}
