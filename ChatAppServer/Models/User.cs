using System;
using System.Collections.Generic;
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
		
		[BsonElement("Password")]
		public string Password { get; set; }
		
		[BsonElement("Topics")]
		public List<string> Topics { get; set; }
		
		[BsonElement("LastSeenAt")]
		public DateTime LastSeenAt { get; set; }

		public User()
		{
			Username = null;
			Password = null;
			Topics = new List<string>();
		}

		public void HashPassword()
		{
			Password = BCrypt.Net.BCrypt.HashPassword(Password, 12);
		}
		
		public void HashPassword(string passwordToHash)
		{
			Password = passwordToHash;
			HashPassword();
		}

		public override string ToString()
		{
			return $"User(Username: {Username}, Password: {Password})";
		}
	}
}
