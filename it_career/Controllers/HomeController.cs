using Humanizer;
using it_career.data.models;
using it_career.infrastructure.Extensions;
using it_career.infrastructure.Interface;
using it_career.infrastructure.Mappings;
using it_career.infrastructure.Repository;
using it_career.models;
using it_career.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using System.Diagnostics;

namespace it_career.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IKinoRepository _kinoRepository;
        private readonly IFilmScheduleRepository _filmScheduleRepository;
        private readonly IFilmRepository _filmRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IKinoRepository kinoRepository, IFilmScheduleRepository filmScheduleRepository, IFilmRepository filmRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _kinoRepository = kinoRepository;
            _filmScheduleRepository = filmScheduleRepository;
            _filmRepository = filmRepository;
        }


        public IActionResult Index()
        {
            List<KinoDto> kinos = _kinoRepository.GetAll<Kino>().Select(x=>x.ToDto()).ToList();
            return View(kinos);
        }
        public IActionResult KinoSchedule(Guid kinoId)
        {
            List<FilmScheduleDto> Schedules = _filmScheduleRepository.Find<FilmSchedule>(x=>x.KinoId == kinoId).Select(x=>x.ToDto()).ToList();

            List<FilmDto> allFilms= _filmRepository.GetAll<Film>().Select(x=>x.ToDto()).ToList();

            var ViewModel = new KinoScheduleViewModel
            {
                FilmSchedules = Schedules,
                Films = allFilms,
                Kino = _kinoRepository.GetById<Kino>(kinoId).ToDto()
            };
            return View(ViewModel);
        }

        public IActionResult Privacy()
        {
            // Workflow for sending info using repository and mapping:
            // UserDto user = new UserDto();
            // var result = _userRepository.GetById<AppUser>(user.ToEntity().Id) ;

            // Workflow for retrieving info using repository and mapping:
            // result.ToDto();


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
            _kinoRepository.Add(kinoDto.ToEntity());
            _kinoRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SaveSchedule(FilmScheduleDto filmScheduleDto)
        {
            _filmScheduleRepository.Add(filmScheduleDto.ToEntity());
            _filmScheduleRepository.Save();
            return RedirectToAction("KinoSchedule?kinoId=" + filmScheduleDto.KinoId);
        }
    }
}
