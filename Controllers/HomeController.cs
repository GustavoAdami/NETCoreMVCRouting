using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Routing.Models;

namespace Routing.Controllers
{
    /*
     * Using conventional routing, the pattern is defined in Program.cs.
     * It defaults to {controller}/{action}/{id?} -> "/Home/Index/{id?}"
     */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // A simple in-memory “database” of recipes
        private static readonly List<Recipe> _recipes = new()
        {
            new Recipe { Id = 1, Title = "Spaghetti Carbonara", Instructions = "Boil pasta. Fry pancetta. Mix eggs & cheese. Toss together." },
            new Recipe { Id = 2, Title = "Tomato Basil Soup", Instructions = "Saute onions & garlic. Add tomatoes. Simmer. Add basil. Blend." },
            new Recipe { Id = 3, Title = "Chocolate Chip Cookies", Instructions = "Cream butter & sugar. Add eggs & flour. Stir in chips. Bake." }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Route: "/Home/Index" or just "/" - Accepts all HTTP methods (GET, POST, etc.)
        public IActionResult Index()
        {
            return View();
        }

        // Route: "/Home/Recipes" - Accepts all HTTP methods (GET, POST, etc.)
        public IActionResult Recipes()
        {
            // Pass the list of recipes to the view
            return View(_recipes);
        }

        // Route: "/Home/Details/{id}" - Accepts all HTTP methods (GET, POST, etc.)
        public IActionResult Details(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // Route: "/Home/Privacy" - Accepts all HTTP methods (GET, POST, etc.)
        public IActionResult Privacy()
        {
            return View();
        }

        // Route: "/Home/Error" - Accepts all HTTP methods (GET, POST, etc.)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
