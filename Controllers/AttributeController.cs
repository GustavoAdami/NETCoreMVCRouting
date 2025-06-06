using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Routing.Models;

namespace Routing.Controllers
{
    /* Duplicate of HomeController.cs but using attributes for routing.
     * To use this controller, you need to enable attribute routing in your Program.cs file using app.MapControllers()
     * and ensure that the tag helpers at the views and the views folder are set up correctly.
     */

    [Route("attribute")]
    /*
     * When using attribute routing at the controller level, this route prefix applies to all action methods in this controller.
     * You need to decorate each action method with its own route attributes to specify the exact URL patterns they respond to.
     */
    public class AttributeController : Controller
    {
        private readonly ILogger<AttributeController> _logger;

        // A simple in-memory “database” of recipes
        private static readonly List<Recipe> _recipes = new()
        {
            new Recipe { Id = 1, Title = "Spaghetti Carbonara", Instructions = "Boil pasta. Fry pancetta. Mix eggs & cheese. Toss together." },
            new Recipe { Id = 2, Title = "Tomato Basil Soup", Instructions = "Saute onions & garlic. Add tomatoes. Simmer. Add basil. Blend." },
            new Recipe { Id = 3, Title = "Chocolate Chip Cookies", Instructions = "Cream butter & sugar. Add eggs & flour. Stir in chips. Bake." }
        };

        public AttributeController(ILogger<AttributeController> logger)
        {
            _logger = logger;
        }

        
        [Route("/index")] // Route: "/index/" - Accepts all HTTP methods (GET, POST, etc.)
        [HttpGet] // restrict to GET requests only - can be written as [HttpGet("/")] to be concise
        public IActionResult Index()
        {
            return View();
        }

        [Route("all-recipes")] // Route: "/attribute/all-recipes" - Accepts all HTTP methods (GET, POST, etc.) - if you start the route with a slash ("/"),
                               // it will be treated as an absolute path and override the controller's route prefix - see the previous and next action for examples
        [HttpGet] // restrict to GET requests only - can be written as [HttpGet("all-recipes")] to be concise
        public IActionResult Recipes()
        {
            // Pass the list of recipes to the view
            return View(_recipes);
        }

        [Route("/recipe-details/{id:int}")] // Route: "/recipe-details/{id:int}" - Accepts all HTTP methods (GET, POST, etc.) and requires id to be an integer
        [HttpGet] // restrict to GET requests only - can be written as [HttpGet("/recipe-details/{id:int}")] to be concise
        public IActionResult Details(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [Route("privacy-policy")] // Route: "/attribute/privacy-policy" - Accepts all HTTP methods (GET, POST, etc.)
        [HttpGet] // restrict to GET requests only - can be written as [HttpGet("privacy-policy")] to be concise
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error-page")] // Route: "/attribute/error-page" - Accepts all HTTP methods (GET, POST, etc.)
        [HttpGet] // restrict to GET requests only - can be written as [HttpGet("error")] to be concise
        public IActionResult Error()
        {
            //Response.StatusCode = 500; // Set the HTTP response code
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
