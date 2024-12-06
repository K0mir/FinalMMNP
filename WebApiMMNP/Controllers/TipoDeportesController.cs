using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalData.Data;
using FinalData.Data.Entitys;
using AutoMapper;
using WebApiMMNP.Dtos;

namespace WebApiMMNP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoDeportesController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/TipoDeportes
        [HttpGet]
        public async Task<IList<TipoDeporteDtos>> GetTipoDeportes()
        {
            var tipoDeporte = await _context.TipoDeportes.ToListAsync();
            IList<TipoDeporteDtos> Dtos = new List<TipoDeporteDtos>();
            foreach (var Entity in tipoDeporte)
            {
                var dep = _mapper.Map<TipoDeporteDtos>(Entity);
                Dtos.Add(dep);
            }
            return Dtos;
        }

        // GET: api/TipoDeportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeporteDtos>> GetTipoDeporte(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tipoDeporte = await _context.TipoDeportes
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoDeporte == null)
            {
                return NotFound();
            }
            var Dtos = _mapper.Map<TipoDeporteDtos>(tipoDeporte);
            return Dtos;
        }

        // PUT: api/TipoDeportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDeporte(int id, TipoDeporteDtos tipoDeporteDtos)
        {
            if (id != tipoDeporteDtos.IdTipo)
            {
                return BadRequest();
            }
            var tipodeporte = await _context.TipoDeportes.FindAsync(id);
            tipodeporte = _mapper.Map<TipoDeporteDtos, TipoDeporte>(tipoDeporteDtos, tipodeporte);
            _context.Entry(tipodeporte).State = EntityState.Modified;
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
        public async Task<ActionResult<TipoDeporte>> PostTipoDeporte(TipoDeporteDtos tipoDeporteDtos)
        {
            var tipodeporte = _mapper.Map<TipoDeporteDtos, TipoDeporte>(tipoDeporteDtos);
            _context.TipoDeportes.Add(tipodeporte);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTipoDeporte", new { id = tipodeporte.IdTipo }, tipodeporte);
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
