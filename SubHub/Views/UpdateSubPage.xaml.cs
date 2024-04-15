using SubHub.Data;
using SubHub.Models;


namespace SubHub.Views;

public partial class UpdateSubPage : ContentPage
{
	int _subID;

    SubManageDatabase _database;
    public UpdateSubPage(int subID, SubManageDatabase database)
	{
		InitializeComponent();
		_subID = subID;
        _database = database;
        LoadSubscriptionAndPaymentData(subID);

    }

    private async void LoadSubscriptionAndPaymentData(int subscriptionID)
    {
        //calls to the database to get subscription and payment information based on the subscriptionID
        var subscription = await _database.GetSubscriptionAsync(subscriptionID);
        var paymentMethodItem = await _database.GetPaymentMethodForSub(subscription);

        //if else statement to help battle null entries.
        if (subscription != null)
        {
            //Assigning UI elements to show the information gotten from the database.
            NameLabel.Text = subscription.Name ?? "No name";
            DescriptionLabel.Text = subscription.Description ?? "No description";
            PriceLabel.Text = string.Format("Price: {0:C}", subscription.Price);
        }
        else
        {
            Console.WriteLine("Null");
        }

        //if else statement to battle null entries
        if (paymentMethodItem != null)
        {
            //Assigning UI elements to show the information gotten from the database.
            PaymentMethodTypeLabel.Text = paymentMethodItem.Type ?? "No payment";
            CardNumberLabel.Text = paymentMethodItem.CardNumber.ToString();
            CvCLabel.Text = paymentMethodItem.CvC.ToString();
        }
        else
        {
            Console.WriteLine("Null");
        }
    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        
    }
}