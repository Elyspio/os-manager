using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OsAgent.Api.Abstractions.Configs;
using OsAgent.Api.Abstractions.Enums;
using OsAgent.Api.Abstractions.Interfaces.Services;
using OsAgent.Api.Core.Services.Internal;
using OsAgent.Api.Adapters.RunnerApi;

namespace OsAgent.Api.Core.Services;

public class MediaService : RunBaseService, IMediaService
{
	private readonly ILogger<MediaService> logger;

	private readonly IRunnerApi runner;


	public MediaService(ILogger<MediaService> logger, IOptions<AppConfig> config, IRunnerApi runner) : base(config)
	{
		this.logger = logger;
		this.runner = runner;
	}

	public async Task Stop(string token)
	{
		if (IsWindows) await InternalWindows(VirtualKeyCodes.MediaStop, token);
	}

	public async Task Toggle(string token)
	{
		if (IsWindows) await InternalWindows(VirtualKeyCodes.MediaToggle, token);
	}

	public async Task MoveNext(string token)
	{
		if (IsWindows) await InternalWindows(VirtualKeyCodes.MediaNextTrack, token);
	}

	public async Task MovePrevious(string token)
	{
		if (IsWindows) await InternalWindows(VirtualKeyCodes.MediaPreviousTrack, token);
	}

	public async Task ChangeVolume(string token, VolumeModifier volume)
	{
		if (IsWindows)
		{
			var key = volume switch
			{
				VolumeModifier.Down => VirtualKeyCodes.VolumeDown,
				VolumeModifier.Up => VirtualKeyCodes.VolumeUp,
				VolumeModifier.Mute => VirtualKeyCodes.VolumeMute,
				_ => throw new ArgumentOutOfRangeException(nameof(volume), volume, null)
			};

			await InternalWindows(key, token);
		}
	}


	private async Task InternalWindows(VirtualKeyCodes code, string token)
	{
		var command
			= $"powershell \"Add-Type -MemberDefinition '[DllImport(\\\"user32.dll\\\")] public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, int extraInfo);' -Name U32 -Namespace W1;[W1.U32]::keybd_event({(int) code}, 0, 1, 0);\"";


		logger.LogCritical(command);

		await runner.RunAsync(new RunRequest
			{
				Command = command,
				Cwd = "/",
				Admin = false
			}, token
		);
	}


	private enum VirtualKeyCodes
	{
		VolumeMute = 0xAD,
		VolumeDown = 0xAE,
		VolumeUp = 0xAF,
		MediaNextTrack = 0xB0,
		MediaPreviousTrack = 0xB1,
		MediaStop = 0xB2,
		MediaToggle = 0xB3
	}
}