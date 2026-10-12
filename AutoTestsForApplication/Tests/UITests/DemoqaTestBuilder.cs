using apitest.ForUI.Pages.Demoqa;
using FluentAssertions;

namespace apitest.UITests;

public class DemoqaTestBuilder: BaseTest
{
    [Test]
    public async Task SubmitFormWithRequiredFieldsOnly()
    {
        var student = new PracticeForm()
            .WithFirstName("Anna")
            .WithLastName("Smith")
            .WithGender(Gender.Female)
            .WithMobile("1234567890")
            .Build();

        var formPage = new PracticeFormPage(Page);
        await formPage.OpenAsync();
        await formPage.FillFormAsync(student);
        await formPage.SubmitAsync();

        var title = await formPage.GetSubmissionTitleAsync();
        title.Should().Be("Thanks for submitting the form");
    }
}