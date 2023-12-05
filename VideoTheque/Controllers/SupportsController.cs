using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Supports;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("supports")]
    public class SupportsController : ControllerBase
    {
        private readonly ISupportsBusiness _supportsBusiness;
        protected readonly ILogger<SupportsController> _logger;

        public SupportsController(ISupportsBusiness supportsBusiness, ILogger<SupportsController> logger)
        {
            _supportsBusiness = supportsBusiness;
            _logger = logger;
        }

        [HttpGet]
        public List<SupportViewModel> GetSupports()
        {
            return _supportsBusiness.GetSupports().Adapt<List<SupportViewModel>>();
        }

        [HttpGet("{id}")]
        public SupportViewModel GetSupport([FromRoute]int id)
        {   
            return _supportsBusiness.GetSupport(id).Adapt<SupportViewModel>();
        }
    }
}
