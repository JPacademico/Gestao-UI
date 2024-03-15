using AlmoxerifadoInteligente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxerifadoInteligente.Operations.Register
{
    public class BenchRegister
    {
        private readonly AlmoxarifadoDBContext _context;

        public BenchRegister(AlmoxarifadoDBContext context)
        {
            _context = context;
        }

        public void RegistrarBench(string nomeMer, string nomeMag, string linkMer, string linkMag, decimal precoMer, decimal precoMag, decimal economia, int idProd)
        {
            
            var benchmarkitem = new BenchmarkingItem
            {
                NomeLoja1 = nomeMer,
                NomeLoja2 = nomeMag,
                PrecoLoja1 = precoMer,
                PrecoLoja2 = precoMag,
                LinkLoja1 = linkMer,
                LinkLoja2 = linkMag,
                Economia = economia,
                IdProduto = idProd
            };
            _context.BenchmarkingItem.Add(benchmarkitem);
            _context.SaveChanges();
        }
    }
}