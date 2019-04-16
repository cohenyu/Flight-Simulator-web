using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /// <summary>
    /// This is the manual model.
    /// </summary>
    class ManualModel : BaseNotify
    {
        // constructor
        public ManualModel()
        {
            // Event registration.
            Info.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        // getting value ​​from the server.
        public double Throttle
        {
            get
            {
                return Info.Instance.Throttle;
            }
        }

        // getting value ​​from the server.
        public double Rudder
        {
            get
            {
                return Info.Instance.Rudder;
            }
        }
    }
}
