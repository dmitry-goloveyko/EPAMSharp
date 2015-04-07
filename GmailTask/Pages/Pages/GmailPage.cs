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
    public class GmailPage : GooglePage, IOpenable
    {
        private const string PAGE_URL = "http://gmail.com";

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'COMPOSE')]")]
        private IWebElement composeEmailButton;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'More') and not(parent::div)]")]
        private IWebElement moreButton;

        [FindsBy(How = How.PartialLinkText, Using = "Spam")]
        private IWebElement spamCategoryButton;

        [FindsBy(How = How.XPath, Using = "//*[@aria-label='Settings']")]
        private IWebElement settingsButton;

        [FindsBy(How = How.XPath, Using = "//div[@role='menuitem']/div[text() = 'Settings']")]
        private IWebElement settingsMenuOption;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Report spam']/div/div")]
        private IWebElement reportSpamButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Delete']/div/div]")]
        private IWebElement deleteButton;

        [FindsBy(How = How.XPath, Using = "//div[@data-tooltip='Select']//div[@role='presentation']")]
        private IWebElement markAllLettersCheckbox;

        [FindsBy(How = How.XPath, Using = "//span[text()='Delete all spam messages now']")]
        private IWebElement deleteAllSpamHyperlink;

        [FindsBy(How = How.XPath, Using = "//button[@name='ok']")]
        private IWebElement okButton;
        

        public GmailPage()
        {
            PageFactory.InitElements(webDriver, this);
        }

        public void OpenPage()
        {
            webDriver.Navigate().GoToUrl(PAGE_URL);
        }

        public void WriteLetter(String recipient, String subject, String text)
        {
            composeEmailButton.Click();
            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();
            composeLetterWindow.WriteLetter(recipient, subject, text);
        }

        public IWebElement GetLetter(String senderEmail, String subjectPart, String messagePart)
        {
            int charactersToCompareStrings = 20;
            if (subjectPart.Length > charactersToCompareStrings)
            {
                subjectPart = subjectPart.Substring(0, charactersToCompareStrings);
            }
            if (subjectPart.Length > charactersToCompareStrings)
            {
                subjectPart = subjectPart.Substring(0, charactersToCompareStrings);
            }

            String senderDescendant = "[descendant::span[@email='" + senderEmail + "']]";
            String subjectDescendant = "[descendant::*[contains(text(), '" + subjectPart + "')]]";
            String messageDescendant = "[descendant::span[contains(text(), '" + messagePart + "')]]";

            String letterElementXPath = "//tr" + senderDescendant + subjectDescendant + messageDescendant;
            Console.WriteLine(letterElementXPath);
            List<IWebElement> letters = webDriver.FindElements(By.XPath(letterElementXPath)).ToList();

            foreach(IWebElement letter in letters) 
            {
                Console.WriteLine(letter.Text);
            }

            if (letters.Count > 0)
            {
                return letters[0];
            }

            return null;
        }

        public void OpenLetter(String senderEmail, String subjectPart, String messagePart)
        {
            IWebElement letter = GetLetter(senderEmail, subjectPart, messagePart);
            if (letter != null)
            {
                letter.Click();
            }
        }

        public void MarkLetter(IWebElement letter)
        {
            String markDivXPath = "//div[@role='checkbox']/div";

            try
            {
                letter.FindElement(By.XPath(markDivXPath)).Click();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Letter does not exist in this folder");
            }
        }

        public void MarkAllLetters()
        {
            System.Threading.Thread.Sleep(1000);
            try 
            {
                webDriver.FindElement(By.XPath("//div[@data-tooltip='Select']//div[@role='presentation']")).Click();
            }
            catch (ElementNotVisibleException)
            {
                Console.WriteLine("markAllLettersCheckbox not found");
            }
        }

        public void ClickReportSpamButton()
        {
            System.Threading.Thread.Sleep(1000);

            try
            {
                webDriver.FindElement(By.XPath("//div[@act='9']/div")).Click();
            }
            catch (ElementNotVisibleException)
            {
                Console.WriteLine("Element delete button is not visible");
            }
        }

        public void ClickDeleteButton()
        {
            System.Threading.Thread.Sleep(1000);

            try
            {
                webDriver.FindElement(By.XPath("//div[@act='10']/div")).Click();
            }
            catch (ElementNotVisibleException)
            {
                Console.WriteLine("Element delete button is not visible");
            }
        }

        public void DeleteAllLettersInbox()
        {
            MarkAllLetters();
            ClickDeleteButton();
        }

        public void OpenSpam()
        {
            moreButton.Click();

            spamCategoryButton.WaitUntilVisible().Click();
        }

        public void DeleteAllSpam()
        {
            OpenSpam();

            try
            {
                deleteAllSpamHyperlink.WaitUntilVisible().Click();
                PageFactory.InitElements(webDriver, this);
                okButton.WaitUntilVisible().Click();
            }
            catch (TimeoutException)
            {
                Console.WriteLine("No spam to delete");
            }
        }

        public void OpenSettings()
        {
            settingsButton.WaitUntilVisible().Click();
            settingsMenuOption.WaitUntilVisible().Click();
        }

        public void ClickComposeEmailButton()
        {
            composeEmailButton.WaitUntilVisible().Click();
        }
    }
}
