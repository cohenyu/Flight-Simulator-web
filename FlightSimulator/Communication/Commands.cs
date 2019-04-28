using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;


namespace FlightSimulator.Model
{
    /// <summary>
    /// This class will be used as a client connection to the simulator which will serve as a server - through
    /// we will send sets command to the simulator.
    /// </summary>
    class Commands
    {
        private TcpClient client;
        private NetworkStream writer;
        bool isConnected = false;

        // constructor
        private Commands()
        {
            this.client = new TcpClient();
        }


        #region Singleton
        private static Commands command_instance = null;
        // property - singleton
        public static Commands Instance
        {
            get
            {
                // if not created yet
                if (command_instance == null)
                {
                    command_instance = new Commands();
                }
                return command_instance;
            }
        }
        #endregion

        /// <summary>
        /// The function opens communication in the corresponding port and ip.
        /// </summary>
        public void OpenClient()
        {
            new Thread(delegate ()
            {
                Connect(ApplicationSettingsModel.Instance.FlightCommandPort, ApplicationSettingsModel.Instance.FlightServerIP);
            }).Start();
        }

        /// <summary>
        /// The function opens communication in the corresponding port and ip.
        /// </summary>
        public void Connect(int port, string ip)
        {
            // Waiting to connection.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            while (!client.Connected)
            {
                try
                {
                    client.Connect(ep);
                }
                catch (Exception)
                {

                }
            }
            isConnected = true;
            writer = client.GetStream();
        }

        /// <summary>
        /// this function sends the data to the client.
        /// </summary>
        public void SendData(string data)
        {
            if (!isConnected)
            {
                return;
            }

            if (!string.IsNullOrEmpty(data))
            {
                string[] commands = data.Split('\n');
                if (writer.CanWrite)
                {
                    foreach (string line in commands)
                    {
                        // Preparing for a line in windows.
                        Byte[] bytes = Encoding.ASCII.GetBytes(line + "\r\n");
                        writer.Write(bytes, 0, bytes.Length);
                        System.Threading.Thread.Sleep(2000);
                    }

                }

            }
        }

        public void Close()
        {
            isConnected = false;
            client.Close();
        }
    }
}