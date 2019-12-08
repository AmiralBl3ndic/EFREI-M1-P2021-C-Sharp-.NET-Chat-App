using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class TopicsService
	{
		public IMongoCollection<Topic> TopicsCollection { get; set; }
		
		/// <summary>
		/// Check if a topic exists with the passed in name
		/// </summary>
		/// <param name="topicName">Name of the topic to check existence of</param>
		/// <returns>Whether a topic with given name exists</returns>
		public bool Exists(Topic topic)
		{
			Topic record = null;
			record = TopicsCollection.Find(item => item.Name == topic.Name).FirstOrDefault();
				
			return record != null;
		}


		/// <summary>
		/// Create a Topic record in database
		/// </summary>
		/// <param name="topic">Topic to create record of</param>
		/// <returns>Created topic, null if not created</returns>
		public Topic Create(Topic topic)
		{
			if (topic == null || Exists(topic))
			{
				return null;
			}
			
			TopicsCollection.InsertOne(topic);
			return topic;
		}

		/// <summary>
		/// Delete a Topic record in database
		/// </summary>
		/// <param name="topic">Topic to delete record of</param>
		/// <returns>Deleted topic, null if not deleted</returns>
		public Topic Delete(Topic topic)
		{
			if (topic == null)
			{
				return null;
			}

			TopicsCollection.DeleteOne(item => item.Name == topic.Name);
			return topic;
		}
	}
}