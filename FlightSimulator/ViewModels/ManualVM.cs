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
     

        public ManualVM()
        {
            _rudderP = "set /controls/flight/rudder ";
            _throttleP = "set /controls/engines/current-engine/throttle ";
            

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
    }
}
