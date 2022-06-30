using OsHub.Api.Abstractions.Transports;

namespace OsHub.Api.Abstractions.Interfaces.Services
{
	public interface IAgentService
	{
		Task<Agent> Add(string url, string hostname);
		Task<Agent> RefreshUptime(Guid id);
		Task Delete(string id);
		Task<List<Agent>> GetAll();
		Task<Agent?> GetById(Guid id);
	}
}