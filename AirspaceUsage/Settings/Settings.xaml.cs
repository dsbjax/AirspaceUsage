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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            mapSettingsControl.Visibility = eventSettingsControl.Visibility = warningAreaSettingsControl.Visibility =
                Visibility.Collapsed;

            mapSettingsButton.FontWeight = eventSettingsButton.FontWeight = warningAreaSettingsButton.FontWeight =
                FontWeights.Normal;

            ((Button)sender).FontWeight = FontWeights.Bold;
            ((UIElement)((Button)sender).Tag).Visibility = Visibility.Visible;
        }
    }
}
