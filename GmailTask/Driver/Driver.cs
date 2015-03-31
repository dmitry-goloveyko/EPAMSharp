using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    public class Driver
    {
        private IWebDriver webdriver;
        private static Driver instance = null;

        private Driver()
        {
            webdriver = new FirefoxDriver();
            webdriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            webdriver.Manage().Window.Maximize();
        }

        public static IWebDriver getDriver()
        {
            if (instance == null)
            {
                instance = new Driver();
            }

            return instance.webdriver;
        }

        public static void closeDriver()
        {
            instance.webdriver.Quit();
        }
    }
}