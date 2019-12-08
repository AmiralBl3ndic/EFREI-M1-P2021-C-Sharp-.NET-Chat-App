using System.Collections.Generic;
using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class MessageRecordService
	{
		public static IMongoCollection<TopicMessageRecord> TopicMessagesCollection { get; set; }
		
		public static IMongoCollection<PrivateMessageRecord> PrivateMessagesCollection { get; set; }
		
		/// <summary>
		/// Get the list of unread private messages of a given user
		/// </summary>
		/// <param name="user">User to get the list of messages for</param>
		/// <returns>List of unread messages for passed user</returns>
		public static List<PrivateMessageRecord> GetPrivateMessages(User user)
		{
			return PrivateMessagesCollection
				.Find(record => record.Receiver == user.Username)
				.ToList();
		}
		
		/// <summary>
		/// Clear the private messages of a given user
		/// </summary>
		/// <param name="user">User to clear the private messages of</param>
		public static void ClearPrivateMessages(User user)
		{
			PrivateMessagesCollection.DeleteMany(record => record.Receiver == user.Username);
		}


		/// <summary>
		/// Create a private message record
		/// </summary>
		/// <param name="privateMessageRecord">Record to insert in the database</param>
		public static void CreatePrivateMessage(PrivateMessageRecord privateMessageRecord)
		{
			PrivateMessagesCollection.InsertOne(privateMessageRecord);
		}
	}
}