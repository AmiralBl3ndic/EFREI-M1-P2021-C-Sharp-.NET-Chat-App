using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatAppServer.Models
{
	public class TopicMessageRecord
	{
		[BsonId]
		public ObjectId Id { get; set; }
		
		[BsonElement("Topic")]
		public string Topic { get; set; }
		
		[BsonElement("Username")]
		public string Username { get; set; }
	}
}