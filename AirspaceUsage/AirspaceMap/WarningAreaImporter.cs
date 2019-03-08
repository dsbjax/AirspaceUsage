using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirspaceUsage.AirspaceMap
{
    public class WarningAreaImporter : IEnumerable<WarningArea>
    {
        private const string AREAHEADER = "AREA: ";
        private const string GROUPHEADER = "GROUP: ";
        private const string LATLONGHEADER = "LAT/LONG: ";
        private const string REGEXEXP = "(?<NS>[NS])(?<LatDegrees>\\d\\d):(?<LatMins>\\d\\d):(?<LatSecs>\\d\\d).*(?<EW>[WE])(?<LongDegrees>\\d\\d\\d):(?<LongMins>\\d\\d):(?<LongSecs>\\d\\d)";

        private Regex latLongRegex = new Regex(REGEXEXP);
        private List<WarningArea> warningAreas = new List<WarningArea>();
        private readonly string appFolder = 
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            "\\" + Properties.Settings.Default.AppFolder + "\\";

#pragma warning disable IDE0044 // Add readonly modifier
        private string currentGroup = null;
#pragma warning restore IDE0044 // Add readonly modifier

        public WarningAreaImporter(string filename)
        {
            if(!File.Exists(filename))
            {
                filename = appFolder + filename;
            }

            if (File.Exists(filename))
            {
                using (StreamReader readFile = new StreamReader(filename))
                {
                    string line;
                    while (!readFile.EndOfStream)
                    {
                        line = readFile.ReadLine();
                        if (line.StartsWith(GROUPHEADER))
                            currentGroup = line.Replace(GROUPHEADER,"");
                        else if (line.StartsWith(AREAHEADER))
                        {
                            var newWarningArea = new WarningArea(line.Replace(AREAHEADER,""), currentGroup);

                            while (!readFile.EndOfStream && (line = readFile.ReadLine()).StartsWith(LATLONGHEADER))
                            {
                                var results = latLongRegex.Match(line);
                                double latitude = double.Parse(results.Groups[2].Value) + (double.Parse(results.Groups[3].Value) / 60) + (double.Parse(results.Groups[4].Value) / 3600);
                                double longitude = double.Parse(results.Groups[6].Value) + (double.Parse(results.Groups[7].Value) / 60) + (double.Parse(results.Groups[8].Value) / 3600);

                                if (results.Groups[1].Value == "S") latitude *= -1;
                                if (results.Groups[5].Value == "E") longitude *= -1;

                                newWarningArea.AddCoordinate(new WarningAreaCoordinates(latitude, longitude));
                            }
                            warningAreas.Add(newWarningArea);
                        }
                    }
                }
            }

        }

        public IEnumerator<WarningArea> GetEnumerator()
        {
            return warningAreas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return warningAreas.GetEnumerator();
        }
    }

    public class WarningArea : IEnumerable<WarningAreaCoordinates>
    {
        public readonly string GroupName;
        public readonly string WarningAreaName;
        private List<WarningAreaCoordinates> Coordinates = new List<WarningAreaCoordinates>();

        internal WarningArea(string name, string groupName)
        {
            WarningAreaName = name;
            GroupName = groupName;
        }

        internal void AddCoordinate(WarningAreaCoordinates coordinates)
        {
            Coordinates.Add(coordinates);
        }

        public IEnumerator<WarningAreaCoordinates> GetEnumerator()
        {
            return Coordinates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Coordinates.GetEnumerator();
        }
    }

    public struct WarningAreaCoordinates
    {
        public readonly double Lat, Long;

        internal WarningAreaCoordinates(double latitude, double longitude)
        {
            Lat = latitude;
            Long = longitude;
        }
    }


}
