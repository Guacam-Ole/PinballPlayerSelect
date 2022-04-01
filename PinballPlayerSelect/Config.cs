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
            ReadPinballXConfig();
        }

        private void AddPaths(string mediaPath, MediaPath target, string prefix)
        {
            if (mediaPath == null) return;
            if (target == null) return;
            if (target.Videos == null) target.Videos = Path.Combine(mediaPath, $"{prefix} Videos");
            if (target.Images == null) target.Images= Path.Combine(mediaPath, $"{prefix} Images");
        }

        public void ReadConfig()
        {
            var configString = System.IO.File.ReadAllText(_configFilePath);
            Configuration = JsonConvert.DeserializeObject<ConfigValues>(configString);
            AddPaths(Configuration.PinballX.Media.Path, Configuration.PinballX.Media.BackGlass, "Backglass");
            AddPaths(Configuration.PinballX.Media.Path, Configuration.PinballX.Media.Table, "Table");
            AddPaths(Configuration.PinballX.Media.Path, Configuration.PinballX.Media.Dmd, "DMD");
        }

        private void ReadPinballXConfig()
        {
            if (string.IsNullOrEmpty(Configuration.PinballX.ConfigIniFile)) return;
            var iniData = new IniParser.FileIniDataParser().ReadFile(Configuration.PinballX.ConfigIniFile);
            Configuration.PinballX.Table = iniData.ParseMainDisplaySettings("Display");
            Configuration.PinballX.BackGlass = iniData.ParseSecondaryDisplaySettings("BackGlass");
            Configuration.PinballX.Dmd = iniData.ParseSecondaryDisplaySettings("DMD");
        }
    }
}
