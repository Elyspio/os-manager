namespace OsHub.Api.Abstractions.Interfaces.Services;

public interface ISystemService
{
	Task Shutdown(string token, Guid guid);
	Task Sleep(string token, Guid guid);
	Task Restart(string token, Guid guid);
	Task Lock(string token, Guid guid);
}