using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.ViewModels;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for flightBoardControls.xaml
    /// </summary>
    public partial class flightBoardControls : UserControl
    {
        // constructor
        public flightBoardControls()
        {
            InitializeComponent();
            this.DataContext = new SettingsVM();
        }
    }
}
