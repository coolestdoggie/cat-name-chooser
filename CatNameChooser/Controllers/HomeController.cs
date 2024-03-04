using CatNameChooser.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace CatNameChooser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Generate()
        {
            string catName = "";
            HttpResponseMessage response = client.GetAsync("https://tools.estevecastells.com/api/cats/v1").Result;
            if (response.IsSuccessStatusCode)
            {
                catName = await response.Content.ReadAsStringAsync();
                char[] charsToTrim = { '[', ']', '"' };

                catName = Regex.Replace(catName, @"[^0-9a-zA-Z\._ ]", string.Empty);
            }

            TempData["CatName"] = catName;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
