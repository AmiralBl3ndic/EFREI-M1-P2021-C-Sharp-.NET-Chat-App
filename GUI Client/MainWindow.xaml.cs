using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Communication;

namespace GUI_Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TcpClient _tcpClient = null;
		
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			
			_tcpClient = new TcpClient(FindResource("ServerIp").ToString(), Int32.Parse(FindResource("ServerPort").ToString()));
		}

		private static void SubmitCommand(string commandText)
		{
			var command = Command.Prepare(commandText);
		}

		private void UserInput_OnKeyDown(object sender, KeyEventArgs key)
		{
			// Process only if current input is at least 2 characters long
			if (key.Key != Key.Enter || UserInputField.Text.Length < 2) return;
			
			SubmitCommand(UserInputField.Text);
			UserInputField.Text = "";
		}
		
		private void UserInput_OnSubmit(object sender, RoutedEventArgs e)
		{
			// Process only if current input is at least 2 characters long
			if (UserInputField.Text.Length < 2) return;
			
			SubmitCommand(UserInputField.Text);
			UserInputField.Text = "";
		}
	}
}