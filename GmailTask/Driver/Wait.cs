﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    public static class Wait
    {

        public static IWebElement WaitUntilVisible(this IWebElement element, int timeoutSeconds)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(timeoutSeconds);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                Exception lastException = null;
                try
                {
                    if (element.Displayed)
                    {
                        return element;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception e) { lastException = e; }

                if (sw.Elapsed > timeOut)
                {
                    string exceptionMessage = lastException == null ? "" : lastException.Message;
                    string errorMessage = string.Format("Wait.UntilVisible: Element was not displayed after {0} Milliseconds" +
                                                        "\r\n Error Message:\r\n{1}", timeOut.TotalMilliseconds, exceptionMessage);
                    throw new TimeoutException(errorMessage);
                }
            }
        }

        public static IWebElement WaitUntilVisible(this IWebElement element)
        {
            return WaitUntilVisible(element, 10);
        }

        public static IWebElement WaitUntilPresent(this IWebElement element, int timeoutSeconds)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(timeoutSeconds);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            IWebElement elem = null;

            while (true)
            {
                Exception lastException = null;
                try
                {
                    elem = element.FindElement(By.XPath("."));

                    if (elem != null)
                    {
                        return element;
                    }

                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception e) { lastException = e; }

                if (sw.Elapsed > timeOut)
                {
                    string exceptionMessage = lastException == null ? "" : lastException.Message;
                    string errorMessage = string.Format("Wait.UntilPresent: Element was not presenet after {0} Milliseconds" +
                                                        "\r\n Error Message:\r\n{1}", timeOut.TotalMilliseconds, exceptionMessage);
                    throw new TimeoutException(errorMessage);
                }
            }
        }

        public static IWebElement WaitUntilPresent(this IWebElement element)
        {
            return WaitUntilPresent(element, 10);
        }
    }
}
