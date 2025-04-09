using Microsoft.AspNetCore.Mvc;
using AnnuaireTelephone.Data;
using AnnuaireTelephone.Models;

namespace AnnuaireTelephone.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client
        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }
    }
}
