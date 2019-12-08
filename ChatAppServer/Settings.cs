namespace ChatAppServer
{
	public static class Settings
	{
		public static readonly string MongoConnectionString = "mongodb+srv://cs_dotnet_project:EFREI-2021-SE-4LIFE@efrei-m1-2021-nrato.gcp.mongodb.net/test?retryWrites=true&w=majority";
		public static readonly string MongoDatabaseName = "testDB";
		public static readonly string MongoUsersCollectionName = "Users";
		public static readonly string MongoTopicsCollectionName = "Topics";
		public static readonly string MongoMessagesCollectionName = "Topics.Messages";
		public static readonly string MongoPrivateMessagesCollectionName = "PrivateMessages";
	}
}
