using Microsoft.AspNetCore.Mvc;
using OsManager.Api.Web.Utils;

namespace OsManager.Api.Web.Controllers.Internal
{
	public class ApiController : ControllerBase
	{
		protected string? Username => AuthHelper.GetUsername(Request);

		protected string? Token => AuthHelper.GetToken(Request);
	}
}