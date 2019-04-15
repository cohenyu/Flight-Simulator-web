using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualModel : BaseNotify
    {
        public ManualModel()
        {
            Info.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public double Throttle
        {
            get
            {
                return Info.Instance.Throttle;
            }
        }

        public double Rudder
        {
            get
            {
                return Info.Instance.Rudder;
            }
        }

        public double Aileron
        {
            get
            {
                return Info.Instance.Aileron;
            }
        }

        public double Elevator
        {
            get
            {
                return Info.Instance.Elevator;
            }
        }


    }
}
