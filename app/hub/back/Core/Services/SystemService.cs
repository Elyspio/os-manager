using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OsHub.Api.Abstractions.Commons.Configs;
using OsHub.Api.Abstractions.Interfaces.Services;
using OsHub.Api.Core.Services.Internal;
using OsManager.Api.Adapters.RunnerApi;

namespace OsHub.Api.Core.Services;

public class SystemService : RunBaseService, ISystemService
{
	private static OSPlatform platform;
	private readonly ILogger<SystemService> logger;

	private readonly IRunnerApi runner;


	static SystemService()
	{
		platform = OperatingSystem.IsWindows() ? OSPlatform.Windows : OSPlatform.Linux;
	}

	public SystemService(ILogger<SystemService> logger, IOptions<AppConfig> config, IRunnerApi runner) : base(config)
	{
		this.logger = logger;
		this.runner = runner;
	}

	public async Task Shutdown(string token, Guid guid)
	{
		await Internal("shutdown /S /T 0", "shutdown now", token);
	}

	public async Task Sleep(string token, Guid guid)
	{
		await Internal("rundll32.exe powrprof.dll, SetSuspendState Sleep", "systemctl suspend", token);
	}

	public async Task Restart(string token, Guid guid)
	{
		await Internal("shutdown /R /T 0", "reboot now", token);
	}

	public async Task Lock(string token, Guid guid)
	{
		await Internal("Rundll32.exe user32.dll,LockWorkStation", "gnome-screensaver-command -l", token);
	}


	private async Task Internal(string winCommand, string linuxCommand, string token)
	{
		if (IsWindows)
			await runner.RunAsync(new RunRequest
				{
					Command = winCommand,
					Cwd = "/"
				}, token
			);
		else if (IsLinux)
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