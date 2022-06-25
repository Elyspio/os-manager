﻿using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OsManager.Api.Abstractions.Helpers;
using OsManager.Api.Abstractions.Interfaces.Injections;
using OsManager.Api.Adapters.Injections;
using OsManager.Api.Core.Injections;
using OsManager.Api.Web.Filters;
using OsManager.Api.Web.Utils;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace OsManager.Api.Web.Server
{
	public class ServerBuilder
	{
		private readonly string appPath = "/example";
		private readonly string frontPath = Env.Get("FRONT_PATH", "/front");

		public ServerBuilder(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.WebHost.ConfigureKestrel((_, options) => {
					options.Listen(IPAddress.Any, 4000, listenOptions => {
							// Use HTTP/3
							listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
						}
					);
				}
			);


			// Setup CORS
			builder.Services.AddCors(options => {
					options.AddPolicy("Cors", b => {
							b.AllowAnyOrigin();
							b.AllowAnyHeader();
							b.AllowAnyMethod();
						}
					);

					options.DefaultPolicyName = "Cors";
				}
			);


			builder.Services.AddModule<OsManagerApiAdapterModule>(builder.Configuration);
			builder.Services.AddModule<OsManagerApiCoreModule>(builder.Configuration);


			// Setup Logging
			builder.Host.UseSerilog((_, lc) => lc
				.Enrich.FromLogContext()
				.Enrich.With(new CallerEnricher())
				.WriteTo.Console(LogEventLevel.Debug, "[{Timestamp:HH:mm:ss} {Level}{Caller}] {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
			);

			// Convert Enum to String 
			builder.Services.AddControllers(o => {
						o.Conventions.Add(new ControllerDocumentationConvention());
						o.OutputFormatters.RemoveType<StringOutputFormatter>();
					}
				)
				.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
				.AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options => {
					if (builder.Environment.IsProduction())
						options.AddServer(new OpenApiServer
							{
								Url = appPath
							}
						);

					options.SwaggerDoc("v1", new OpenApiInfo {Title = "OS Manager API", Version = "1"});

					options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

					options.OperationFilter<RequireAuthAttribute.Swagger>();
				}
			);

			// Setup SPA Serving
			if (builder.Environment.IsProduction()) Console.WriteLine($"Server in production, serving SPA from {frontPath} folder");

			Application = builder.Build();
		}

		public WebApplication Application { get; }
	}
}