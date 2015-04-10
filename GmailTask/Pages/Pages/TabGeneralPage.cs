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
    public class TabGeneralPage
    {
        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Signature']")]
        private IWebElement signatureTextArea;

        [FindsBy(How = How.XPath, Using = "//button[text() ='Save Changes']")]
        private IWebElement saveChangesButton;

        public TabGeneralPage()
        {
            PageFactory.InitElements(Driver.Driver.getDriver(), this);
        }

        public void setSignature(String signatureText)
        {
            signatureTextArea.WaitUntilPresent().Clear();
            signatureTextArea.WaitUntilPresent().SendKeys(signatureText);
            saveChangesButton.WaitUntilVisible().Click();
        }
    }
}
