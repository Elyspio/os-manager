using Microsoft.Extensions.Logging;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsManager.Api.Adapters.RunnerApi;

namespace OsHub.Api.Core.Services
{
	public class SystemService : ISystemService
	{
		private readonly ILogger<SystemService> logger;

		private readonly IRunnerApi runner;


		public SystemService(ILogger<SystemService> logger)
		{
			this.logger = logger;
		}

		public async Task Shutdown(string token, Guid guid) { }

		public async Task Sleep(string token, Guid guid) { }

		public async Task Restart(string token, Guid guid) { }

		public async Task Lock(string token, Guid guid) { }
	}
}