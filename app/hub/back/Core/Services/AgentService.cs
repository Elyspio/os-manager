using Microsoft.Extensions.Logging;
using OsHub.Api.Abstractions.Common.Helpers;
using OsHub.Api.Abstractions.Interfaces.Repositories;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Abstractions.Transports;
using OsHub.Api.Core.Assemblers;

namespace OsHub.Api.Core.Services
{
	public class AgentService : IAgentService
	{
		private readonly AgentAssembler assembler = new();
		private readonly ILogger<AgentService> logger;
		private readonly IAgentRepository repository;

		public AgentService(IAgentRepository repository, ILogger<AgentService> logger)
		{
			this.repository = repository;
			this.logger = logger;
		}

		public async Task<Agent> Add(string url, string hostname)
		{
			logger.Enter($"{LogHelper.Get(url)}, {LogHelper.Get(hostname)}");

			var entity = await repository.Add(hostname, url );

			logger.Exit($"{LogHelper.Get(url)}, {LogHelper.Get(hostname)}");

			return assembler.Convert(entity);
		}

		public async Task<Agent> RefreshUptime(Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)}");

			var entity = await repository.RefreshUptime(id);

			logger.Exit($"{LogHelper.Get(id)}");

			return assembler.Convert(entity);
		}

		public async Task Delete(string id)
		{
			logger.Enter($"{LogHelper.Get(id)}");

			await repository.Delete(id);

			logger.Exit($"{LogHelper.Get(id)}");
		}

		public async Task<List<Agent>> GetAll()
		{
			logger.Enter();

			var entities = await repository.GetAll();

			logger.Exit();

			return assembler.Convert(entities);
		}

		public async Task<Agent?> GetById(Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)}");

			var entity = await repository.GetById(id);

			logger.Exit($"{LogHelper.Get(id)}");

			return entity != null ? assembler.Convert(entity) : null;
		}
	}
}