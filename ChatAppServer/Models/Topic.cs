using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatAppServer.Models
{
	public class Topic
	{
		[BsonId] [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		
		[BsonElement("Name")]
		public string Name { get; set; }
	}
}