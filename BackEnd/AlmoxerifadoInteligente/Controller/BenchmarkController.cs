using RaspagemMagMer.Operations;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SuaAplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkingController : ControllerBase
    {
        private readonly Benchmarking _benchmarking;

        public BenchmarkingController(Benchmarking benchmarking)
        {
            _benchmarking = benchmarking;
        }
        [HttpGet]
        [Route("compare")]
        public async Task<ActionResult<List<object>>> ComparePreco(string descricaoProduto, int idProd)
        {
            try
            {
                var resultadoComparacao = await _benchmarking.CompareValue(descricaoProduto, idProd);
                return Ok(resultadoComparacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao comparar preços: {ex.Message}");
            }
        }
    }
}