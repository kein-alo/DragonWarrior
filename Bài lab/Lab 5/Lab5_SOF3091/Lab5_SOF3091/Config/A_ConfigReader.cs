using System.Text.Json;

namespace Lab5_SOF3091.Config;

/// <summary>PHẦN A - Đọc appsettings.json (copy ra bin/Debug nhờ CopyToOutputDirectory).</summary>
public static class ConfigReader
{
    private static readonly Lazy<TestConfig> _config = new(LoadFromFile);

    public static TestConfig Settings => _config.Value;

    private static TestConfig LoadFromFile()
    {
        // AppContext.BaseDirectory luôn trỏ đúng bin/Debug/..., không phụ thuộc cách chạy test
        string path = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");

        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Không tìm thấy file cấu hình: {path}. Kiểm tra CopyToOutputDirectory trong .csproj.", path);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var cfg = JsonSerializer.Deserialize<TestConfig>(File.ReadAllText(path), options)
                  ?? throw new InvalidOperationException("Không đọc được appsettings.json.");

        Validate(cfg);
        return cfg;
    }

    // Chỉ kiểm tra giá trị hợp lệ. Việc "trình duyệt nào được hỗ trợ" thuộc về DriverFactory
    // (một nơi duy nhất, để thêm trình duyệt mới chỉ sửa một chỗ).
    private static void Validate(TestConfig cfg)
    {
        if (string.IsNullOrWhiteSpace(cfg.BaseUrl) || !Uri.TryCreate(cfg.BaseUrl, UriKind.Absolute, out _))
            throw new InvalidOperationException("BaseUrl không hợp lệ.");

        if (string.IsNullOrWhiteSpace(cfg.Browser))
            throw new InvalidOperationException("Browser không được để trống.");

        if (cfg.TimeoutSeconds <= 0)
            throw new InvalidOperationException("TimeoutSeconds phải lớn hơn 0.");
    }
}
