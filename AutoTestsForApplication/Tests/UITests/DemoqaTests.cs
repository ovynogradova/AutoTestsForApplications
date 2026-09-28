using Microsoft.Playwright;

namespace apitest.UITests;

public class DemoqaTests
{
    public class SelectMenu : BaseTest
    {
        [Test]
        public async Task Selectmenu()
        {
            await Page.GotoAsync("https://demoqa.com/select-menu");
            var selectMenuContainer = Page.Locator("#selectMenuContainer")
                .Filter(new() { HasText = "Select Menu" });
            await Assertions.Expect(selectMenuContainer).ToContainTextAsync("Select Menu");

            var selectOne = Page.Locator("#selectOne");
            await selectOne.ClickAsync();

            var profOption = Page.GetByRole(AriaRole.Option, new() { Name = "Prof." });
            await profOption.ClickAsync();

            await Assertions.Expect(selectOne).ToContainTextAsync("Prof.");

        }
    }
}