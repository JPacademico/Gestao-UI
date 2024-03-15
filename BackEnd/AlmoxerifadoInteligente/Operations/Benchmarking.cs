using AlmoxerifadoInteligente.API.Scraps;
using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
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

        private readonly BenchRegister _bench;

        public Benchmarking(BenchRegister bench, LogRegister logRegister)
        {
            _bench = bench;
            _logRegister = logRegister;
        }
        private readonly LogRegister _logRegister;

        
        public async Task<List<object>> CompareValue(string descricaoProduto, int idProduto)
        {
            List<object> data = new List<object>();
            char[] charRemove = { 'R', '$', ' ' };

            try
            {
                MercadoLivreScraper mercadoLivre = new(_logRegister);
                mercadoLivre.ObterData(descricaoProduto, idProduto);

                MagazineScraper magazineLuiza = new(_logRegister);
                magazineLuiza.ObterData(descricaoProduto, idProduto);

                decimal mercadoPreco = Convert.ToDecimal(mercadoLivre.Preco.Trim(charRemove));
                decimal magazinePreco = Convert.ToDecimal(magazineLuiza.Preco.Trim(charRemove));

                decimal result = (magazinePreco > mercadoPreco) ? EconomiaOperation(magazinePreco, mercadoPreco) :
                                 (magazinePreco < mercadoPreco) ? EconomiaOperation(mercadoPreco, magazinePreco) :
                                 0;

                data.Add(Convert.ToString(result));
                data.Add((magazinePreco > mercadoPreco) ? mercadoLivre.Nome : magazineLuiza.Nome);
                data.Add((magazinePreco > mercadoPreco) ? mercadoLivre.Link : magazineLuiza.Link);
                data.Add((magazinePreco > mercadoPreco) ? mercadoPreco : magazinePreco);
                
                _bench.RegistrarBench(mercadoLivre.Nome, magazineLuiza.Nome, mercadoLivre.Link, magazineLuiza.Link, mercadoPreco, magazinePreco, result, idProduto);
                _logRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Sucesso", idProduto);

                try
                {
                    var log = new InsertLogDTO(1806, "LRJevs", descricaoProduto, Convert.ToDouble(mercadoPreco), Convert.ToDouble(magazinePreco), Convert.ToDouble(result));
                    await log.InsertAsync();
                    _logRegister.RegistrarLog(DateTime.Now, "DTO", "Sucesso", idProduto);
                    Console.WriteLine("Informações enviadas com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao enviar as informações: {ex.Message}");
                }

                return data;
            }
            catch (Exception ex)
            {
                _logRegister.RegistrarLog(DateTime.Now, "Benchmarking", "Erro", idProduto);
                List<object> erro = new()
                {
                    ex.Message
                };
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
