using NetworkTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.AuthLib
{
    public class Client
    {
        /// <summary>
        /// The Server ClientSocket
        /// </summary>
        TcpClient clientSocket = null;

        /// <summary>
        /// The Network Stream
        /// </summary>
        NetworkStream serverStream = null;

        bool Busy = false;

        /// <summary>
        /// Entry Point - Establishes Socket Connection
        /// </summary>
        public Client()
        {
            //Console Title
            //Console.Title = "Maverick Logs";

            //Log Init
            Logs.LogEntries.Add("Logging Init");

            //Establish Server Connection
            Logs.LogEntries.Add("Client Started");

            Connect();

            Logs.LogEntries.Add("Client Socket Program - Server Connected ...");
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        private bool Connect()
        {
            int tries = 0;
            while (tries <= 5)
            {
                try
                {
                    clientSocket = new TcpClient();

                    clientSocket.NoDelay = true;

                    if (!clientSocket.ConnectAsync("94.23.27.204", 6060).Wait(1000))
                        goto Continue;

                    Logs.LogEntries.Add("Socket Connected");

                    return true;
                }
                catch
                {
                    Logs.LogEntries.Add("Socket Not Available");
                }

                Continue:
                tries++;
            }

            Logs.LogEntries.Add("Server Unavailable");

            return false;
        }

        /// <summary>
        /// Connect to the server
        /// </summary>
        private bool IsConnected()
        {
            if (((clientSocket.Client.Poll(1000, SelectMode.SelectRead) && clientSocket.Client.Poll(1000, SelectMode.SelectWrite)) && clientSocket.Client.Available == 0) || !clientSocket.Connected)
                return false;

            return true;
        }

        public bool Version(out string Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Version");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Recieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = "Unknown Error - " + r.Message + ", " + ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Output = (string)r.Object;

            Busy = false;

            return true;
        }

        public bool Updater(out object Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Updater");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            stopwatch.Stop();

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = "Unknown Error - " + r.Message + ", " + ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Output = (MemoryStream)r.Object;

            Busy = false;

            return true;
        }

        public bool Update(out object Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Update");

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            stopwatch.Stop();

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = "Unknown Error - " + r.Message + ", " + ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Output = (MemoryStream)r.Object;

            Busy = false;

            return true;
        }

        public bool Login(string Username, string Password, string HWID, ref Token Token, out string Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Login", null, new NetworkTypes.Login(Username, Password, HWID));

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Token = (Token)r.Object;

            Output = "Logged In";

            Busy = false;

            return true;
        }

        /// <summary>
        /// Contacts server for Login Check
        /// </summary>
        /// <returns></returns>
        public bool OAuth_Finish(string PrivateKey, ref Token Token, out string Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("OAuth", new NetworkTypes.OAuth(PrivateKey, ""));

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Token = (Token)r.Object;

            Output = "";

            Busy = false;

            return true;
        }

        public bool Products(Token token, out object Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            //List<Product>
            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Products", token);

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            stopwatch.Stop();

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Output = (List<Product>)r.Object;

            Busy = false;

            return true;
        }

        public bool Download(Token token, Product product, out object Output)
        {
            IsBusy:
            if (Busy)
            {
                Thread.Sleep(1000);

                goto IsBusy;
            }

            Busy = true;

            if (!IsConnected())
            {
                if (!Connect())
                {
                    Output = "Server Unavailable";

                    Busy = false;

                    return false;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logs.LogEntries.Add("Starting Request");

            Request message = new Request("Download", token, product);

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            serverStream = clientSocket.GetStream(); // the stream 

            Logs.LogEntries.Add("Stream Instance Obtained - " + stopwatch.Elapsed.TotalMilliseconds);

            formatter.Serialize(serverStream, message); // the serialization process 

            Logs.LogEntries.Add("Request Sent and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Response r = (Response)formatter.Deserialize(serverStream); // you have to cast the deserialized object 

            Logs.LogEntries.Add("Response Reciieved and Serialized - " + stopwatch.Elapsed.TotalMilliseconds);

            Logs.LogEntries.Add("Recieved: " + r.Message);

            stopwatch.Stop();

            if (r.Error)
            {
                if (r.Message == "RateLimited")
                {
                    TimeSpan TimeSpan = DateTime.SpecifyKind((DateTime)r.Object, DateTimeKind.Utc).ToLocalTime().Subtract(DateTime.Now);

                    Output = "Rate Limited for " + TimeSpan.Minutes + "m  " + TimeSpan.Seconds + "s";

                    Busy = false;

                    return false;
                }
                else
                {
                    Output = ((r.Object is string) ? (string)r.Object : "");

                    Busy = false;

                    return false;
                }
            }

            Output = (MemoryStream)r.Object;

            Busy = false;

            return true;
        }
    }
}
