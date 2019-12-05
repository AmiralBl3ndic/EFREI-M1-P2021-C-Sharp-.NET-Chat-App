﻿using MongoDB.Bson;
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

		public void HashPassword()
		{
			Password = BCrypt.Net.BCrypt.HashPassword(Password, 12);
		}
		
		public void HashPassword(string passwordToHash)
		{
			Password = BCrypt.Net.BCrypt.HashPassword(passwordToHash, 12);
		}

		public override string ToString()
		{
			return $"User(Username: {Username}, Password: {Password})";
		}
	}
}
