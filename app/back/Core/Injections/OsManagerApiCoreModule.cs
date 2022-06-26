using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsManager.Api.Abstractions.Interfaces.Injections;

namespace OsManager.Api.Core.Injections
{
	public class OsManagerApiCoreModule : IDotnetModule
	{
		public void Load(IServiceCollection services, IConfiguration configuration)
		{
			var nsp = typeof(OsManagerApiCoreModule).Namespace!;
			var baseNamespace = nsp[..nsp.LastIndexOf(".")];
			services.Scan(scan => scan
				.FromAssemblyOf<OsManagerApiCoreModule>()
				.AddClasses(classes => classes.InNamespaces(baseNamespace + ".Services"))
				.AsImplementedInterfaces()
				.WithSingletonLifetime()
			);
		}
	}
}