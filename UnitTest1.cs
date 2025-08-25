using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using static System.Net.WebRequestMethods;

namespace MovieCatalogTests
{
    public class MovieCatalogTests
    {
        private IWebDriver driver;
        Random random;
        private static readonly string baseUrl = "https://d24hkho2ozf732.cloudfront.net/";
        string lastCreatedMovieTitle = "";
        string lastCreatedMovieDescription = "";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(baseUrl);

            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("test@mail.com");
            driver.FindElement(By.Id("Password")).SendKeys("123456");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        [Test, Order(1)]
        public void CreateMovieWithInvalidData()
        {
            driver.FindElement(By.LinkText("Add Movie"))?.Click();

            driver.FindElement(By.Id("Title"))?.SendKeys("");
            driver.FindElement(By.Id("Description"))?.SendKeys("");
            driver.FindElement(By.CssSelector("button[type='submit']"))?.Click();

            string errorMessage = driver.FindElement(By.CssSelector(".text-danger.validation-summary-errors li")).Text;
            Assert.That(errorMessage, Is.EqualTo("The Title field is required."));
        }

        [Test, Order(2)]
        public void CreateMovieWithValidData()
        {
            lastCreatedMovieTitle = GenerateRandomString("Movie Title ");
            lastCreatedMovieDescription = GenerateRandomString("Movie Description ");

            driver.FindElement(By.LinkText("Add Movie"))?.Click();

            driver.FindElement(By.Id("Title"))?.SendKeys(lastCreatedMovieTitle);
            driver.FindElement(By.Id("Description"))?.SendKeys(lastCreatedMovieDescription);
            driver.FindElement(By.CssSelector("button[type='submit']"))?.Click();

            Assert.That(driver.Url, Is.EqualTo(baseUrl + "Movies/All"));
            Assert.That(driver.PageSource, Does.Contain(lastCreatedMovieTitle));
        }

        [Test, Order(3)]
        public void ViewLastCreatedMovie()
        {
            driver.FindElement(By.LinkText("My Movies"))?.Click();

            var movieCards = driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            var lastCard = movieCards.Last();
            lastCard.FindElement(By.LinkText("Details")).Click();

            var title = driver.FindElement(By.CssSelector("h1"))?.Text.Trim();
            Assert.That(title, Is.EqualTo(lastCreatedMovieTitle));
        }

        [Test, Order(4)]
        public void EditLastCreatedMovieTitle()
        {
            driver.FindElement(By.LinkText("My Movies"))?.Click();

            var movieCards = driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            var lastCard = movieCards.Last();
            lastCard.FindElement(By.LinkText("Edit")).Click();

            var titleField = driver.FindElement(By.Id("Title"));
            var newTitle = "Edited " + lastCreatedMovieTitle;
            titleField.Clear();
            titleField.SendKeys(newTitle);

            driver.FindElement(By.CssSelector("button[type='submit']"))?.Click();

            driver.FindElement(By.LinkText("My Movies"))?.Click();
            Assert.That(driver.PageSource, Does.Contain(newTitle));

            lastCreatedMovieTitle = newTitle;
        }

        [Test, Order(5)]
        public void EditLastCreatedMovieDescription()
        {
            driver.FindElement(By.LinkText("My Movies"))?.Click();

            var movieCards = driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            var lastCard = movieCards.Last();
            lastCard.FindElement(By.LinkText("Edit")).Click();

            var descField = driver.FindElement(By.Id("Description"));
            var newDescription = "Edited " + lastCreatedMovieDescription;
            descField.Clear();
            descField.SendKeys(newDescription);

            driver.FindElement(By.CssSelector("button[type='submit']"))?.Click();

            driver.FindElement(By.LinkText("My Movies"))?.Click();
            Assert.That(driver.PageSource, Does.Contain(newDescription));

            lastCreatedMovieDescription = newDescription;
        }

        [Test, Order(6)]
        public void DeleteLastCreatedMovie()
        {
            driver.FindElement(By.LinkText("My Movies"))?.Click();

            var movieCards = driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            var lastCard = movieCards.Last();
            lastCard.FindElement(By.LinkText("Delete")).Click();

            driver.FindElement(By.CssSelector(".btn-danger"))?.Click();

            var cardsAfterDelete = driver.FindElements(By.CssSelector(".card.mb-4.box-shadow"));
            bool isDeleted = cardsAfterDelete.All(card => !card.Text.Contains(lastCreatedMovieTitle));
            Assert.That(isDeleted, Is.True);
        }

        private string GenerateRandomString(string prefix)
        {
            var rand = new Random();
            return prefix + rand.Next(1000, 9999);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}