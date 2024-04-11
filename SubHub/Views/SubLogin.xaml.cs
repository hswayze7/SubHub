using Microsoft.Maui.Controls;


namespace SubHub.Views;

public partial class SubLogin : ContentPage
{
	public SubLogin()
	{
		InitializeComponent();
	}

	private async void SignInBtn_Clicked(object sender, EventArgs e)
	{
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;

		bool isAuthenticated = AuthenticateUser(username, password);

		if (isAuthenticated)
		{
			await DisplayAlert("Success", "You have logged in", "OK");
		}
		else
		{
			await DisplayAlert("Error", "Invalid username or password", "OK");
		}

	}

	private bool AuthenticateUser(string username, string password)
	{
		return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
	}
}