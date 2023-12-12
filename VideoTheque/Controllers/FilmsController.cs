using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmsBusiness _filmsBusiness;
        protected readonly ILogger<FilmsController> _logger;
        private readonly TypeAdapterConfig _config;

        public FilmsController(ILogger<FilmsController> logger, IFilmsBusiness filmsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
            _config = new TypeAdapterConfig();
            _config.ForType<FilmDto, FilmViewModel>()
                .Map(dest => dest.FirstActor, src => $"{src.FirstActor.FirstName} {src.FirstActor.LastName}")
                .Map(dest => dest.Director, src => $"{src.Director.FirstName} {src.Director.LastName}")
                .Map(dest => dest.Scenarist, src => $"{src.Scenarist.FirstName} {src.Scenarist.LastName}")
                .Map(dest => dest.AgeRating, src => $"{src.AgeRating.Name}")
                .Map(dest => dest.Genre, src => $"{src.Genre.Name}")
                .Map(dest => dest.Support, src => $"{src.Support.Name}");
        }

        [HttpGet]
        public List<FilmViewModel> GetFilms()
        {
            var films = _filmsBusiness.GetFilms();
            return films.Adapt<List<FilmViewModel>>(_config);
        }
    }
}
