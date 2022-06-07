using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace Selenium_Tests
{
    public class Tests
    {
        IWebDriver driver;
        Random random = new Random(DateTime.Now.Millisecond);

        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5_";
        int bannerTime = 5000;
        int smallDelay = 500;

        [SetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            
            // to ignore security issues
            var options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;

            driver = new ChromeDriver(path + @"\drivers", options);
        }

        //[Test]
        public void LoginUser()
        {
            driver.Navigate().GoToUrl("http://130.162.232.178/login");
            Assert.IsTrue(driver.FindElement(By.Id("loginForm")).Displayed);
            // already existing user
            string login = existingLogin;
            string password = existingPassword;

            WebElement usernameInput = (WebElement)driver.FindElement(By.Id("loginForm_login"));
            WebElement passwordInput = (WebElement)driver.FindElement(By.Id("loginForm_password"));

            usernameInput.SendKeys(login);
            passwordInput.SendKeys(password);

            WebElement loginButton = (WebElement)driver.FindElement(By.CssSelector("button[class='ant-btn ant-btn-primary ant-btn-block']"));
            loginButton.Click();
        }

        //[Test]
        public void RegisterUser()
        {
            driver.Navigate().GoToUrl("http://130.162.232.178/registration");
            Assert.IsTrue(driver.FindElement(By.ClassName("registration")).Displayed);
            // generate random unique login
            string username = "stest_";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string end = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            string password = "aBcD45_/";
            username += end;
            username += "@seleniumtest.amol";
            string age = "19";

            WebElement usernameInput = (WebElement)driver.FindElement(By.Id("registrationForm_login"));
            WebElement passwordInput = (WebElement)driver.FindElement(By.Id("registrationForm_password"));
            WebElement ageInput = (WebElement)driver.FindElement(By.Name("registrationAge"));

            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            ageInput.SendKeys(age);

            WebElement registerButton = (WebElement)driver.FindElement(By.CssSelector("button[class='ant-btn ant-btn-primary ant-btn-block']"));
            registerButton.Click();
        }

        [Test]
        public void TakeMoodTest()
        {
            driver.Navigate().GoToUrl("http://130.162.232.178/login");
            // already existing user
            string login = existingLogin;
            string password = existingPassword;

            WebElement usernameInput = (WebElement)driver.FindElement(By.Id("loginForm_login"));
            WebElement passwordInput = (WebElement)driver.FindElement(By.Id("loginForm_password"));

            usernameInput.SendKeys(login);
            passwordInput.SendKeys(password);

            WebElement loginButton = (WebElement)driver.FindElement(By.CssSelector("button[class='ant-btn ant-btn-primary ant-btn-block']"));
            loginButton.Click();

            // at this point we are at main page

            // wait explicitly
            Thread.Sleep(bannerTime + 100);

            WebElement moodTestsButton = (WebElement)(driver.FindElement(By.CssSelector("[class='moodTestsParagraph']")));
            moodTestsButton.Click();
            // choose category
            Thread.Sleep(smallDelay);
            WebElement happinessImage = (WebElement)driver.FindElement(By.CssSelector("img[alt='happiness'"));

            happinessImage.Click();

            // mark everything as 5
            IList<IWebElement> questions_answer5 = driver.FindElements(By.CssSelector("div[aria-posinset='5'"));
            
            foreach(IWebElement question in questions_answer5)
                question.Click();

            Thread.Sleep(smallDelay);
            WebElement submitTestButton = 
                (WebElement)driver.FindElement(By.CssSelector("button[class='ant-btn ant-btn-primary moodTestSubmit']"));
            submitTestButton.Click();

            Assert.IsTrue(driver.FindElement(By.Id("moodTestForm")).Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            //driver.Quit();
        }
    }
}