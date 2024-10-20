using Newtonsoft.Json;

using System.IO;

namespace PPS
{
    public class Config
    {
        private const string _configFilePath = "config.json";

        public static ConfigValues ReadConfig(string file=_configFilePath)
        {
            var configString = File.ReadAllText(file);
            var configuration = JsonConvert.DeserializeObject<ConfigValues>(configString);
            return configuration;
        }
    }
}