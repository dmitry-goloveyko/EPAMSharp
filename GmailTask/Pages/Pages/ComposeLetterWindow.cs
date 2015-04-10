using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;
using OpenQA.Selenium.Interactions;
using System.Windows.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Pages
{
    public class ComposeLetterWindow : GmailPage
    {
        [FindsBy(How = How.Name, Using = "to")]
        private IWebElement recipientsTextArea;

        [FindsBy(How = How.Name, Using = "subjectbox")]
        private IWebElement subjectInput;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Message Body']")]
        private IWebElement textInput;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Send')]")]
        private IWebElement sendEmailButton;

        [FindsBy(How = How.XPath, Using = "//button[@name='cancel']")]
        private IWebElement cancelButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Attach files']")]
        private IWebElement attachFilesButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Insert emoticon ‪(Ctrl-Shift-2)‬']")]
        private IWebElement insertEmoticonsButton;

        [FindsBy(How = How.XPath, Using = "//div[@role='button' and text() = 'Insert']")]
        private IWebElement insertEmoticonsToTextareaButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Save & Close']")]
        private IWebElement closeWindowButton;
        
        [FindsBy(How = How.XPath, Using = "//*[text() = 'The file you are trying to send " + 
            "exceeds the 25MB attachment limit.'")]
        private IWebElement tooBigFileErrorMessage;

        public ComposeLetterWindow()
        {
            PageFactory.InitElements(webDriver, this);
        }

        public void WriteLetter(String recipient, String subject, String text)
        {
            recipientsTextArea.SendKeys(recipient);
            subjectInput.SendKeys(subject);
            textInput.SendKeys(text);
            sendEmailButton.Click();
        }

        public bool WriteLetterWithLargeFile(String recipient, String subject, String text, String absoluteFilePath)
        {
            recipientsTextArea.SendKeys(recipient);
            subjectInput.SendKeys(subject);
            textInput.SendKeys(text);

            if (!AttachFile(absoluteFilePath))
            {
                closeWindowButton.WaitUntilPresent().Click();
                return false;
            }

            sendEmailButton.Click();

            return true;
        }

        public void WriteLetterWithEmoticons(String recipient, String subject, String text, List<String> emoticonsGoomojiAttributes)
        {
            recipientsTextArea.SendKeys(recipient);
            subjectInput.SendKeys(subject);
            textInput.SendKeys(text);

            InsertEmoticons(emoticonsGoomojiAttributes);

            sendEmailButton.Click();
        }

        public bool verifySignature(String signature)
        {
            return textInput.Text.Contains(signature);
        }

        private bool AttachFile(String absolutePath)
        {
            attachFilesButton.WaitUntilPresent().Click();

            System.Windows.Forms.SendKeys.SendWait(absolutePath);
            System.Threading.Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");

            try
            {
                cancelButton.WaitUntilPresent().Click();
            }
            catch (TimeoutException)
            {
                return true;
            }
            
            return false;
        }

        private void InsertEmoticons(List<String> emoticonsGoomojiAttributes)
        {
            insertEmoticonsButton.WaitUntilPresent().Click();

            //adding multiple emoticons
            Actions action = new Actions(webDriver);
            action.KeyDown(Keys.Shift).Perform();

            foreach (var goomoji in emoticonsGoomojiAttributes)
	        {
                webDriver.FindElement(By.XPath("//div[@goomoji='" + goomoji + "']")).WaitUntilPresent().Click();
	        }

            action.KeyUp(Keys.Shift).Perform();

            insertEmoticonsToTextareaButton.WaitUntilPresent().Click();
        }
    

    }
}
