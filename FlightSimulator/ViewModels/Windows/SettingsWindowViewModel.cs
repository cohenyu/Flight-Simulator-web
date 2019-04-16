using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        private settingsWindow setWindow;

        // constructor
        public SettingsWindowViewModel(ISettingsModel model, settingsWindow setWindow)
        {
            this.model = model;
            this.setWindow = setWindow;
        }

        // property
        public string FlightServerIP
        {
            get
            {
                return model.FlightServerIP;
            }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        // property
        public int FlightCommandPort
        {
            get
            {
                return model.FlightCommandPort;
            }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        // property
        public int FlightInfoPort
        {
            get
            {
                return model.FlightInfoPort;
            }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        #region Commands
        #region OKCommand
        private ICommand _okCommand;
        // property
        public ICommand OKCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OkClick()));
            }
        }
        private void OkClick()
        {
            // Save the data and close the window.
            model.SaveSettings();
            if (this.setWindow != null)
            {
                setWindow.Close();
            }
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        // property
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }

        private void OnCancel()
        {
            // Reload the data and close the window.
            model.ReloadSettings();
            if(this.setWindow != null)
            {
                setWindow.Close();
            }

        }
        #endregion
        #endregion
    }
}

