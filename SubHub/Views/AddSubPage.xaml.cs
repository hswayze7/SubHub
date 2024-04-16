using SubHub.Models;
using SubHub.Data;
using SQLite;
using SubHub.ViewModels;

namespace SubHub.Views;

public partial class AddSubPage : ContentPage
{
    readonly SubManageDatabase manageDatabase;
	//private Action<SubscriptionItem> _updateCall; //Helps update the UI when adding / deleting items and going back to SubPage
	private int _loggedInUserID; //Holds the UserID that is currently signed in.
	Action<SubscriptionItem> _updateCall;
// private SubcViewModels _viewModels;

    public AddSubPage(Action<SubscriptionItem> updateCall, int loggedInUserID)
	{
		InitializeComponent();
		
		 manageDatabase = new SubManageDatabase();
		// BindingContext = this;
		_updateCall = updateCall;
		_loggedInUserID = loggedInUserID;
		//_viewModels = subcView;
		
    }

	//Function for when user clicks on the save button. Assigns the user input from the Nameentry, descriptionentry, and price entry to entities of SubscriptionItem
	private async void SaveSubBtn_Clicked(object sender, EventArgs e)
	{
		try
		{


			//Create a new subscription from user input
			var subscription = new SubscriptionItem
			{
				
				Name = subNameEntry.Text,
				Description = subDescripEntry.Text,
				Price = (double)Convert.ToDecimal(priceEntry.Text),
				//UserID = _loggedInUserID
			};
			//await manageDatabase.AddSubAynsc(subscription);

			//Create a new paymentItem from user input
			var payment = new PaymentMethodItem
			{
				Type = typeEntry.Text,
				CardNumber = int.Parse(paymentMethodEntry.Text),
				CvC = int.Parse(CvCEntry.Text),
				SubID = subscription.SubID

			};

			// await manageDatabase.UpdatePayment(payment.SubID, payment);
			var remind = new ReminderItem
			{
				ReminderMessage = reminderMessageEntry.Text,
				SubID = subscription.SubID,
				//UserID = _loggedInUserID
			};

			//Add the sub to the database
			await manageDatabase.AddSubAynsc(subscription);
			//await manageDatabase.AddSubscriptionForUser(_loggedInUserID, subscription);
			//Assigning foreign key SubID to the primary key SubID
			payment.SubID = subscription.SubID;

            ///Add payment info to database
            await manageDatabase.AddPaymentMethod(payment);

			remind.SubID = subscription.SubID;
			await manageDatabase.AddReminder(remind);

            //Update the UI
			//UpdateSubUI(subscription);
            _updateCall?.Invoke(subscription);
           // await _viewModels.AddSubs(subscription);
			//await LoadSubscriptions(_loggedInUserID);
            //Go back to the SubPage 
            await Navigation.PopAsync();
		}
		catch (Exception ex)
		{	
			await DisplayAlert("Error", $"Failed to add subs: {ex.Message}", "OK");
		
		
		}
	}
}