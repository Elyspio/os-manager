using OsHub.Api.Abstractions.Enums;

namespace OsHub.Api.Abstractions.Interfaces.Services;

public interface IMediaService
{
	Task Stop(string token, Guid guid);
	Task Toggle(string token, Guid guid);
	Task MoveNext(string token, Guid guid);
	Task MovePrevious(string token, Guid guid);
	Task ChangeVolume(string token, VolumeModifier volume, Guid guid);
}