using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalData.Data;
using FinalData.Data.Entitys;
using FinalMMNP.WebApiClients;
using FinalMMNP.ViewModels;

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

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var wrapper = webApiClient.GetDeportes<List<DeporteViewModel>>();
            for (int i = 0; i < wrapper.Data.Count; i++)
            {
                var tipoDeporte = webApiClient.GetTipoDeporteById<TipoDeporteViewModel>(wrapper.Data[i].IdTipo);
                wrapper.Data[i].NombreTipo = tipoDeporte.Data.NombreTipo;
            }
            return View(wrapper.Data);
        }

        // GET: Deportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var wrapper = webApiClient.GetDeporteById<DeporteViewModel>(id.Value);
                if (wrapper.Data != null)
                {
                    var categoria = webApiClient.GetTipoDeporteById<TipoDeporteViewModel>(wrapper.Data.IdTipo);
                    wrapper.Data.NombreTipo = categoria.Data.NombreTipo;
                    return View(wrapper.Data);
                }
            }
            return NotFound();
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
        public async Task<IActionResult> Create([Bind("DeporteId,Nombre,Descripcion,CantJugadores,FechaCracion,Popularidad,IdTipo")] DeporteViewModel deporte)
        {
            if (ModelState.IsValid)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var wrapper = webApiClient.CrearDeporte<DeporteViewModel>(deporte);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", deporte.IdTipo);
            return View(deporte);
        }

        // GET: Deportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var wrapper = webApiClient.GetDeporteById<DeporteViewModel>(id.Value);
                if (wrapper.Data != null)
                {
                    ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", wrapper.Data.IdTipo);
                    return View(wrapper.Data);
                }
            }
            return NotFound();
        }

        // POST: Deportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeporteId,Nombre,Descripcion,CantJugadores,FechaCracion,Popularidad,IdTipo")] DeporteViewModel deporte)
        {
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            webApiClient.UpdateDeporteById<DeporteViewModel, DeporteViewModel>(id, deporte);
            ViewData["IdTipo"] = new SelectList(_context.TipoDeportes, "IdTipo", "Descripcion", deporte.IdTipo);
            return View(deporte);
        }

        // GET: Deportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var wrapper = webApiClient.GetDeporteById<DeporteViewModel>(id.Value);
                if (wrapper.Data != null)
                {
                    var categoria = webApiClient.GetTipoDeporteById<TipoDeporteViewModel>(wrapper.Data.IdTipo);
                    wrapper.Data.NombreTipo = categoria.Data.NombreTipo;
                    return View(wrapper.Data);
                }
            }
            return NotFound();
        }

        // POST: Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var wrapper = webApiClient.DeleteDeporteyId<DeporteViewModel>(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DeporteExists(int id)
        {
            return _context.Deportes.Any(e => e.DeporteId == id);
        }
    }
}
