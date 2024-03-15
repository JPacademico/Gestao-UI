using AlmoxerifadoInteligente.API.Scraps;
using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
using Newtonsoft.Json;
using RaspagemMagMer.Scraps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RaspagemMagMer.Operations
{
    public class DBCheck
    {
        private readonly AlmoxarifadoDBContext _context;

        public DBCheck(AlmoxarifadoDBContext context)
        {
            _context = context;
        }
        private readonly LogRegister _logRegister;

        public DBCheck(LogRegister logRegister)
        {
            _logRegister = logRegister;
        }
        static List<Produto> produtosVerificados = new();
        public async void VerificarNovoProduto()

        {
            
            string codUsu = LogRegister.CodRobo;
            string url = "https://localhost:7286/api/Produtos";

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(url);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseData);
                        List<Produto> novosProdutos = ObterNovosProdutos(responseData);

                        foreach (Produto produto in novosProdutos)
                        {
                           
                            if (!produtosVerificados.Exists(p => p.IdProduto == produto.IdProduto))
                            {

                                Console.WriteLine($"Novo produto encontrado: ID {produto.IdProduto}, Nome: {produto.Descricao}\n");

                                produtosVerificados.Add(produto);

                                if (!ProdutoJaRegistrado(produto.IdProduto,codUsu, _context))
                                {
                                    _logRegister.RegistrarLog(DateTime.Now, "ConsultaAPI - Verificar Produto", "Sucesso", produto.IdProduto);

                                   

                                    

                                }
                            }
                        }
                    }
                    else
                    {
                        // Imprimir mensagem de erro caso a requisição falhe
                        Console.WriteLine($"Erro: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            }
        }

        static List<Produto> ObterNovosProdutos(string responseData)
        {

            List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(responseData);
            return produtos;
        }

        static bool ProdutoJaRegistrado(int idProduto, string codRobo, AlmoxarifadoDBContext context)
        {
            return context.Logs.Any(log => log.IdProduto == idProduto && log.CodigoRobo == codRobo);
        }

    }
}
