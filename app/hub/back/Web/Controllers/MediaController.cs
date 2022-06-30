using Microsoft.AspNetCore.Mvc;
using OsHub.Api.Abstractions.Enums;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Web.Controllers.Internal;
using OsHub.Api.Web.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace OsHub.Api.Web.Controllers
{
	[ApiController]
	[Route("api/media/{id:guid}")]
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
		public async Task<IActionResult> ChangeVolume([FromRoute] Guid id, [FromRoute] VolumeModifier modifier)
		{
			await mediaService.ChangeVolume(Token!, modifier, id);
			return NoContent();
		}


		[HttpPost("move/next")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> MoveNext([FromRoute] Guid id)
		{
			await mediaService.MoveNext(Token!, id);
			return NoContent();
		}

		[HttpPost("move/previous")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> MovePrevious([FromRoute] Guid id)
		{
			await mediaService.MovePrevious(Token!, id);
			return NoContent();
		}


		[HttpPost("stop")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> StopMedia([FromRoute] Guid id)
		{
			await mediaService.Stop(Token!, id);
			return NoContent();
		}

		[HttpPost("toggle")]
		[SwaggerResponse(204)]
		public async Task<IActionResult> ToggleMedia([FromRoute] Guid id)
		{
			await mediaService.Toggle(Token!, id);
			return NoContent();
		}
	}
}