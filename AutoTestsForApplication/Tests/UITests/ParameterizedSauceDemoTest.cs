using apitest.ForUI.Pages.Saucedemo;
using FluentAssertions;

namespace apitest.UITests;

public class ParameterizedSauceDemoTest : BaseTest
{
    [TestCase("standard_user", "secret_sauce")]
    [TestCase("problem_user", "secret_sauce")]
    [TestCase("performance_glitch_user", "secret_sauce")]
    [TestCase("error_user", "secret_sauce")]
    [TestCase("visual_user", "secret_sauce")]
    public async Task LoginAllValidUsers(string username, string password)
    {
        var loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.FillLoginFormAsync(username, password);

        var productsPage = new ProductsPage(Page);
        var checkTextAfterLogin = await productsPage.GetCheckTextAfterLoginAsync();
        checkTextAfterLogin.Should().Contain("Products");
    }
}