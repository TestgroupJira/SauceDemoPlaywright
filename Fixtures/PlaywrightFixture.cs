using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using SauceDemoLogin.Config;
using Allure.Net.Commons;
using SauceDemoLogin.Pages;

namespace SauceDemoLogin.Fixtures
{
    public class PlaywrightFixture
    {
        // Declarar las variables para Context y Page
        public IBrowserContext Context { get; private set; } = null!;
        public IPage Page { get; private set; } = null!;

        // Metodo para crear un nuevo contexto y página para cada test
        public async Task<IPage> CrearPage(IBrowser browser)
        {
            var timeout = ConfigReader.GetConfig.Timeout;

            // Configurar las opciones del contexto, incluyendo el tamaño de la ventana y la autenticación si es necesario
            var opcionesContexto = new BrowserNewContextOptions
            {
                // Iniciar el navegador para que se ajuste al tamaño de la ventana 
                ViewportSize = ViewportSize.NoViewport, 
            };

            // Crear un nuevo contexto con las opciones configuradas
            Context = await browser.NewContextAsync(opcionesContexto);

            // Configurar un tiempo de espera
            Context.SetDefaultTimeout(timeout);

            // Iniciar el tracing para cada test
            await Context.Tracing.StartAsync(new TracingStartOptions
            {   // Configurar el título de la traza con el nombre del test actual
                Title = TestContext.CurrentContext.Test.Name,
                Screenshots = true, // Habilitar la captura de pantallas durante el tracing
                Snapshots = true, // Habilitar la captura de snapshots del DOM durante el tracing
                Sources = true // Habilitar la captura de fuentes de red durante el tracing
            });

            // Crear una nueva página para cada test
            Page = await Context.NewPageAsync();
            return Page;
        }

        // Metodo para cerrar el contexto después de cada test
        public async Task CerrarContext()
        {
            // Obtener el estado del test actual
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            // Obtener el nombre del usuario desde los argumentos del test
            var args = TestContext.CurrentContext.Test.Arguments;
            // Si no hay argumentos, asignar "SinDatos". Si el primer argumento es null, asignar "Desconocido". De lo contrario, usar el valor del primer argumento.
            string usuario = args.Length > 0 ? args[0]?.ToString() ?? "Desconocido" : "SinDatos"; 

            // Si el test ha fallado, detener el tracing y guardar la traza en un archivo zip
            if (testStatus == TestStatus.Failed)
            {   // Detener el tracing y guardar la traza en un archivo zip con un nombre que incluya el nombre del test y el usuario
                string traceName = $"{TestContext.CurrentContext.Test.MethodName}_{usuario}.zip";
                // Crear la ruta completa para guardar la traza
                string tracePath = Path.Combine(Environment.CurrentDirectory, "Evidencia", "Traces", traceName);
                // Asegurarse de que el directorio para guardar la traza exista
                Directory.CreateDirectory(Path.GetDirectoryName(tracePath)!);
                // Detener el tracing y guardar la traza en el archivo zip especificado
                await Context.Tracing.StopAsync(new() 
                {
                    Path = tracePath 
                });

                // Adjuntar la traza al reporte de Allure
                AllureApi.AddAttachment("Traza del Error", "application/zip", tracePath);

                // Tomar screenshot para Allure
                var screenshotPath = $"Evidencia/Screenshots/error_{DateTime.Now:HHmmss}.png";
                await Page.ScreenshotAsync(new() { Path = screenshotPath });

                // Adjuntar al reporte de Allure
                AllureApi.AddAttachment("Captura del Error", "image/png", screenshotPath);             

            }
            else
            {
                // Detener el tracing si el test no ha fallado
                await Context.Tracing.StopAsync();
            }
            
            // Cerrar el contexto después de cada test
            await Context.CloseAsync();
        }
    }
}
