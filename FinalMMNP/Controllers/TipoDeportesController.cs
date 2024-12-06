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
    public class TipoDeportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoDeportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoDeportes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDeportes.ToListAsync());
        }

        // GET: TipoDeportes/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(tipoDeporte);
        }

        // GET: TipoDeportes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDeportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,NombreTipo,Descripcion,Estado")] TipoDeporte tipoDeporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDeporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDeporte);
        }

        // GET: TipoDeportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDeporte = await _context.TipoDeportes.FindAsync(id);
            if (tipoDeporte == null)
            {
                return NotFound();
            }
            return View(tipoDeporte);
        }

        // POST: TipoDeportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,NombreTipo,Descripcion,Estado")] TipoDeporte tipoDeporte)
        {
            if (id != tipoDeporte.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDeporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDeporteExists(tipoDeporte.IdTipo))
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
            return View(tipoDeporte);
        }

        // GET: TipoDeportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(tipoDeporte);
        }

        // POST: TipoDeportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDeporte = await _context.TipoDeportes.FindAsync(id);
            if (tipoDeporte != null)
            {
                _context.TipoDeportes.Remove(tipoDeporte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDeporteExists(int id)
        {
            return _context.TipoDeportes.Any(e => e.IdTipo == id);
        }
    }
}
