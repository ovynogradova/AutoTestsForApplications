using apitest.ForUI.Pages.Heroku;
using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.UITests;

public class HerokuTests : BaseTest
{
    [Test]
    public async Task SuccessLogin()
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.FillLoginFormAsync("wrong", "wrong");
        string errorMessage = await loginPage.GetTextFromErrorMessageLabelAsync();
        errorMessage.Should().Contain("Your username is invalid!");
    }

    [Test]
    public async Task DropDown()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/dropdown");
        
        var dropdown = Page.Locator("#dropdown");
        await Assertions.Expect(dropdown).ToBeVisibleAsync();
        await dropdown.SelectOptionAsync("1");
        await Assertions.Expect(dropdown).ToHaveValueAsync("1");
        
        var selected1 = dropdown.Locator("option:checked");
        await Assertions.Expect(selected1).ToHaveTextAsync("Option 1");
        
        var text = await dropdown.InnerTextAsync();
        text.Should().Contain("Option 1");
        
        var opt1 = Page.Locator("//option[@selected='selected']");
        var textOpt1 = await opt1.InnerTextAsync();
        textOpt1.Should().Be("Option 1");
      
    }
    
    [Test]
    public async Task AddRemoveElements()
    {
        AddRemovePage addRemovePage = new AddRemovePage(Page);
        await addRemovePage.OpenAddRemovePageAsync();

        await addRemovePage.CheckPageOpenAsync();

        await addRemovePage.ClickButtonByNameAsync("Add Element");

        await addRemovePage.CheckNumberOfButtonAsync("Delete", 1); 
            
        await addRemovePage.ClickButtonByNameAsync("Add Element"); 
            
        await addRemovePage.CheckNumberOfButtonAsync("Delete", 2); 

        await addRemovePage.ClickButtonByNameAndNumberAsync("Delete", 2);

        await addRemovePage.CheckNumberOfButtonAsync("Delete", 1);
    }


}