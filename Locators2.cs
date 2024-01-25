using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using WebDriverManager.DriverConfigs.Impl;
using SeleniumExtras.WaitHelpers;
using System.Text.Json;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;


namespace seltest_1
{
    public class Locators2
    {
        private static ConnectionStorage LocalFile;
        ExtentReports extent;
        ExtentHtmlReporter htmlReporter;
        IWebDriver driver;
        private ExtentTest test;
        [SetUp]
        public void StartBrowser()
        {
            extent = new ExtentReports();
            htmlReporter = new ExtentHtmlReporter("C:\\Users\\DELL\\source\\repos\\seltest-1\\report.html");
            extent.AttachReporter(htmlReporter);
            string jsonFilePath = "C:\\Users\\DELL\\source\\repos\\seltest-1\\Local.File.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            LocalFile = JsonSerializer.Deserialize<ConnectionStorage>(jsonContent);
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = LocalFile.Values.websiteUrl;
            test = extent.CreateTest("StartBrowser");
        }

        [Test]
        public void RunAllTests()
        {
            ExecuteTest("LoginDetails",LoginDetails);
            ExecuteTest("Password",Password);
            ExecuteTest("HomePage",HomePage);
            ExecuteTest("Azure",Azure);
            ExecuteTest("MainPage",MainPage);
            ExecuteTest("CreatePost",CreatePost);
            ExecuteTest("RadioButtons",RadioButtons);
            ExecuteTest("SubmitButton",SubmitButton);
            ExecuteTest("SubjectKeys",SubjectKeys);
            ExecuteTest("BodyKeys",BodyKeys);
            ExecuteTest("SaveButton",SaveButton);
            ExecuteTest("ShareButton",ShareButton);
            ExecuteTest("KudoButton",KudoButton);
        }
        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            extent.Flush();
        }

        private void ExecuteTest(string testName, Action testMethod)
        {
            try
            {
                test = extent.CreateTest(testName);
                testMethod();
                test.Pass("Test Passed");
            }
            catch (Exception ex)
            {
                test.Fail($"Test Failed: {ex.Message}");
                throw;
            }
        }
        public void LoginDetails()
        {


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.FindElement(By.Name("loginfmt")).SendKeys(LocalFile.Values.siginEmailid);
            driver.FindElement(By.CssSelector("input[value='Next']")).Click();

        }
        public void Password()
        {

            driver.FindElement(By.Name("passwd")).SendKeys(LocalFile.Values.SigInPassword);
            driver.FindElement(By.XPath("//input[@value='Sign in']")).Click();
        }
        public void HomePage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.FindElement(By.CssSelector("input[type='button']")).Click();
            wait.Until(ExpectedConditions.UrlContains(LocalFile.Values.PortalUrl));
        }
        public void Azure()
        {
            driver.FindElement(By.CssSelector("button[id='https://login.windows.net/e55fe08a-e4e3-4627-ad40-d9961c612aaa/']")).Click();
        }
        public void MainPage()
        {


            driver.FindElement(By.LinkText("Power BI")).Click();
            driver.FindElement(By.LinkText("Instuctor led training")).Click();
        }
        public void CreatePost()
        {
            driver.FindElement(By.XPath("//fluent-button[@id='createPostBtn']")).Click();
            Thread.Sleep(3000);
        }
        public void RadioButtons()
        {
            IList<IWebElement> radioButton = driver.FindElements(By.CssSelector("fluent-radio[class='option']"));
            radioButton[0].Click();
        }
        public void SubmitButton()
        {
            driver.FindElement(By.CssSelector("fluent-button[id='redirectButton']")).Click();
            Thread.Sleep(3000);
        }
        public void SubjectKeys()
        {

            driver.FindElement(By.Id("fc_subject")).SendKeys("Test subject");
        }
        public void BodyKeys()
        {
            Thread.Sleep(15000);
            driver.SwitchTo().Frame(0);
            driver.SwitchTo().Frame(0);
            //IWebElement elementInsideIframe = driver.FindElement(By.XPath("//body[@class='cke_editable ']"));
            IWebElement elementInsideIframe = driver.FindElement(By.XPath("//body[@class='cke_editable cke_editable_themed cke_contents_ltr']"));
            elementInsideIframe.Click();
            elementInsideIframe.SendKeys("This data is for testing purpose");
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }
        public void SaveButton()
        {
            driver.FindElement(By.Id("InsertButton")).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
        }
        public void ShareButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement shareButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("fluent-button[class='neutral']")));
            shareButton.Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("a[class='facebook']")).Click();
            driver.FindElement(By.CssSelector("a[class='twitter']")).Click();
            driver.FindElement(By.CssSelector("a[class='linkedin']")).Click();
            Thread.Sleep(2000);
            string ParentWindowName = driver.WindowHandles[0];
            driver.SwitchTo().Window(ParentWindowName);
            Thread.Sleep(2000);
        }
        public void KudoButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement kudoImage = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//fluent-button[@class='kudoBtn neutral']")));
            kudoImage.Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
        }
    }


}