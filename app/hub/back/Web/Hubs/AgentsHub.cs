using Microsoft.AspNetCore.SignalR;
using OsHub.Api.Abstractions.Common.Helpers;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Abstractions.Transports;

namespace OsHub.Api.Web.Hubs
{
	public class AgentsHub : Hub
	{
		private static readonly Dictionary<string, string> agentsUrl = new();
		private readonly IAgentService agentService;

		private readonly ILogger<AgentsHub> logger;

		public AgentsHub(IAgentService agentService, ILogger<AgentsHub> logger)
		{
			this.agentService = agentService;
			this.logger = logger;
		}


		[HubMethodName("agent-connection")]
		public async Task OnAgentConnection(AgentSubscribe config)
		{
			logger.Enter($"builder {LogHelper.Get(config.Url)}");
			agentsUrl[Context.ConnectionId] = config.Url;
			await agentService.Add(config.Url, config.Hostname);
			logger.Exit($"builder {LogHelper.Get(config.Url)}");
		}


		public override async Task OnDisconnectedAsync(Exception exception)
		{
			if (agentsUrl.TryGetValue(Context.ConnectionId, out var hostname)) await agentService.Delete(hostname);

			await base.OnDisconnectedAsync(exception);
		}
	}
}