using SubHub.Models;
using SubHub.Data;

using SQLite;



namespace SubHub.Views;

public partial class AddSubPage : ContentPage
{
	SubManageDatabase manageDatabase;
	private Action<SubscriptionItem> _updateCall;
	
	public AddSubPage(Action<SubscriptionItem> updateCall)
	{
		InitializeComponent();
		
		 manageDatabase = new SubManageDatabase();
		// BindingContext = this;
		_updateCall = updateCall;
    }

	//Function for when user clicks on the save button. Assigns the user input from the Nameentry, descriptionentry, and price entry to entities of SubscriptionItem
	private async void SaveSubBtn_Clicked(object sender, EventArgs e)
	{
		var subscription = new SubscriptionItem
		{
			Name = subNameEntry.Text,
			Description = subDescripEntry.Text,
			Price = (double)Convert.ToDecimal(priceEntry.Text)
		};

		//Calls AddSubAsync to add the subscription to the database
		await manageDatabase.AddSubAynsc(subscription);

		//Update the UI
		_updateCall?.Invoke(subscription);

		//Go back to the SubPage 
		await Navigation.PopAsync();
	}
}