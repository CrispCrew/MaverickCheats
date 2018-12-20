﻿using NetworkTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Starting Request");

            Request message = new Request("Version");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 

            Console.WriteLine("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(strm, message); // the serialization process 

            Console.WriteLine("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Response Recieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Console.WriteLine("Recieved: " + r.Message);

            return (string)r.Object;
        }

        public MemoryStream Update()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Starting Request");

            Request message = new Request("Updater");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 

            Console.WriteLine("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(strm, message); // the serialization process 

            Console.WriteLine("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Console.WriteLine("Recieved: " + r.Message);

            stopwatch.Stop();

            return (MemoryStream)r.Object;
        }

        public Token Login(string Username, string Password, string HWID)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Starting Request");

            Request message = new Request("Login", new NetworkTypes.Login(Username, Password, HWID));

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 

            Console.WriteLine("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(strm, message); // the serialization process 

            Console.WriteLine("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Console.WriteLine("Recieved: " + r.Message);

            return (Token)r.Object;
        }

        public List<Product> Products(Token token)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Starting Request");

            Request message = new Request("Products", token);

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = clientSocket.GetStream(); // the stream 

            Console.WriteLine("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(strm, message); // the serialization process 

            Console.WriteLine("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(strm); // you have to cast the deserialized object 

            Console.WriteLine("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Console.WriteLine("Recieved: " + r.Message);

            stopwatch.Stop();

            return (List<Product>)r.Object;
        }
    }
}
