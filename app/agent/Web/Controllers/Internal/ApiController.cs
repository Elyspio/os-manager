using Microsoft.AspNetCore.Mvc;
using OsAgent.Api.Web.Utils;

namespace OsAgent.Api.Web.Controllers.Internal;

public class ApiController : ControllerBase
{
	protected string? Username => AuthHelper.GetUsername(Request);

	protected string? Token => AuthHelper.GetToken(Request);
}