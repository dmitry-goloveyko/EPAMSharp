using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Pages
{
    public class LetterPage : GmailPage
    {
        [FindsBy(How = How.XPath, Using = "//span[child::span[text() = '<']][child::span[text() = '>']]")]
        private IWebElement senderEmailElement;

        public bool verifyEmailWithEmoticons(String senderEmail, String subject, 
            String message, List<String> emoticonsGoomoji)
        {
            //sender Check
            Console.WriteLine(senderEmailElement.Text);
            Console.WriteLine(senderEmail);
            //trimming < and >
            String senderEmailElementText = senderEmailElement.Text.
                Substring(1, senderEmailElement.Text.Length - 2);
            Console.WriteLine(senderEmailElementText);
            if (senderEmailElementText != senderEmail)
            {
                return false;
            }

            List<IWebElement> elements;
            //subject Check
            Console.WriteLine("subjectCheck");
            String subjectXPath = "//h2[text() = '" + subject + "']";
            if (!Utils.ElementIsUnique(subjectXPath))
            {
                Console.WriteLine("subjectDone");
                return false;
            }

            //text Check
            Console.WriteLine("textCheck");
            String messageXpath = "//*[text() = '" + message + "']";
            if (!Utils.ElementIsUnique(messageXpath))
            {
                Console.WriteLine("textCheckDone");
                return false;
            }

            //emoticons Check
            Console.WriteLine("emoticonsCheck");
            foreach (String goomoji in emoticonsGoomoji)
            {
                if (!Utils.ElementIsPresent("//img[@goomoji = '" + goomoji + "']"))
                {
                    return false;
                }
                Console.WriteLine("emoticonsCheck " + goomoji + " Done");
            }

            return true;
        }
    }
}
