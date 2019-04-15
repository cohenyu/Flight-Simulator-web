using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualVM : BaseNotify
    {
        String _rudderP;
        String _throttleP;
        String _elevatorP;
        String _aileronP;
        private ManualModel model;

        public ManualVM()
        {
            _rudderP = "set /controls/flight/rudder ";
            _throttleP = "set /controls/engines/current-engine/throttle ";
            _elevatorP = "set /controls/flight/elevator ";
            _aileronP = "set /controls/flight/aileron ";
            model = new ManualModel();
            model.PropertyChanged += Model_PropertyChanged; ;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        //properties
        public double Rudder
        {
            set
            {
                set(_rudderP, value);
            }
            get
            {
                return model.Rudder;
            }

        }

        //properties
        public double Throttle
        {
            set
            {
                set(_throttleP, value);
            }
            get
            {
                return model.Throttle;
            }

        }

        public double Aileron
        {
            set
            {
               set(_aileronP, value);
            }
            get
            {
                return model.Aileron;
            }
        }
     
        public double Elevator
        {
            set
            {
                set(_elevatorP, value);
            }
            get
            {
                return model.Elevator;
            }

        }


        void set(string path, double value)
        {
            String messageToClient = path + value;
            new Thread(() => Commands.CommandInstance.sendData(messageToClient)).Start();
        }

    }
}
