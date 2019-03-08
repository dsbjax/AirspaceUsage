using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AirspaceUsage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadUserSettings();
        }

        private void LoadUserSettings()
        {
            Properties.Settings.Default.Reload();

            if (Properties.Settings.Default.MapBoarder.Color.ToString() != Brushes.White.Color.ToString())
                airspaceOwner.Content =
                    Properties.Settings.Default.MapBoarder.Color.ToString() == Brushes.Red.Color.ToString() ?
                    "Airspace Owner: FACSFAC" : "Airspace Owner: FAA";
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            mapArea.Width = mapArea.ActualHeight;
            base.OnRender(drawingContext);
        }

        private void AirspaceOwnerClick(object sender, RoutedEventArgs e)
        {
            airspaceOwner.Content = airspaceOwner.Content.ToString().Contains("FACSFAC") ? "Airspace Owner: FAA" : "Airspace Owner: FACSFAC";
            Properties.Settings.Default.MapBoarder = airspaceOwner.Content.ToString().Contains("FACSFAC") ? Brushes.Red : Brushes.Green;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
            base.OnClosing(e);
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            settings.Content = settings.Content.ToString().Equals("Settings") ? "Schedule" : "Settings";
            settingsControl.Visibility = settingsControl.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            scheduleControl.Visibility = scheduleControl.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
