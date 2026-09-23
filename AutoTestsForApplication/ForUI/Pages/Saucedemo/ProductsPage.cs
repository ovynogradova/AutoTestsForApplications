using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class ProductsPage
{
    private readonly IPage Page;
    private ILocator CheckTextAfterLogin => Page.Locator("//span[text()='Products']");

    public ProductsPage(IPage page)
    {
        Page = page;
    }
    
    public async Task<string?> GetCheckTextAfterLoginAsync()
    {
        return await CheckTextAfterLogin.TextContentAsync();
    }
}