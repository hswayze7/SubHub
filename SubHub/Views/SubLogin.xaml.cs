using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
using SQLite;
using SubHub.ViewModels;
using System.Collections.ObjectModel;


namespace SubHub.Views;


public partial class SubLogin : ContentPage
{

    readonly SubManageDatabase database;
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
            int userId = await GetUserId(username, password);

			if (userId != -1)
			{
				await DisplayAlert("Success", "You have logged in", "OK");
				await Navigation.PushAsync(new SubPage(userId, database));	
			}
			else
			{
				await DisplayAlert("Error", "User not found", "OK");
			}
			
		}
		else
		{
			await DisplayAlert("Error", "Invalid username or password", "OK");
		}

	}
	private async void SaveBtn_Clicked(Object sender, EventArgs e)
	{
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;
		bool IsAuthenitcated = SubLogin.AuthenticateUser(username, password);
		if (IsAuthenitcated)
		{
			var newUser = new SubItem
			{
				Username = UsernameEntry.Text,
				Password = PasswordEntry.Text
				
			};

			await database.SaveUserAsync(newUser);
			//int userId = newUser.UserID;
			await DisplayAlert("Creation Success", "You have logged in", "OK");
		}
		else
		{
			await DisplayAlert("Error", "Cannot create user", "OK");
		}
	}
    private async Task<int> GetUserId(string username, string password)
    {
        var user = await database.GetUserByUsernameAndPassword(username, password);
        if (user != null)
        {
            return user.UserID;
        }
        else
        {
            
            return -1; 
        }
    }

    //Determines whether or not username and password has been entered.
    private static bool AuthenticateUser(string username, string password)
	{
		return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
	}

}