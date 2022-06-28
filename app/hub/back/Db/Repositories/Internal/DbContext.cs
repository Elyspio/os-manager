using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OsHub.Api.Db.Configs;

namespace OsHub.Api.Db.Repositories.Internal;

public class MongoContext
{
	public MongoContext(IConfiguration configuration)
	{
		var conf = new DbConfig();
		configuration.GetSection(DbConfig.Section).Bind(conf);

		var url = $"mongodb://{conf.Username}:{conf.Password}@{conf.Host}:{conf.Port}/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-256";

		Console.WriteLine($"Connecting to Database '{conf.Database}' on {url}");
		var client = new MongoClient(url);
		MongoDatabase = client.GetDatabase(conf.Database);

		var pack = new ConventionPack
		{
			new EnumRepresentationConvention(BsonType.String)
		};
		ConventionRegistry.Register("EnumStringConvention", pack, t => true);
		BsonSerializer.RegisterSerializationProvider(new EnumAsStringSerializationProvider());
	}

	/// <summary>
	///     Récupération de la IMongoDatabase
	/// </summary>
	/// <returns></returns>
	public IMongoDatabase MongoDatabase { get; }
}