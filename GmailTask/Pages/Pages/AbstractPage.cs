using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Driver;


namespace Pages
{
    public abstract class AbstractPage
    {
        protected static IWebDriver webDriver = Driver.Driver.getDriver();
        public abstract void OpenPage();

    }


}
