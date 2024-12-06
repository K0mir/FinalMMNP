using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalData.Data;
using FinalData.Data.Entitys;

namespace WebApiMMNP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Deportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deporte>>> GetDeportes()
        {
            return await _context.Deportes.ToListAsync();
        }

        // GET: api/Deportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deporte>> GetDeporte(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);

            if (deporte == null)
            {
                return NotFound();
            }

            return deporte;
        }

        // PUT: api/Deportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeporte(int id, Deporte deporte)
        {
            if (id != deporte.DeporteId)
            {
                return BadRequest();
            }

            _context.Entry(deporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeporteExists(id))
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

        // POST: api/Deportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deporte>> PostDeporte(Deporte deporte)
        {
            _context.Deportes.Add(deporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeporte", new { id = deporte.DeporteId }, deporte);
        }

        // DELETE: api/Deportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeporte(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null)
            {
                return NotFound();
            }

            _context.Deportes.Remove(deporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeporteExists(int id)
        {
            return _context.Deportes.Any(e => e.DeporteId == id);
        }
    }
}
