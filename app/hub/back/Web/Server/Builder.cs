using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OsAgent.Api.Abstractions.Configs;
using OsAgent.Api.Abstractions.Enums;
using OsAgent.Api.Abstractions.Helpers;
using OsAgent.Api.Abstractions.Interfaces.Injections;
using OsAgent.Api.Adapters.Injections;
using OsAgent.Api.Core.Injections;
using OsAgent.Api.Web.Filters;
using OsAgent.Api.Web.Utils;

namespace OsAgent.Api.Web.Server;

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
		builder.Services.Configure<AppConfig>(config => { config.Platform = builder.Configuration["Platform"] == "Windows" ? ApplicationPlatform.Windows : ApplicationPlatform.Linux; });


		// Setup Logging
		builder.Logging.ClearProviders();
		builder.Logging.AddSimpleConsole(o => {
			o.ColorBehavior = LoggerColorBehavior.Enabled;
			o.SingleLine = true;
				
		});

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