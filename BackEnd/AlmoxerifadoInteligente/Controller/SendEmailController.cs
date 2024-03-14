using AlmoxerifadoInteligente.Models;
using Microsoft.AspNetCore.Mvc;
using RaspagemMagMer.Operations;

namespace AlmoxerifadoInteligente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AlmoxarifadoDBContext _dbContext;

        public EmailController(AlmoxarifadoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("enviar")]
        public ActionResult<string> EnviarEmail(string destinatario, int idProduto)
        {
            SendEmail sendEmail = new(_dbContext);
            bool enviado = sendEmail.EnviarEmail(destinatario, idProduto);

            if (enviado)
            {
                return Ok("Email enviado com sucesso.");
            }
            else
            {
                return BadRequest("Falha ao enviar o email.");
            }
        }
    }
}
