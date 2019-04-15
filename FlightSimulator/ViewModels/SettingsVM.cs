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
    class SettingsVM
    {
        private ICommand _settingsCommand;
        //properties
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnClick()));
            }
        }

        private void OnClick()
        {
            settingsWindow s = new settingsWindow();
            s.ShowDialog();
        }

        private ICommand _connectCommand;
        //properies
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnect()));
            }

        }
        private void OnConnect()
        {
            Console.WriteLine("connecttttt");
            new Thread(() =>
            {
                SingletonInfoServer.Instance.openServer();
                Commands.CommandInstance.openClient();
            }).Start();
        }
    }
}