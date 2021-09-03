using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spicyo.Data;
using Spicyo.Models;

namespace Spicyo.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly SpicyoContext _context;

        public AuthenticateController(SpicyoContext context)
        {
            _context = context;
        }

        // GET: Authenticate
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authenticate.ToListAsync());
        }

        public IActionResult Erorr()
        {
            return View();
        }

        // POST: Authenticate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AdminAuthenticate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAuthenticate([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            HttpContext.Session.Clear();
            
            if ((Authenticate.LoginUser.Equals("Spicyo@admin.com") && Authenticate.Password.Equals("Spicyo@admin.com") ))
            {
                    HttpContext.Session.SetString("LoginUser", Authenticate.LoginUser.ToString());
                    return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                return RedirectToAction(nameof(UserAuthenticate));
            }            
        }
        
        public IActionResult UserAuthenticate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAuthenticate([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            HttpContext.Session.Clear();
            var AuthenticateData = _context.Authenticate.Where(m => m.LoginUser == Authenticate.LoginUser && m.Password == Authenticate.Password).FirstOrDefault();

            if ((Authenticate.LoginUser.Equals("Spicyo@admin.com") && Authenticate.Password.Equals("Spicyo@admin.com") )|| AuthenticateData != null)
            {
                if (AuthenticateData != null)
                {
                    HttpContext.Session.SetString("Id", AuthenticateData.Id.ToString());
                    return RedirectToAction("UserIndex", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("LoginUser", Authenticate.LoginUser.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction(nameof(UserAuthenticate));
            }            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Authenticate);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", Authenticate.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(Authenticate);
        }

        private bool AuthenticateExists(int id)
        {
            return _context.Authenticate.Any(e => e.Id == id);
        }
    }
}
