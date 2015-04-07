using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages
{
    public class ForwardingPage : GmailSettingsPage
    {
        [FindsBy(How = How.XPath, Using = "//input[@type='button' and @value='Add a forwarding address']")]
        private IWebElement addForwardingAddressButton;

        [FindsBy(How = How.XPath, Using = "//*[@role='alertdialog']//input")]
        private IWebElement forwardingEmailInput;

        [FindsBy(How = How.XPath, Using = "//button[@name='next']")]
        private IWebElement nextButton;

        [FindsBy(How = How.XPath, Using = "//input[@value='Proceed']")]
        private IWebElement proceedButton;

        [FindsBy(How = How.XPath, Using = "//button[@name='ok']")]
        private IWebElement okButton;

        public ForwardingPage()
        {
            PageFactory.InitElements(webDriver, this);
        }
    }
}
