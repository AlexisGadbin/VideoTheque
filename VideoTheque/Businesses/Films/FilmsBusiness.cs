﻿using VideoTheque.DTOs;
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
    }
}