﻿using Example.Api.Adapters.AuthenticationApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsManager.Api.Abstractions.Interfaces.Injections;
using OsManager.Api.Adapters.Configs;
using OsManager.Api.Adapters.RunnerApi;

namespace OsManager.Api.Adapters.Injections
{
	public class OsManagerApiAdapterModule : IDotnetModule
	{
		public void Load(IServiceCollection services, IConfiguration configuration)
		{
			var conf = new EndpointConfig();
			configuration.GetSection(EndpointConfig.Section).Bind(conf);

			services.AddHttpClient<IUsersClient, UsersClient>(client => { client.BaseAddress = new Uri(conf.Authentication); });
			services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(client => { client.BaseAddress = new Uri(conf.Authentication); });

			services.AddHttpClient<IRunnerApi, RunnerApi.RunnerApi>(client => { client.BaseAddress = new Uri(conf.Runner); });
		}
	}
}