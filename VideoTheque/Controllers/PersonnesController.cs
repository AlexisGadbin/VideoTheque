using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Personnes;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("personnes")]
    public class PersonnesController
    {
        private readonly IPersonnesBusiness _personnesBusiness;
        protected readonly ILogger<PersonnesController> _logger;
        private readonly TypeAdapterConfig _config;

        public PersonnesController(ILogger<PersonnesController> logger, IPersonnesBusiness personnesBusiness)
        {
            _logger = logger;
            _personnesBusiness = personnesBusiness;
            _config = new TypeAdapterConfig();
            _config.ForType<PersonneDto, PersonneViewModel>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }

        [HttpGet]
        public async Task<List<PersonneViewModel>> GetPersonnes()
        {
            var personnes = await _personnesBusiness.GetPersonnes();

            return personnes.Adapt<List<PersonneViewModel>>(_config);
        }

        [HttpGet("{id}")]
        public async Task<PersonneViewModel> GetPersonne([FromRoute] int id)
        {
            var personne = _personnesBusiness.GetPersonne(id);

            return personne.Adapt<PersonneViewModel>(_config);
        }

        [HttpPost]
        public async Task<IResult> InsertPersonne([FromBody] PersonneViewModel personne)
        {
            var created = _personnesBusiness.InsertPersonne(personne.Adapt<PersonneDto>());

            return Results.Created($"/personnes/{created.Id}", created.Adapt<PersonneViewModel>(_config));
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdatePersonne([FromRoute] int id, [FromBody] PersonneViewModel personne)
        {
            _personnesBusiness.UpdatePersonne(id, personne.Adapt<PersonneDto>(_config));

            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeletePersonne([FromRoute] int id)
        {
            _personnesBusiness.DeletePersonne(id);

            return Results.Ok();
        }
    }
}
