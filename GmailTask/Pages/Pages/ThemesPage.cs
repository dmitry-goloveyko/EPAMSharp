using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;

namespace Pages
{
    public class ThemesPage : GmailSettingsPage
    {
        public void SetTheme(String theme)
        {
            try
            {
                webDriver.FindElement(By.XPath("//*[text() = '" + theme + "']")).WaitUntilPresent().Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Theme doesn't exist");
            }
        }
    }
}
