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

            _config.ForType<PersonneDto, PersonneViewModel>()
                        .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }

        [HttpGet]
        public List<FilmViewModel> GetFilms()
        {
            var films = _filmsBusiness.GetFilms();
            return films.Adapt<List<FilmViewModel>>(_config);
        }

        [HttpGet("{id}")]
        public FilmViewModel GetFilm([FromRoute] int id)
        {
            var film = _filmsBusiness.GetFilm(id);
            return film.Adapt<FilmViewModel>(_config);
        }

        [HttpPost]
        public async Task<IResult> InsertFilm([FromBody] FilmViewModel film)
        {
            var created = _filmsBusiness.InsertFilm(film.Adapt<FilmDto>(_config));

            return Results.Created($"/hosts/{created.Id}", created.Adapt<FilmViewModel>(_config));
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateFilm([FromRoute] int id, [FromBody] FilmViewModel film)
        {
            _filmsBusiness.UpdateFilm(id, film.Adapt<FilmDto>(_config));
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteFilm([FromRoute] int id)
        {
            _filmsBusiness.DeleteFilm(id);
            return Results.Ok();
        }

        [HttpGet("empruntables")]
        public List<EmpruntableViewModel> GetFilmsEmpruntables()
        {
            var films = _filmsBusiness.GetFilmsEmpruntables();

            return films.Adapt<List<EmpruntableViewModel>>(_config);
        }

        [HttpPost("empruntables/{id}")]
        public async Task<EmpruntViewModel> EmpruntFilm([FromRoute]  int id)
        {
            var emprunt = _filmsBusiness.EmpruntFilm(id);

            return emprunt.Adapt<EmpruntViewModel>(_config);
        }

        [HttpDelete("empruntables/{titre}")]
        public async Task<IResult> RetourFilm([FromRoute] string titre)
        {
            _filmsBusiness.RetourFilm(titre);

            return Results.Ok();
        }
    }
}
