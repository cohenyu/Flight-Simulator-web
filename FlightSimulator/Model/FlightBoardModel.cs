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
    public class FlightBoardModel : BaseNotify
    {
        public FlightBoardModel()
        {
            Info.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public float? Lat
        {
            get
            {
                return Info.Instance.Lat;
            }
        }

        public float? Lon
        {
            get
            {
                return Info.Instance.Lon;
            }
        }

    }
}