namespace PPS
{
    public static class ImportHelper
    {
        public static int IntParse(this IniParser.Model.KeyDataCollection data, string key, string prefix = null)
        {
            
            var content = data[$"{prefix}{key}"];
            content??= data[$"{prefix}{key.ToLower()}"];
            _ = int.TryParse(content, out int value);
            return value;
        }
    }
}