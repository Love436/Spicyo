using Spicyo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spicyo.Models;

namespace Spicyo.Controllers
{
    public class OrderedRecipesController : Controller
    {
        private readonly SpicyoContext _context;

        public OrderedRecipesController(SpicyoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                return View(await _context.OrderedRecipes.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
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

            var Recipes = await _context.OrderedRecipes
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
        public async Task<IActionResult> Create([Bind("Id,Recipe,RecipeType,Price")] OrderedRecipes Recipes)
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

            var Recipes = await _context.OrderedRecipes.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recipe,RecipeType,Price")] OrderedRecipes Recipes)
        {
            if (id != Recipes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = new OrderedRecipes();
                    item.Recipe = Recipes.Recipe;
                    item.RecipeType = Recipes.RecipeType;
                    item.Price = Recipes.Price;
                    _context.OrderedRecipes.Update(item);
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

            var Recipes = await _context.OrderedRecipes
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
            var Recipes = await _context.OrderedRecipes.FindAsync(id);
            _context.OrderedRecipes.Remove(Recipes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipesExists(int id)
        {
            return _context.OrderedRecipes.Any(e => e.Id == id);
        }
    }
}
