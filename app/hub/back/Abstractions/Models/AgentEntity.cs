using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OsHub.Api.Abstractions.Transports;

namespace OsHub.Api.Abstractions.Models
{
	public sealed class AgentEntity : AgentRaw
	{
		[BsonId] public ObjectId Id { get; set; }
	}
}