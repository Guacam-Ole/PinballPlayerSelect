using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PinballPlayerSelect
{
    public class Config
    {
        private const string _configFilePath = "config.json";
        public ConfigValues Configuration { get; set; }

        public Config()
        {
            ReadConfig();
        }

        private void AutoSetRotation()
        {
            foreach  (var displaySettings in Configuration.Displays)
            {
                if (displaySettings.Table!=null)
                {
                    SetRotationForScreen(Configuration.PinballX.Table.Rotate, displaySettings.Table);
                }
            }
        }

        private void SetRotationForScreen(int pbxRotation, DisplaySettings displaySettings)
        {
            if (displaySettings.Rotate != -1) return;
            displaySettings.Rotate = pbxRotation - 90;
        }


        private void AddPaths(string mediaPath, PinballXDisplay target, string prefix)
        {
            if (mediaPath == null) return;
            if (target == null) return;
            if (target.ImagePath == null) target.ImagePath = Path.Combine(mediaPath, $"{prefix} Images");
        }

        public void ReadConfig()
        {
            var configString = File.ReadAllText(_configFilePath);
            Configuration = JsonConvert.DeserializeObject<ConfigValues>(configString);
            if (string.IsNullOrEmpty(Configuration.PinballX.ConfigIniFile)) return;
            var iniData = new IniParser.FileIniDataParser().ReadFile(Configuration.PinballX.ConfigIniFile);
            Configuration.PinballX.Table = iniData.ParseMainDisplaySettings("Display");
            Configuration.PinballX.BackGlass = iniData.ParseSecondaryDisplaySettings("BackGlass");
            Configuration.PinballX.Dmd = iniData.ParseSecondaryDisplaySettings("DMD");
            AddPaths(Configuration.PinballX.MediaPath, Configuration.PinballX.BackGlass, "Backglass");
            AddPaths(Configuration.PinballX.MediaPath, Configuration.PinballX.Table, "Table");
            AddPaths(Configuration.PinballX.MediaPath, Configuration.PinballX.Dmd, "DMD");
            AutoSetRotation();
        }
    }
}
