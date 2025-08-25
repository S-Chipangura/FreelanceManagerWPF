using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace FreelanceManager.Utils
{
    public static class JsonHelper
    {
        public static T? LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SaveToJson<T>(string filePath, T data)
        {
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

