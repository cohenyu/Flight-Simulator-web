using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardVM : BaseNotify
    {
        /// <summary>
        /// This is the view model of the flight board.
        /// </summary>
        private FlightBoardModel info;
        public FlightBoardVM()
        {
            info = new FlightBoardModel();
            info.PropertyChanged += FlightBoardViewModel_PropertyChanged;
        }

        private void FlightBoardViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        // property
        public float? Lon
        {
            get
            {
                return info.Lon;
            }
        }

        // property
        public float? Lat
        {
            get
            {
                return info.Lat;
            }
        }
    }
}
