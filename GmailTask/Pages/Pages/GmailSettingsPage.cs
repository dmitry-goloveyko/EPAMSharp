using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;

namespace Pages
{
    public class GmailSettingsPage : GooglePage
    {
        public void OpenTab(String tabName)
        {
            String tabXPath = "//a[@role='tab' and text() = '" + tabName + "']";

            webDriver.FindElement(By.LinkText(tabName)).Click();
        }
    }
}
