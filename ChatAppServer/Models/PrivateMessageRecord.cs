using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatAppServer.Models
{
	public class PrivateMessageRecord
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("SenderUsername")]
		public string Sender { get; set; }
		
		[BsonElement("ReceiverUsername")]
		public string Receiver { get; set; }
		
		[BsonElement("Content")]
		public string Content { get; set; }
	}
}