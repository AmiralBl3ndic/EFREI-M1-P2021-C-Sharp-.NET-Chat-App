using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatAppServer.Models
{
	public class User
	{
		[BsonId] [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		
		[BsonElement("Username")]
		public string Username { get; set; }

		[BsonElement("Password")] private string _password;
		
		public string Password
		{
			get => _password;
			set => _password = BCrypt.Net.BCrypt.HashPassword(value, workFactor: 12);
		}
	}
}
