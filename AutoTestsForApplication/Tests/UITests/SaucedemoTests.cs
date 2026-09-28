using apitest.ForUI.Pages.Saucedemo;
using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.UITests;

public class SaucedemoTests: BaseTest

{
    //[Test]
    /*public async Task SuccessLoginOld()
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
        await Assertions.Expect(checkMessage).ToBeVisibleAsync();
    }*/
    
    [Test]
    public async Task SuccessLoginPom()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.FillLoginFormAsync("standard_user", "secret_sauce");
        
        var expectedItems = new[] { "Sauce Labs Fleece Jacket", "Sauce Labs Bolt T-Shirt" };

        var productsPage = new ProductsPage(Page);
        foreach (var item in expectedItems)
        {
            await productsPage.AddToCartAsync(item);
        }
        await productsPage.GoToCartAsync();

        var cartPage = new CartPage(Page);
        var cartItems = await cartPage.GetCartItemNamesAsync();
        cartItems.Should().BeEquivalentTo(expectedItems);

        await cartPage.ClickCheckoutAsync();

       
    }

    
    
}