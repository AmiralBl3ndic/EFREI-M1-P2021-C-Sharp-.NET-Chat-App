using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class MessageRecordService
	{
		public static IMongoCollection<TopicMessageRecord> TopicMessagesCollection { get; set; }
		
		public static IMongoCollection<PrivateMessageRecord> PrivateMessagesCollection { get; set; }
	}
}