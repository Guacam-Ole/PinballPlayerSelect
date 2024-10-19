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
            return configuration;
        }
    }
}