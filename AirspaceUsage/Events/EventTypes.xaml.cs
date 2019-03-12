using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

namespace AirspaceUsage.Events
{
    /// <summary>
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class EventTypes : UserControl, IEnumerable<Event>
    {
        private readonly string defaultFilename = Properties.Settings.Default.EventsFile;

        private readonly string customFilename =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            Properties.Settings.Default.AppFolder + "\\" + Properties.Settings.Default.EventsFile;

        internal void Save()
        {
            using (var writer = new StreamWriter(customFilename))
            {
                foreach (var ev in events)
                    writer.WriteLine(ev.Save());
            }
        }

        private List<Event> events = new List<Event>();

        public EventTypes()
        {
            InitializeComponent();

            if (!File.Exists(customFilename))
                File.Copy(defaultFilename, customFilename, true);

            string[] eventFields;
            using (var reader = new StreamReader(customFilename))
            {
                while (!reader.EndOfStream)
                {
                    eventFields = reader.ReadLine().Split(',');

                    events.Add(new Event(
                        eventFields[0],
                        eventFields[1],
                        eventFields[2],
                        eventFields[3],
                        eventFields[4].Equals("TRUE"),
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(eventFields[5]))));
                }
            }

            eventsComboBox.Items.Clear();
            events.Sort();

            foreach (var e in events)
                if (e.IsEnabled)
                    eventsComboBox.Items.Add(new ComboBoxItem() { Tag = e, Content = e.ToString() });
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return events.GetEnumerator();
        }

        private void EventSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //prompt.Visibility = eventsComboBox.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
