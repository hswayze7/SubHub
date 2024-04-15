using SubHub.Models;
using SubHub.Data;
using SQLite;

namespace SubHub.Views;

public partial class AddSubPage : ContentPage
{
	SubManageDatabase manageDatabase;
	private Action<SubscriptionItem> _updateCall; //Helps update the UI when adding / deleting items and going back to SubPage
	private int _loggedInUserID; //Holds the UserID that is currently signed in.
	
	public AddSubPage(Action<SubscriptionItem> updateCall, int loggedInUserID)
	{
		InitializeComponent();
		
		 manageDatabase = new SubManageDatabase();
		// BindingContext = this;
		_updateCall = updateCall;
		_loggedInUserID = loggedInUserID;
		
    }

	//Function for when user clicks on the save button. Assigns the user input from the Nameentry, descriptionentry, and price entry to entities of SubscriptionItem
	private async void SaveSubBtn_Clicked(object sender, EventArgs e)
	{
		//Create a new subscription from user input
		var subscription = new SubscriptionItem
		{
			Name = subNameEntry.Text,
			Description = subDescripEntry.Text,
			Price = (double)Convert.ToDecimal(priceEntry.Text),
			UserID = _loggedInUserID
		};

		//Create a new paymentItem from user input
		var payment = new PaymentMethodItem
		{
			Type = typeEntry.Text,
			CardNumber = int.Parse(paymentMethodEntry.Text),
			CvC = int.Parse(CvCEntry.Text),
			SubID = subscription.SubID

		};

		//Add the sub to the database
		await manageDatabase.AddSubAynsc(subscription);

		//Assigning foreign key SubID to the primary key SubID
		payment.SubID = subscription.SubID;

		///Add payment info to database
		await manageDatabase.AddPaymentMethod(payment);

		//Update the UI
		_updateCall?.Invoke(subscription);

		//Go back to the SubPage 
		await Navigation.PopAsync();
	}
}