using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Emprunts;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("emprunts")]
    public class EmpruntsController
    {
        private readonly IEmpruntsBusiness _filmsBusiness;
        protected readonly ILogger<FilmsController> _logger;

        public EmpruntsController(ILogger<FilmsController> logger, IEmpruntsBusiness filmsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
        }

        [HttpGet("{idHost}")]
        public List<EmpruntableViewModel> GetEmprunts([FromRoute] int idHost)
        {
            List<EmpruntableViewModel> films = _filmsBusiness.GetEmprunts(idHost).Result;

            return films.Adapt<List<EmpruntableViewModel>>();
        }

    }
}
