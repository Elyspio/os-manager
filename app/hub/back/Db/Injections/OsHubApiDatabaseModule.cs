using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsHub.Api.Abstractions.Interfaces.Injections;

namespace OsHub.Api.Db.Injections
{
	public class OsHubApiDatabaseModule : IDotnetModule
	{
		public void Load(IServiceCollection services, IConfiguration configuration)
		{
			var nsp = typeof(OsHubApiDatabaseModule).Namespace!;
			var baseNamespace = nsp[..nsp.LastIndexOf(".")];
			services.Scan(scan =>
				scan.FromAssemblyOf<OsHubApiDatabaseModule>()
					.AddClasses(classes => classes.InNamespaces($"{baseNamespace}.Repositories", $"{baseNamespace}.Watchers"))
					.AsImplementedInterfaces()
					.WithSingletonLifetime()
			);
		}
	}
}