using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.AgeRatings;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("ageratings")]
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
    }
}
