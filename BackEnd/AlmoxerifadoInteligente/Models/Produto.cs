using System;
using System.Collections.Generic;

namespace AlmoxerifadoInteligente.Models
{
    public partial class Produto
    {
        
        public int IdProduto { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public int? EstoqueAtual { get; set; }
        public int? EstoqueMinimo { get; set; }
        public bool? Status { get;set; }
        public bool? EmailStatus { get; set; } = default;

    }
}
