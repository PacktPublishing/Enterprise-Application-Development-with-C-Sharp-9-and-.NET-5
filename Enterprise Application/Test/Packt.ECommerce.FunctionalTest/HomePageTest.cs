namespace Packt.ECommerce.FunctionalTest
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    [TestClass]
    public class HomePageTest
    {
        ChromeDriver _webDriver = null;

        [TestInitialize]
        public void InitializeWebDriver()
        {
            var d = new DriverManager();
            d.SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }

        [TestMethod]
        public void When_Application_Launched_Title_Should_be_ECommerce_Pact()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:44365/");
            Assert.AreEqual("Ecommerce Packt", _webDriver.Title);
        }

        [TestMethod]
        public void When_Searched_For_Item()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:44365/");
            var searchTextBox = _webDriver.FindElement(By.Name("SearchString"));
            searchTextBox.SendKeys("Orange Shirt");

            _webDriver.FindElement(By.Name("searchButton")).Click();

            var items = _webDriver.FindElements(By.ClassName("product-description"));
            var invaidProductCout = items.Where(e => e.Text != "Orange Shirt").Count();
            Assert.AreEqual(0, invaidProductCout);
        }

        [TestCleanup]
        public void WebDriverCleanup()
        {
            _webDriver.Quit();
        }
    }
}
