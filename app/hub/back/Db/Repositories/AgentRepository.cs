using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OsHub.Api.Abstractions.Common.Extensions;
using OsHub.Api.Abstractions.Interfaces.Repositories;
using OsHub.Api.Abstractions.Models;
using OsHub.Api.Db.Repositories.Internal;

namespace OsHub.Api.Db.Repositories
{
	internal class AgentRepository : BaseRepository<AgentEntity>, IAgentRepository
	{
		public AgentRepository(IConfiguration configuration, ILogger<AgentRepository> logger) : base(configuration, logger)
		{
			EntityCollection.DeleteMany(_ => true);
		}


		public async Task<AgentEntity> Add(string hostname, string url)
		{
			var entity = new AgentEntity
			{
				LastUptime = DateTime.Now,
				Url = url,
				Hostname = hostname
			};

			await EntityCollection.InsertOneAsync(entity);

			return entity;
		}

		public async Task<AgentEntity> RefreshUptime(Guid id)
		{
			var updater = Builders<AgentEntity>.Update.Set(agent => agent.LastUptime, DateTime.Now);
			return await EntityCollection.FindOneAndUpdateAsync(agent => agent.Id == id.AsObjectId(), updater);
		}

		public async Task Delete(string url)
		{
			await EntityCollection.DeleteOneAsync(agent => agent.Url == url);
		}

		public async Task<List<AgentEntity>> GetAll()
		{
			return await EntityCollection.AsQueryable().ToListAsync();
		}


		public async Task<AgentEntity?> GetById(Guid id)
		{
			return await EntityCollection.AsQueryable().FirstOrDefaultAsync(agent => agent.Id == id.AsObjectId());
		}
	}
}