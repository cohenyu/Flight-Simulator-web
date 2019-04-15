using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulator
{
    class SingletonInfoServer : BaseNotify
    {
        private bool shouldStop;
        private float? lon;
        private float? lat;

        private static SingletonInfoServer _serverInstance;
        //properties
        public static SingletonInfoServer Instance
        {
            get
            {
                //if not create yet
                if (_serverInstance == null)
                {
                    _serverInstance = new SingletonInfoServer();
                }
                return _serverInstance;
            }

        }

        private SingletonInfoServer()
        {
            shouldStop = false;
            lon = null;
            lat = null;

        }

        //properties
        public float? Lon
        {
            get
            {
                return lon;
            }
            private set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        //properties
        public float? Lat
        {
            get { return lat; }
            private set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        private static string readUntilNewLine(BinaryReader reader)
        {
            char[] buffer = new char[1024];
            int i = 0;
            char lastChar = '\0';

            while (i < 1024 && lastChar != '\n')
            {
                try
                {
                    char input = reader.ReadChar();
                    buffer[i] = input;
                    lastChar = buffer[i];
                    i++;
                }
                catch
                {
                    return null;
                }
            }

            return new string(buffer);
        }


        public void openServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                  Properties.Settings.Default.FlightInfoPort);
            TcpListener server = new TcpListener(ep);

            server.Start();
            TcpClient clientSocket = server.AcceptTcpClient();
            Thread thread = new Thread(() => listenFlight(server,clientSocket));
            thread.Start();
        }

        private void listenFlight(TcpListener server, TcpClient clientSocket)
        {
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            DateTime start = DateTime.UtcNow;

            string inputLine;
            string[] splitStr;

            Thread.Sleep(100 * 1000);
            while (!shouldStop)
            {
                inputLine = readUntilNewLine(reader);
                if (inputLine == null)
                {
                    break;
                }

                //if (Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
                //{
                //    continue;
                //}

                splitStr = inputLine.Split(',');
                Lon = float.Parse(splitStr[0]);
                Lat = float.Parse(splitStr[1]);
            }

            clientSocket.Close();
            server.Stop();
        }

    }
}