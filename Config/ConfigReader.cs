using Microsoft.Extensions.Configuration;

namespace SauceDemoLogin.Config
{
    public static class ConfigReader
    {
        private static readonly PlaywrightSettings _settings;

        // El constructor estático asegura que se lea el archivo UNA SOLA VEZ
        static ConfigReader()
        {
            
            // Configuración para leer el archivo JSON de configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) 
                .AddJsonFile("Config/playwright-settings.json", optional: false, reloadOnChange: true)
                .Build();

            // Mapear la configuración al objeto PlaywrightSettings
            _settings = new PlaywrightSettings();
            configuration.GetSection("Playwright").Bind(_settings);
        }

        // Propiedad pública para acceder a la configuración desde cualquier lugar
        public static PlaywrightSettings GetConfig => _settings;
    }
}