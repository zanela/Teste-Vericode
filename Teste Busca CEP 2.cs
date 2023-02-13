using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BuscaCEPAutomation
{
    [Binding]
    public class BuscaCEPSteps
    {
        private IWebDriver driver;

        [Given(@"que eu esteja na tela inicial do site dos correios")]
        public void DadoQueEuEstejaNaTelaInicialDoSiteDosCorreios()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.buscacep.correios.com.br/");
        }

        [When(@"eu pesquiso pelo CEP (.*)")]
        public void QuandoEuPesquisoPeloCEP(string cep)
        {
            driver.FindElement(By.Id("Geral")).SendKeys(cep);
            driver.FindElement(By.Id("btnSearch")).Click();
        }

        [Then(@"o resultado da busca do CEP (.*) é (.*)")]
        public void EntaoOResultadoDaBuscaDoCEPEE(string cep, string resultadoEsperado)
        {
            var resultado = driver.FindElement(By.XPath("//*[@id='resultado']/span")).Text;

            if (cep == "80700000")
            {
                Assert.IsTrue(resultado.Contains("DADOS NAO ENCONTRADOS"), "CEP não encontrado");
            }
            else
            {
                Assert.IsTrue(resultado.Contains(resultadoEsperado), "Resultado da busca do CEP diferente do esperado");
            }
        }

        [When(@"eu pesquiso pelo rastreamento de código (.*)")]
        public void QuandoEuPesquisoPeloRastreamentoDeCodigo(string codigoRastreamento)
        {
            driver.Navigate().GoToUrl("http://www.buscacep.correios.com.br/sistemas/rastreamento/");
            driver.FindElement(By.Id("objetos")).SendKeys(codigoRastreamento);
            driver.FindElement(By.CssSelector("input[value='Buscar']")).Click();
        }

        [Then(@"o resultado da pesquisa do rastreamento de código (.*) é inválido")]
        public void EntaoOResultadoDaPesquisaDoRastreamentoDeCodigoEInvalido(string codigoRastreamento)
        {
            var resultado = driver.FindElement(By.CssSelector("div.erro")).Text;
            Assert.IsTrue(resultado.Contains("Não encontramos nenhum resultado"), "Código de rastreamento vá
