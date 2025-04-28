using Microsoft.AspNetCore.Mvc;
using PFA_RDV_Medecin.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFA_RDV_Medecin.Controllers
{
    public class RendezVousController : Controller

    {
        private readonly ApplicationDbContext _context;

        public RendezVousController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: RendezVous
        public async Task<IActionResult> Index()
        {
            var rendezVous = _context.RendezVous.Include(r => r.Medecin).Include(r => r.Patient);
            return View(await rendezVous.ToListAsync());
        }
        // GET: RendezVous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var rendezVous = await _context.RendezVous
                .Include(r => r.Medecin)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rendezVous == null)
                return NotFound();

            return View(rendezVous);
        }

        // GET: RendezVous/Create
        public IActionResult Create()
        {
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "Id", "Nom");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Nom");
            return View();
        }

        // POST: RendezVous/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedecinId,PatientId,Date")] RendezVous rendezVous)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rendezVous);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "Id", "Nom", rendezVous.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Nom", rendezVous.PatientId);
            return View(rendezVous);
        }

        // GET: RendezVous/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rendezVous = await _context.RendezVous.FindAsync(id);
            if (rendezVous == null)
                return NotFound();

            ViewData["MedecinId"] = new SelectList(_context.Medecins, "Id", "Nom", rendezVous.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Nom", rendezVous.PatientId);
            return View(rendezVous);
        }
        // POST: RendezVous/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedecinId,PatientId,Date")] RendezVous rendezVous)
        {
            if (id != rendezVous.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rendezVous);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RendezVousExists(rendezVous.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "Id", "Nom", rendezVous.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Nom", rendezVous.PatientId);
            return View(rendezVous);
        }
        // GET: RendezVous/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rendezVous = await _context.RendezVous
                .Include(r => r.Medecin)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rendezVous == null)
                return NotFound();

            return View(rendezVous);
        }
        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rendezVous = await _context.RendezVous.FindAsync(id);
            _context.RendezVous.Remove(rendezVous);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool RendezVousExists(int id)
        {
            return _context.RendezVous.Any(e => e.Id == id);
        }


    }
}
