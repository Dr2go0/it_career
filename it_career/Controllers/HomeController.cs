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
using System.Security.Claims;

namespace it_career.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IKinoRepository _kinoRepository;
        private readonly IFilmScheduleRepository _filmScheduleRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IBookedFilmRepository _bookedFilmRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IKinoRepository kinoRepository, IFilmScheduleRepository filmScheduleRepository, IFilmRepository filmRepository, IBookedFilmRepository bookedFilmRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _kinoRepository = kinoRepository;
            _filmScheduleRepository = filmScheduleRepository;
            _filmRepository = filmRepository;
            _bookedFilmRepository = bookedFilmRepository;
        }


        public IActionResult Index()
        {
            List<KinoDto> kinos = _kinoRepository.GetAll<Kino>().Select(x=>x.ToDto()).ToList();
            var ViewModel = new HomeIndexViewModel
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Kinos = kinos
            };
            return View( ViewModel);
        }
        public IActionResult KinoSchedule(Guid kinoId)
        {
            List<FilmScheduleDto> Schedules = _filmScheduleRepository
                .FindWithIncludes<FilmSchedule>(
                    x => x.KinoId == kinoId,
                    x => x.Film
                )
                .Select(x => x.ToDto())
                .ToList();

            List<FilmDto> allFilms = _filmRepository.GetAll<Film>().Select(x => x.ToDto()).ToList();

            var ViewModel = new KinoScheduleViewModel
            {
                FilmSchedules = Schedules,
                Films = allFilms,
                Kino = _kinoRepository.GetById<Kino>(kinoId).ToDto(),
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ManagerId = _kinoRepository.GetById<Kino>(kinoId).ManagerId
            };
            return View(ViewModel);
        }

        public IActionResult Booked(Guid filmScheduleId = default)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepository.GetById<AppUser>(userIdString).ToDto();

            if (filmScheduleId != Guid.Empty)
            {
                var filmSchedule = _filmScheduleRepository.GetById<FilmSchedule>(filmScheduleId).ToDto();
                var BookedFilm = new BookedFilmDto
                {
                    FilmScheduleId = filmSchedule.Id,
                    AppUserId = user.Id
                };
                _bookedFilmRepository.Add(BookedFilm.ToEntity());
                _bookedFilmRepository.Save();
            }

            var UsersBookedFilms = _bookedFilmRepository
                .FindWithIncludes<BookedFilm>(
                    x => x.UserId == userIdString,
                    x => x.FilmSchedule
                )
                .ToList();

            List<BookedFilmsViewModel> BookedFilmsViewModel = new List<BookedFilmsViewModel>();
            foreach (var item in UsersBookedFilms)
            {
                BookedFilmsViewModel bookedFilm = new BookedFilmsViewModel
                {
                    FilmName = _filmRepository.GetById<Film>(item.FilmSchedule.FilmId).Name,
                    ProjectionDate = item.FilmSchedule.ProjectionDate
                };
                BookedFilmsViewModel.Add(bookedFilm);
            }

            ViewBag.UserEmail = user.Email;
            return View(BookedFilmsViewModel);
        }
        public IActionResult SaveBooked()
        {
            return RedirectToAction("Booked");
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
            kinoDto.ManagerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _kinoRepository.Add(kinoDto.ToEntity());
            _kinoRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SaveSchedule(FilmScheduleDto filmScheduleDto)
        {
            _filmScheduleRepository.Add(filmScheduleDto.ToEntity());
            _filmScheduleRepository.Save();
            return RedirectToAction("KinoSchedule", new { kinoId = filmScheduleDto.KinoId });
        }// ── KINO ──────────────────────────────────────────────
        [HttpPost]
        public IActionResult EditKino(KinoDto kinoDto)
        {
            var existing = _kinoRepository.GetById<Kino>(kinoDto.ToEntity().Id);
            if (existing == null) return NotFound();

            existing.Name = kinoDto.Name;
            existing.Location = kinoDto.Location;
            existing.Capacity = kinoDto.Capacity;

            
            _kinoRepository.Save();
            return RedirectToAction("Index");
        }

        // HomeController - delete schedules before deleting the kino
        [HttpPost]
        public IActionResult DeleteKino(Guid id)
        {
            var schedules = _filmScheduleRepository
                .Find<FilmSchedule>(x => x.KinoId == id)
                .ToList();

            foreach (var schedule in schedules)
            {
                _filmScheduleRepository.Delete(schedule); // cascade removes bookings
            }
            _filmScheduleRepository.Save();

            var kino = _kinoRepository.GetById<Kino>(id);
            if (kino == null) return NotFound();

            _kinoRepository.Delete(kino);
            _kinoRepository.Save();
            return RedirectToAction("Index");
        }

        // ── SCHEDULE ──────────────────────────────────────────
        [HttpPost]
        public IActionResult EditSchedule(FilmScheduleDto filmScheduleDto)
        {
            var existing = _filmScheduleRepository.GetById<FilmSchedule>(filmScheduleDto.ToEntity().Id);
            if (existing == null) return NotFound();

            existing.FilmId = filmScheduleDto.FilmId;
            existing.ProjectionDate = filmScheduleDto.ProjectionDate;

            
            _filmScheduleRepository.Save();
            return RedirectToAction("KinoSchedule", new { kinoId = filmScheduleDto.KinoId });
        }

        [HttpPost]
        public IActionResult DeleteSchedule(Guid id, Guid kinoId)
        {
            var existing = _filmScheduleRepository.GetById<FilmSchedule>(id);
            if (existing == null) return NotFound();

            _filmScheduleRepository.Delete(existing);
            _filmScheduleRepository.Save();
            return RedirectToAction("KinoSchedule", new { kinoId });
        }
    }
}
