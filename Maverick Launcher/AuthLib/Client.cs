using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Main.AuthLib
{
    public class Client
    {
        /// <summary>
        /// The Server ClientSocket
        /// </summary>
        TcpClient clientSocket = new TcpClient();

        /// <summary>
        /// The Network Stream
        /// </summary>
        NetworkStream serverStream = null;

        /// <summary>
        /// Entry Point - Establishes Socket Connection
        /// </summary>
        public Client()
        {
            //Console Title
            Console.Title = "Maverick Logs";

            //Log Init
            Console.WriteLine("Logging Init");

            //Establish Server Connection
            Console.WriteLine("Client Started");

            Connect();

            Console.WriteLine("Client Socket Program - Server Connected ...");
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        private bool Connect()
        {
            try
            {
                clientSocket.NoDelay = true;

                clientSocket.Connect("94.23.27.204", 6060);

                Console.WriteLine("Socket Connected");

                return true;
            }
            catch
            {
                Console.WriteLine("Socket Disconnected");

                return false;
            }
        }

        public string Version()
        {
            Request message = new Request("Version");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 
            formatter.Serialize(strm, message); // the serialization process 

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Recieved: " + r.Message);

            return (string)r.Object;
        }

        public string Login(string Username, string Password, string HWID)
        {
            Request message = new Request("Login", new Login(Username, Password, HWID));

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 
            formatter.Serialize(strm, message); // the serialization process 

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Recieved: " + r.Message);

            return (string)r.Object;
        }
    }
}
