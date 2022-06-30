using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OsHub.Api.Abstractions.Common.Helpers;
using OsHub.Api.Abstractions.Interfaces.Injections;
using OsHub.Api.Adapters.Injections;
using OsHub.Api.Core.Injections;
using OsHub.Api.Db.Injections;
using OsHub.Api.Web.Filters;
using OsHub.Api.Web.Utils;

namespace OsHub.Api.Web.Server
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


			builder.Services.AddModule<OsHubApiAdapterModule>(builder.Configuration);
			builder.Services.AddModule<OsHubApiCoreModule>(builder.Configuration);
			builder.Services.AddModule<OsHubApiDatabaseModule>(builder.Configuration);


			// Setup Logging
			builder.Logging.ClearProviders();
			builder.Logging.AddSimpleConsole(o => {
					o.ColorBehavior = LoggerColorBehavior.Enabled;
					o.SingleLine = true;
				}
			);

			// Convert Enum to String 
			builder.Services.AddControllers(o => {
						o.Conventions.Add(new ControllerDocumentationConvention());
						o.OutputFormatters.RemoveType<StringOutputFormatter>();
					}
				)
				.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
				.AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));


			builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; })
				.AddJsonProtocol(options => {
						options.PayloadSerializerOptions.IncludeFields = true;
						options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
					}
				);
			;


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