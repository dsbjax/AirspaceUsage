using Microsoft.Win32;
using System;
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

namespace AirspaceUsage.Settings
{
    /// <summary>
    /// Interaction logic for MapSettings.xaml
    /// </summary>
    public partial class MapSettings : UserControl
    {

        private string appFolder = 
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + 
            Properties.Settings.Default.AppFolder;

        public MapSettings()
        {
            InitializeComponent();
        }

        private void GetMapAreasFilenameClick(object sender, RoutedEventArgs e)
        {


            var openFile = new OpenFileDialog
            {
                Title = "Select Warning Ares Coordinates File",
                InitialDirectory = appFolder,
                Filter = "Areas|*.areas|FACTS Display Map|*.src"
            };

            if ((bool)openFile.ShowDialog())
            {
                var selectedFile = openFile.FileName;
                var selectedFilename = openFile.SafeFileName;
                var extention = openFile.SafeFileName.Substring(openFile.SafeFileName.LastIndexOf('.') + 1);

                if(extention == "areas")
                {
                    if(!File.Exists(appFolder + "\\" + selectedFilename))
                        File.Copy(selectedFile, appFolder + "\\" + selectedFilename, true);

                    Properties.Settings.Default.MapAreasFile = selectedFilename;
                }else
                {
                    if (ConvertToAreasFile(selectedFile))
                        Properties.Settings.Default.MapAreasFile = selectedFilename.Replace(".src", ".areas");
                }

            }
        }

        private bool ConvertToAreasFile(string selectedFile)
        {
            var inputFile = new StreamReader(selectedFile);
            var outputFile = new StreamWriter(appFolder + "\\" +
                selectedFile.Substring(selectedFile.LastIndexOf('\\') + 1).Replace(".src", ".areas"));

            string line;
            while (!inputFile.EndOfStream)
            {
                line = inputFile.ReadLine();

                if(line.StartsWith("HGS"))
                    outputFile.WriteLine("\nGROUP: " + line.Substring(line.IndexOf(' ') + 1).Substring(line.IndexOf(' ') + 1));

                if(line.StartsWith("HAN"))
                    outputFile.WriteLine("\nAREA: " + line.Substring(line.IndexOf(' ') + 1));

                if (line.StartsWith("LST") || line.StartsWith("LEN"))
                    outputFile.WriteLine("LAT/LONG: " + line.Substring(line.IndexOf(' ') + 1));

            }

            outputFile.Flush();
            outputFile.Close();

            return true;
        }
    }
}
