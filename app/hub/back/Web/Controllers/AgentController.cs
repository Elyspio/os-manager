using Microsoft.AspNetCore.Mvc;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Abstractions.Transports;
using OsHub.Api.Web.Controllers.Internal;
using OsHub.Api.Web.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace OsHub.Api.Web.Controllers
{
	[ApiController]
	[Route("api/agents")]
	[RequireAuth]
	public class AgentController : ApiController
	{
		private readonly IAgentService service;

		public AgentController(IAgentService service)
		{
			this.service = service;
		}

		[HttpGet]
		[SwaggerResponse(200, type: typeof(List<Agent>))]
		public async Task<IActionResult> GetAgents()
		{
			return Ok(await service.GetAll());
		}


		[HttpGet("{id:guid}")]
		[SwaggerResponse(200, type: typeof(List<Agent>))]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await service.GetById(id));
		}
	}
}