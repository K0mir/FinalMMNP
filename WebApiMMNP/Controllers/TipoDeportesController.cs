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
    public class TipoDeportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipoDeportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoDeportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeporte>>> GetTipoDeportes()
        {
            return await _context.TipoDeportes.ToListAsync();
        }

        // GET: api/TipoDeportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeporte>> GetTipoDeporte(int id)
        {
            var tipoDeporte = await _context.TipoDeportes.FindAsync(id);

            if (tipoDeporte == null)
            {
                return NotFound();
            }

            return tipoDeporte;
        }

        // PUT: api/TipoDeportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDeporte(int id, TipoDeporte tipoDeporte)
        {
            if (id != tipoDeporte.IdTipo)
            {
                return BadRequest();
            }

            _context.Entry(tipoDeporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDeporteExists(id))
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

        // POST: api/TipoDeportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoDeporte>> PostTipoDeporte(TipoDeporte tipoDeporte)
        {
            _context.TipoDeportes.Add(tipoDeporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoDeporte", new { id = tipoDeporte.IdTipo }, tipoDeporte);
        }

        // DELETE: api/TipoDeportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoDeporte(int id)
        {
            var tipoDeporte = await _context.TipoDeportes.FindAsync(id);
            if (tipoDeporte == null)
            {
                return NotFound();
            }

            _context.TipoDeportes.Remove(tipoDeporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoDeporteExists(int id)
        {
            return _context.TipoDeportes.Any(e => e.IdTipo == id);
        }
    }
}
