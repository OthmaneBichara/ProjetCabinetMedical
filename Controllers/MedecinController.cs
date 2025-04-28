using Microsoft.AspNetCore.Mvc;
using PFA_RDV_Medecin.Models;


namespace PFA_RDV_Medecin.Controllers
{
    public class MedecinController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedecinController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {  var medecins= _context.Medecins.ToList();
            return View(medecins);
        }
        public IActionResult Create()
        {
            return View();
        }
        // POST: Medecin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                _context.Medecins.Add(medecin);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(medecin);
        }
        // GET: Medecin/Edit/{id}
        public IActionResult Edit(int id)
        {
            var medecin = _context.Medecins.Find(id);
            if (medecin == null)
            {
                return NotFound();
            }
            return View(medecin);
        }
        // POST: Medecin/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Medecin medecin)
        {
            if (id != medecin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(medecin);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(medecin);
        }
        // GET: Medecin/Delete/{id}
        public IActionResult Delete(int id)
        {
            var medecin = _context.Medecins.Find(id);
            if (medecin == null)
            {
                return NotFound();
            }

            return View(medecin);
        }
        // POST: Medecin/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var medecin = _context.Medecins.Find(id);
            if (medecin != null)
            {
                _context.Medecins.Remove(medecin);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMedecin(Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                _context.Medecins.Add(medecin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(medecin);
        }

    }
}
