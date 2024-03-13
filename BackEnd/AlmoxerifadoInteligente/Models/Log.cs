using System;
using System.Collections.Generic;

namespace AlmoxerifadoInteligente.Models
{
    public partial class Log
    {
        public int IdLog { get; set; }
        public string CodigoRobo { get; set; } = null!;
        public string UsuarioRobo { get; set; } = null!;
        public DateTime Datetime { get; set; }
        public string Etapa { get; set; } = null!;
        public string InformacaoLog { get; set; } = null!;
        public int IdProduto { get; set; }
    }
}
