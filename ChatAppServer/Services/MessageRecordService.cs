using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class MessageRecordService
	{
		public IMongoCollection<TopicMessageRecord> TopicMessagesCollection { get; set; }
	}
}