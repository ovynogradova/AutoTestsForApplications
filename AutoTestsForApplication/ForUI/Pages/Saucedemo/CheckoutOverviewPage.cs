using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;
    private ILocator CartItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });

    public CheckoutOverviewPage(IPage page)
    {
        Page = page;
    }

    public async Task<List<string>> GetCartItemNamesAsync()
    {
        return (await CartItemNames.AllTextContentsAsync()).ToList();
    }

    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
}