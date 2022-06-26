namespace OsAgent.Api.Abstractions.Interfaces.Services;

public interface ISystemService
{
	Task Shutdown(string token);
	Task Sleep(string token);
	Task Restart(string token);
	Task Lock(string token);
}