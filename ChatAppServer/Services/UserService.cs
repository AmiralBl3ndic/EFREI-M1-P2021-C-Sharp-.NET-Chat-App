using System.Collections.Generic;
using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	/// <summary>
	/// Wrapper for all methods related to Users handling in database
	/// </summary>
	public static class UserService
	{
		public static IMongoCollection<User> UsersCollection { get; set; }

		/// <summary>
		/// Get a User record from its id
		/// </summary>
		/// <param name="id">ID of the User record to retrieve</param>
		/// <returns>User record retrieved from database if exists, null otherwise</returns>
		public static User Get(string id) => UsersCollection.Find(user => user.Id == id).FirstOrDefault();

		/// <summary>
		/// Get a User record from its username
		/// </summary>
		/// <param name="username">Username of the User record to retrieve</param>
		/// <returns>User record retrieved from database if exists, null otherwise</returns>
		public static User GetByUsername(string username) =>
			UsersCollection.Find(user => user.Username == username).FirstOrDefault();

		/// <summary>
		/// Get a list of all User database records from the database
		/// </summary>
		/// <returns>List of all User database records from the database</returns>
		public static List<User> GetAll() => UsersCollection.Find(user => true).ToList();

		/// <summary>
		/// Create a database User record
		/// </summary>
		/// <param name="user">User to create a database record for</param>
		/// <returns>Created user</returns>
		public static User Create(User user)
		{
			UsersCollection.InsertOne(user);
			return user;
		}

		/// <summary>
		/// Update a User database record
		/// </summary>
		/// <param name="id">id of the User database record to update</param>
		/// <param name="userToUpdate">User object representing the database record to update</param>
		public static void Update(string id, User userToUpdate) => UsersCollection.ReplaceOne(user => user.Id == id, userToUpdate);
		
		/// <summary>
		/// Remove a database User record
		/// </summary>
		/// <param name="userToRemove">User to remove the database record of</param>
		public static void Remove(User userToRemove) => UsersCollection.DeleteOne(user => user.Username == userToRemove.Username);

		/// <summary>
		/// Remove a database User record
		/// </summary>
		/// <param name="id">ID of the database User record to remove</param>
		public static void Remove(string id) => UsersCollection.DeleteOne(user => user.Id == id);
	}
}