using System.IO;
using System.Text.Json;

namespace ASM_SOF3091.Config
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string Browser { get; set; }
        public int TimeoutSeconds { get; set; }
        public bool Headless { get; set; }
    }

    public static class ConfigReader
    {
        public static AppSettings LoadSettings()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Config", "appsettings.json");
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
    }
}