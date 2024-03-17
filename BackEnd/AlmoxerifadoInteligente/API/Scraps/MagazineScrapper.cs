using System;
using System.Data.SqlTypes;
using System.Security.Policy;
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
        }

        public void ObterData(string descricaoProduto, int idProduto)
        {
            string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto.Replace(' ', '+')}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string content = response.Content.ReadAsStringAsync().Result;

                        var docHtml = new HtmlDocument();

                        docHtml.LoadHtml(content);

                        var produtos = docHtml.DocumentNode.SelectNodes("//a");

                        foreach (var item in produtos)
                        {
                            if (item.OuterHtml.Contains("data-testid=\"product-card-container\""))
                            {

                                var card = item;
                                var nodePriceValue = card.SelectSingleNode(".//p[@data-testid=\"price-value\"]");
                                var nodeNameValue = card.SelectSingleNode(".//h2[@data-testid=\"product-title\"]");

                                Link = url;
                                Nome = nodeNameValue.InnerText;
                                Preco = nodePriceValue.InnerText;
                                Console.WriteLine("Nome Magalu: "+Nome);
                                Console.WriteLine("Preco Magalu: "+Preco);
                                Console.WriteLine("Link Magalu: " + Link);
                                return;
                            }
                            
                        }

                    }
                    _logRegister.RegistrarLog(DateTime.Now, "WebScraping - Magazine Luiza", "Sucesso", idProduto);

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


