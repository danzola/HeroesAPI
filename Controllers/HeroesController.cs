using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HeroesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly TestDAASContext _context;

        public HeroesController(ILogger<HeroesController> logger, TestDAASContext context)
        {
            _logger = logger;
            _context = context;
        }

        private bool HeroExists(Heroes heroes)
        {
            return _context.Heroes.Any(h => h.Id == heroes.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroes>>> Get()
        {
            var result = await _context.Heroes.ToListAsync();

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Heroes>> Get(int id)
        {
            var result = await _context.Heroes.FindAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<IEnumerable<Heroes>>> Get(string name)
        {
            var result = await (from h in _context.Heroes
                            where h.Hero.Contains(name)
                            select h).ToListAsync();

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Heroes hero)
        {
            if (id != hero.Id)            
            {
                return BadRequest();
            }
                
            _context.Entry(hero).State = EntityState.Modified;
                
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(hero))
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

        [HttpPost]
        public async Task<ActionResult<Heroes>> Post(Heroes hero)
        {
             _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = hero.Id }, hero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Heroes>> Delete(int id)
        {
            var result = await _context.Heroes.FindAsync(id);
            if (result is null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
