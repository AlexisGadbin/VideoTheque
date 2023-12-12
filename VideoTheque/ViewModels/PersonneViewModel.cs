using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class PersonneViewModel
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        [Required]
        public string LastName { get; set; }

        [JsonPropertyName("prenom")]
        [Required]
        public string FirstName { get; set; }

        [JsonPropertyName("date-naissance")]
        [Required]
        public DateTime BirthDay { get; set; }

        [JsonPropertyName("nationalite")]
        [Required]
        public string Nationality { get; set; }

        [JsonPropertyName("nom-prenom")]
        [Required]
        public string FullName { get; set; }

        public PersonneDto ToDto()
        {
            return new PersonneDto
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                Nationality = this.Nationality,
                BirthDay = this.BirthDay
            };
        }

        public static PersonneViewModel ToModel(PersonneDto dto)
        {
            return new PersonneViewModel
            {
                Id = dto.Id,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Nationality = dto.Nationality,
                BirthDay = dto.BirthDay,
                FullName = dto.FirstName + " " + dto.LastName
            };
        }

    }
}
