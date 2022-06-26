using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using OsManager.Api.Abstractions.Interfaces.Services;
using OsManager.Api.Adapters.RunnerApi;

namespace OsManager.Api.Core.Services
{
	public class SystemService : ISystemService
	{
		private static OSPlatform platform;
		private readonly ILogger<SystemService> logger;

		private readonly IRunnerApi runner;


		static SystemService()
		{
			platform = OperatingSystem.IsWindows() ? OSPlatform.Windows : OSPlatform.Linux;
		}

		public SystemService(ILogger<SystemService> logger, IRunnerApi runner)
		{
			this.logger = logger;
			this.runner = runner;
		}

		public async Task Shutdown(string token)
		{
			await Internal("shutdown /S /T 0", "shutdown now", token);
		}

		public async Task Sleep(string token)
		{
			await Internal("rundll32.exe powrprof.dll, SetSuspendState Sleep", "systemctl suspend", token);
		}

		public async Task Restart(string token)
		{
			await Internal("shutdown /R /T 0", "reboot now", token);
		}

		public async Task Lock(string token)
		{
			await Internal("Rundll32.exe user32.dll,LockWorkStation", "gnome-screensaver-command -l", token);
		}

		private static void SystemNotSupported()
		{
			throw new NotSupportedException("Only windows and linux based system are supported");
		}


		private async Task Internal(string winCommand, string linuxCommand, string token)
		{
			if (OperatingSystem.IsWindows())
				await runner.RunAsync(new RunRequest
					{
						Command = winCommand,
						Cwd = "/"
					}, token
				);
			else if (OperatingSystem.IsLinux())
				await runner.RunAsync(new RunRequest
					{
						Command = linuxCommand,
						Cwd = "/",
						Admin = true
					}, token
				);
			else
				SystemNotSupported();
		}
	}
}