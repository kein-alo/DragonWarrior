using System;
using System.IO;
using System.Text.Json;

namespace Lab5_SOF3091_phanBC.Config;

public static class ConfigReader
{
    private static readonly Lazy<TestConfig> _config = new(LoadFromFile);

    public static TestConfig Settings => _config.Value;

    public static TestConfig Load() => _config.Value;

    private static TestConfig LoadFromFile()
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            "Config",
            "appsettings.json");

        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Không tìm thấy file cấu hình: {path}", path);

        string json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Hỗ trợ cả kiểu JSON phẳng ở root lẫn kiểu bọc trong "TestSettings"
        using var doc = JsonDocument.Parse(json);
        TestConfig? cfg;
        if (doc.RootElement.TryGetProperty("TestSettings", out var testSettingsElem))
        {
            cfg = testSettingsElem.Deserialize<TestConfig>(options);
        }
        else
        {
            cfg = JsonSerializer.Deserialize<TestConfig>(json, options);
        }

        if (cfg == null)
            throw new InvalidOperationException("Không thể đọc dữ liệu cấu hình từ appsettings.json.");

        Validate(cfg);
        return cfg;
    }

    private static void Validate(TestConfig settings)
    {
        if (string.IsNullOrWhiteSpace(settings.BaseUrl) ||
            !Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out _))
            throw new InvalidOperationException("BaseUrl không hợp lệ.");

        if (!settings.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase) &&
            !settings.Browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException(
                $"Trình duyệt '{settings.Browser}' không được hỗ trợ. Chỉ hỗ trợ Chrome hoặc Firefox.");

        if (settings.TimeoutSeconds <= 0)
            throw new InvalidOperationException("TimeoutSeconds phải lớn hơn 0.");
    }
}
