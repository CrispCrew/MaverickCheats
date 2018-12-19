using NetworkTypes;

using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using MaverickServer.Database;
using Main.HandleClient;

namespace Main
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(6060);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Opened Socket");

                new Thread(() => ClientThread(client)).Start();

                Thread.Sleep(1000);
            }
        }

        public void ClientThread(TcpClient client)
        {
            while (true)
            {
                try
                {
                    //Initial Request
                    NetworkStream strm = client.GetStream();

                    IFormatter formatter = new BinaryFormatter();

                    Request r = (Request)formatter.Deserialize(strm); // you have to cast the deserialized object 

                    //Response
                    Console.WriteLine("Recieved: " + r.Command);

                    if (r.Command == "Version")
                    {
                        Version version = (Version)r.Object;

                        //HandleVersion
                        Connect connect = new Connect();

                        formatter.Serialize(strm, new Response("Version", connect.Version()));

                        connect.Close();

                        //Return Response
                    }
                    else if (r.Command == "Login")
                    {
                        Login login = (Login)r.Object;

                        //Login -> Returns False/True and OUT/REF Token*?
                        string Response = HandleLogin.Login(client, login.Username, login.Password, login.HWID);
                    }
                    else if (r.Command == "Products")
                    {

                    }
                    else
                    {
                        formatter.Serialize(strm, new Response(r.Command, " is an Invalid Command"));
                    }
                }
                catch
                {
                    break;
                }
            }

            client.Close();

            Console.Write("Exited Thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
