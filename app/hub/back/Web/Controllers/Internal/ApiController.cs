using Microsoft.AspNetCore.Mvc;
using OsHub.Api.Web.Utils;

namespace OsHub.Api.Web.Controllers.Internal
{
	public class ApiController : ControllerBase
	{
		protected string? Username => AuthHelper.GetUsername(Request);

		protected string? Token => AuthHelper.GetToken(Request);
	}
}