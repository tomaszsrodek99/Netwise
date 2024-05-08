using Microsoft.AspNetCore.Mvc;
using Netwise.Interfaces;
using Netwise.Models;
using System.Text;
using System.Text.Json;

namespace Netwise.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;

        public HomeController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _fileService.ReadFile();

                if (model == null)
                {
                    return View(new List<CatFact>());
                }
                else
                {
                    return View(model);
                }
            }          
            catch (Exception e) {
                TempData["ErrorMessage"] = $"B³¹d: {e.Message}";
                return View(new List<CatFact>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNewData()
        {
            try
            {
                await _fileService.WriteToFile();
                TempData["SuccessMessage"] = "Poprawnie zapisano dane w pliku tekstowym!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"B³¹d: {e.Message}";
                return RedirectToAction("Index");
            }
        }    
    }
}
