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

test('Login page', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Click menu item
        .expect(Selector('#app > div > main > article > h1').innerText) // Find innerText of Headline
        .eql('Login') // Headline should be
        .expect(Selector('#app > div > main > article > div').child().count) // Find children count of LoginDiv
        .eql(3) // LoginDiv children amount should be
});

test('Login test', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Click menu item
        .typeText(Selector('#app > div > main > article > div > input:nth-child(1)'), 'Testemail') // typeText in email field
        .typeText(Selector('#app > div > main > article > div > input:nth-child(2)'), 'Testpassword') // typeText in password field
        .click('#app > div > main > article > div > button') // Click Login button
        .wait(5000) // Wait for login
        .expect(Selector('#app > div > main > article > h3').innerText) // Find innerText of Headline
        .eql('Normal Tasks') // Headline should be
});

test('NormalTasks page', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(5)') // Click menu item
        .expect(Selector('#app > div > main > article > h3').innerText) // Find innerText of Headline
        .eql('Normal Tasks') // Headline should be
        .expect(Selector('#app > div > main > article > div > div').child().count) // Find children count of TaskCreator
        .eql(4) // TaskCreator children amount should be
});

test('NormalTasks add task and remove', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Login test
        .typeText(Selector('#app > div > main > article > div > input:nth-child(1)'), 'Testemail')  //
        .typeText(Selector('#app > div > main > article > div > input:nth-child(2)'), 'Testpassword') //
        .click('#app > div > main > article > div > button') //
        .wait(2000) // Wait for login
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(5)') // NormalTask page
        .expect(Selector('#app > div > main > article > h3').innerText) //
        .eql('Normal Tasks') // Test Start From Here
        .typeText(Selector('#taskTitle'), 'TitleOfTask') // typeText in title field
        .typeText(Selector('#taskDescription'), 'DescriptionOfTask') // typeText in description field
        .click('#app > div > main > article > div > div > button') // Click AddTask button
        .wait(1000) // Wait for it to register
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Go to another page
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(5)') // Go back to the other page
        .wait(2000) // Wait for refresh
        .expect(Selector('#app > div > main > article > table > tbody').child().count) // Find children count of TastList
        .eql(1) // TaskList children amount should be
        .click('#app > div > main > article > table > tbody > tr > td:nth-child(1) > button') // Mark as done
        .expect(Selector('#app > div > main > article > table > tbody').child().count) // Find children count of TaskList
        .eql(0) // TaskList children amount should be
});

test('ScheduledTasks page', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(6)') // Click menu item
        .expect(Selector('#app > div > main > article > h3').innerText) // Find innerText of Headline
        .eql('Scheduled Tasks') // Headline should be
        .expect(Selector('#app > div > main > article > div > div').child().count) // Find children count of TaskCreator
        .eql(4) // TaskCreator children amount should be
});

test('ScheduledTasks add task and remove', async t => {
    await t
        .wait(2000) // Startup
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Login test
        .typeText(Selector('#app > div > main > article > div > input:nth-child(1)'), 'Testemail') //
        .typeText(Selector('#app > div > main > article > div > input:nth-child(2)'), 'Testpassword') //
        .click('#app > div > main > article > div > button') //
        .wait(2000) // Wait for login
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(6)') // ScheduledTask page
        .expect(Selector('#app > div > main > article > h3').innerText) //
        .eql('Scheduled Tasks') // Test Start From Here
        .typeText(Selector('#taskTitle'), 'TitleOfTask') // typeText in title field
        .typeText(Selector('#taskDescription'), 'DescriptionOfTask') // typeText in description field
        .click('#app > div > main > article > div > div > button') // Click AddTask button
        .wait(1000) // Wait for it to register
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(4)') // Go to another page
        .click('#app > div > div > div.nav-scrollable > nav > div:nth-child(5)') // Go the Normal Tast page
        .wait(2000) // Wait for refresh
        .expect(Selector('#app > div > main > article > table > tbody').child().count) // Find children count of TastList
        .eql(1) // TaskList children amount should be
        .click('#app > div > main > article > table > tbody > tr > td:nth-child(1) > button') // Mark as done
        .expect(Selector('#app > div > main > article > table > tbody').child().count) // Find children count of TaskList
        .eql(0) // TaskList children amount should be
});