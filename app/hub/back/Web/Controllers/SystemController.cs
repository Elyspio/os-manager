using Microsoft.AspNetCore.Mvc;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Web.Controllers.Internal;
using OsHub.Api.Web.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace OsHub.Api.Web.Controllers
{
	[ApiController]
	[Route("api/system/{id:guid}")]
	[RequireAuth]
	public class SystemController : ApiController
	{
		private readonly ISystemService systemService;

		public SystemController(ISystemService systemService)
		{
			this.systemService = systemService;
		}

		[HttpPost("shutdown")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Shutdown([FromRoute] Guid id)
		{
			await systemService.Shutdown(Token!, id);
			return NoContent();
		}

		[HttpPost("sleep")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Sleep([FromRoute] Guid id)
		{
			await systemService.Sleep(Token!, id);
			return NoContent();
		}


		[HttpPost("restart")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Restart([FromRoute] Guid id)
		{
			await systemService.Restart(Token!, id);
			return NoContent();
		}


		[HttpPost("lock")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Lock([FromRoute] Guid id)
		{
			await systemService.Lock(Token!, id);
			return NoContent();
		}
	}
}