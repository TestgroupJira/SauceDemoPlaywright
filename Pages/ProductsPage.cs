using Microsoft.Playwright;

namespace SauceDemoLogin.Pages;

public class ProductsPage
{   
    // El objeto Page es el motor de busqueda
    private readonly IPage _page;

    // Constructor de la clase
    public ProductsPage(IPage page)
    {
        _page = page;
    }

    // Localizadores de los elementos de la página
    private ILocator _productsTitle => _page.GetByText("Products");
    private ILocator _itemDiv => _page.Locator(".inventory_item");
    private ILocator _cart => _page.Locator(".shopping_cart_link");

    // Métodos para interactuar con la página

    // Método para agregar un producto al carrito por su nombre
    public async Task AgregarProductoAlCarritoAsync(string nombreProducto)
    {
        // Buscar el producto por su nombre y hacer clic en el botón "Add to cart"
        var boton = _itemDiv
            .Filter(new() { HasTextString = nombreProducto })
            .GetByRole(AriaRole.Button, new() { Name = "Add to cart" });

        await boton.ClickAsync();
    }

     public async Task QuitarProductoDelCarritoAsync(string nombreProducto)
    {
        // Buscar el producto por su nombre y hacer clic en el botón "Add to cart"
        var boton = _itemDiv
            .Filter(new() { HasTextString = nombreProducto })
            .GetByRole(AriaRole.Button, new() { Name = "Remove" });

        await boton.ClickAsync();
    }


    // Método para obtener el carrito
    public async Task<ILocator> ObtenerCarritoAsync()
    {
        return _cart;
    }

    public async Task<ILocator> ObtenerTituloProductosAsync()
    {
        return _productsTitle;
    }
}
