namespace Lab4_SOF3091.Config;

public sealed class TestConfig
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = "Chrome";
    public int TimeoutSeconds { get; set; } = 10;
    public bool Headless { get; set; } = false;
    public int WindowWidth { get; set; } = 1920;
    public int WindowHeight { get; set; } = 1080;
}
