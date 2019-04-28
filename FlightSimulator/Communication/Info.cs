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
    /// <summary>
    /// info is a server that will listen to airplane data.
    /// </summary>
    class Info : BaseNotify
    {
        private bool shouldStop;
        private float? lon;
        private float? lat;
        private double throttle;
        private double rudder;

        #region Singleton
        private static Info _serverInstance = null;
        // property - singleton
        public static Info Instance
        {
            get
            {
                // if not created yet
                if (_serverInstance == null)
                {
                    _serverInstance = new Info();
                }
                return _serverInstance;
            }
        }
        #endregion

        // constructor
        private Info()
        {
            shouldStop = false;
            lon = null;
            lat = null;
            throttle = 0;
            rudder = 0;
        }

        // property
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

        // property
        public float? Lat
        {
            get { return lat; }
            private set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        // property
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

        // property
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

        /// <summary>
        /// The function reads from the reader until the line breaks.
        /// </summary>
        private static string ReadUntilNewLine(BinaryReader reader)
        {
            char[] buffer = new char[1024];
            int i = 0;
            char input = '\0';

            while (i < 1024 && input != '\n')
            {
                try
                {
                    input = reader.ReadChar();
                    buffer[i] = input;
                    i++;
                }
                catch
                {
                    return null;
                }
            }
            return new string(buffer);
        }

    
        /// <summary>
        /// The function opens a server that accepts and listens a client.
        /// </summary>
        public void OpenServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP), Properties.Settings.Default.FlightInfoPort);
            TcpListener server = new TcpListener(ep);
            server.Start();
            TcpClient clientSocket = server.AcceptTcpClient();
            (new Thread(() => ListenFlight(server,clientSocket))).Start();
        }

        /// <summary>
        ///  The function listens to the values ​​that the simulator sends.
        /// </summary>
        private void ListenFlight(TcpListener server, TcpClient clientSocket)
        {
            // get the data from client
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            DateTime start = DateTime.UtcNow;

            string inputLine;
            string[] splitStr;

            //Thread.Sleep(100 * 1000);
            while (!shouldStop)
            {
                inputLine = ReadUntilNewLine(reader);
                if (inputLine == null)
                {
                    break;
                }

                // Wait for the values ​​to update
                if (Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
                {
                    continue;
                }

                // Taking the relevant values
                splitStr = inputLine.Split(',');
                Lon = float.Parse(splitStr[0]);
                Lat = float.Parse(splitStr[1]);
                Rudder = float.Parse(splitStr[21]);
                Throttle = float.Parse(splitStr[23]);
            }
            // Closing the communication.
            stream.Close();
            clientSocket.Close();
            server.Stop();
        }

       public void Close()
       {
            shouldStop = true;
       }
    }
}