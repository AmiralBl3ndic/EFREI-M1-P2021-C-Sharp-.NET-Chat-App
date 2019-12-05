using System;
using System.Net.Sockets;
using Communication;

namespace ChatAppClient
{
	class ChatAppClient
	{
		static void Main(string[] args)
		{
			TcpClient tcpClient = new TcpClient("127.0.0.1", 4321);
		}
	}
}