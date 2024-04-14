using Microsoft.Maui.Controls;
using SubHub.Models;
using SubHub.Data;
using System;
using System.Collections.ObjectModel;

using SQLite;



namespace SubHub.Views;


public partial class SubPage : ContentPage
{
	//SubscriptionItem subscriptionItem;
    SubManageDatabase database;
	private int _userID;
    public ObservableCollection<SubscriptionItem> Subs { get; set; } = new ObservableCollection<SubscriptionItem>();
  
//	public ObservableCollection<SubManageDatabase> Subs { get; set; } = new();
	public SubPage()
	{
		InitializeComponent();
		database = new SubManageDatabase();
		BindingContext = this;
		//_userID = userId;
		

	

    }

	protected override async void OnAppearing()
	{
		Subs.Clear();
		base.OnAppearing();

       
       
		var subscriptions = await database.GetSubscriptionsByUserId(_userID);
      
		
			Subs.Clear();
			foreach (var subscription in subscriptions)
			{
				Subs.Add(subscription);
			}
		
	}

	//User can click on a "Add New Subscription" button which will navigate to another page for the user to add information about the subscription.
	private async void AddNewSubBtn_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddSubPage(UpdateSubUI));
	}

	private void UpdateSubUI(SubscriptionItem item)
	{
		 Subs.Add(item);
	}
}