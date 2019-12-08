using System;

namespace Communication
{
	[Serializable]
	public class Message
	{
		/// <summary>
		/// Type of message (normal message or error)
		/// </summary>
		public MessageType Type { get; set; }

		/// <summary>
		/// Content of the message
		/// </summary>
		public string Content { get; set; }

		public Message()
		{
			Type = MessageType.Message;
			Content = "";
		}
		
		public Message(string content)
		{
			Content = content;
		}

		public override string ToString()
		{
			if (Type == MessageType.Error)
			{
				return $"Error: {Content}";
			}

			return Content;
		}
	}
}