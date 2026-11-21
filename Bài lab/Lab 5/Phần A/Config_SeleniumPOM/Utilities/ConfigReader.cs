using System.Text.Json;

namespace SeleniumPOM_CSharp.Utilities;

public sealed class TestSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = string.Empty;
    public int Timeout { get; set; }
    public bool Headless { get; set; }
}

public static class ConfigReader
{
    private static readonly Lazy<TestSettings> _settings = new(LoadSettings);

    public static TestSettings Settings => _settings.Value;

    private static TestSettings LoadSettings()
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            "Config",
            "appsettings.json");

        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Không tìm thấy file cấu hình: {path}", path);

        string json = File.ReadAllText(path);

        var root = JsonSerializer.Deserialize<ConfigRoot>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        TestSettings settings = root?.TestSettings
            ?? throw new InvalidOperationException(
                "Không tìm thấy mục TestSettings trong appsettings.json.");

        Validate(settings);
        return settings;
    }

    private static void Validate(TestSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.BaseUrl) ||
            !Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out _))
            throw new InvalidOperationException("BaseUrl không hợp lệ.");

        if (!settings.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase) &&
            !settings.Browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException(
                "Browser chỉ hỗ trợ Chrome hoặc Firefox.");

        if (settings.Timeout <= 0)
            throw new InvalidOperationException("Timeout phải lớn hơn 0.");
    }

    private sealed class ConfigRoot
    {
        public TestSettings? TestSettings { get; set; }
    }
}
