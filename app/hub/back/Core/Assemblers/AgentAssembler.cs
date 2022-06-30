using OsHub.Api.Abstractions.Common.Assemblers;
using OsHub.Api.Abstractions.Common.Extensions;
using OsHub.Api.Abstractions.Models;
using OsHub.Api.Abstractions.Transports;

namespace OsHub.Api.Core.Assemblers
{
	public class AgentAssembler : BaseAssembler<Agent, AgentEntity>
	{
		public override AgentEntity Convert(Agent obj)
		{
			return new AgentEntity
			{
				Id = obj.Id.AsObjectId(),
				LastUptime = obj.LastUptime,
				Url = obj.Url,
				Hostname = obj.Hostname
			};
		}

		public override Agent Convert(AgentEntity obj)
		{
			return new Agent
			{
				Id = obj.Id.AsGuid(),
				LastUptime = obj.LastUptime,
				Url = obj.Url,
				Hostname = obj.Hostname
			};
		}
	}
}