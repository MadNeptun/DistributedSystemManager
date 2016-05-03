using System.Windows;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    /// <summary>
    /// Interaction logic for InitMessageWindow.xaml
    /// </summary>
    public partial class InitMessageWindow : Window
    {
        public string Message { get; set; }

        public InitMessageWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Message = txtInitMessage.Text;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
