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

namespace AirspaceUsage.AirspaceMap
{
    /// <summary>
    /// Interaction logic for MapZoomPan.xaml
    /// </summary>
    public partial class MapZoomPan : UserControl
    {
        public MapZoomPan()
        {
            InitializeComponent();
        }

        private void ZoomInClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
            {
                Properties.Settings.Default.RedrawMap = false;
                Properties.Settings.Default.NorthWestLat -= .25;
                Properties.Settings.Default.NorthWestLong -= .25;
                Properties.Settings.Default.MapSize -= .5;
                Properties.Settings.Default.RedrawMap = true;
            }
        }

        private void MouseEnterControl(object sender, MouseEventArgs e)
        {
            Opacity = .8;
        }

        private void ZoomOutClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
            {
                Properties.Settings.Default.RedrawMap = false;
                Properties.Settings.Default.NorthWestLat += .25;
                Properties.Settings.Default.NorthWestLong += .25;
                Properties.Settings.Default.MapSize += .5;
                Properties.Settings.Default.RedrawMap = true;
            }

        }

        private void MouseLeaveControl(object sender, MouseEventArgs e)
        {
            Opacity = .1;
        }

        private void UpMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
                Properties.Settings.Default.NorthWestLat += .25;
        }

        private void LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
                Properties.Settings.Default.NorthWestLong += .25;

        }

        private void RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
                Properties.Settings.Default.NorthWestLong -= .25;

        }

        private void DownMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ChangedButton == MouseButton.Left)
                Properties.Settings.Default.NorthWestLat -= .25;

        }
    }
}
