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
    public static IEnumerable<TestCaseData> LeerUsuarios()
    {
        // Leer los datos del archivo CSV utilizando el método LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("usuarios.csv", false))
        {
            // Crear un objeto TestCaseData con los valores leidos del archivo CSV
            yield return new TestCaseData(valores[0], valores[1]);
        }
    }

    [Test]
    [AllureName("Login exitoso con datos de CSV")]
    [AllureDescription("Este test verifica que un usuario pueda iniciar sesión exitosamente utilizando datos obtenidos de un archivo CSV.")]
    [AllureTag("Login", "CSV", "Exitoso")]
    [AllureSeverity(SeverityLevel.critical)]
    [TestCaseSource(nameof(LeerUsuarios))]
    public async Task LoginExitoso(string username, string password)
    {
        // Navegar a la página de login
        await Page.GotoAsync(ConfigReader.GetConfig.Urls["UrlBase"]);

        // Realizar el login con los datos obtenidos del archivo CSV
        await _loginPage.LoginAsync(username, password);
        // Verificar que se haya navegado a la página de productos
        var tituloPagina = await _productsPage.ObtenerTituloProductosAsync();
        await Assertions.Expect(tituloPagina).ToBeVisibleAsync();
        
    }

    // Metodo para leer los datos del archivo de productos
    public static IEnumerable<TestCaseData> LeerProductos()
    {
        // Leer los datos del archivo CSV usando el metodo LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("productos.csv", true))
        {
            // Crear un objeto TestCaseData con los valores leidos del archivo CSV
            yield return new TestCaseData(valores[0], valores[1], valores[2], valores[3]);
        }
    }


    [Test]
    [AllureName("Validar carrito de compras con productos del CSV")]
    [AllureDescription("Este test verifica que los productos se agreguen y quiten correctamente del carrito.")]
    [AllureTag("Carrito", "Compras")]
    [AllureSeverity(SeverityLevel.normal)]
    [TestCaseSource(nameof(LeerProductos))]
    public async Task ValidarCarrito(string username, string password, string producto1, string producto2)
    {
        // Navegar a la página de login
        await Page.GotoAsync(ConfigReader.GetConfig.Urls["UrlBase"]);

        // Realizar el login con los datos obtenidos del archivo CSV
        await _loginPage.LoginAsync(username, password);

        // Verificar que se haya navegado a la página de productos
        var tituloPagina = await _productsPage.ObtenerTituloProductosAsync();
        await Assertions.Expect(tituloPagina).ToBeVisibleAsync();

        // Agregar el primer producto al carrito
        await _productsPage.AgregarProductoAlCarritoAsync(producto1);
        // Agregar el segundo producto al carrito
        await _productsPage.AgregarProductoAlCarritoAsync(producto2);
        // Quitar el primer producto del carrito
        await _productsPage.QuitarProductoDelCarritoAsync(producto1);

        // Verificar que el carrito tenga un producto
        var carrito = await _productsPage.ObtenerCarritoAsync();
        await Assertions.Expect(carrito).ToHaveTextAsync("1");
    }
}
