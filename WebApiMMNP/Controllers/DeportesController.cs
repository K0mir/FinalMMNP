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
    public class DeportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public DeportesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;

        }

        // GET: api/Deportes
        [HttpGet]
        public async Task<IList<DeporteDtos>> GetDeportes()
        {
            var deportes = await _context.Deportes.ToListAsync();
            var tipoDeportes = await _context.TipoDeportes.ToListAsync();
            IList<DeporteDtos> Dtos = new List<DeporteDtos>();
            foreach (var Entity in deportes)
            {
                var depTip = _mapper.Map<DeporteDtos>(Entity);
                var tipoDeporte = tipoDeportes.Find(x => x.IdTipo == depTip.IdTipo);
                depTip.NombreTipo = tipoDeporte.NombreTipo;
                Dtos.Add(depTip);
            }
            return Dtos;
        }

        // GET: api/Deportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeporteDtos>> GetDeporte(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deporte = await _context.Deportes
                .FirstOrDefaultAsync(m => m.DeporteId == id);
            if (deporte == null)
            {
                return NotFound();
            }
            var Dtos = _mapper.Map<DeporteDtos>(deporte);
            var tipoDeportes = await _context.TipoDeportes.ToListAsync();
            var tipoDeporte = tipoDeportes.Find(x => x.IdTipo == Dtos.IdTipo);
            Dtos.NombreTipo = tipoDeporte.NombreTipo;
            return Dtos;
        }

        // PUT: api/Deportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeporte(int id, DeporteDtos deporteDtos)
        {
            if (id != deporteDtos.DeporteId)
            {
                return BadRequest();
            }
            var deporte = await _context.Deportes.FindAsync(id);
            deporte = _mapper.Map<DeporteDtos, Deporte>(deporteDtos, deporte);
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
        public async Task<ActionResult<Deporte>> PostDeporte(DeporteDtos deporteDtos)
        {
            var deporte = _mapper.Map<Deporte>(deporteDtos);
            _context.Deportes.Add(deporte);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeportes", new { id = deporte.DeporteId }, deporte);
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
