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
            char[] charRemove = { 'R', '$', ' ' };

            try
            {

                MercadoLivreScraper mercadoLivre = new();
                mercadoLivre.ObterData(descricaoProduto, idProduto);

                MagazineScraper magazineLuiza = new();
                magazineLuiza.ObterData(descricaoProduto,idProduto);

               

                decimal mercadoPreco = Convert.ToDecimal(mercadoLivre.Preco.Trim(charRemove));
                decimal magazinePreco = Convert.ToDecimal(magazineLuiza.Preco.Trim(charRemove));

                if (magazinePreco > mercadoPreco)

                {
                    decimal result = EconomiaOperation(magazinePreco, mercadoPreco);
                   
                    data[0] = Convert.ToString(result);
                    data[1] = mercadoLivre.Nome;
                    data[2] = mercadoLivre.Link;
                    data[3] = mercadoLivre.Preco;

                    BenchRegister.RegistrarBench(mercadoLivre.Nome, magazineLuiza.Nome, mercadoLivre.Link, magazineLuiza.Link, mercadoPreco, magazinePreco, result, idProduto);

                    LogRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Sucesso", idProduto);

                    return data;


                }
                else if (magazinePreco < mercadoPreco)
                {
                    decimal result = EconomiaOperation(mercadoPreco, magazinePreco);
                    data[0] = Convert.ToString(result);
                    data[1] = magazineLuiza.Nome;
                    data[2] = magazineLuiza.Link;
                    data[3] = magazineLuiza.Preco;

                    BenchRegister.RegistrarBench(mercadoLivre.Nome, magazineLuiza.Nome, mercadoLivre.Link, magazineLuiza.Link, mercadoPreco, magazinePreco, result, idProduto);

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
