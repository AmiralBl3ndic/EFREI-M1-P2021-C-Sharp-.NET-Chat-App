using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GUI_Client
{
	public partial class MainWindow : Window
	{
		private void ListenToServer()
		{
			// Display a "connected to server" message
			Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
			{
				var connectedMessage = new TextBlock() {Text = $"Connected to server {(FindResource("ServerIp"))}:{(FindResource("ServerPort"))}"};
				connectedMessage.FontStyle = FontStyles.Italic;
				MessagesPanel.Children.Add(connectedMessage);
			}));
			
			// Listen to messages from server
			while (true)
			{
				// Sample message
				Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
				{
					var sampleMessage = new TextBlock() {Text = "I am a sample message"};
					MessagesPanel.Children.Add(sampleMessage);
				}));
				
				Thread.Sleep(1500);
			}
		}
	}
}