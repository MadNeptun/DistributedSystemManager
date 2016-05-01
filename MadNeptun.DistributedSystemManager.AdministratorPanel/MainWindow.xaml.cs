using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using MadNeptun.DistributedSystemManager.AdministratorPanel.LogService;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LogManager _logManager;
        private NodesConnectionManager _nodesConnectionManager;

        public NodesConnectionManager NodesConnectionManager
        {
            get { return _nodesConnectionManager;}
            set { _nodesConnectionManager = value; }
        }

        private List<string> _logs = new List<string>();

        public MainWindow()
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            
            var path = ConfigurationManager.AppSettings["xmlConfigFile"];
            using (var file = new FileStream(path, FileMode.Open))
            {
                var configuration = (Configuration)serializer.Deserialize(file);
                _logManager = new LogManager(configuration.UseLogFile, configuration.LogServiceAddress);
                _nodesConnectionManager = new NodesConnectionManager(configuration.Nodes);

                InitializeComponent();
                NodeListView.DataContext = this;
                NodeListView.ItemsSource = configuration.Nodes;
                LogListView.DataContext = this;
                LogListView.ItemsSource = _logManager.Collection;
            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if(_logManager != null)
                _logManager.Dispose();

            if (_nodesConnectionManager != null)
                _nodesConnectionManager.Dispose();
        }

        private void EndHeartbeat_OnClick(object sender, RoutedEventArgs e)
        {
            _nodesConnectionManager.StopStatusCheck();
        }

        private void StartHeartbeat_OnClick(object sender, RoutedEventArgs e)
        {
            _nodesConnectionManager.StartStatusCheck();
        }

        private void SendInitMessage_OnClick(object sender, RoutedEventArgs e)
        {
            var node = GetActiveNode();
            if (node == null)
                return;

            var dialog = new InitMessageWindow();
            var result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _nodesConnectionManager.SendInitToActiveNode(node, dialog.Message);
            }
        }

        private Node GetActiveNode()
        {
            return (Node)NodeListView.SelectedItem;
        }
    }
}
