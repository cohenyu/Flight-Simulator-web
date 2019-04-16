using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// This is the view model of the manual - joystick and sliders.
    /// </summary>
    class ManualVM : BaseNotify
    {
        String _rudder;
        String _throttle;
        String _elevator;
        String _aileron;
        private ManualModel model;

        // constructor
        public ManualVM()
        {
            _rudder = "set /controls/flight/rudder ";
            _throttle = "set /controls/engines/current-engine/throttle ";
            _elevator = "set /controls/flight/elevator ";
            _aileron = "set /controls/flight/aileron ";

            model = new ManualModel();
            model.PropertyChanged += Model_PropertyChanged; ;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        //property
        public double Rudder
        {
            set
            {
                set(_rudder, value);
            }
            get
            {
                return model.Rudder;
            }

        }

        //property
        public double Throttle
        {
            set
            {
                set(_throttle, value);
            }
            get
            {
                return model.Throttle;
            }

        }

        //property
        public double Aileron
        {
            set
            {
                set(_aileron, value);

            }

        }

        //property
        public double Elevator
        {
            set
            {
                set(_elevator, value);

            }

        }

        // send the data - path and value
        void set(string path, double value)
        {
            String messageToClient = path + value;
            new Thread(() => Commands.Instance.sendData(messageToClient)).Start();
        }

    }
}
