using OsAgent.Api.Abstractions.Enums;

namespace OsAgent.Api.Abstractions.Interfaces.Services;

public interface IMediaService
{
	Task Stop(string token);
	Task Toggle(string token);
	Task MoveNext(string token);
	Task MovePrevious(string token);
	Task ChangeVolume(string token, VolumeModifier volume);
}