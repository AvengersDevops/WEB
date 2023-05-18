import { Selector } from 'testcafe';

// Used for local testing ->
//fixture('Getting Started').page('http://localhost:5070/')

//Used for Jenkins testing ->
fixture('Getting Started').page(process.env.HTTP_ENV)

test('Counter test', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(2)') // Click menu item
        .expect(Selector('#app > div > main > article > p').innerText) // Find innerText of CounterText
        .eql('Current count: 0') // CounterText should be
        .click('#app > div > main > article > button') // Click counter button
        .expect(Selector('#app > div > main > article > p').innerText) // Find innerText of CounterText
        .eql('Current count: 1') // CounterText should be
});

test('Fetch data test', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(3)') // Click menu item
        .expect(Selector('#app > div > main > article > table > tbody').child().count) // Find children count of list
        .eql(5) // Children amount should be
});
