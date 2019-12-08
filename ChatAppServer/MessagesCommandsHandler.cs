using Communication;

namespace ChatAppServer
{
	public partial class ClientHandler
	{
		/// <summary>
		/// Handle the "say" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleSayCommand(Command command, Message response)
		{
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "You must be logged in to say something";
				return;
			}
			
			// Build message
			var topic = command.Arguments[0];
			response.Content = $"[{_user.Username}@{topic}] - {command.Arguments[1]}";

			// Explore list of all connected clients 
			foreach (var (connectedUser, tcpClient) in ConnectedClients)
			{
				if (connectedUser.Topics.Contains(topic))  // Check if the user we are checking is subscribed to the concerned topic
				{
					Net.SendMessage(tcpClient.GetStream(), response);
				}
			}
		}

		/// <summary>
		/// Handle the "create-topic" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleCreateTopicCommand(Command command, Message response)
		{
			
		}
	}
}