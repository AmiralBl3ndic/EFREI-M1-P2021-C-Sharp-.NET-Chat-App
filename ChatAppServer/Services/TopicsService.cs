using System.Collections.Generic;
using ChatAppServer.Models;
using MongoDB.Driver;

namespace ChatAppServer.Services
{
	public class TopicsService
	{
		public static IMongoCollection<Topic> TopicsCollection { get; set; }
		
		/// <summary>
		/// Check if a topic exists with the passed in name
		/// </summary>
		/// <param name="topic">Topic to check existence of</param>
		/// <returns>Whether a topic with given name exists</returns>
		public static bool Exists(Topic topic)
		{
			Topic record = null;
			record = TopicsCollection.Find(item => item.Name == topic.Name).FirstOrDefault();
				
			return record != null;
		}
		
		/// <summary>
		/// Get the list of all the topics
		/// </summary>
		/// <returns>List of all the topics</returns>
		public static List<Topic> GetAll()
		{
			return TopicsCollection.Find(record => true).ToList();
		}


		/// <summary>
		/// Create a Topic record in database
		/// </summary>
		/// <param name="topic">Topic to create record of</param>
		/// <returns>Created topic, null if not created</returns>
		public static Topic Create(Topic topic)
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