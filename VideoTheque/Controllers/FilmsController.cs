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

        private PersonneDto MapName(string name)
        {
            var splitName = name.Split(" ");
            PersonneDto personne = new PersonneDto();
            personne.FirstName = splitName[0];
            personne.LastName = splitName.Length > 1 ? splitName[1] : string.Empty;
            return personne;
        }

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

            _config.ForType<FilmViewModel, FilmDto>()
                    .Map(dest => dest.FirstActor, src => MapName(src.FirstActor))
                .Map(dest => dest.Director, src => MapName(src.Director))
                .Map(dest => dest.Scenarist, src => MapName(src.Scenarist))
                .Map(dest => dest.AgeRating, src => new AgeRatingDto { Name = src.AgeRating })
                .Map(dest => dest.Genre, src => new GenreDto { Name = src.Genre })
                .Map(dest => dest.Support, src => new SupportDto { Name = src.Support });
        }

        [HttpGet]
        public List<FilmViewModel> GetFilms()
        {
            var films = _filmsBusiness.GetFilms();
            return films.Adapt<List<FilmViewModel>>(_config);
        }

        [HttpPost]
        public async Task<IResult> InsertFilm([FromBody] FilmViewModel film)
        {
            var created = _filmsBusiness.InsertFilm(film.Adapt<FilmDto>(_config));

            return Results.Created($"/hosts/{created.Id}", created.Adapt<FilmViewModel>(_config));
        }
    }
}
