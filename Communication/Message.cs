using System;

namespace Communication
{
	[Serializable]
	public class Message
	{
		private string _username;

		private string _content;

		private string _topic;
		
		/// <summary>
		/// Username of the user sending the message
		/// </summary>
		public string Username
		{
			get => _username;
			set => _username = value;
		}

		/// <summary>
		/// Content of the message
		/// </summary>
		public string Content
		{
			get => _content;
			set => _content = value;
		}

		/// <summary>
		/// Topic the message belongs to
		/// </summary>
		public string Topic
		{
			get => _topic;
			set => _topic = value;
		}

		public override string ToString()
		{
			return $"[{Topic}] @{Username}: {Content}";
		}
	}
	
	
	[Serializable]
	public class AuthenticatedMessage
	{
		private string _password;

		/// <summary>
		/// Password of the user sending the message.
		/// <br />
		/// Needed for authentication purposes
		/// </summary>
		public string Password
		{
			get => _password;
			set => _password = value;
		}
	}
}