using Microsoft.Extensions.Logging;
using OsHub.Api.Abstractions.Common.Helpers;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Adapters.OsAgent;
using VolumeModifier = OsHub.Api.Abstractions.Enums.VolumeModifier;

namespace OsHub.Api.Core.Services
{
	public class MediaService : IMediaService
	{
		private readonly IAgentService agentService;
		private readonly ILogger<MediaService> logger;


		public MediaService(ILogger<MediaService> logger, IAgentService agentService)
		{
			this.logger = logger;
			this.agentService = agentService;
		}

		public async Task Stop(string token, Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)} {LogHelper.Get(token)}");

			var agent = await agentService.GetById(id);
			if (agent == default) throw new Exception($"No agent with id={id} found");

			await new MediaAgentApi(agent.Url).StopMediaAsync(token);

			logger.Exit($"{LogHelper.Get(id)} {LogHelper.Get(token)}");
		}

		public async Task Toggle(string token, Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)} {LogHelper.Get(token)}");

			var agent = await agentService.GetById(id);
			if (agent == default) throw new Exception($"No agent with id={id} found");

			await new MediaAgentApi(agent.Url).ToggleMediaAsync(token);

			logger.Exit($"{LogHelper.Get(id)} {LogHelper.Get(token)}");
		}

		public async Task MoveNext(string token, Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)} {LogHelper.Get(token)}");

			var agent = await agentService.GetById(id);
			if (agent == default) throw new Exception($"No agent with id={id} found");

			await new MediaAgentApi(agent.Url).MoveNextAsync(token);


			logger.Exit($"{LogHelper.Get(id)} {LogHelper.Get(token)}");
		}

		public async Task MovePrevious(string token, Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)} {LogHelper.Get(token)}");


			var agent = await agentService.GetById(id);
			if (agent == default) throw new Exception($"No agent with id={id} found");

			await new MediaAgentApi(agent.Url).MovePreviousAsync(token);

			logger.Exit($"{LogHelper.Get(id)} {LogHelper.Get(token)}");
		}

		public async Task ChangeVolume(string token, VolumeModifier volume, Guid id)
		{
			logger.Enter($"{LogHelper.Get(id)} {LogHelper.Get(volume)} {LogHelper.Get(token)}");

			var agent = await agentService.GetById(id);
			if (agent == default) throw new Exception($"No agent with id={id} found");


			var modifier = volume switch
			{
				VolumeModifier.Up => Adapters.OsAgent.VolumeModifier.Up,
				VolumeModifier.Down => Adapters.OsAgent.VolumeModifier.Down,
				VolumeModifier.Mute => Adapters.OsAgent.VolumeModifier.Mute,
				_ => throw new ArgumentOutOfRangeException(nameof(volume), volume, null)
			};

			await new MediaAgentApi(agent.Url).ChangeVolumeAsync(modifier, token);


			logger.Exit($"{LogHelper.Get(id)} {LogHelper.Get(volume)} {LogHelper.Get(token)}");
		}
	}
}