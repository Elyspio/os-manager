using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsAgent.Api.Abstractions.Interfaces.Injections;

namespace OsAgent.Api.Core.Injections;

public class OsAgentApiCoreModule : IDotnetModule
{
	public void Load(IServiceCollection services, IConfiguration configuration)
	{
		var nsp = typeof(OsAgentApiCoreModule).Namespace!;
		var baseNamespace = nsp[..nsp.LastIndexOf(".")];
		services.Scan(scan => scan
			.FromAssemblyOf<OsAgentApiCoreModule>()
			.AddClasses(classes => classes.InNamespaces(baseNamespace + ".Services"))
			.AsImplementedInterfaces()
			.WithSingletonLifetime()
		);
	}
}