using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.UITests;

public class HerokuTests : BaseTest
{
    [Test]
    public async Task SuccessLogin()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
        var loginInput = Page.Locator("//input[@id='user-name']");
        await loginInput.FillAsync("standard_user");
        var passwordInput = Page.GetByRole(AriaRole.Textbox, new() {Name = "password"});
        await passwordInput.FillAsync("secret_sauce");
        var loginButton = Page.Locator("//input[@id='login-button']");
        await loginButton.ClickAsync();
        var checkMessage = Page.Locator("//span[text()='Products']");
        var state = await checkMessage.IsVisibleAsync();
        state.Should().BeTrue();

    }
    
    
}