using System;
using System.Collections.Generic;
using System.Linq;

namespace Communication
{
	public class Command
	{
		/// <summary>
		/// Dictionary mapping valid command names to their minimum number of arguments
		/// </summary>
		public static readonly Dictionary<string, int> ValidCommands = new Dictionary<string, int>()
		{
			{"help", 0},          // help
			{"login", 2},         // login {username} {password}
			{"register", 2},      // register {username} {password}
			{"list-topics", 0},   // list-topics
			{"create-topic", 1},  // create-topic {topicName}
			{"join", 1},          // join {topicName}
			{"leave", 1},         // leave {topicName}
			{"say", 2},           // say {topicName} {messageContent}
			{"mp", 2},            // mp {userName} {messageContent}
			{"logout", 0}         // logout
		};

		public string Name { get; set; }
		
		public string[] Arguments { get; set; }


		public static Command Prepare(string input)
		{
			if (input.Length == 0) return null; // No need to perform preparation if no input 

			string[] parts = input.Split(" ");  // Split input by spaces


			if (!ValidCommands.Keys.Contains(parts[0])) return null; // Check if command is known

			if (parts.Length - 1 < ValidCommands[parts[0]]) return null; // Check if enough arguments were provided
			
			// Now, we just have to build the command, the command is valid and has enough arguments 

			var command = new Command {Name = parts[0], Arguments = new string[ValidCommands[parts[0]]]};

			if (ValidCommands[command.Name] == 0) return command;
			
			// Get all needed arguments
			var i = 1;
			for (; i <= ValidCommands[command.Name] - 1; i++)
			{
				command.Arguments[i - 1] = parts[i];
			}
  
			// Merge remaining parts as the last argument
			var remainingParts = new ArraySegment<string>(parts, i, parts.Length - i);
			command.Arguments[i-1] = string.Join(" ", remainingParts);

			return command;
		}
	}
}