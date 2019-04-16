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
    /// <summary>
    /// This is the flight board model.
    /// </summary>
    public class FlightBoardModel : BaseNotify
    {
        // constructor
        public FlightBoardModel()
        {
            // Event registration.
            Info.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        // getting value ​​from the server.
        public float? Lat
        {
            get
            {
                return Info.Instance.Lat;
            }
        }

        // getting value ​​from the server.
        public float? Lon
        {
            get
            {
                return Info.Instance.Lon;
            }
        }

    }
}