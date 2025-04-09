using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnnuaireTelephone.Data;
using AnnuaireTelephone.Models;

namespace AnnuaireTelephone.Controllers
{
    public class TelephoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TelephoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Telephone tel)
        {
            if (ModelState.IsValid && tel.Numero.Length == 9)
            {
                _context.Telephones.Add(tel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom", tel.ClientId);
            return View(tel);
        }

        public IActionResult Index()
        {
            var telephones = _context.Telephones.Include(t => t.Client).ToList();
            return View(telephones);
        }

        public IActionResult FilterByClient(int clientId)
        {
            var telephones = _context.Telephones
                .Where(t => t.ClientId == clientId)
                .Include(t => t.Client)
                .ToList();

            return View("Index", telephones);
        }

        public IActionResult FilterByOperateur(string operateur)
        {
            var telephones = _context.Telephones
                .Where(t => t.Operateur == operateur)
                .Include(t => t.Client)
                .ToList();

            return View("Index", telephones);
        }
    }
}
public IActionResult Statistiques()
{
    var clientPlusNumeros = _context.Clients
        .Select(c => new
        {
            Client = c,
            Count = c.Telephones.Count
        })
        .OrderByDescending(x => x.Count)
        .FirstOrDefault();

    var operateurPlusNombre = _context.Telephones
        .GroupBy(t => t.Operateur)
        .Select(g => new
        {
            Operateur = g.Key,
            Count = g.Count()
        })
        .OrderByDescending(x => x.Count)
        .FirstOrDefault();

    ViewBag.Client = clientPlusNumeros?.Client;
    ViewBag.Operateur = operateurPlusNombre?.Operateur;

    return View();
}
public IActionResult Filtrer(int? clientId, string? operateur)
{
    var query = _context.Telephones.Include(t => t.Client).AsQueryable();

    if (clientId.HasValue)
        query = query.Where(t => t.ClientId == clientId.Value);

    if (!string.IsNullOrWhiteSpace(operateur))
        query = query.Where(t => t.Operateur == operateur);

    ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");
    return View(query.ToList());
}
