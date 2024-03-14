using HtmlAgilityPack;
using OpenQA.Selenium;
using RaspagemMagMer.Operations;
using System.Drawing;
using System.Security.Policy;

namespace RaspagemMagMer.Scraps
{
    public class MercadoLivreScraper
    {
        public string Link { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }

        public MercadoLivreScraper() { }

        public MercadoLivreScraper(string link, string nome, string preco)
        {
            Link = link;
            Nome = nome;
            Preco = preco;
        }

        public void ObterData(string descricaoProduto, int idProduto)
        {

            string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";

            try
            {

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount ui-search-price__part ui-search-price__part--medium andes-money-amount--cents-superscript']");
                HtmlNode firstProductNameNode = document.DocumentNode.SelectSingleNode("//h2[@class='ui-search-item__title']");


                if (firstProductPriceNode != null)
                {

                    Preco = firstProductPriceNode.InnerText.Trim();
                    Nome = firstProductNameNode.InnerText.Trim();
                    Link = url;

                    LogRegister.RegistrarLog(DateTime.Now, "WebScraping - Mercado Livre", "Sucesso", idProduto);
                    Console.Write("Preço Mercado: " + Preco + "\n");

                }
                else
                {
                    Console.WriteLine("Preço não encontrado.");

                    LogRegister.RegistrarLog(DateTime.Now, "WebScraping - Mercado Livre", "Preço não encontrado", idProduto);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                LogRegister.RegistrarLog(DateTime.Now, "Web Scraping - Mercado Livre", $"Erro: {ex.Message}", idProduto);

            }
        }
    }

      
}