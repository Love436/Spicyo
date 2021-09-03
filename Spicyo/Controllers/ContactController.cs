using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spicyo.Data;
using Spicyo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spicyo.Controllers
{
    namespace Spicyo.Controllers
    {
        public class ContactController : Controller
        {
            private readonly SpicyoContext _context;

            public ContactController(SpicyoContext context)
            {
                _context = context;
            }

            // GET: Contact
            public async Task<IActionResult> Index()
            {                
                
                        return View(await _context.Contact.ToListAsync());                    
            }

            // GET: Contact/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Contact = await _context.Contact
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Contact == null)
                {
                    return NotFound();
                }

                return View(Contact);
            }

            public IActionResult Display()
            {
                return View();
            }

            // GET: Contact/Create
            public IActionResult Create()
            {
                return View();
            }
            
            // POST: Contact/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Email,Mobile,Address,Message")] Contact Contact)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Display));
                }
                return View(Contact);
            }

            // GET: Contact/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Contact = await _context.Contact.FindAsync(id);
                if (Contact == null)
                {
                    return NotFound();
                }
                return View(Contact);
            }

            // POST: Contact/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Mobile,Address,Message")] Contact Contact)
            {
                if (id != Contact.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Contact);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ContactExists(Contact.Id))
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
                return View(Contact);
            }

            // GET: Contact/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Contact = await _context.Contact
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Contact == null)
                {
                    return NotFound();
                }

                return View(Contact);
            }

            // POST: Contact/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Contact = await _context.Contact.FindAsync(id);
                _context.Contact.Remove(Contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ContactExists(int id)
            {
                return _context.Contact.Any(e => e.Id == id);
            }
        }
    }
}
