using it_career.data.models;
using it_career.infrastructure.Extensions;
using it_career.infrastructure.Interface;
using it_career.infrastructure.Mappings;
using it_career.infrastructure.Repository;
using it_career.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace it_career.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ILogger<FilmsController> _logger;
        private readonly IFilmRepository  _filmsRepository;

        public FilmsController(ILogger<FilmsController> logger, IFilmRepository filmsRepository)
        {
            _logger = logger;
            _filmsRepository = filmsRepository;
        }

        public ActionResult Index()
        {
            List<FilmDto> films = _filmsRepository.GetAll<Film>().Select(x=>x.ToDto()).ToList();
            return View(films);
        }

        [HttpPost]
        public ActionResult Save(FilmDto filmDto)
        {
            _filmsRepository.Add(filmDto.ToEntity());
            _filmsRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
