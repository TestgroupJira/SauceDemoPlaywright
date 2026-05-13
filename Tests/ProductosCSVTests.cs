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
[AllureSuite("Suite de Productos SauceDemo")]
public class ProductosCSVTests : BaseTest
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

    // Metodo para leer los datos del archivo de productos
    public static IEnumerable<TestCaseData> LeerProductosSmoke()
    {
        // Leer los datos del archivo CSV usando el metodo LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("productos_smoke.csv", true))
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
    [Category("Smoke")]
    [TestCaseSource(nameof(LeerProductosSmoke))]
    public async Task ValidarCarritoSmoke(string username, string password, string producto1, string producto2)
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


        // Metodo para leer los datos del archivo de productos
    public static IEnumerable<TestCaseData> LeerProductosRegression()
    {
        // Leer los datos del archivo CSV usando el metodo LeerCsv de la clase CsvReader
        foreach (var valores in CsvReader.LeerCsv("productos_regression.csv", true))
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
    [Category("Regression")]
    [TestCaseSource(nameof(LeerProductosRegression))]
    public async Task ValidarCarritoRegression(string username, string password, string producto1, string producto2)
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
