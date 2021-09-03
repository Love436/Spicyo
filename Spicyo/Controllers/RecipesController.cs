using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spicyo.Data;
using Spicyo.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Spicyo.Controllers
{
        public class RecipesController : Controller
        {
            private readonly SpicyoContext _context;

            public RecipesController(SpicyoContext context)
            {
                _context = context;
            }

            // GET: Recipes
            public async Task<IActionResult> Index()
            {
            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("LoginUser") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("LoginUser");
                var Authenticate = _context.Authenticate.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                if (name == "Spicyo@admin.com")
                {
                    return View(await _context.Recipes.ToListAsync());
                    }
                    else
                    {
                    if (_context.Recipes.ToList().Count == 0)
                    {
                        return RedirectToAction(nameof(NoRecpies));
                    }
                    else
                    return RedirectToAction("Index", "Recipe");
                    }
            }
            else
                return RedirectToAction("UserAuthenticate", "Authenticate");
            }

            // GET: Recipes/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Recipes = await _context.Recipes
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Recipes == null)
                {
                    return NotFound();
                }

                return View(Recipes);
            }
        public IActionResult NoRecpies()
        {
            return View();
        }
        
        // GET: Recipes/Create
        public IActionResult Create()
            {
            return View();
            }

            // POST: Recipes/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Recipe,RecipeType,Price")] Recipes Recipes)
            {
                if (ModelState.IsValid)
                {
                    //Recipes.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(Recipes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Recipes);
            }

            // GET: Recipes/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Recipes = await _context.Recipes.FindAsync(id);
                if (Recipes == null)
                {
                    return NotFound();
                }
                return View(Recipes);
            }

            // POST: Recipes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Recipe,RecipeType,Price")] Recipes Recipes)
            {
                if (id != Recipes.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Recipes);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RecipesExists(Recipes.Id))
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
                return View(Recipes);
            }

            // GET: Recipes/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Recipes = await _context.Recipes
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Recipes == null)
                {
                    return NotFound();
                }

                return View(Recipes);
            }

            // POST: Recipes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Recipes = await _context.Recipes.FindAsync(id);
                _context.Recipes.Remove(Recipes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool RecipesExists(int id)
            {
                return _context.Recipes.Any(e => e.Id == id);
            }
        }
    }
