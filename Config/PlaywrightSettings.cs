namespace SauceDemoLogin.Config
{

    public class ConfigModel
    {
        public PlaywrightSettings Playwright { get; set; } = new();
    }


    public class PlaywrightSettings
    {
        public Dictionary<string, string> Urls { get; set; } = new();
        public int Timeout { get; set; }
        public string Browser { get; set; } = "Chromium";
    }
}