using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsHub.Api.Abstractions.Interfaces.Injections;

namespace OsHub.Api.Core.Injections
{
	public class OsHubApiCoreModule : IDotnetModule
	{
		public void Load(IServiceCollection services, IConfiguration configuration)
		{
			var nsp = typeof(OsHubApiCoreModule).Namespace!;
			var baseNamespace = nsp[..nsp.LastIndexOf(".")];
			services.Scan(scan => scan.FromAssemblyOf<OsHubApiCoreModule>().AddClasses(classes => classes.InNamespaces(baseNamespace + ".Services")).AsImplementedInterfaces().WithSingletonLifetime());
		}
	}
}