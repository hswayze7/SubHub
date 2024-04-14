using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
namespace SubHub.Views;
using SQLite;

public partial class SubLogin : ContentPage
{
	
	SubManageDatabase database;
	private int tempUserId;
	//SubItem subItem;

/*	public SubItem SubItem
	{
		get => BindingContext as SubItem;
		set => BindingContext = value;
	}*/

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
			/*SubManageDatabase database = new SubManageDatabase();
			SubItem user = new SubItem
			{
				Username = username,
				Password = password
			};*/

			//await database.AddNewUser(username, password);
			await database.CreateUser(new SubItem
			{
				Username = username,
				Password = password
			});

            var testUser = await database.GetUserByUsernameAndPassword(username, password);
            int userId = testUser.UserID;

            Console.WriteLine("Navigating to next page");
			await Shell.Current.GoToAsync($"{nameof(SubPage)}?userId={userId}");
			
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