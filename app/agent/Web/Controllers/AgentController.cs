using Microsoft.AspNetCore.Mvc;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Abstractions.Transports;
using OsHub.Api.Web.Controllers.Internal;
using OsHub.Api.Web.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace OsHub.Api.Web.Controllers;

[ApiController]
[Route("api/agents")]
[RequireAuth]
public class AgentController : ApiController
{
	private readonly ISystemService systemService;

	public AgentController(ISystemService systemService)
	{
		this.systemService = systemService;
	}

	[HttpGet]
	[SwaggerResponse(200, type: typeof(List<Agent>))]
	public async Task<IActionResult> GetAgents()
	{
		return Ok();
	}
}