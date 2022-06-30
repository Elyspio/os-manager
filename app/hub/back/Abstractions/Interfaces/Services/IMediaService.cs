using OsHub.Api.Abstractions.Enums;

namespace OsHub.Api.Abstractions.Interfaces.Services
{
	public interface IMediaService
	{
		Task Stop(string token, Guid id);
		Task Toggle(string token, Guid id);
		Task MoveNext(string token, Guid id);
		Task MovePrevious(string token, Guid id);
		Task ChangeVolume(string token, VolumeModifier volume, Guid id);
	}
}