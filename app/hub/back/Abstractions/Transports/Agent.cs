using System.ComponentModel.DataAnnotations;

namespace OsHub.Api.Abstractions.Transports;

public class Agent : AgentRaw
{
	[Required] public Guid Id { get; set; }
}