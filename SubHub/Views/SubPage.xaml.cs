using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
using System;
using System.Collections.ObjectModel;
using SQLite;
using SubHub.ViewModels;

namespace SubHub.Views;

public partial class SubPage : ContentPage
{
    readonly SubManageDatabase _database;
    int _userID;
    private readonly SubcViewModels _viewModels;
    public ObservableCollection<SubscriptionItem> Subs { get; set; } = new ObservableCollection<SubscriptionItem>();

    public SubPage(int userId, SubManageDatabase database)
    {
        InitializeComponent();
        //database = new SubManageDatabase();
        _database = database;
        _viewModels = new SubcViewModels();
        BindingContext = _viewModels;
        //_userID = userId;
        _userID = userId;

        //Helps update the UI when subscription are being added / deleted
        MessagingCenter.Subscribe<SubInfoPage, int>(this, "SubscriptionDeleted", async (sender, deletedSubscriptionID) =>
        {
            // Find and remove the deleted subscription from the Subs collection
            var deletedSubscription = Subs.FirstOrDefault(s => s.SubID == deletedSubscriptionID);
            if (deletedSubscription != null)
            {
                Subs.Remove(deletedSubscription);
            }
        });
    }

    //Bool to check if a sub is already showing / inside the collection Subs
    private bool isSubsLoaded = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Makes sure userID is not 0 and that subs are not showing
        /*if (_userID != 0 && !isSubsLoaded)
		{
			//Gets subscriptions based on the userID from the database
			var subscriptions = await _database.GetSubscriptionsByUserId(_userID);

			//Puts the subscription (added / deleted) into Subs to be displayed
			foreach (var subscription in subscriptions)
			{
				Subs.Add(subscription);
			}

			isSubsLoaded = true;
		}
		*/
        await _viewModels.ReloadSubs();
    }

    //User can click on a "Add New Subscription" button which will navigate to another page for the user to add information about the subscription.
    private async void AddNewSubBtn_Clicked(object sender, EventArgs e)
    {
        var _viewModel = (SubcViewModels)BindingContext;
        await Navigation.PushAsync(new AddSubPage(UpdateSubUI, _userID));
        //await Navigation.PushAsync(new AddSubPage(UpdateSubUI, _userID));
    }

    //Function to help with updating UI regarding changes to Subs
    private void UpdateSubUI(SubscriptionItem item)
    {
        Subs.Add(item);
    }

    //Allows user to interact with the Listview with a tap, navigating to another page to delete or update that specific subscription
    private async void SubsListView_Tapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is SubscriptionItem selectedSubscription)
        {

            SubInfoPage subInfoPage = new SubInfoPage(selectedSubscription.SubID, _database);
            await Navigation.PushAsync(subInfoPage);

            ((ListView)sender).SelectedItem = null;
        }
    }
}