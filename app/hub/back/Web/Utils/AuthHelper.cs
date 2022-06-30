namespace OsHub.Api.Web.Utils
{
	public static class AuthHelper
	{
		public const string UsernameField = "auth_username";

		public const string TokenField = "auth_token";

		public static string? GetUsername(HttpRequest request)
		{
			return request.Headers[UsernameField].First();
		}

		public static string? GetToken(HttpRequest request)
		{
			return request.Headers[TokenField].First();
		}
	}
}