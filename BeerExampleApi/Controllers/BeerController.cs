using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerExampleApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerExampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly BeerContext _context;

        public BeerController(BeerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers([FromQuery(Name = "limit")] int limit = 50, [FromQuery(Name = "offset")] int offset = 0)
        {
            return await _context
                .Beers
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        [HttpPost]
        public async Task<IActionResult> PostBeer(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBeer), new {id = beer.Id}, beer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.Entry(beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BeerExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Any(beer => beer.Id == id);
        }
    }
}