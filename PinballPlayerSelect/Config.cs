using Newtonsoft.Json;

using System.IO;

namespace PPS
{
    public class Config
    {
        private const string _configFilePath = "config.json";

        private static string AddPaths(string rootPath, string target, string prefix)
        {
            if (rootPath == null) return target;
            if (target != null) return target;
            return Path.Combine(rootPath, $"{prefix} Images");
        }

        public static ConfigValues ReadConfig()
        {
            var configString = File.ReadAllText(_configFilePath);
            var configuration = JsonConvert.DeserializeObject<ConfigValues>(configString);

            // Set Paths using default PinballX-Config if not specified otherwise:
            foreach (var emulator in configuration.Emulators)
            {
                emulator.Media.BackGlass = AddPaths(emulator.Media.Root, emulator.Media.BackGlass, "Backglass");
                emulator.Media.PlayField = AddPaths(emulator.Media.Root, emulator.Media.PlayField, "Table");
                emulator.Media.Dmd = AddPaths(emulator.Media.Root, emulator.Media.Dmd, "DMD");
            }
            return configuration;
        }
    }
}