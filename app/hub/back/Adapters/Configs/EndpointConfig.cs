﻿namespace OsHub.Api.Adapters.Configs;

public class EndpointConfig
{
	public const string Section = "Endpoints";
	public string Authentication { get; set; }

	public string Runner { get; set; }
}