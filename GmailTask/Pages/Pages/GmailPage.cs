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

        [FindsBy(How = How.PartialLinkText, Using = "Inbox")]
        private IWebElement inboxCategoryButton;

        [FindsBy(How = How.PartialLinkText, Using = "Spam")]
        private IWebElement spamCategoryButton;

        [FindsBy(How = How.XPath, Using = "//*[@gh='s']/div")]
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
            composeEmailButton.WaitUntilPresent().Click();

            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();
            composeLetterWindow.WriteLetter(recipient, subject, text);
        }

        public void WriteLetterWithEmoticons(String recipient, String subject, String text, List<String> emoticons)
        {
            composeEmailButton.WaitUntilPresent().Click();

            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();
            composeLetterWindow.WriteLetterWithEmoticons(recipient, subject, text, emoticons);
        }

        public bool WriteLetterWithLargeFile(String recipient, String subject, String text, String absolutePath)
        {
            composeEmailButton.WaitUntilPresent().Click();

            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();

            return composeLetterWindow.WriteLetterWithLargeFile(recipient, subject, text, absolutePath);
        }

        public IWebElement GetLetterWebElement(String senderEmail, String subjectPart, String messagePart)
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
                return letters.First();
            }

            return null;
        }

        public void OpenLetter(String senderEmail, String subjectPart, String messagePart)
        {
            IWebElement letter = GetLetterWebElement(senderEmail, subjectPart, messagePart);
            if (letter != null)
            {
                letter.Click();
            }
        }

        public void MarkLetter(IWebElement letter)
        {
            try
            {
                letter.FindElement(By.XPath("//div[@role='checkbox']/div")).Click();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Letter does not exist in this folder");
            }
        }

        public void DeleteAllLetters()
        {
            markAllLettersCheckbox.WaitUntilPresent().Click();
            deleteButton.WaitUntilPresent().Click();
        }

        public void MoveLetterToSpam(String senderEmail, String subjectPart, String messagePart)
        {
            IWebElement letter = GetLetterWebElement(senderEmail, subjectPart, messagePart);
            MarkLetter(letter);
            reportSpamButton.WaitUntilPresent().Click();
        }

        public void OpenSpam()
        {
            moreButton.WaitUntilPresent().Click();
            spamCategoryButton.WaitUntilVisible().Click();
        }

        public void OpenInbox()
        {
            inboxCategoryButton.WaitUntilPresent().Click();
        }

        public void DeleteAllSpam()
        {
            OpenSpam();

            deleteAllSpamHyperlink.WaitUntilPresent().Click();
            okButton.WaitUntilPresent().Click();
        }

        public void OpenSettings()
        {
            settingsButton.WaitUntilPresent().Click();
            settingsMenuOption.WaitUntilPresent().Click();
        }

    }
}
