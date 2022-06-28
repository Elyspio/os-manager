using System.ComponentModel;

namespace OsHub.Api.Abstractions.Commons.Helpers;

public class Env
{
	public static T Get<T>(string variableName, T falback)
	{
		var env = Environment.GetEnvironmentVariable(variableName);
		if (env != null)
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			return (T) converter.ConvertFromString(env)!;
		}

		return falback;
	}
}