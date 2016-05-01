using System.ComponentModel;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    public class Node : INotifyPropertyChanged
    {
        static SolidColorBrush orange = new SolidColorBrush(Colors.Orange);
        static SolidColorBrush green = new SolidColorBrush(Colors.Green);
        static SolidColorBrush red = new SolidColorBrush(Colors.Red);

        private Brush _statusColor;
        [XmlIgnore]
        public Brush StatusColor { 
            get
            {
                if (_statusColor == null)
                    return orange;
                return _statusColor; 
            }
            set
            {
                _statusColor = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StatusColor"));
            }
        }

        public string NodeAddress { get; set; }

        public string NodeName { get; set; }

        public void ClearStatus()
        {
            StatusColor = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void Alive()
        {
            StatusColor = green;
        }

        internal void Dead()
        {
            StatusColor = red;
        }
    }
}
