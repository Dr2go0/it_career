using it_career.data.models;
using it_career.infrastructure.Extensions;
using it_career.infrastructure.Interface;
using it_career.infrastructure.Repository;
using it_career.models;
using Microsoft.AspNetCore.Mvc;

public class FilmsController : Controller
{
    private readonly ILogger<FilmsController> _logger;
    private readonly IFilmRepository _filmsRepository;
    private readonly IFilmScheduleRepository _filmScheduleRepository;

    public FilmsController(ILogger<FilmsController> logger, IFilmRepository filmsRepository, IFilmScheduleRepository filmScheduleRepository)
    {
        _logger = logger;
        _filmsRepository = filmsRepository;
        _filmScheduleRepository = filmScheduleRepository;
    }

    public ActionResult Index()
    {
        List<FilmDto> films = _filmsRepository.GetAll<Film>().Select(x => x.ToDto()).ToList();
        return View(films);
    }

    [HttpPost]
    public ActionResult Save(FilmDto filmDto)
    {
        _filmsRepository.Add(filmDto.ToEntity());
        _filmsRepository.Save();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Edit(FilmDto filmDto)
    {
        var existing = _filmsRepository.GetById<Film>(filmDto.ToEntity().Id);
        if (existing == null) return NotFound();

        existing.Name = filmDto.Name;
        existing.Genre = filmDto.Genre;
        existing.Duration = filmDto.Duration;
        existing.ReleaseDate = filmDto.ReleaseDate;

        
        _filmsRepository.Save();
        return RedirectToAction("Index");
    }

    
[HttpPost]
    public ActionResult Delete(Guid id)
    {
        var schedules = _filmScheduleRepository
            .Find<FilmSchedule>(x => x.FilmId == id)
            .ToList();

        foreach (var schedule in schedules)
        {
            _filmScheduleRepository.Delete(schedule); // cascade removes bookings
        }
        _filmScheduleRepository.Save();

        var film = _filmsRepository.GetById<Film>(id);
        if (film == null) return NotFound();

        _filmsRepository.Delete(film);
        _filmsRepository.Save();
        return RedirectToAction("Index");
    }
}