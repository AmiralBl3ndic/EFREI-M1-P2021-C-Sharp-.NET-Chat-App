using System;

namespace Communication
{
	[Serializable]
	public class Message
	{
		private string _content;
		/// <summary>
		/// Content of the message
		/// </summary>
		public string Content
		{
			get => _content;
			set => _content = value;
		}
		
		public Message(string username, string topic, string content)
		{
			Content = content;
		}

		public override string ToString()
		{
			return Content;
		}
	}
}