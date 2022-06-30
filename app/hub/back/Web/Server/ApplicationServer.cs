using OsHub.Api.Web.Hubs;

namespace OsHub.Api.Web.Server
{
	public static class ApplicationServer
	{
		public static WebApplication Initialize(this WebApplication application)
		{
			// Allow CORS
			application.UseCors("Cors");

			application.UseSwagger();
			application.UseSwaggerUI();

			// Start Dependency Injection
			application.UseAdvancedDependencyInjection();


			// Setup Controllers
			application.MapControllers();

			application.MapHub<AgentsHub>("/ws/agents");


			application.UseAuthentication();

			// Start SPA serving
			if (application.Environment.IsProduction())
			{
				application.UseRouting();

				application.UseDefaultFiles(new DefaultFilesOptions
					{
						DefaultFileNames = new List<string> {"index.html"},
						RedirectToAppendTrailingSlash = true
					}
				);
				application.UseStaticFiles();

				application.UseEndpoints(endpoints => { endpoints.MapFallbackToFile("/index.html"); });
			}

			return application;
		}
	}
}