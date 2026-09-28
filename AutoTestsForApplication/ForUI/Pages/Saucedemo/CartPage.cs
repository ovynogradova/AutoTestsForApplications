using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class CartPage
{
    private readonly IPage Page;
    private ILocator CartItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    public CartPage(IPage page)
    {
        Page = page;
    }

    public async Task<List<string>> GetCartItemNamesAsync()
    {
        return (await CartItemNames.AllTextContentsAsync()).ToList();
    }

    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}