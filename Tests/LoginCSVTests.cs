using Microsoft.Playwright;
using SauceDemoLogin.Base;
using SauceDemoLogin.Config;
using SauceDemoLogin.Pages;
using SauceDemoLogin.Utils;
using Allure.NUnit;
using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;

namespace SauceDemoLogin.Tests;

// Clase de pruebas para el login utilizando datos de un archivo CSV
[TestFixture]
[AllureNUnit]
[AllureSuite("Suite de Login SauceDemo")]
public class LoginCSVTests : BaseTest
{
    // Declarar las variables para las páginas de login y productos
    private LoginPage _loginPage;
    private ProductsPage _productsPage;

    [SetUp]
    public async Task SetUp()
    {
        // Inicializar las páginas de login y productos
        _loginPage = new LoginPage(Page);
        _productsPage = new ProductsPage(Page);
    }

    // Metodo para obtener los datos del archivo CSV
    public static IEnumerable<TestCaseData> LeerUsuariosSmoke()
    {
        // Leer los datos del archivo CSV utilizando el método LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("usuarios_smoke.csv", false))
        {
            // Crear un objeto TestCaseData con los valores leidos del archivo CSV
            yield return new TestCaseData(valores[0], valores[1]);
        }
    }

    [Test]
    [AllureName("Login exitoso con datos de CSV Smoke")]
    [AllureDescription("Este test verifica que un usuario pueda iniciar sesión exitosamente utilizando datos obtenidos de un archivo CSV.")]
    [AllureTag("Login", "CSV", "Exitoso", "Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [Category("Smoke")]
    [TestCaseSource(nameof(LeerUsuariosSmoke))]
    public async Task LoginExitosoSmoke(string username, string password)
    {
        // Navegar a la página de login
        await Page.GotoAsync(ConfigReader.GetConfig.Urls["UrlBase"]);

        // Realizar el login con los datos obtenidos del archivo CSV
        await _loginPage.LoginAsync(username, password);
        // Verificar que se haya navegado a la página de productos
        var tituloPagina = await _productsPage.ObtenerTituloProductosAsync();
        await Assertions.Expect(tituloPagina).ToBeVisibleAsync();
        
    }

    // Metodo para obtener los datos del archivo CSV
    public static IEnumerable<TestCaseData> LeerUsuariosRegression()
    {
        // Leer los datos del archivo CSV utilizando el método LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("usuarios_regression.csv", false))
        {
            // Crear un objeto TestCaseData con los valores leidos del archivo CSV
            yield return new TestCaseData(valores[0], valores[1]);
        }
    }

    [Test]
    [AllureName("Login exitoso con datos de CSV Regression")]
    [AllureDescription("Este test verifica que un usuario pueda iniciar sesión exitosamente utilizando datos obtenidos de un archivo CSV.")]
    [AllureTag("Login", "CSV", "Exitoso", "Regression")]
    [AllureSeverity(SeverityLevel.critical)]
    [Category("Regression")]
    [TestCaseSource(nameof(LeerUsuariosRegression))]
    public async Task LoginExitosoRegression(string username, string password)
    {
        // Navegar a la página de login
        await Page.GotoAsync(ConfigReader.GetConfig.Urls["UrlBase"]);

        // Realizar el login con los datos obtenidos del archivo CSV
        await _loginPage.LoginAsync(username, password);
        // Verificar que se haya navegado a la página de productos
        var tituloPagina = await _productsPage.ObtenerTituloProductosAsync();
        await Assertions.Expect(tituloPagina).ToBeVisibleAsync();   
    }
}
