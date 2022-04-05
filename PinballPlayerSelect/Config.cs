using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PPS
{
    public class Config
    {
        private const string _configFilePath = "config.json";
        public static ConfigValues Configuration { get; set; }

   
        private string AddPaths(string rootPath,  string target, string prefix)
        {
            if (rootPath == null) return target;
            if (target != null) return target;
            return Path.Combine(rootPath, $"{prefix} Images");
        }

        public void ReadConfig()
        {
            var configString = File.ReadAllText(_configFilePath);
            Configuration = JsonConvert.DeserializeObject<ConfigValues>(configString);

            // Set Paths using default PinballX-Config if not specified otherwise:
            Configuration.Media.BackGlass = AddPaths(Configuration.Media.Root, Configuration.Media.BackGlass, "Backglass");
            Configuration.Media.PlayField = AddPaths(Configuration.Media.Root, Configuration.Media.PlayField, "Table");
            Configuration.Media.Dmd= AddPaths(Configuration.Media.Root, Configuration.Media.Dmd, "DMD");
        }
    }
}
