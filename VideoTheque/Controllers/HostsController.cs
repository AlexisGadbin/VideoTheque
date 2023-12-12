using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Hosts;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("hosts")]
    public class HostsController
    {
        private readonly IHostsBusiness _hostsBusiness;
        protected readonly ILogger<HostsController> _logger;

        public HostsController(ILogger<HostsController> logger, IHostsBusiness hostsBusiness)
        {
            _logger = logger;
            _hostsBusiness = hostsBusiness;
        }

        [HttpGet]
        public async Task<List<HostViewModel>> GetHosts()
        {
            var hosts = await _hostsBusiness.GetHosts();

            return hosts.Adapt<List<HostViewModel>>();
        }

        [HttpGet("{id}")]
        public async Task<HostViewModel> GetHost([FromRoute] int id)
        {
            var host = _hostsBusiness.GetHost(id);

            return host.Adapt<HostViewModel>();
        }

        [HttpPost]
        public async Task<IResult> InsertHost([FromBody] HostViewModel host)
        {
            var created = _hostsBusiness.InsertHost(host.Adapt<HostDto>());

            return Results.Created($"/hosts/{created.Id}", created.Adapt<HostViewModel>());
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateHost([FromRoute] int id, [FromBody] HostViewModel host)
        {
            _hostsBusiness.UpdateHost(id, host.Adapt<HostDto>());

            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteHost([FromRoute] int id)
        {
            _hostsBusiness.DeleteHost(id);

            return Results.NoContent();
        }
    }
}
