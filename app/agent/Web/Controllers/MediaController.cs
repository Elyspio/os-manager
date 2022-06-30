using Microsoft.AspNetCore.Mvc;
using OsAgent.Api.Web.Controllers.Internal;
using OsAgent.Api.Web.Filters;
using OsAgent.Api.Abstractions.Enums;
using OsAgent.Api.Abstractions.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OsAgent.Api.Web.Controllers
{
	[ApiController]
	[Route("api/media")]
	[RequireAuth]
	public class MediaController : ApiController
	{
		private readonly IMediaService mediaService;

		public MediaController(IMediaService mediaService)
		{
			this.mediaService = mediaService;
		}

		[HttpPut("volume/{modifier}")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> ChangeVolume([FromRoute] VolumeModifier modifier)
		{
			await mediaService.ChangeVolume(Token!, modifier);
			return NoContent();
		}

		[HttpPost("move/next")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> MoveNext()
		{
			await mediaService.MoveNext(Token!);
			return NoContent();
		}

		[HttpPost("move/previous")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> MovePrevious()
		{
			await mediaService.MovePrevious(Token!);
			return NoContent();
		}


		[HttpPost("stop")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> StopMedia()
		{
			await mediaService.Stop(Token!);
			return NoContent();
		}

		[HttpPost("toggle")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> ToggleMedia()
		{
			await mediaService.Toggle(Token!);
			return NoContent();
		}
	}
}