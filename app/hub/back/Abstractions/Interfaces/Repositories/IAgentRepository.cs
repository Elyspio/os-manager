using OsHub.Api.Abstractions.Models;

namespace OsHub.Api.Abstractions.Interfaces.Repositories
{
	public interface IAgentRepository
	{
		Task<AgentEntity> Add(string hostname, string url);
		Task<AgentEntity> RefreshUptime(Guid id);
		Task Delete(string url);
		Task<List<AgentEntity>> GetAll();
		Task<AgentEntity?> GetById(Guid id);
	}
}