using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlmoxerifadoInteligente.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AlmoxerifadoInteligente.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AlmoxarifadoDBContext _context;

        public ProdutosController(AlmoxarifadoDBContext context)
        {
            _context = context;
        }


        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
          if (_context.Produtos == null)
          {
              return NotFound();
          }
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
          if (_context.Produtos == null)
          {
              return NotFound();
          }
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
          if (_context.Produtos == null)
          {
              return Problem("Entity set 'AlmoxarifadoDBContext.Produtos'  is null.");
          }
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.IdProduto }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (_context.Produtos == null)
            {
                return NotFound();
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Produtos/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Produto>> UpdateProduto(int id, [FromBody] Produto produtoPatch)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            if (produtoPatch.Descricao != null)
            {
                produto.Descricao = produtoPatch.Descricao;
            }

            if (produtoPatch.Preco.HasValue)
            {
                produto.Preco = produtoPatch.Preco.Value;
            }

            if (produtoPatch.EstoqueAtual.HasValue)
            {
                produto.EstoqueAtual = produtoPatch.EstoqueAtual.Value;
            }

            if (produtoPatch.EstoqueMinimo.HasValue)
            {
                produto.EstoqueMinimo = produtoPatch.EstoqueMinimo.Value;
            }
            
            if(produtoPatch.Status.HasValue)
            {
                produto.Status = produtoPatch.Status;
            }

            _context.Entry(produto).State = EntityState.Modified;
            var updatedProduto = await _context.SaveChangesAsync();

            return Ok(updatedProduto);
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.IdProduto == id)).GetValueOrDefault();
        }
    }
}
