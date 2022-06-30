using System.ComponentModel.DataAnnotations;

namespace OsHub.Api.Abstractions.Transports
{
	public class AgentRaw
	{
		[Required] public DateTime LastUptime { get; set; }

		[Required] public string Url { get; set; }

		[Required]  public string Hostname { get; set; }

	}
}