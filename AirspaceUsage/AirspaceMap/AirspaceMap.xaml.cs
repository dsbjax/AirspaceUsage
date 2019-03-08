using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace AirspaceUsage.AirspaceMap
{
    /// <summary>
    /// Interaction logic for AirspaceMap.xaml
    /// </summary>
    public partial class AirspaceMap : UserControl
    {
        private static List<MapWarningArea> mapAreas = new List<MapWarningArea>();

        public AirspaceMap()
        {
            InitializeComponent();

            Properties.Settings.Default.SettingChanging += SettingsChanged;
        }

        private void SettingsChanged(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            if(e.SettingName == "MapAreasFile" || 
                e.SettingName == "NorthWestLat" || 
                e.SettingName == "NorthWestLong" || 
                e.SettingName == "MapSize")

                InvalidateVisual();
        }

        internal static IEnumerable<WarningArea> GetWarningAreas()
        {
            return new WarningAreaImporter(Properties.Settings.Default.MapAreasFile);
        }

        internal static void SetAirspaceAreaEventStatus(string areaName, SolidColorBrush brush)
        {
            var area = mapAreas.Find(a => a.WarningAreaName == areaName);
            area.AreaBackgroundBrush = brush;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            mapCanvas.Children.Clear();

            foreach (var area in new WarningAreaImporter(Properties.Settings.Default.MapAreasFile))
            {
                var newArea = new MapWarningArea(area, ActualHeight);
                mapCanvas.Children.Add(newArea);
            }
        }
    }
}
