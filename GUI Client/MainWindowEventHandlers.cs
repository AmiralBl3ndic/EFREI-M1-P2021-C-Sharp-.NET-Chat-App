using System.Windows;
using System.Windows.Input;

namespace GUI_Client
{
	public partial class MainWindow : Window
	{
		private void UserInput_OnKeyDown(object sender, KeyEventArgs key)
		{
			// Process only if current input is at least 2 characters long
			if (key.Key != Key.Enter || UserInputField.Text.Length < 2) return;
			
			Dispatcher.Invoke(() =>
			{
				SubmitCommand(UserInputField.Text);
				UserInputField.Text = "";
			});
		}
		
		private void UserInput_OnSubmit(object sender, RoutedEventArgs e)
		{
			// Process only if current input is at least 2 characters long
			if (UserInputField.Text.Length < 2) return;

			Dispatcher.Invoke(() =>
			{
				SubmitCommand(UserInputField.Text);
				UserInputField.Text = "";
			});
		}
	}
}