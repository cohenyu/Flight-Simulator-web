using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;

namespace FlightSimulator.ViewModels
{
    class AutoPilotVM : BaseNotify
    {
        private int countColurTime;
        public Commands commandsClient;

        //counstractor
        public AutoPilotVM()
        {
            commandsClient = Commands.CommandInstance;
            countColurTime = 0;
        }

        private String _stringFromUser;

        //properties
        public String StringCommandFromUser
        {
            get
            {
                return _stringFromUser;
            }
            set
            {
                _stringFromUser = value;
                NotifyPropertyChanged("StringCommandFromUser");
                NotifyPropertyChanged("ChangeColor");
            }

        }

        private String _colorToChange;
        public String ChangeColor
        {
            get
            {
                if (_stringFromUser == "" || countColurTime == 0)
                {
                    countColurTime = countColurTime + 1;
                    _colorToChange = "White";
                }
                else
                {

                    _colorToChange = "Pink";
                }
                return _colorToChange;
            }
        }
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClickClear()));
            }
        }
        private void OnClickClear()
        {
            _stringFromUser = "";
            NotifyPropertyChanged(_stringFromUser);
        }

        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OnClickOK()));

            }
         
        }
        private void OnClickOK()
        {
            string textToSend = StringCommandFromUser;
            StringCommandFromUser = "";
            Thread thread = new Thread(() => commandsClient.sendData(textToSend));
            thread.Start();
        }

    }
}

