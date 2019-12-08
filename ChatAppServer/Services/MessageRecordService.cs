using System.Collections.Generic;
using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class MessageRecordService
	{
		public static IMongoCollection<TopicMessageRecord> TopicMessagesCollection { get; set; }
		
		public static IMongoCollection<PrivateMessageRecord> PrivateMessagesCollection { get; set; }
		
		public List<PrivateMessageRecord> GetPrivateMessages(User user)
		{
			return PrivateMessagesCollection
				.Find(record => record.Receiver == user.Username)
				.ToList();
		}
	}
}