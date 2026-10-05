using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;
    private ILocator CompleteHeader => Page.Locator(".complete-header");

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task<string?> GetCompleteMessageAsync()
    {
        return await CompleteHeader.TextContentAsync();
    }
}