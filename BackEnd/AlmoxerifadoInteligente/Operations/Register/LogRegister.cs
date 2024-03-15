using AlmoxerifadoInteligente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxerifadoInteligente.Operations.Register
{
    public class LogRegister
    {
        private readonly AlmoxarifadoDBContext _context;

        public LogRegister(AlmoxarifadoDBContext context)
        {
            _context = context;
        }
        public static string CodRobo { get; set; } = "1806";

        public static string UsuRob { get; set; } = "rafaelMecenas";

        public void RegistrarLog(DateTime dateLog, string processo, string infLog, int idProd)
        {
            
            var log = new Log
            {
                CodigoRobo = CodRobo,
                UsuarioRobo = UsuRob,
                Datetime = dateLog,
                Etapa = processo,
                InformacaoLog = infLog,
                IdProduto = idProd
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
