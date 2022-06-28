using OsHub.Api.Abstractions.Models;

namespace OsHub.Api.Abstractions.Interfaces.Repositories;

public interface IAgentRepository
{
	Task<AgentEntity> Add(string url);
	Task<AgentEntity> RefreshUptime(Guid id);
	Task Delete(Guid id);
	Task<List<AgentEntity>> GetAll();
	Task<AgentEntity?> GetById(Guid id);
}