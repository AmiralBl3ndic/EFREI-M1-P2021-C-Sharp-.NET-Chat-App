﻿using ChatAppServer.Models;
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
	}
}