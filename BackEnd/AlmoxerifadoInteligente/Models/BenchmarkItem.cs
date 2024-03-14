using System;
using System.Collections.Generic;

namespace AlmoxerifadoInteligente.Models
{
    public partial class BenchmarkingItem
    {
        public int Id { get; set; }
        public string NomeLoja1 { get; set; } = null!;
        public string LinkLoja1 { get; set; } = null!; // Adicionando link para a Loja 1
        public string NomeLoja2 { get; set; } = null!;
        public string LinkLoja2 { get; set; } = null!; // Adicionando link para a Loja 2
        public decimal PrecoLoja1 { get; set; }
        public decimal PrecoLoja2 { get; set; }
        public decimal Economia { get; set; }
        public string Email { get; set; } = null!;
        public int IdProduto { get; set; }
        public virtual Produto? IdProdutoNavigation { get; set; }
    }
}