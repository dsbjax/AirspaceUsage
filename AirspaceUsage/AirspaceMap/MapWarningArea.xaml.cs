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
    /// Interaction logic for MapWarningArea.xaml
    /// </summary>
    public partial class MapWarningArea : UserControl
    {
        private readonly WarningArea warningArea;
        private readonly double mapSize;

        public string WarningAreaName => warningArea.WarningAreaName;
        public SolidColorBrush AreaBackgroundBrush { get { return areaBackground; } set { areaBackground = value; } }

        public MapWarningArea(WarningArea warningArea, double mapSize)
        {
            InitializeComponent();

            this.warningArea = warningArea;
            this.mapSize = mapSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            /*
            var mapNorthWestPoints = Properties.Settings.Default.MapNorthWest.Split(',');
            var mapSouthEastPoints = Properties.Settings.Default.MapSouthEast.Split(',');

            var longMapSize = Properties.Settings.Default.MapSize;
            var latMapSize = longMapSize;
            */
            mapArea.Points.Clear();
            foreach (var coordinate in warningArea)
                mapArea.Points.Add(
                    new Point(
                        (Properties.Settings.Default.NorthWestLong - coordinate.Long) * (mapSize / Properties.Settings.Default.MapSize),
                        (Properties.Settings.Default.NorthWestLat - coordinate.Lat) * (mapSize / Properties.Settings.Default.MapSize))
                        );
        }
    }
}
