using System.ComponentModel;

namespace OsAgent.Api.Abstractions.Helpers;

public static class Env
{
	public static T Get<T>(string variableName, T fallback)
	{
		var env = Environment.GetEnvironmentVariable(variableName);
		if (env == null) return fallback;
		var converter = TypeDescriptor.GetConverter(typeof(T));
		return (T) converter.ConvertFromString(env)!;
	}
}