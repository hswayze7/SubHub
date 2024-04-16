using SubHub.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SubHub.Data;

namespace SubHub.ViewModels;

public class SubcViewModels : INotifyPropertyChanged
{
	private ObservableCollection<SubscriptionItem> _subs;

	public ObservableCollection<SubscriptionItem> Subs
	{
		get { return _subs;  }
		set
		{
			_subs = value;
			OnPropertyChanged(nameof(Subs));
		}
	}
    private int _userId;

    public int UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }

    readonly SubManageDatabase _database;
	public SubcViewModels()
	{
		_database = new SubManageDatabase();
		Subs = new ObservableCollection<SubscriptionItem>();
      //  _userId = userID;
	}

    public async Task AddSubs(SubscriptionItem item)
    {
        Subs.Add(item);
        OnPropertyChanged(nameof(Subs));
    }

    public async Task LoadSubscriptions(int userId)
    {
        try
        {
            
            Subs.Clear();
            var subscriptions = await _database.GetSubscriptionsByUserId(userId);

            foreach (var subscription in subscriptions)
            {
                var existingSubscription = Subs.FirstOrDefault(s => s.SubID == subscription.SubID);
                if (existingSubscription != null)
                {
                    // Update existing subscription
                    existingSubscription.Name = subscription.Name;
                    existingSubscription.Description = subscription.Description;
                    existingSubscription.Price = subscription.Price;
                    // Update other properties as needed
                }
                else
                {
                    // Add new subscription
                    Subs.Add(subscription);
                }
            }

            // Remove subscriptions that are no longer present
            var subscriptionsToRemove = Subs.Where(s => !subscriptions.Any(sub => sub.SubID == s.SubID)).ToList();
            foreach (var subscriptionToRemove in subscriptionsToRemove)
            {
                Subs.Remove(subscriptionToRemove);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading: ", ex.Message);
        }
    }

    public async Task ReloadSubs()
    {
        await LoadSubscriptions(_userId);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}