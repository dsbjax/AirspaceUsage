using AirspaceUsage.Events;
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

namespace AirspaceUsage.Settings
{
    /// <summary>
    /// Interaction logic for EventSettings.xaml
    /// </summary>
    public partial class EventSettings : UserControl
    {
        EventTypes events = new EventTypes();
        public EventSettings()
        {
            InitializeComponent();

            foreach(var ev in events)
                eventsListBox.Items.Add(ev);
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            events.Save();
        }
    }
}
