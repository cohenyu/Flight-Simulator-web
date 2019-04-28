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
    /// <summary>
    /// This is the view model of the AutoPilot tab.
    /// </summary>
    class AutoPilotVM : BaseNotify
    {
        public Commands commandsClient;
        private String _stringFromUser;
        private String _colorToChange;

        // constructor
        public AutoPilotVM()
        {
            _stringFromUser = "";
            commandsClient = Commands.Instance;
        }

        // property
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

        // property
        public String ChangeColor
        {
            // Paints the text box with the matches color according to the content.
            get
            {
                if (_stringFromUser == "")
                {
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
        // property
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClickClear()));
            }
        }

        private void OnClickClear()
        {
            // Clear the text box.
            _stringFromUser = "";
            NotifyPropertyChanged(_stringFromUser);
        }

        private ICommand _okCommand;
        // property
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OnClickOK()));

            }
         
        }
        private void OnClickOK()
        {
            // Clear the text box and send the data.
            string textToSend = StringCommandFromUser;
            StringCommandFromUser = "";
            new Thread(() => commandsClient.SendData(textToSend)).Start();
        }

    }
}

