using IniParser.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace PinballPlayerSelect
{
    public static class ConfigHelper
    {
        private static PinballXDisplay ParseDisplaySettings(this IniData inidata, string parent, string x, string y, string width, string height, string monitor, string rotate = null)
        {
            var display = new PinballXDisplay
            {
                X = int.Parse(inidata[parent][x]),
                Y = int.Parse(inidata[parent][y]),
                Width = int.Parse(inidata[parent][width]),
                Height = int.Parse(inidata[parent][height]),
                Monitor = int.Parse(inidata[parent][monitor]),
            };
            if (rotate!= null) display.Rotate = int.Parse(inidata[parent][rotate]);
            return display;
        }

        public static PinballXDisplay ParseMainDisplaySettings(this IniData inidata, string parent)
        {
            return ParseDisplaySettings(inidata, parent, "WindowX", "WindowY", "WindowWidth", "WindowHeight", "Monitor", "rotate");
        }

        public static PinballXDisplay ParseSecondaryDisplaySettings(this IniData inidata, string parent)
        {
            return ParseDisplaySettings(inidata, parent, "x", "y", "width", "height", "monitor");
        }
    }


}
