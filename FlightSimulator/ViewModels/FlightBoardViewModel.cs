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
    public class FlightBoardViewModel : BaseNotify
    {
        private Info info;
        public FlightBoardViewModel()
        {
            info = new Info();
            info.PropertyChanged += FlightBoardViewModel_PropertyChanged;
        }

        private void FlightBoardViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public float? Lon
        {
            get
            {
                return info.Lon;
            }
        }

        public float? Lat
        {
            get
            {
                return info.Lat;
            }
        }
    }
}
