using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MadNeptun.DistributedSystemManager.AdministratorPanel.LogService;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogManager _logManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _logManager = new LogManager(false, "http://localhost:5005/LogService.svc", (o, args) => { });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _logManager.Dispose();
        }


    }
}
