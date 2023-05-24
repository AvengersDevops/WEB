using Microsoft.Playwright;

namespace PlaywrightTests;

[TestFixture]
public class Tests
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
        });
        _page = await _browser.NewPageAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [SetUp]
    public async Task SetUp()
    {
        await _page.GotoAsync("http://128.140.9.68:81");
    }

    [Test]
    public async Task LoginPage()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h1");
        Assert.That(headline, Is.EqualTo("Login"));
        var childrenCount = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > div", "el => el.children.length");
        Assert.That(childrenCount, Is.EqualTo(3));
    }

    [Test]
    public async Task LoginTest()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(1)", "Testemail");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(2)", "Testpassword");
        await _page.ClickAsync("#app > div > main > article > div > button");
        await _page.WaitForTimeoutAsync(1000);
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h3");
        Assert.That(headline, Is.EqualTo("Normal Tasks"));
    }

    [Test]
    public async Task NormalTasksPage()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(5)");
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h3");
        Assert.That(headline, Is.EqualTo("Normal Tasks"));
        var childrenCount = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > div > div", "el => el.children.length");
        Assert.That(childrenCount, Is.EqualTo(4));
    }

    [Test]
    public async Task NormalTasksAddTaskAndRemove()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(1)", "Testemail");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(2)", "Testpassword");
        await _page.ClickAsync("#app > div > main > article > div > button");
        await _page.WaitForTimeoutAsync(1000);
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(5)");
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h3");
        Assert.That(headline, Is.EqualTo("Normal Tasks"));
        await _page.TypeAsync("#taskTitle", "TitleOfTask");
        await _page.TypeAsync("#taskDescription", "DescriptionOfTask");
        await _page.ClickAsync("#app > div > main > article > div > div > button");
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(5)");
        var taskListChildrenCount1 = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > table > tbody", "el => el.children.length");
        Assert.That(taskListChildrenCount1, Is.Not.EqualTo(0));
        await _page.ClickAsync("#app > div > main > article > table > tbody > tr > td:nth-child(1) > button");
        var taskListChildrenCount2 = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > table > tbody", "el => el.children.length");
        Assert.That(taskListChildrenCount2, Is.Not.EqualTo(taskListChildrenCount1));
    }

    [Test]
    public async Task ScheduledTasksPage()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(6)");
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h3");
        Assert.That(headline, Is.EqualTo("Scheduled Tasks"));
        var childrenCount = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > div > div", "el => el.children.length");
        Assert.That(childrenCount, Is.EqualTo(4));
    }

    [Test]
    public async Task ScheduledTasksAddTaskAndRemove()
    {
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(1)", "Testemail");
        await _page.TypeAsync("#app > div > main > article > div > input:nth-child(2)", "Testpassword");
        await _page.ClickAsync("#app > div > main > article > div > button");
        await _page.WaitForTimeoutAsync(1000);
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(6)");
        string headline = await _page.InnerHTMLAsync("#app > div > main > article > h3");
        Assert.That(headline, Is.EqualTo("Scheduled Tasks"));
        await _page.TypeAsync("#taskTitle", "TitleOfTask");
        await _page.TypeAsync("#taskDescription", "DescriptionOfTask");
        await _page.ClickAsync("#app > div > main > article > div > div > button");
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(4)");
        await _page.ClickAsync("#app > div > div > div.nav-scrollable > nav > div:nth-child(5)");
        var taskListChildrenCount1 = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > table > tbody", "el => el.children.length");
        Assert.That(taskListChildrenCount1, Is.Not.EqualTo(0));
        await _page.ClickAsync("#app > div > main > article > table > tbody > tr > td:nth-child(1) > button");
        var taskListChildrenCount2 = await _page.EvalOnSelectorAsync<int>("#app > div > main > article > table > tbody", "el => el.children.length");
        Assert.That(taskListChildrenCount2, Is.Not.EqualTo(taskListChildrenCount1));
    }
}