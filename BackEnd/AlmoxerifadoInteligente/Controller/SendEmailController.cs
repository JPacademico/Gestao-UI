using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
using Microsoft.AspNetCore.Mvc;
using RaspagemMagMer.Operations;

namespace AlmoxerifadoInteligente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AlmoxarifadoDBContext _dbContext;
        private readonly LogRegister _logRegister;

        public EmailController(AlmoxarifadoDBContext dbContext,LogRegister logRegister)
        {
            _logRegister = logRegister;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("enviar")]
        public ActionResult<string> EnviarEmail(string destinatario, int idProduto)
        {
            SendEmail sendEmail = new(_dbContext,_logRegister);
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
