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
    public class GoogleAccountsPage : AbstractPage
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

        
        override public void OpenPage()
        {
            webDriver.Navigate().GoToUrl(PAGE_URL);
        }

        public void LoginAsUser1()
        {
            emailInput.WaitUntilVisible().SendKeys(UserCredentials.LOGIN1);
            passwordInput.SendKeys(UserCredentials.PASSWORD1);
            if (staySignedInCheckBox.Selected)
            {
                staySignedInCheckBox.Click();
            }
            signInButton.Click();
        }

        public void LoginAsUser2()
        {
            emailInput.WaitUntilVisible().SendKeys(UserCredentials.LOGIN2);
            passwordInput.SendKeys(UserCredentials.PASSWORD2);
            if (staySignedInCheckBox.Selected)
            {
                staySignedInCheckBox.Click();
            }
            signInButton.Click();
        }

        public void LoginAsUser3()
        {
            emailInput.WaitUntilVisible().SendKeys(UserCredentials.LOGIN3);
            passwordInput.SendKeys(UserCredentials.PASSWORD3);
            if (staySignedInCheckBox.Selected)
            {
                staySignedInCheckBox.Click();
            }
            signInButton.Click();
        }
    }
}
