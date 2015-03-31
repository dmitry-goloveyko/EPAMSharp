using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using Driver;
using Pages;
using OpenQA.Selenium.Support.UI;


namespace Tests
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void Test1()
        {
            IWebDriver driver = Driver.Driver.getDriver();
            
            GoogleAccountsPage googleAccountsPage = new GoogleAccountsPage();

            googleAccountsPage.OpenPage();
            googleAccountsPage.LoginAsUser1();
            

            GmailPage gmailPage = new GmailPage();

            gmailPage.OpenPage();
            gmailPage.writeLetter(UserCredentials.LOGIN1, "Test1", "Hello!");
        }
    }
}
