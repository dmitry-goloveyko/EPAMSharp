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
using System.Windows.Forms;

namespace Pages
{
    public class ComposeLetterWindow : GmailPage
    {
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [FindsBy(How = How.Name, Using = "to")]
        private IWebElement recipientsTextArea;

        [FindsBy(How = How.Name, Using = "subjectbox")]
        private IWebElement subjectInput;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Message Body']")]
        private IWebElement textInput;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Send')]")]
        private IWebElement sendEmailButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-tooltip='Attach files'")]
        private IWebElement attachFilesButton;

        [FindsBy(How = How.XPath, Using = "//button[@name='cancel']")]
        private IWebElement cancelButton;

        public ComposeLetterWindow()
        {
            PageFactory.InitElements(webDriver, this);
        }

        public void SetRecipient(String recipient) 
        {
            recipientsTextArea.SendKeys(recipient);
        }

        public void SetSubject(String subject)
        {
            subjectInput.SendKeys(subject);
        }

        public void SetMessage(String message)
        {
            textInput.SendKeys(message);
        }

        public void ClickSendButton()
        {
            sendEmailButton.Click();
        }

        public void WriteLetter(String recipient, String subject, String text)
        {
            recipientsTextArea.SendKeys(recipient);
            subjectInput.SendKeys(subject);
            textInput.SendKeys(text);
            sendEmailButton.Click();
        }

        public bool AttachFile(String absolutePath)
        {
            System.Threading.Thread.Sleep(1000);
            webDriver.FindElement(By.XPath("//*[@data-tooltip='Attach files']")).Click();

            SendKeys.SendWait(absolutePath);
            System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait("{TAB}");
            System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(1000);

            String errorMessage = "The file you are trying to send exceeds the 25MB attachment limit.";

            if (webDriver.FindElements(By.XPath("//*[text() = '" + errorMessage + "']")).ToList().Count > 0)
            {
                cancelButton.Click();
                return false;
            }

            return true;
        }
    }
}
