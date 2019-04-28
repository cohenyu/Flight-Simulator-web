using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// This is the view model of the buttons -settings and ok.
    /// </summary>
    class SettingsVM
    {
        private ICommand _settingsCommand;
        //property
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnClick()));
            }
        }

        private void OnClick()
        {
            // show the settings window
            settingsWindow s = new settingsWindow();
            s.ShowDialog();
        }

        private ICommand _connectCommand;
        //propery
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnect()));
            }

        }

        private void OnConnect()
        {
            // start the communication
            new Thread(() =>
            {
                Info.Instance.OpenServer();
                Commands.Instance.OpenClient();
            }).Start();
        }

        private ICommand _disconnectCommand;
        // property
        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new CommandHandler(() => OnDisconnect()));
            }
        }

        // close the info and commands
        private void OnDisconnect()
        {
            Info.Instance.Close();
            Commands.Instance.Close();
        }

    }
}