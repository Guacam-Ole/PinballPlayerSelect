namespace PPS
{
    public static class ImportHelper
    {
        public static int IntParse(this IniParser.Model.KeyDataCollection data, string key, string prefix = null)
        {
            
            var content = data[$"{prefix}{key}"];
            if (content == null) content= data[$"{prefix}{key.ToLower()}"];
            int .TryParse(content, out int value);
            return value;
        }
    }
}