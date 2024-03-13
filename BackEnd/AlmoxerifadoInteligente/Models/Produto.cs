using System;
using System.Collections.Generic;

namespace AlmoxerifadoInteligente.Models
{
    public partial class Produto
    {
        public Produto()
        {
            BenchmarkingItems = new HashSet<BenchmarkingItem>();
        }

        public int IdProduto { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public int EstoqueAtual { get; set; }
        public int EstoqueMinimo { get; set; }

        public virtual ICollection<BenchmarkingItem> BenchmarkingItems { get; set; }
    }
}
