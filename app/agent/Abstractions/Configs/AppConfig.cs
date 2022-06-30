using OsAgent.Api.Abstractions.Enums;

namespace OsAgent.Api.Abstractions.Configs;

public class AppConfig
{
	public ApplicationPlatform Platform { get; set; }

	public HubConfig HubConfig { get; set; }

	public string ApplicationUrl { get; set; }
}

public class HubConfig : AgentSubscribe { }

public class AgentSubscribe
{
	public string Url { get; set; } = null!;
	public string? Hostname { get; set; }
}