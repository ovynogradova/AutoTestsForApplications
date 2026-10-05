using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Saucedemo;

public class CheckoutPage
{
    private readonly IPage Page;
    private ILocator FirstNameInput => Page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" });
    private ILocator LastNameInput => Page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" });
    private ILocator PostalCodeInput => Page.GetByRole(AriaRole.Textbox, new() { Name = "Zip/Postal Code" });
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public CheckoutPage(IPage page)
    {
        Page = page;
    }

    public async Task FillCheckoutFormAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await PostalCodeInput.FillAsync(postalCode);
        await ContinueButton.ClickAsync();
    } 
}