using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using Driver;

namespace Pages
{
    public class GoogleAccountsPage : GooglePage, IOpenable
    {
        private const string PAGE_URL = "http://accounts.google.com";

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement emailInput;

        [FindsBy(How = How.Id, Using = "Passwd")]
        private IWebElement passwordInput;

        [FindsBy(How = How.Id, Using = "PersistentCookie")]
        private IWebElement staySignedInCheckBox;

        [FindsBy(How = How.Id, Using = "signIn")]
        private IWebElement signInButton;

        public GoogleAccountsPage()
        {
            PageFactory.InitElements(webDriver, this);
        }

        
        
        public void OpenPage()
        {
            webDriver.Navigate().GoToUrl(PAGE_URL);
        }

        public void Login(String login, String password)
        {
            emailInput.WaitUntilVisible().SendKeys(login);
            passwordInput.SendKeys(password);
            if (staySignedInCheckBox.Selected)
            {
                staySignedInCheckBox.Click();
            }
            signInButton.Click();
        }
    }
}
