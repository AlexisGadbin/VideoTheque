using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRatings;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Repositories.Supports;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IFilmsRepository _filmDao;
        private readonly IPersonnesRepository _personneDao;
        private readonly IAgeRatingsRepository _ageRatingDao;
        private readonly IGenresRepository _genreDao;
        private readonly ISupportsRepository _supportDao;

        public FilmsBusiness(IFilmsRepository filmDao, IPersonnesRepository personneDao, IAgeRatingsRepository ageRatingDao, IGenresRepository genreDao, ISupportsRepository supportDao)
        {
            _filmDao = filmDao;
            _personneDao = personneDao;
            _ageRatingDao = ageRatingDao;
            _genreDao = genreDao;
            _supportDao = supportDao;
        }

        public List<FilmDto> GetFilms()
        {
            List<FilmDto> films = new List<FilmDto>();
            List<BluRayDto> blurays = _filmDao.GetFilms().Result;

            foreach(BluRayDto bluray in blurays)
            {
                FilmDto film = new FilmDto();
                film.Id = bluray.Id;
                film.Title = bluray.Title;
                film.Duration = bluray.Duration;

                film.FirstActor = _personneDao.GetPersonne(bluray.IdFirstActor).Result;
                film.Director = _personneDao.GetPersonne(bluray.IdDirector).Result;
                film.Scenarist = _personneDao.GetPersonne(bluray.IdScenarist).Result;

                film.AgeRating = _ageRatingDao.GetAgeRating(bluray.IdAgeRating).Result;

                film.Genre = _genreDao.GetGenre(bluray.IdGenre).Result;

                film.Support = _supportDao.GetSupport(1);

                films.Add(film);

            }

            return films;

        }

        public FilmDto GetFilm(int id)
        {
            BluRayDto bluray = _filmDao.GetFilm(id).Result;

            FilmDto film = new FilmDto();
            film.Id = bluray.Id;
            film.Title = bluray.Title;
            film.Duration = bluray.Duration;

            film.FirstActor = _personneDao.GetPersonne(bluray.IdFirstActor).Result;
            film.Director = _personneDao.GetPersonne(bluray.IdDirector).Result;
            film.Scenarist = _personneDao.GetPersonne(bluray.IdScenarist).Result;

            film.AgeRating = _ageRatingDao.GetAgeRating(bluray.IdAgeRating).Result;

            film.Genre = _genreDao.GetGenre(bluray.IdGenre).Result;

            film.Support = _supportDao.GetSupport(1);

            return film;
        }

        public FilmDto InsertFilm(FilmDto film)
        {
            BluRayDto bluRay = new BluRayDto();
            bluRay.Title = film.Title;
            bluRay.Duration = film.Duration;

            bluRay.IdFirstActor = _personneDao.GetPersonne(film.FirstActor.FirstName, film.FirstActor.LastName).Result.Id;
            bluRay.IdDirector = _personneDao.GetPersonne(film.Director.FirstName, film.Director.LastName).Result.Id;
            bluRay.IdScenarist = _personneDao.GetPersonne(film.Scenarist.FirstName, film.Scenarist.LastName).Result.Id;
            bluRay.IdAgeRating = _ageRatingDao.GetAgeRating(film.AgeRating.Name).Result.Id;
            bluRay.IdGenre = _genreDao.GetGenre(film.Genre.Name).Result.Id;

            if (_filmDao.InsertFilm(bluRay).IsFaulted)
            {
                throw new InternalErrorException($"Error while inserting film : {film.Title}");
            }

            return film;
        }

        public void UpdateFilm(int id, FilmDto film)
        {
            BluRayDto bluRay = new BluRayDto();
            bluRay.Title = film.Title;
            bluRay.Duration = film.Duration;

            bluRay.IdFirstActor = _personneDao.GetPersonne(film.FirstActor.FirstName, film.FirstActor.LastName).Result.Id;
            bluRay.IdDirector = _personneDao.GetPersonne(film.Director.FirstName, film.Director.LastName).Result.Id;
            bluRay.IdScenarist = _personneDao.GetPersonne(film.Scenarist.FirstName, film.Scenarist.LastName).Result.Id;
            bluRay.IdAgeRating = _ageRatingDao.GetAgeRating(film.AgeRating.Name).Result.Id;
            bluRay.IdGenre = _genreDao.GetGenre(film.Genre.Name).Result.Id;

            if (_filmDao.UpdateFilm(id, bluRay).IsFaulted)
            {
                throw new InternalErrorException($"Error while updating film : {film.Title}");
            }
        }   

        public void DeleteFilm(int id)
        {
            if (_filmDao.DeleteFilm(id).IsFaulted)
            {
                throw new InternalErrorException($"Error while deleting film with id {id}");
            }
        }
    }
}
