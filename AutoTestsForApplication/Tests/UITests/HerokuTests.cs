using FluentAssertions;

namespace apitest.UITests;

public class HerokuTests : BaseTest
{
    [Test]
    public async Task CheckBoxTest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
        var first = Page.Locator("input[type='checkbox']").Nth(0);
        await first.CheckAsync();
        (await first.IsCheckedAsync()).Should().BeTrue();
    }
    
    
}