using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace CorreiosTest
{
    [TestFixture]
    public class CorreiosTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void BuscaCEP()
        {
            _driver.Navigate().GoToUrl("http://www.correios.com.br");

            IWebElement txtCEP = _driver.FindElement(By.Id("acesso-busca"));
            txtCEP.SendKeys("80700000");
            txtCEP.Submit();

            IWebElement lblResultado = _driver.FindElement(By.Id("resultadoBusca"));
            Assert.AreEqual("DADOS NAO ENCONTRADOS", lblResultado.Text);

            _driver.Navigate().GoToUrl("http://www.correios.com.br");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
