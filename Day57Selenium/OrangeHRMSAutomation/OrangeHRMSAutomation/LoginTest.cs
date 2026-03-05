using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SeleniumExtras.WaitHelpers;
using ExtentReportsLib = AventStack.ExtentReports.ExtentReports;

namespace OrangeHRMSAutomation
{
    public class LoginTest
    {
        ChromeDriver driver;
        ExtentReportsLib extent;
        ExtentTest test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            var htmlReporter = new ExtentHtmlReporter("TestReport.html");
            extent = new ExtentReportsLib();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(@"C:\Users\aryan\Downloads\chromedriver-win64\chromedriver-win64"); // Update path
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
        }

        [Test]
        public void LoginToOrangeHRMS()
        {
            test = extent.CreateTest("Login Test").Info("Test Started");

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                IWebElement username = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.Name("username"))
                );

                IWebElement password = driver.FindElement(By.Name("password"));
                IWebElement loginBtn = driver.FindElement(By.CssSelector("button[type='submit']"));

                username.SendKeys("Admin");
                password.SendKeys("admin123");
                loginBtn.Click();

                Assert.IsTrue(driver.Url.Contains("dashboard"));
                test.Pass("Login successful");
            }
            catch (Exception ex)
            {
                test.Fail("Test failed: " + ex.Message);
                throw;
            }
        }

        [TearDown]
        public void EndTest()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }
    }
}