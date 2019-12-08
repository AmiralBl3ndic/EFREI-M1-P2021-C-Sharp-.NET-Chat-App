using System.Text;
using ChatAppServer.Models;
using ChatAppServer.Services;
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
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}
			
			var topic = command.Arguments[0];

			// Check if topic exists
			if (!TopicsService.Exists(new Topic {Name = topic}))
			{
				response.Type = MessageType.Error;
				response.Content = $"Topic {topic} does not exist, you can create it with: create-topic {topic}";
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}
			
			// Check if user should be able to talk in the topic
			if (!_user.Topics.Contains(topic))
			{
				response.Type = MessageType.Error;
				response.Content = $"You cannot send messages in {topic} since you haven't joined it, you can join it with: join {topic}";
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}

			// Build message
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
		/// Handle the "list-topics" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleListTopicsCommand(Command command, Message response)
		{
			var sb = new StringBuilder();
			var topics = TopicsService.GetAll();

			sb.AppendLine("=== List of all the topics ===");
			foreach (var topic in topics)
			{
				sb.AppendLine($"- {topic.Name} {(_user != null && _user.Topics.Contains(topic.Name) ? "(joined)" : "")}");
			}
			sb.Append("====================");

			response.Type = MessageType.Message;
			response.Content = sb.ToString();
		}
		
		/// <summary>
		/// Handle the "create-topic" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleCreateTopicCommand(Command command, Message response)
		{
			// Check if user is logged in
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "You must be logged in to create topics";
				return;
			}

			var topic = new Topic {Name = command.Arguments[0]};

			// Check if topic already exists
			if (TopicsService.Exists(topic))
			{
				response.Type = MessageType.Error;
				response.Content = $"Topic {command.Arguments[0]} already exists, consider joining it.";
				return;
			}

			// Actually create the topic
			TopicsService.Create(topic);
			
			// Add the topic to the list of joined topics
			_user.Topics.Add(topic.Name);
			UserService.Update(_user.Id, _user);

			response.Type = MessageType.Info;
			response.Content = $"Topic {topic.Name} created and joined.";
		}

		/// <summary>
		/// Handle the "join" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleJoinCommand(Command command, Message response)
		{
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "You must be logged in to join a topic";
				return;
			}

			var topic = new Topic {Name = command.Arguments[0]};

			if (!TopicsService.Exists(topic))
			{
				response.Type = MessageType.Error;
				response.Content = $"Topic {topic.Name} does not exist, you can create it with: create-topic {topic.Name}";
				return;
			}
			
			// Update user's list of topics
			_user.Topics.Add(topic.Name);
			UserService.Update(_user.Id, _user);

			response.Type = MessageType.Info;
			response.Content = $"Joined topic {topic.Name}";
		}

		/// <summary>
		/// Handle the "leave" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleLeaveCommand(Command command, Message response)
		{
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "You must be logged in to leave a topic";
				return;
			}
			
			var topic = new Topic {Name = command.Arguments[0]};
			
			if (!TopicsService.Exists(topic))
			{
				response.Type = MessageType.Error;
				response.Content = $"Topic {topic.Name} does not exist, you can create it with: create-topic {topic.Name}";
				return;
			}

			if (!_user.Topics.Contains(topic.Name))
			{
				response.Type = MessageType.Error;
				response.Content = $"You haven't joined topic {topic.Name}";
				return;
			}
			
			// Update user's list of topics
			_user.Topics.Remove(topic.Name);
			UserService.Update(_user.Id, _user);

			response.Type = MessageType.Info;
			response.Content = $"Left topic {topic.Name}";
		}
		
		/// <summary>
		/// Handle the "leave" command
		/// </summary>
		/// <param name="command">Command to parse and execute</param>
		/// <param name="response">Message object to send to the user</param>
		private void HandleDmCommand(Command command, Message response)
		{
			if (_user == null)
			{
				response.Type = MessageType.Error;
				response.Content = "You must be logged in to send private messages";
				return;
			}

			var receiverUsername = command.Arguments[0];
			var messageContent = command.Arguments[1];


			// Send message to 
			foreach (var (connectedUser, tcpClient) in ConnectedClients)
			{
				if (connectedUser.Username != receiverUsername) continue;
				
				response.Type = MessageType.Message;
					
				// Build and send message to receiver
				response.Content = $"[From: {_user.Username}] - {messageContent}";
				Net.SendMessage(tcpClient.GetStream(), response);
					
				// Build and send message feedback to sender
				response.Content = $"[To: {receiverUsername}] - {messageContent}";
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}

			// From this point, the message has not been sent to the receiver
			
			// Check if receiver exists
			if (UserService.GetByUsername(receiverUsername) == null)
			{
				response.Type = MessageType.Error;
				response.Content = $"No user found with username \"{receiverUsername}\"";
				Net.SendMessage(_tcpClient.GetStream(), response);
				return;
			}

			// Store private message in database
			var privateMessageRecord = new PrivateMessageRecord
			{
				Receiver = receiverUsername,
				Sender = _user.Username,
				Content = messageContent
			};
			MessageRecordService.CreatePrivateMessage(privateMessageRecord);

			// Send message feedback
			response.Type = MessageType.Message;
			response.Content = $"[To: {receiverUsername}] - {messageContent}";
			Net.SendMessage(_tcpClient.GetStream(), response);
			
			// Send information that receiver is not connected and will read the message when connecting
			response.Type = MessageType.Info;
			response.Content = $"User {receiverUsername} is not connected. The message will be delivered upon next connection";
			Net.SendMessage(_tcpClient.GetStream(), response);
		}
	}
}