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
        public Info()
        {
            SingletonInfoServer.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public float? Lat
        {
            get
            {
                return SingletonInfoServer.Instance.Lat;
            }
        }

        public float? Lon
        {
            get
            {
                return SingletonInfoServer.Instance.Lon;
            }
        }

    }
}