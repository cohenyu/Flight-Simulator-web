using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace FlightSimulator.Model
{
    class Commands
    {
        private TcpClient client;
        private BinaryWriter writer;

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
                if(command_instance == null)
                {
                    command_instance = new Commands();
                }
                return command_instance;
            }
        }
        #endregion

        public void Connect(int port, string ip)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            while (!client.Connected)
            {
                try
                {
                    client.Connect(ep);
                }
                catch(Exception)
                {
                   
                }
            }
            writer = new BinaryWriter(client.GetStream());
        }

        public void sendData(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string[] commands = data.Split('\n');
                foreach(string line in commands)
                {
                    writer.Write(System.Text.Encoding.ASCII.GetBytes(line + "\r\n"));
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }

    }
}
