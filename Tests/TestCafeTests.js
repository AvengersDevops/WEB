import { Selector } from 'testcafe';

fixture('Getting Started').page(process.env.HTTP_ENV)

test('Counter test', async t => {
    await t
        .wait(2000)
        .takeScreenshot()
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(2)')
        .takeScreenshot()
        .expect(Selector('#app > div > main > article > p').innerText)
        .eql('Current count: 0')
        .click('#app > div > main > article > button')
        .takeScreenshot()
        .expect(Selector('#app > div > main > article > p').innerText)
        .eql('Current count: 1')
});

test('Fetch data test', async t => {
    await t
        .wait(2000)
        .takeScreenshot()
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(3)')
        .takeScreenshot()
        .expect(Selector('#app > div > main > article > table > tbody').child().count)
        .eql(5)
});