using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlmoxerifadoInteligente.Models;

namespace AlmoxerifadoInteligente.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkingItemsController : ControllerBase
    {
        private readonly AlmoxarifadoDBContext _context;

        public BenchmarkingItemsController(AlmoxarifadoDBContext context)
        {
            _context = context;
        }

        // GET: api/BenchmarkingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenchmarkingItem>>> GetBenchmarkingItem()
        {
            if (_context.BenchmarkingItem == null)
            {
                return NotFound();
            }
            return await _context.BenchmarkingItems.Include(x => x.IdProdutoNavigation).ToListAsync();
        }

        // GET: api/BenchmarkingItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BenchmarkingItem>> GetBenchmarkingItem(int id)
        {
            if (_context.BenchmarkingItem == null)
            {
                return NotFound();
            }
            var benchmarkingItem = await _context.BenchmarkingItems.Include(x => x.IdProdutoNavigation).FirstOrDefaultAsync(x => x.Id == id);

            if (benchmarkingItem == null)
            {
                return NotFound();
            }

            return benchmarkingItem;
        }

        // PUT: api/BenchmarkingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenchmarkingItem(int id, BenchmarkingItem benchmarkingItem)
        {
            if (id != benchmarkingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(benchmarkingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenchmarkingItemExists(id))
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

        // POST: api/BenchmarkingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BenchmarkingItem>> PostBenchmarkingItem(BenchmarkingItem benchmarkingItem)
        {
            if (_context.BenchmarkingItem == null)
            {
                return Problem("Entity set 'AlmoxarifadoDBContext.BenchmarkingItem'  is null.");
            }
            _context.BenchmarkingItem.Add(benchmarkingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBenchmarkingItem", new { id = benchmarkingItem.Id }, benchmarkingItem);
        }

        // DELETE: api/BenchmarkingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenchmarkingItem(int id)
        {
            if (_context.BenchmarkingItem == null)
            {
                return NotFound();
            }
            var benchmarkingItem = await _context.BenchmarkingItem.FindAsync(id);
            if (benchmarkingItem == null)
            {
                return NotFound();
            }

            _context.BenchmarkingItem.Remove(benchmarkingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BenchmarkingItemExists(int id)
        {
            return (_context.BenchmarkingItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
