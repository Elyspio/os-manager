using Example.Api.Adapters.AuthenticationApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsAgent.Api.Abstractions.Interfaces.Injections;
using OsAgent.Api.Adapters.Configs;
using OsAgent.Api.Adapters.RunnerApi;
using OsAgent.Api.Adapters.Socket;

namespace OsAgent.Api.Adapters.Injections;

public class OsAgentApiAdapterModule : IDotnetModule
{
	public void Load(IServiceCollection services, IConfiguration configuration)
	{
		var conf = new EndpointConfig();
		configuration.GetSection(EndpointConfig.Section).Bind(conf);

		services.AddHttpClient<IUsersClient, UsersClient>(client => { client.BaseAddress = new Uri(conf.Authentication); });
		services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(client => { client.BaseAddress = new Uri(conf.Authentication); });

		services.AddHttpClient<IRunnerApi, RunnerApi.RunnerApi>(client => {
			client.BaseAddress = new Uri(conf.Runner);
		});

		services.AddSingleton<HubSocket>();

	}
}