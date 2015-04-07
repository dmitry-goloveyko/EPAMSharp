using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using Driver;
using Pages;
using OpenQA.Selenium.Support.UI;


namespace Tests
{
    [TestFixture]
    class Tests
    {
        IWebDriver driver = Driver.Driver.getDriver();
        GoogleAccountsPage googleAccountsPage = new GoogleAccountsPage();
        GmailPage gmailPage = new GmailPage();

        User user1 = new User("webdriverspaghetti@gmail.com", "spaghetti2015");
        User user2 = new User("webdriverspaghetti2@gmail.com", "spaghetti2015");
        User user3 = new User("webdriverspaghetti3@gmail.com", "spaghetti2015");
        

        [Test]
        public void Test1()
        {
            String sender = user1.login;
            String senderPassword = user1.password;

            String receiver = user2.login;
            String receiverPassword = user2.password;

            String letter1Subject = "letter1Subject";
            String letterText = "Hello";

            String letter2Subject = "letter2Subject";


            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(sender, senderPassword);
            
            //writing first letter from user1 to user2
            gmailPage.OpenPage();
            gmailPage.WriteLetter(receiver, letter1Subject, letterText);

            gmailPage.Unlogin();

            googleAccountsPage.Login(receiver, receiverPassword);

            //moving letter to Spam
            IWebElement letter = gmailPage.GetLetter(sender, letter1Subject, letterText);
            gmailPage.MarkLetter(letter);
            gmailPage.ClickReportSpamButton();

            gmailPage.Unlogin();

            //writing second letter
            googleAccountsPage.Login(sender, senderPassword);
            //auto-redirect to gmail page is done
            gmailPage.WriteLetter(receiver, letter2Subject, letterText);

            gmailPage.Unlogin();

            //user2 checking spam
            googleAccountsPage.Login(receiver, receiverPassword);
            //auto-redirect to gmail page is done
            gmailPage.OpenSpam();

            letter = null;
            letter = gmailPage.GetLetter(sender, letter2Subject, letterText);

            if (letter != null)
            {
                Console.WriteLine(letter.ToString());
                Console.WriteLine(letter.TagName);
                Console.WriteLine(letter.Text);
            }
            else
            {
                Console.WriteLine("null");
            }
            Assert.IsNotNull(letter);
        }


        //[Test]
        //public void Test2()
        //{
        //    googleAccountsPage.OpenPage();
        //    googleAccountsPage.Login(user2.login, user2.password);
        //    gmailPage.OpenPage();
        //    gmailPage.OpenSettings();

        //    //redirect to settings page
        //    GmailSettingsPage settingsPage = new GmailSettingsPage();
        //    settingsPage.
        //}

        [Test]
        public void Test3()
        {
            String file = "C:\\Users\\Dzmitry_Halaveika\\Downloads\\vs2013.3_dskexp_ENU.iso";

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user1.login, user1.password);

            gmailPage.OpenPage();
            gmailPage.ClickComposeEmailButton();

            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();
            bool fileAttached = composeLetterWindow.AttachFile(file);

            Assert.IsFalse(fileAttached);
        }

        //[TearDown]
        //public void Cleanup()
        //{
        //    User[] users = new User[3] { user1, user2, user3 };

        //    gmailPage.Unlogin();

        //    //clearing inbox and spam
        //    foreach (var user in users)
        //    {
        //        googleAccountsPage.Login(user.login, user.password);

        //        //clearing inbox
        //        gmailPage.DeleteAllLettersInbox();

        //        //clearing spam
        //        gmailPage.DeleteAllSpam();

        //        gmailPage.Unlogin();
        //    }
        //}
    }
}
