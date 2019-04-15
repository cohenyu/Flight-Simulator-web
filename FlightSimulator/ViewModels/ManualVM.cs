using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualVM
    {
        String _rudderP;
        String _throttleP;
        String _elevatorP;
        String _aileronP;

        public ManualVM()
        {
            _rudderP = "set /controls/flight/rudder ";
            _throttleP = "set /controls/engines/current-engine/throttle ";
            _elevatorP = "set /controls/flight/elevator ";
            _aileronP = "set /controls/flight/aileron ";


        }

        //properties
        public double Rudder
        {
            set
            {
                set(_rudderP, value);
            }

        }

        //properties
        public double Throttle
        {
            set
            {
                set(_throttleP, value);
            }

        }

        public double Aileron
        {
            set
            {
               set(_aileronP, value);

            }

        }
     
        public double Elevator
        {
            set
            {
                set(_elevatorP, value);
            }

        }


        void set(string path, double value)
        {
            String messageToClient = path + value;
            new Thread(() => Commands.CommandInstance.sendData(messageToClient)).Start();
        }

    }
}
