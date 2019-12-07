using System;

namespace Communication
{
	[Serializable]
	public class Message
	{
		
		
		/// <summary>
		/// Content of the message
		/// </summary>
		public string Content { get; set; }
		
		public Message(string content)
		{
			Content = content;
		}

		public override string ToString()
		{
			return Content;
		}
	}
}