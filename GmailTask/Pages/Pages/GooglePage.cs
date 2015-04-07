using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;

namespace Pages
{
    public class GooglePage : AbstractPage
    {
        [FindsBy(How = How.XPath, Using = "//a[contains(@title, 'Account')]")]
        private IWebElement accountButton;

        [FindsBy(How = How.LinkText, Using = "Sign out")]
        private IWebElement exitAccountButton;

        public void Unlogin()
        {
            accountButton.WaitUntilVisible().Click();
            exitAccountButton.WaitUntilVisible().Click();
        }

    }
}
