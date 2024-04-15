using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
using SQLite;


namespace SubHub.Views;


public partial class SubLogin : ContentPage
{
	
	SubManageDatabase database;
	private int tempUserId;

	public SubLogin(SubManageDatabase manageDatabase)
	{
		InitializeComponent();
		database = manageDatabase;
	}

	//Click command for signing in via button.
	private async void SignInBtn_Clicked(object sender, EventArgs e)
	{
		//String variables to hold what the user entered into the entries.
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;

		//Calls bool isAuthenticated which calls onto AuthenicateUser to check that username and password were entered
		bool isAuthenticated = SubLogin.AuthenticateUser(username, password);

		//Checks to see if user entered username and password and will display a message whether or not the login was a success.
		if (isAuthenticated)
		{
			await DisplayAlert("Success", "You have logged in", "OK");
			//Create a new User, saving password and username to that user.
			var user = new SubItem
			{
				Username = username,
				Password = password
			};

			//await database.AddNewUser(username, password);
			/*await database.CreateUser(new SubItem
			{
				Username = username,
				Password = password
			});*/

			//Calling the database to create the user above
			await database.CreateUser(user);

			//Getting user by username and password and assigning the UserID to userId which will be used to display the subscription on the next page for that specific user.
			var testUser = await database.GetUserByUsernameAndPassword(username, password);
            int userId = testUser.UserID;

            Console.WriteLine("Navigating to next page");
			//await Shell.Current.GoToAsync($"{nameof(SubPage)}?userId={userId}");
			await Navigation.PushAsync(new SubPage(userId));
			
		}
		else
		{
			await DisplayAlert("Error", "Invalid username or password", "OK");
		}

	}

	//Determines whether or not username and password has been entered.
	private static bool AuthenticateUser(string username, string password)
	{
		return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
	}

}