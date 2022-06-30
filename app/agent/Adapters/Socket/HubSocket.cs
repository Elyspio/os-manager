using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OsAgent.Api.Abstractions.Configs;
using Polly.Retry;

namespace OsAgent.Api.Adapters.Socket
{
	public class HubSocket
	{
		private readonly HubConnection connection;
		private readonly AgentSubscribe subscribe;
 		public HubSocket(IOptions<AppConfig> config, ILogger<HubSocket> logger)
		{
			var configValue = config.Value;
			connection = new HubConnectionBuilder()
				.WithUrl(configValue.HubConfig.Url)
				.WithAutomaticReconnect()
				.ConfigureLogging(log => {
					log.SetMinimumLevel(LogLevel.Debug);
				})
				.Build();

			subscribe = new AgentSubscribe()
			{
				Hostname = configValue.HubConfig.Hostname,
				Url = configValue.ApplicationUrl 
			};

			connection.Reconnected += async (_) => {
				logger.LogInformation("Connected");
				await Connect();
			};


			connection.Closed += async (error) =>
			{
				logger.LogError($"Hub Socket error {error?.Message}");
				throw error!;
			};

			connection.StartAsync().GetAwaiter().GetResult();

		}


        public async Task Connect()
        {
	        await connection.InvokeAsync("agent-connection", subscribe);
		}
	}
}