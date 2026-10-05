using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class LoginPage
{
    private readonly IPage Page;
    private ILocator LoginInput => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PasswordInput => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPage(IPage page)
    {
        Page = page;
    }
    
    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
    }

    public async Task FillLoginFormAsync(string username, string password)
    {
        await LoginInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
    
}