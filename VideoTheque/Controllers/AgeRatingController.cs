using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.AgeRatings;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("age-ratings")]
    public class AgeRatingController : ControllerBase
    {
        private readonly IAgeRatingsBusiness _ageRatingsBusiness;
        protected readonly ILogger<AgeRatingController> _logger;

        public AgeRatingController(ILogger<AgeRatingController> logger, IAgeRatingsBusiness ageRatingsBusiness)
        {
            _logger = logger;
            _ageRatingsBusiness = ageRatingsBusiness;
        }

        [HttpGet]
        public async Task<List<AgeRatingViewModel>> GetAgeRatings()
        {
            var ageRatings = await _ageRatingsBusiness.GetAgeRatings();

            return ageRatings.Adapt<List<AgeRatingViewModel>>();
        }

        [HttpGet("{id}")]
        public async Task<AgeRatingViewModel> GetAgeRating([FromRoute] int id)
        {
            var ageRating = _ageRatingsBusiness.GetAgeRating(id);

            return ageRating.Adapt<AgeRatingViewModel>();
        }

        [HttpPost]
        public async Task<IResult> InsertAgeRating([FromBody] AgeRatingViewModel ageRating)
        {
            var created = _ageRatingsBusiness.InsertAgeRating(ageRating.Adapt<AgeRatingDto>());

            return Results.Created($"/ageratings/{created.Id}", created.Adapt<AgeRatingViewModel>());
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateAgeRating([FromRoute] int id, [FromBody] AgeRatingViewModel ageRating)
        {
            _ageRatingsBusiness.UpdateAgeRating(id, ageRating.Adapt<AgeRatingDto>());

            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAgeRating([FromRoute] int id)
        {
            _ageRatingsBusiness.DeleteAgeRating(id);

            return Results.Ok();
        }
    }
}
