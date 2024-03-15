using AlmoxerifadoInteligente.Operations.Register;
using HtmlAgilityPack;
using OpenQA.Selenium;
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

        private readonly LogRegister _logRegister;

        public MercadoLivreScraper(LogRegister logRegister)
        {
            _logRegister = logRegister;
        }
        public void ObterData(string descricaoProduto, int idProduto)
        {

            string url = $"https://lista.mercadolivre.com.br/{descricaoProduto.Replace(' ', '+')}";

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

                    _logRegister.RegistrarLog(DateTime.Now, "WebScraping - Mercado Livre", "Sucesso", idProduto);
                    Console.Write("Preço Mercado: " + Preco + "\n");

                }
                else
                {
                    Console.WriteLine("Item não encontrado no Mercado Livre.");

                    _logRegister.RegistrarLog(DateTime.Now, "WebScraping - Mercado Livre", "Preço não encontrado", idProduto);
                    Preco = null;
                    Nome = "";
                    Link = url;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

                _logRegister.RegistrarLog(DateTime.Now, "Web Scraping - Mercado Livre", $"Erro: {ex.Message}", idProduto);

            }
        }
    }

      
}