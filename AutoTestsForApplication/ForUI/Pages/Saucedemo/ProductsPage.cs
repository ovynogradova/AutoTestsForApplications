using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class ProductsPage
{
    private readonly IPage Page;
    private ILocator CheckTextAfterLogin => Page.Locator("//span[text()='Products']");
    private ILocator ShoppingCartLink => Page.Locator(".shopping_cart_link");
    private ILocator InventoryItems => Page.Locator(".inventory_item");
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
        await InventoryItems
            .Filter(new() { HasText = productName })
            .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
            .ClickAsync();
    }
   
    public async Task GoToCartAsync()
    {
        await ShoppingCartLink.ClickAsync();
    }
    
}