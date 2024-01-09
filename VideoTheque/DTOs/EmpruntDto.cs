using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.ViewModels;

namespace VideoTheque.DTOs
{
    public class EmpruntDto
    {
        public String Title { get; set; }
        public long Duration { get; set; }
        public PersonneDto FirstActor { get; set; }
        public PersonneDto Director { get; set; }
        public PersonneDto Scenarist { get; set; }
        public AgeRatingDto AgeRating { get; set; }
        public GenreDto Genre { get; set; }
        public String Support { get; set; }
    }
}
