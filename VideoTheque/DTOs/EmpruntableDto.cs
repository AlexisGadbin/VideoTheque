using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VideoTheque.DTOs
{
    public class EmpruntableDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
