using Microsoft.Playwright;

namespace apitest.ForUI.Pages.Demoqa;

public class PracticeFormPage
{
    private readonly IPage Page;
    private ILocator FirstNameInput => Page.Locator("#firstName");
    private ILocator LastNameInput => Page.Locator("#lastName");
    private ILocator GenderWrapper => Page.Locator("#genterWrapper");
    private ILocator MobileInput => Page.Locator("#userNumber");
    private ILocator SubmitButton => Page.Locator("#submit");
    private ILocator SubmissionTitle => Page.Locator("#example-modal-sizes-title-lg");

    public PracticeFormPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAsync()
    {
        await Page.GotoAsync("https://demoqa.com/automation-practice-form");
    }

    public async Task FillFormAsync(PracticeFormData data)
    {
        await FirstNameInput.FillAsync(data.FirstName);
        await LastNameInput.FillAsync(data.LastName);
        await GenderWrapper.GetByText(data.Gender.ToString(), new() { Exact = true }).ClickAsync();
        await MobileInput.FillAsync(data.Mobile);
    }

    public async Task SubmitAsync()
    {
        await SubmitButton.ClickAsync();
    }

    public async Task<string?> GetSubmissionTitleAsync()
    {
        return await SubmissionTitle.TextContentAsync();
    }
}