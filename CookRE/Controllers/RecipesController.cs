using CookRE.Data;
using CookRE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookRE.Controllers
{

    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search)
        {
            var recipes = _context.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                recipes = recipes.Where(r =>
                    r.Name.Contains(search) ||
                    r.Overview.Contains(search) ||
                    r.Ingredients.Contains(search)
                );
            }

            return View(recipes.ToList());
        }

        public IActionResult Details(int id)
        {
            var recipe = _context.Recipes
                .FirstOrDefault(x => x.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Recipe recipe)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            recipe.UserId = userId;



            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            return View(recipe);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Recipe recipe)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingRecipe = _context.Recipes.Find(recipe.Id);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            if (existingRecipe.UserId != userId)
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            existingRecipe.Name = recipe.Name;
            existingRecipe.Overview = recipe.Overview;
            existingRecipe.Ingredients = recipe.Ingredients;
            existingRecipe.Steps = recipe.Steps;
            existingRecipe.ImageUrl = recipe.ImageUrl;

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = recipe.Id });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToAction("Index");   

        }

        [Authorize]

        public IActionResult MyRecipes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = _context.Recipes
                .Where(r => r.UserId == userId)
                .ToList();

            return View(recipes);
        }
    }
}
