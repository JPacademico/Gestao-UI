using Microsoft.AspNetCore.Mvc;
using RaspagemMagMer.Operations;
using System;
using System.Collections.Generic;

namespace SuaAplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkingController : ControllerBase
    {
        [HttpGet]
        [Route("compare")]
        public ActionResult<List<object>> ComparePreco(string descricaoProduto, int idProd)
        {
            try
            {
                var resultadoComparacao = Benchmarking.CompareValue(descricaoProduto, idProd).Result;

                return Ok(resultadoComparacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao comparar preços: {ex.Message}");
            }
        }
    }
}