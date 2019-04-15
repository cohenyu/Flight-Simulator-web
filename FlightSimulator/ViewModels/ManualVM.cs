using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                String messageToClient = _rudderP + value;
                Commands.CommandInstance.sendData(messageToClient);
            }

        }

        //properties
        public double Throttle
        {
            set
            {
                String messageToClient = _throttleP + value;
                Commands.CommandInstance.sendData(messageToClient);
            }

        }

        public double Aileron
        {
            set
            {
                String messageToClient = _aileronP + value;
                Commands.CommandInstance.sendData(messageToClient);

            }

        }
     
        public double Elevator
        {
            set
            {
                String messageToClient = _elevatorP + value;
                Commands.CommandInstance.sendData(messageToClient);

            }

        }


    }
}
