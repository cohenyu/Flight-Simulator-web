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
    class Info : BaseNotify
    {
        private bool shouldStop;
        private float? lon;
        private float? lat;
        private double throttle;
        private double rudder;
        private double aileron;
        private double elevator;

        private static Info _serverInstance;
        //properties
        public static Info Instance
        {
            get
            {
                //if not create yet
                if (_serverInstance == null)
                {
                    _serverInstance = new Info();
                }
                return _serverInstance;
            }
        }

        private Info()
        {
            shouldStop = false;
            lon = null;
            lat = null;
            throttle = 0;
            rudder = 0;
            aileron = 0;
            elevator = 0;
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

        //properties
        public double Throttle
        {
            get
            {
                return throttle;
            }
            private set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        //properties
        public double Rudder
        {
            get
            {
                return rudder;
            }
            private set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        //properties
        public double Aileron
        {
            get
            {
                return aileron;
            }
            private set
            {
                rudder = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        //properties
        public double Elevator
        {
            get
            {
                return elevator;
            }
            private set
            {
                rudder = value;
                NotifyPropertyChanged("Elevator");
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

            //Thread.Sleep(100 * 1000);
            while (!shouldStop)
            {
                inputLine = readUntilNewLine(reader);
                if (inputLine == null)
                {
                    break;
                }

                if (Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
                {
                    continue;
                }

                splitStr = inputLine.Split(',');
                Lon = float.Parse(splitStr[0]);
                Lat = float.Parse(splitStr[1]);
                Aileron = float.Parse(splitStr[19]);
                Elevator = float.Parse(splitStr[20]);
                Rudder = float.Parse(splitStr[21]);
                Throttle = float.Parse(splitStr[23]);
            }

            clientSocket.Close();
            server.Stop();
        }
    }
}