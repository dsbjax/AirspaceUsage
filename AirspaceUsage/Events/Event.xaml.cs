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

namespace AirspaceUsage.Events
{
    /// <summary>
    /// Interaction logic for Event.xaml
    /// </summary>
    public partial class Event : UserControl, IComparable<Event>
    {
        public SolidColorBrush ActiveColor;

        public string EventName { get; }
        public string Description { get; }
        public string TrimsType { get; }
        public string Exclusivity { get; }
        public bool EventEnabled { get; private set; }

        public Event()
        {
            InitializeComponent();
        }

        public Event(string name, string description, string trimsType, string exclusivity, bool enabled, SolidColorBrush activeColor)
        {
            InitializeComponent();

            EventName = name;
            Description = description;
            TrimsType = trimsType;
            Exclusivity = exclusivity;
            EventEnabled = enabled;
            ActiveColor = activeColor;

            this.enabled.Visibility = EventEnabled ? Visibility.Visible : Visibility.Collapsed;
            this.disabled.Visibility = EventEnabled ? Visibility.Collapsed : Visibility.Visible;
            eventNameLabel.Content = EventName;
            eventDescriptionLabel.Content = Description;
            exclusivityLabel.Content = Exclusivity;
            selectColor.SelectedColor = ActiveColor.Color;
        }

        internal string Save()
        {
            return EventName + "," + Description + "," + TrimsType + "," +
                Exclusivity + "," + (EventEnabled ? "TRUE" : "FALSE") + "," +
                ActiveColor.Color.ToString();
        }

        public override string ToString()
        {
            return Description + ": " + EventName;
        }

        public int CompareTo(Event other)
        {
            return ToString().CompareTo(other.ToString());
        }

        private void EnableChange(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                EventEnabled = !EventEnabled;
                enabled.Visibility = EventEnabled ? Visibility.Visible : Visibility.Collapsed;
                disabled.Visibility = EventEnabled ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void ActiveColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            ActiveColor = new SolidColorBrush((Color) e.NewValue);
        }
    }
}
