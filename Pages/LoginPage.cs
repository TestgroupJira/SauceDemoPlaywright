using Microsoft.Playwright;

namespace SauceDemoLogin.Pages
{
    public class LoginPage
    {
        // El objeto Page es el motor de busqueda
        private readonly IPage _page;

        // Constructor de la clase
        public LoginPage(IPage page)
        {
            _page = page;
        }

        // Localizadores de los elementos de la página
        private ILocator _usernameInput => _page.GetByPlaceholder("Username");
        private ILocator _passwordInput => _page.GetByPlaceholder("Password");
        private ILocator _loginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        private ILocator _errorMsjLockedOut => _page.GetByText("Epic sadface: Sorry, this user has been locked out.");
        private ILocator _errorMsjPasswordIncorrecto => _page.GetByText("Epic sadface: Username and password do not match any user in this service");

        // Métodos para interactuar con la página
        public async Task LoginAsync(string username, string password)
        {
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }

        // Métodos para obtener elementos de la página


        public async Task<ILocator> ObtenerMensajeErrorLockedOutAsync()
        {
            return _errorMsjLockedOut;
        }

        public async Task<ILocator> ObtenerMensajeErrorPasswordIncorrectoAsync()
        {
            return _errorMsjPasswordIncorrecto;
        }

    }
}