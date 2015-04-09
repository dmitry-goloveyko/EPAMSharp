using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    public static class Utils
    {
        public static void CreateLargeFile(String absolutePath, int megabytes)
        {
            FileStream fs = new FileStream(absolutePath, FileMode.CreateNew);
            fs.Seek(megabytes * 1024 * 1024, SeekOrigin.Begin);
            fs.WriteByte(0);
            fs.Close();
        }

        public static bool ElementIsUnique(String xpath)
        {
            List<IWebElement> elements = Driver.getDriver().FindElements(By.XPath(xpath)).ToList();

            if (elements.Count == 1)
            {
                return true;
            }

            return false;
        }

        public static bool ElementIsPresent(String xpath)
        {
            try
            {
                Driver.getDriver().FindElement(By.XPath(xpath));
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        public static bool ImagesEqual(String file1, String file2)
        {
            FileStream fs1 = new FileStream(file1, FileMode.Open);
            FileStream fs2 = new FileStream(file2, FileMode.Open);

            Bitmap bmp1 = new Bitmap(fs1);
            Bitmap bmp2 = new Bitmap(fs2);

            fs1.Close();
            fs2.Close();

            Color pix1 = new Color();
            Color pix2 = new Color();
            bool equal = true;
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    pix1 = bmp1.GetPixel(x, y);
                    pix2 = bmp2.GetPixel(x, y);
                    if (pix1 != pix2)
                    {
                        equal = false;
                        break;
                    }
                }

                if (!equal)
                {
                    break;
                }
            }

            return equal;
        }

        public static void TakeScreenshot(String filePath)
        {
            System.Threading.Thread.Sleep(10000);

            Screenshot ss = ((ITakesScreenshot)Driver.getDriver()).GetScreenshot();

            File.WriteAllBytes(filePath, ss.AsByteArray);
        }
    }
}
