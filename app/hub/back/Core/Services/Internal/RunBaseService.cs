using Microsoft.Extensions.Options;
using OsHub.Api.Abstractions.Commons.Configs;
using OsHub.Api.Abstractions.Enums;

namespace OsHub.Api.Core.Services.Internal;

public class RunBaseService
{
	protected readonly bool IsLinux;
	protected readonly bool IsWindows;

	protected RunBaseService(IOptions<AppConfig> config)
	{
		IsWindows = config.Value.Platform == ApplicationPlatform.Windows;
		IsLinux = config.Value.Platform == ApplicationPlatform.Windows;
	}

	protected static void SystemNotSupported()
	{
		throw new NotSupportedException("Only windows and linux based system are supported");
	}
}