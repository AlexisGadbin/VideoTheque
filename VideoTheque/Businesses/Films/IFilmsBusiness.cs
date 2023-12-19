using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        List<FilmDto> GetFilms();

        FilmDto InsertFilm(FilmDto film);
    }
}
