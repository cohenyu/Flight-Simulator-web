using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;

namespace FlightSimulator
{
    class SingletonInfoServer
    {

        private static Info _infoInstance;

        //properties
        public static Info InfoInstance
        {
            get
            {
                //if not create yet
                if (_infoInstance== null)
                {
                    _infoInstance = new Info();
                   
                }
                return _infoInstance;
            }

        }


    }
}
