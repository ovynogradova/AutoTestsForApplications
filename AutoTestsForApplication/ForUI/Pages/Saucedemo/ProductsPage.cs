using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class ProductsPage
{
    private readonly IPage Page;
    private ILocator CheckTextAfterLogin => Page.Locator("//span[text()='Products']");
    private ILocator ShoppingCartLink => Page.Locator(".shopping_cart_link");

    public ProductsPage(IPage page)
    {
        Page = page;
    }
    
    public async Task<string?> GetCheckTextAfterLoginAsync()
    {
        return await CheckTextAfterLogin.TextContentAsync();
    }
    
    public async Task AddToCartAsync(string productName)
    {
        await Page.Locator(".inventory_item")
            .Filter(new() { HasText = productName })
            .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
            .ClickAsync();
    }
   
    public async Task GoToCartAsync()
    {
        await ShoppingCartLink.ClickAsync();
    }
    
}