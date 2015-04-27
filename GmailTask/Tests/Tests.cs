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
using System.IO;
using System.Drawing;


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


        [Test, Category("GM#1.1"), Description("Letters are being sent to spam after one report")]
        public void lettersGoToSpam()
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

            gmailPage.Logout();

            googleAccountsPage.Login(receiver, receiverPassword);

            //auto-redirect to gmail page is done
            gmailPage.MoveLetterToSpam(sender, letter1Subject, letterText);

            gmailPage.Logout();

            //writing second letter
            googleAccountsPage.Login(sender, senderPassword);
            //auto-redirect to gmail page is done
            gmailPage.WriteLetter(receiver, letter2Subject, letterText);

            gmailPage.Logout();

            //user2 checking spam
            googleAccountsPage.Login(receiver, receiverPassword);
            //auto-redirect to gmail page is done
            gmailPage.OpenSpam();

            IWebElement letter = null;
            letter = gmailPage.GetLetterWebElement(sender, letter2Subject, letterText);

            Assert.IsNotNull(letter);
        }


        [Test, Category("GM#1.3"), Description("User can't send email with file over 25mb")]
        public void sendingEmailWithFileOver25Mb()
        {
            String file = "C:\\Users\\Dzmitry_Halaveika\\Downloads\\1";

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user1.login, user1.password);

            gmailPage.OpenPage();

            Utils.CreateLargeFile(file, 50);
            bool emailSent = gmailPage.WriteLetterWithLargeFile(user1.login, "theme", "Hello", file);

            Assert.IsFalse(emailSent);

            File.Delete(file);
        }

        [Test, Category("GM#1.5"), Description("Sending mail with emoticons")]
        public void oneCanSendMailWithEmoticons()
        {
            String user = user1.login;
            String userPassword = user1.password;

            String subject = "LetterWithEmoticons";
            String message = "Hello";
            List<String> emoticons = new List<String>() { "330", "338", "32B"};

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user, userPassword);

            gmailPage.OpenPage();
            gmailPage.WriteLetterWithEmoticons(user, subject, message, emoticons);

            gmailPage.OpenInbox();
            gmailPage.OpenLetter(user, subject, message);

            Console.WriteLine("verifying");
            LetterPage letterPage = new LetterPage();
            bool letterVerified = letterPage.verifyEmailWithEmoticons(user, subject, message, emoticons);
            Console.WriteLine(letterVerified);

            Assert.IsTrue(letterVerified);
        }

        [Test, Category("GM#1.6"), Description("Changing decoration theme")]
        public void oneCanChangeDecorationTheme()
        {
            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user1.login, user1.password);

            gmailPage.OpenPage();
            gmailPage.OpenInbox();

            String file1 = @"C:\Users\Dzmitry_Halaveika\Downloads\SeleniumTestingScreenshot1.jpg";
            String file2 = @"C:\Users\Dzmitry_Halaveika\Downloads\SeleniumTestingScreenshot2.jpg";

            Utils.TakeScreenshot(file1);

            gmailPage.OpenSettings();
            GmailSettingsPage settingsPage = new GmailSettingsPage();
            settingsPage.OpenTab("Themes");
            ThemesPage themesPage = new ThemesPage();
            themesPage.SetTheme("Beach");
            gmailPage.OpenInbox();

            Utils.TakeScreenshot(file2);

            Assert.IsFalse(Utils.ImagesEqual(file1, file2));

            File.Delete(file1);
            File.Delete(file2);
        }

        [Test, Category("GM#1.11"), Description("Mark item as spam and mark spam item as not spam")]
        public void spamMarking()
        {
            String user = user1.login;
            String subject = "Letter";
            String message = "Hello";

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user, user1.password);

            gmailPage.OpenPage();
            gmailPage.WriteLetter(user, subject, message);
            gmailPage.OpenInbox();
            gmailPage.MoveLetterToSpam(user, subject, message);

            gmailPage.OpenSpam();

            Assert.IsNotNull(gmailPage.GetLetterWebElement(user, subject, message));

            gmailPage.MoveLetterOutOfSpam(user, subject, message);

            gmailPage.OpenInbox();

            Assert.IsNotNull(gmailPage.GetLetterWebElement(user, subject, message));

        }

        [Test, Category("GM#1.12"), Description("Setting signature")]
        public void settingSignature()
        {
            String user = user1.login;
            String signatureText = "my signature";

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user, user1.password);

            gmailPage.OpenPage();
            gmailPage.OpenSettings();

            TabGeneralPage tabGeneralPage = new TabGeneralPage();
            tabGeneralPage.setSignature(signatureText);

            gmailPage.OpenInbox();

            gmailPage.ClickComposeButton();

            ComposeLetterWindow composeLetterWindow = new ComposeLetterWindow();
            Assert.IsTrue(composeLetterWindow.verifySignature(signatureText));
        }

        [Test, Category("GM#1.13"), Description("Check 'star' selection")]
        public void starringLetter()
        {
            String user = user1.login;
            String subject = "Letter";
            String message = "Hello";

            googleAccountsPage.OpenPage();
            googleAccountsPage.Login(user, user1.password);

            gmailPage.OpenPage();
            gmailPage.WriteLetter(user, subject, message);

            gmailPage.OpenInbox();
            IWebElement letter = gmailPage.GetLetterWebElement(user, subject, message);
            gmailPage.StarLetter(letter);

            gmailPage.OpenStarred();

            Assert.IsNotNull(gmailPage.GetLetterWebElement(user, subject, message));
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
