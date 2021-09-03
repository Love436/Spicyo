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
    public class RecipeController : Controller
    {
        private readonly SpicyoContext _context;

        public RecipeController(SpicyoContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
          return View(await _context.Recipes.ToListAsync());
        }
        public async Task<IActionResult> AddItem(int? id)
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                OrderedRecipes buyBook = new OrderedRecipes();
                var book = _context.Recipes.Where(x => x.Id == id).FirstOrDefault();
                buyBook.Recipe = book.Recipe;
                buyBook.RecipeType = book.RecipeType;
                buyBook.Price = book.Price;
                buyBook.UserId = Convert.ToInt32(value);
                _context.Add(buyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "OrderedRecipes");
            }
            else
                return RedirectToAction("UserAuthenticate", "Authenticate");
        }

    }
}
