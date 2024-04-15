using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
using System;
using System.Collections.ObjectModel;

using SQLite;

namespace SubHub.Views;

public partial class SubInfoPage : ContentPage
{

    int _subscriptionID;

    SubManageDatabase _database;

    public SubInfoPage(int subscriptionID, SubManageDatabase database)
    {
        InitializeComponent();
        this._database = database;
        //BindingContext = this;
        this._subscriptionID = subscriptionID;
        LoadSubscriptionAndPaymentData(subscriptionID);
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

    //Delete button for user to delete a subscription if they wanted.
    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        //Displays an alert to confirm if the user really wants to delete said item
        if (await DisplayAlert("Confirmation", "Are you sure you want to delete?", "Yes", "Cancel"))
        {
            //Making sure the database is not null and we have the subscriptionID to get the subscription that is supposed to be deleted.
            if (_database != null && _subscriptionID != 0)
            {
                await _database.DeleteSubAsync(_subscriptionID);
                MessagingCenter.Send(this, "SubscriptionDeleted", _subscriptionID); //Helps with updating the UI
            }
            await Navigation.PopAsync();
        }
    }

    private async void UpdateBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UpdateSubPage(_subscriptionID, _database));
    }
}

