using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.ViewModels;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace FlightSimulator.Model
{
    public class Info : BaseNotify
    {
        private bool shouldStop;
        private float lon;
        private float lat;

        //cunstractor
        public Info()
        {
            shouldStop = false;
            lon = 0.0f;
            lat = 0.0f;
        }

        //properties
        public float Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                //
                NotifyPropertyChanged("Lon");
            }
        }
        //properties
        public float Lat
        {
            get { return lat; }
            set
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
                char input = reader.ReadChar();
                buffer[i] = input;
                lastChar = buffer[i];
                i++;
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
            Thread thread = new Thread(() => listenFlight(server, clientSocket));
            thread.Start();
        }

        private void listenFlight(TcpListener server, TcpClient clientSocket)
        {
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            DateTime start = DateTime.UtcNow;

            string inputLine;
            string[] splitStr;

            while (!shouldStop)
            {
                inputLine = readUntilNewLine(reader);

                if (Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
                {
                    continue; 
                }
                //yuyuyuyu
                splitStr = inputLine.Split(',');
                Lon = float.Parse(splitStr[0]);
                Lat = float.Parse(splitStr[1]);
                Console.WriteLine("Lon {0} Lat {1}", Lon, Lat);
                //Thread.Sleep(250);
            }

            clientSocket.Close();
            server.Stop();

        }
    }
}