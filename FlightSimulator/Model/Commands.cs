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
    class Commands
    {
        private TcpClient client;
        private NetworkStream writer;
        bool isConnected = false;

        private Commands()
        {
            this.client = new TcpClient();
        }


        #region Singleton
        private static Commands command_instance = null;
        public static Commands CommandInstance
        {
            get
            {
                if (command_instance == null)
                {
                    command_instance = new Commands();
                }
                return command_instance;
            }
        }
        #endregion

        public void openClient()
        {
            new Thread(delegate ()
            {
                Connect(ApplicationSettingsModel.Instance.FlightCommandPort, ApplicationSettingsModel.Instance.FlightServerIP);
            }).Start();
        }

        public void Connect(int port, string ip)
        {
       
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

        public void sendData(string data)
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
                        Byte[] bytes = Encoding.ASCII.GetBytes(line + "\r\n");
                        writer.Write(bytes, 0, bytes.Length);
                        System.Threading.Thread.Sleep(2000);
                    }

                }

            }
        }

    }
}