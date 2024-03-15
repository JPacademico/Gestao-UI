using System;
using System.Threading;
using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AlmoxerifadoInteligente.API.Scraps
{
    public class MagazineScraper
    {
        private readonly ChromeOptions _chromeOptions;
        public string Link { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }

        private readonly LogRegister _logRegister;
        public MagazineScraper(LogRegister logRegister)
        {
            _logRegister = logRegister;
            _chromeOptions = new ChromeOptions();
            _chromeOptions.AddArgument("--headless");
            _chromeOptions.AddArgument("--disable-gpu");
            _chromeOptions.AddArgument("--disable-dev-shm-usage");
            _chromeOptions.AddArgument("--no-sandbox");
            _chromeOptions.AddArgument("--silent");
            _chromeOptions.AddArgument("--log-level=3");
        }

        private IWebDriver InitializeDriver()
        {
            return new ChromeDriver(_chromeOptions);
        }

        public void ObterData(string descricaoProduto, int idProduto)
        {
            
            try
            {
                using (IWebDriver driver = InitializeDriver())
                {
                    string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto.Replace(' ', '+')}";
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(5000);

                    IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));
                    IWebElement nameElement = driver.FindElement(By.CssSelector("[data-testid='product-title']"));

                    if (priceElement != null)
                    {
                        string firstProductPrice = priceElement.Text;
                        string firstProductName = nameElement.Text;
                        
                        Preco = firstProductPrice;
                        Nome = firstProductName;
                        Link = url;
                        
                        _logRegister.RegistrarLog(DateTime.Now, "WebScraping - Magazine Luiza", "Sucesso", idProduto);
                        Console.WriteLine("Preco Magalu: " + firstProductPrice + "\n");
                        Console.WriteLine("Nome Magalu: " + nameElement.Text + "\n");
                    }
                    else
                    {
                        Console.WriteLine("Preço não encontrado.");

                        _logRegister.RegistrarLog(DateTime.Now, "WebScraping - Magazine Luiza", "Preço não encontrado", idProduto);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                _logRegister.RegistrarLog(DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro: {ex.Message}", idProduto);
            }
        }

   
}
}


