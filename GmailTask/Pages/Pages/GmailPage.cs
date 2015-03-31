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
    public class GmailPage : AbstractPage
    {
        private const string PAGE_URL = "http://gmail.com";

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'НАПИСАТЬ')]")]
        private IWebElement writeEmailButton;

        [FindsBy(How = How.Name, Using = "to")]
        private IWebElement recipientsTextArea;

        [FindsBy(How = How.Name, Using = "subjectbox")]
        private IWebElement subjectInput;

        [FindsBy(How = How.XPath, Using = "//*[@aria-label='Тело письма']")]
        private IWebElement textInput;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Отправить')]")]
        private IWebElement sendEmailButton;

        override public void OpenPage()
        {
            webDriver.Navigate().GoToUrl(PAGE_URL);
        }

        public void writeLetter(String recipient, String subject, String text)
        {
            writeEmailButton.WaitUntilVisible().Click();
            recipientsTextArea.SendKeys(recipient);
            subjectInput.SendKeys(subject);
            textInput.SendKeys(text);
            sendEmailButton.Click();
        }
    }
}
