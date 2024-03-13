using AlmoxerifadoInteligente.Models;
using Microsoft.AspNetCore.Mvc;
using RaspagemMagMer.Operations;

namespace SuaAplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkingController : ControllerBase
    {
        [HttpGet]
        [Route("compare")]
        public ActionResult<string[]> ComparePreco(string descricaoProduto, int idProd)
        {
            // Chama a função de comparação de preços da classe Benchmarking
            string[] resultadoComparacao = Benchmarking.CompareValue(descricaoProduto,idProd);

            // Retorna o resultado da comparação
            return resultadoComparacao;
        }
    }
}
