using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace GUI_Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			
			TcpClient tcpClient = new TcpClient("127.0.0.1", 4321);
		}

		private void SubmitCommand(string commandText)
		{
			Console.WriteLine("Submitted: " + commandText);
		}

		private void UserInput_OnKeyDown(object sender, KeyEventArgs key)
		{
			if (key.Key == Key.Enter)
			{
				SubmitCommand(UserInputField.Text);
				UserInputField.Text = "";
			}
		}
		
		private void UserInput_OnSubmit(object sender, RoutedEventArgs e)
		{
			SubmitCommand(UserInputField.Text);
			UserInputField.Text = "";
		}
	}
}