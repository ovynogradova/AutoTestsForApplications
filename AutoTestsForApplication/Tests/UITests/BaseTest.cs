using apitest.ForUI.Framework;
using Microsoft.Playwright;

namespace apitest.UITests;

public class BaseTest
{
    protected IPage Page { get; private set; }
    protected PlaywrightFixture Fixture { get; }

    protected BaseTest()
    {
        Fixture = new PlaywrightFixture();
        Fixture.InitializeAsync().Wait();
    }

    [SetUp]
    public async Task SetUp()
    {
        Page = await Fixture.Browser.NewPageAsync(new BrowserNewPageOptions
        {
            ViewportSize = null
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        await Page.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTearDown()
    {
        await Fixture.DisposeAsync();
    }
}