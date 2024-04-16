using SubHub.Data;
using SubHub.Models;


namespace SubHub.Views;

public partial class UpdateSubPage : ContentPage
{
	int _subID;

    readonly SubManageDatabase _database;
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
        var remind = await _database.GetReminderBySub(subscriptionID);

        //if else statement to help battle null entries.
        if (subscription != null)
        {
            //Assigning UI elements to show the information gotten from the database.
            NameLabel.Text = subscription.Name ?? "No name";
            DescriptionLabel.Text = subscription.Description ?? "No description";
            PriceLabel.Text = subscription.Price.ToString();
            //await _database.UpdateSub(subscription);
        }
        else
        {
            Console.WriteLine("Null");
        }

        //if else statement to battle null entries
        if (paymentMethodItem != null)
        {
            //paymentMethodItem.SubID = _subID;
            //Assigning UI elements to show the information gotten from the database.
            PaymentMethodTypeLabel.Text = paymentMethodItem.Type ?? "No payment";
            CardNumberLabel.Text = paymentMethodItem.CardNumber.ToString();
            CvCLabel.Text = paymentMethodItem.CvC.ToString();
            //await _database.UpdatePayment(paymentMethodItem.SubID, paymentMethodItem);
        }
        else
        {
            Console.WriteLine("Null");
        }

        if (remind != null)
        {
            RemindLabel.Text = remind.ReminderMessage ?? "No message";
        }
        else
        {
            Console.WriteLine("Null");
        }
    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        var subscription = await _database.GetSubscriptionAsync(_subID);
        if (subscription == null)
        {
            await DisplayAlert("Error", "Subscription not found", "OK");
            return;
        }

        var updatedSub = new SubscriptionItem
        {
            SubID = _subID,
            UserID = subscription.UserID,
            Name = NameLabel.Text,
            Description = DescriptionLabel.Text,
            Price = (Double)Convert.ToDecimal(PriceLabel.Text)
        };

        var updatePayment = new PaymentMethodItem
        {
            SubID = _subID,
            Type = PaymentMethodTypeLabel.Text,
            CardNumber = int.Parse(CardNumberLabel.Text),
            CvC = int.Parse(CvCLabel.Text)
        };

        var updateRemind = new ReminderItem
        {
            SubID = _subID,
            ReminderMessage = RemindLabel.Text
        };

        await _database.AddSubAynsc(updatedSub);
        //await _database.SavePaymentAsync(updatePayment);
        await _database.UpdatePayment(_subID, updatePayment);

        await _database.UpdateReminder(_subID, updateRemind);

        await DisplayAlert("Success", "Subscription updated successfully", "OK");
        MessagingCenter.Send(this, "SubscriptionUpdated");


        await Navigation.PopAsync();
    }
}