using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalData.Data;
using FinalData.Data.Entitys;

namespace FinalMMNP.Controllers
{
    public class DeportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deportes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Deportes.Include(d => d.TipoDeporte);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Deportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deportes
                .Include(d => d.TipoDeporte)
                .FirstOrDefaultAsync(m => m.DeporteId == id);
            if (deporte == null)
            {
                return NotFound();
            }

            return View(deporte);
        }

        // GET: Deportes/Create
        public IActionResult Create()
        {
            ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion");
            return View();
        }

        // POST: Deportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeporteId,Nombre,Descripcion,CantJugadores,FechaCracion,Popularidad,IdTipo")] Deporte deporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", deporte.IdTipo);
            return View(deporte);
        }

        // GET: Deportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte == null)
            {
                return NotFound();
            }
            ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", deporte.IdTipo);
            return View(deporte);
        }

        // POST: Deportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeporteId,Nombre,Descripcion,CantJugadores,FechaCracion,Popularidad,IdTipo")] Deporte deporte)
        {
            if (id != deporte.DeporteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeporteExists(deporte.DeporteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", deporte.IdTipo);
            return View(deporte);
        }

        // GET: Deportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deportes
                .Include(d => d.TipoDeporte)
                .FirstOrDefaultAsync(m => m.DeporteId == id);
            if (deporte == null)
            {
                return NotFound();
            }

            return View(deporte);
        }

        // POST: Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deporte = await _context.Deportes.FindAsync(id);
            if (deporte != null)
            {
                _context.Deportes.Remove(deporte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeporteExists(int id)
        {
            return _context.Deportes.Any(e => e.DeporteId == id);
        }
    }
}
