using System;
using System.Threading;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RaspagemMagMer.Operations;

namespace AlmoxerifadoInteligente.API.Scraps
{
    public class MagazineScraper
    {
        private readonly ChromeOptions _chromeOptions;

        public MagazineScraper()
        {
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

        public string ObterPreco(string descricaoProduto, int idProduto)
        {
            string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto}";

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//p[@data-testid='price-value']");

                if (firstProductPriceNode != null)
                {
                    string firstProductPrice = firstProductPriceNode.InnerText.Trim();
                    Console.WriteLine("Preco Magalu: " + firstProductPrice + "\n");
                    return firstProductPrice;
                }
                else
                {
                    Console.WriteLine("Preço não encontrado no Agility Pack.\n");

                    return ObterPreco2(descricaoProduto, idProduto);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                LogRegister.RegistrarLog(DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro: {ex.Message}", idProduto);

                return null;
            }
        }

        public string ObterPreco2(string descricaoProduto, int idProduto)
        {
            try
            {
                using (IWebDriver driver = InitializeDriver())
                {
                    string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto}";
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(5000);

                    IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));

                    if (priceElement != null)
                    {
                        string firstProductPrice = priceElement.Text;

                        LogRegister.RegistrarLog(DateTime.Now, "WebScraping - Magazine Luiza", "Sucesso - Selenium", idProduto);
                        Console.WriteLine("Preco Magalu: " + firstProductPrice + "\n");
                        return firstProductPrice;
                    }
                    else
                    {
                        Console.WriteLine("Preço não encontrado.");

                        LogRegister.RegistrarLog(DateTime.Now, "WebScraping - Magazine Luiza", "Preço não encontrado - Selenium", idProduto);

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                LogRegister.RegistrarLog(DateTime.Now, "Web Scraping - Magazine Luiza", $"Erro: {ex.Message}", idProduto);

                return null;
            }
        }

        public string ObterNome(string descricaoProduto)
    {
           

            using (IWebDriver driver = InitializeDriver())
            {
                string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto}";
                driver.Navigate().GoToUrl(url);

                IWebElement nameElement = driver.FindElement(By.CssSelector("[data-testid='product-title']"));

                if (nameElement != null)
                {   
                    Console.WriteLine("Nome Magalu: " + nameElement.Text + "\n");
                    return nameElement.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ObterLink(string descricaoProduto)
    {
        string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto.Replace(' ', '+')}";
        return url;
    }
}
}


