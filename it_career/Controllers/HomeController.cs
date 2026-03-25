using Humanizer;
using it_career.data.models;
using it_career.infrastructure.Interface;
using it_career.models;
using it_career.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace it_career.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IKinoRepository _kinoRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        
        public IActionResult Index()
        {
            //List<KinoDto> kinos = _kinoRepository.GetAll<KinoDto>().ToList();
            List<KinoDto> kinos = new List<KinoDto>();
                KinoDto kino1 = new KinoDto() { Name = "Kino1", Location = "Location1", Capacity = 100 };
                KinoDto kino2 = new KinoDto() { Name = "Kino2", Location = "Location2", Capacity = 150 };
                kinos.Add(kino1);
                kinos.Add(kino2);
            return View(kinos);
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
        public IActionResult SaveKino(KinoDto kinoDto)
        {
            var kino = new Kino
            {
                Name = kinoDto.Name,
                Location=kinoDto.Location,
                Capacity = kinoDto.Capacity
            };

            //_kinoRepository.Kinos.Add(kino);
            _kinoRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
