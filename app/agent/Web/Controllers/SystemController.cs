using Microsoft.AspNetCore.Mvc;
using OsAgent.Api.Web.Controllers.Internal;
using OsAgent.Api.Web.Filters;
using OsAgent.Api.Abstractions.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OsAgent.Api.Web.Controllers
{
	[ApiController]
	[Route("api/system")]
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
		public async Task<IActionResult> Shutdown()
		{
			await systemService.Shutdown(Token!);
			return NoContent();
		}

		[HttpPost("sleep")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Sleep()
		{
			await systemService.Sleep(Token!);
			return NoContent();
		}


		[HttpPost("restart")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Restart()
		{
			await systemService.Restart(Token!);
			return NoContent();
		}


		[HttpPost("lock")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> Lock()
		{
			await systemService.Lock(Token!);
			return NoContent();
		}
	}
}