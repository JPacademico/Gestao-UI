using AlmoxerifadoInteligente.API.Scraps;
using AlmoxerifadoInteligente.Models;
using RaspagemMagMer.Scraps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspagemMagMer.Operations
{
    public class Benchmarking
    {
        public static string[] CompareValue(string descricaoProduto, int idProduto)
        {
            string[] data = new string[4];
            
            try
            {

                MercadoLivreScraper mercadoLivreScraper = new();

                string mercadoLivrePreco = mercadoLivreScraper.ObterPreco(descricaoProduto, idProduto);
                string mercadoLivreNome = mercadoLivreScraper.ObterNome(descricaoProduto);
                string mercadoLivreLink = mercadoLivreScraper.ObterLink(descricaoProduto);

                MagazineScraper magazineLuizaScraper = new();

                string magazineLuizaPreco = magazineLuizaScraper.ObterPreco(descricaoProduto, idProduto);
                string magazineLuizaNome = magazineLuizaScraper.ObterNome(descricaoProduto);
                string magazineLuizaLink = magazineLuizaScraper.ObterLink(descricaoProduto);

                char[] charRemove = { 'R', '$', ' ' };

                decimal mercadoPreco = Convert.ToDecimal(mercadoLivrePreco.Trim(charRemove));
                decimal magazinePreco = Convert.ToDecimal(magazineLuizaPreco.Trim(charRemove));

                if (magazinePreco > mercadoPreco)

                {
                    decimal result = EconomiaOperation(magazinePreco, mercadoPreco);
                   
                    data[0] = Convert.ToString(result);
                    data[1] = mercadoLivreNome;
                    data[2] = mercadoLivreLink;
                    data[3] = mercadoLivrePreco;

                    BenchRegister.RegistrarBench(mercadoLivreNome, magazineLuizaNome, mercadoLivreLink,magazineLuizaLink,mercadoPreco, magazinePreco, result, idProduto);

                    LogRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Sucesso", idProduto);

                    return data;


                }
                else if (magazinePreco < mercadoPreco)
                {
                    decimal result = EconomiaOperation(mercadoPreco, magazinePreco);
                    data[0] = Convert.ToString(result);
                    data[1] = magazineLuizaNome;
                    data[2] = magazineLuizaLink;
                    data[3] = magazineLuizaPreco;

                    BenchRegister.RegistrarBench(mercadoLivreNome, magazineLuizaNome, mercadoLivreLink, magazineLuizaLink, mercadoPreco, magazinePreco, result, idProduto);

                    LogRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Sucesso", idProduto);

                    return data;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Erro", idProduto);
                string[] erro = new string[1];
                erro[0] = ex.Message;
                Console.WriteLine(erro[0]);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return erro;
                

            }
        }
            
        public static decimal EconomiaOperation(decimal precoMaior, decimal precoMenor)
        {
            decimal result = precoMaior - precoMenor;
            return result;
        }
    }
}
