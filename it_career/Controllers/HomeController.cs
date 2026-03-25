using System.Diagnostics;
using it_career.infrastructure.Interface;
using it_career.models;
using it_career.Models;
using Microsoft.AspNetCore.Mvc;

namespace it_career.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        
        public IActionResult Index()
        {
            return View();
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
        [HttpPost]
        public IActionResult SaveKino(KinoDto kino)
        {
            
            return View();
        }
    }
}
