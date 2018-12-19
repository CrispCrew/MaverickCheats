﻿using MaverickServer.HandleClients.Tokens;
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
        public static string Login(TcpClient client, string Username, string Password, string HWID)
        {
            int UserID = 0;
            string Response = new WebClient().DownloadString("http://api.maverickcheats.eu/community/maverickcheats/login.php?Username=" + HttpUtility.UrlEncode(Username) + "&Password=" + HttpUtility.UrlEncode(Password) + "&HWID=" + HttpUtility.UrlEncode(HWID));

            if (Response.Contains("-"))
            {
                if (Response.Split('-')[0] == "Login Found")
                {
                    UserID = Convert.ToInt32(Response.Split('-')[1]);

                    return "Login Found" + "-" + Tokens.GenerateToken(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), UserID, Username);
                }
                else
                {
                    return Response;
                }
            }
            else
            {
                return Response;
            }
        }
    }
}