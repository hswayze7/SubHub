
using SQLite;
using SubHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Data;

public class SubManageDatabase
{
     SQLiteAsyncConnection _connection;
    public SubManageDatabase()
    {
        
    }

    async Task Init()
    {
        if (_connection is not null)
        {
            return;
        }

        //Creation of the tables so far.
        _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await _connection.CreateTableAsync<SubItem>();
        var result2 = await _connection.CreateTableAsync<SubscriptionItem>();
        var result3 = await _connection.CreateTableAsync<PaymentMethodItem>();
        var result4 = await _connection.CreateTableAsync<ReminderItem>();
    }
    public async Task<ReminderItem> GetReminderAsync(ReminderItem reminder)
    {
        await Init();
        return await _connection.Table<ReminderItem>().FirstOrDefaultAsync(r => r.SubID == reminder.SubID);
    }
    public async Task UpdateReminder(int subscriptionID, ReminderItem reminderItem)
    {
        await Init();

        
        var existingReminderItem = await _connection.Table<ReminderItem>()
                                                    .FirstOrDefaultAsync(p => p.SubID == subscriptionID);

        if (existingReminderItem != null)
        {
            
            existingReminderItem.ReminderMessage = reminderItem.ReminderMessage;
        

            
            await _connection.UpdateAsync(existingReminderItem);
        }
        else
        {
            
            throw new Exception($"No reminder found for subscription with ID: {subscriptionID}");
        }
    }
    public async Task<ReminderItem> GetReminderBySub(int subId)
    {
        await Init();
        return await _connection.Table<ReminderItem>().FirstOrDefaultAsync(s => s.SubID == subId);
    }
    public async Task<int> AddReminder(ReminderItem item)
    {
        await Init();
        if (item.ReminderID != 0)
        {
            return await _connection.UpdateAsync(item);
        }
        else
        {
            return await _connection.InsertAsync(item);
        }

    }
    public async Task<int> SavePaymentAsync(PaymentMethodItem sub)
    {
        await Init();
        if (sub.PaymentMethodID != 0)
        {
            return await _connection.UpdateAsync(sub);
        }
        else
        {
            return await _connection.InsertAsync(sub);
        }
    }
    //Update payment
    public async Task UpdatePayment(int subscriptionID, PaymentMethodItem updatedPaymentMethod)
    {
        await Init();

        
        var existingPaymentMethod = await _connection.Table<PaymentMethodItem>()
                                                    .FirstOrDefaultAsync(p => p.SubID == subscriptionID);

        if (existingPaymentMethod != null)
        {
           
            existingPaymentMethod.Type = updatedPaymentMethod.Type;
            existingPaymentMethod.CardNumber = updatedPaymentMethod.CardNumber;
            existingPaymentMethod.CvC = updatedPaymentMethod.CvC;

          
            await _connection.UpdateAsync(existingPaymentMethod);
        }
        else
        {
            
            throw new Exception($"No payment method found for subscription with ID: {subscriptionID}");
        }
    }
    //Update subscription
    public async Task UpdateSub(SubscriptionItem item)
    {
        await Init();
        await _connection.UpdateAsync(item);
    }

    //Gets paymentforSubs
    public async Task<PaymentMethodItem> GetPaymentMethodForSub(SubscriptionItem sub)
    {
        await Init();
        return await _connection.Table<PaymentMethodItem>().FirstOrDefaultAsync(p => p.SubID == sub.SubID);
    }

    //Adds payment info for a subscription
    public async Task<int> AddPaymentMethod(PaymentMethodItem item)
    {
        await Init();
        if (item.PaymentMethodID != 0)
        {
            return await _connection.UpdateAsync(item);
        }
        else
        {
            return await _connection.InsertAsync(item);
        }
        
    }
    //Deletes a subscription
    public async Task DeleteSubAsync(int subID)
    {
        await Init();
        var subToDel = await _connection.Table<SubscriptionItem>().FirstOrDefaultAsync(s => s.SubID == subID);
        if (subToDel != null)
        {
            await _connection.DeleteAsync(subToDel);

            
        }
       
    }
    //Gets subscription
    public async Task<SubscriptionItem> GetSubscriptionAsync(int SubID)
    {
        await Init();
        return await _connection.Table<SubscriptionItem>().FirstOrDefaultAsync(s  => s.SubID == SubID);
    }

    //Function that will get subscriptions based on UserID
    public async Task<List<SubscriptionItem>> GetSubscriptionsByUserId(int userId)
    {
        await Init();
        return await _connection.Table<SubscriptionItem>().Where(s =>  s.UserID == userId).ToListAsync();
    }
 /*   public async Task<List<SubscriptionItem>> GetSubscriptionsByUserId(int userId)
    {
        await Init();
        return await _connection.Table<SubscriptionItem>().Where(s => s.UserID == userId).ToListAsync();
    }
*/
    //Obtains the user via password and username
    public async Task<SubItem> GetUserByUsernameAndPassword(string username, string password)
    {
        await Init();
        return await _connection.Table<SubItem>().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }
    

    //Function to Add a subcription
    public async Task<int> AddSubAynsc(SubscriptionItem sub)
    {
        await Init();
        if (sub.SubID != 0)
        {
            return await _connection.UpdateAsync(sub);
        }
        else
        {
            return await _connection.InsertAsync(sub);
            
        }
    }

    //Add subscription for user.
    public async Task AddSubscriptionForUser(int userId, SubscriptionItem subscription)
    {
        await Init();
        // Assign the user ID to the subscription
        subscription.UserID = userId;

        // Insert the subscription into the database
        await _connection.InsertAsync(subscription);
    }
    //Create a new user
    public async Task<int> CreateUser(SubItem user)
    {
        await Init();
        return await _connection.InsertAsync(user);
    }

    //Add new user
    public async Task AddNewUser(string name, string pass)
    {
        int result = 0;
        try
        {
            await Init();
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("valid name required");
            }
            result = await _connection.InsertAsync(new SubItem { Username = name, Password = pass });

            Console.WriteLine("{0} records(s) added [Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to add");
        }
    }

    //Saves the user into the database (Create user)
    public async Task<int> SaveUserAsync(SubItem sub)
    {
        await Init();
        if (sub.UserID != 0)
        {
            return await _connection.UpdateAsync(sub);
        }
        else
        {
            return await _connection.InsertAsync(sub);
        }
    }

    //All get all subscriptions for the user.
    public async Task<List<SubscriptionItem>> GetSubsAsync()
    {
        await Init();
        return await _connection.Table<SubscriptionItem>().ToListAsync();
    }

    //Task to get a subscription for the user based on the ID
    public async Task<SubItem> GetSubAsync(int SubID)
    {
        await Init();
        return await _connection.Table<SubItem>().Where(i => i.UserID == SubID).FirstOrDefaultAsync();
    }

    //Task that will save a subscription. Will check if the ID is already assigned or not to determine if the subscription needs to be added or updated.
    public async Task<int> SaveSubAsync(SubscriptionItem sub)
    {
        await Init();
        if (sub.SubID != 0)
        {
            return await _connection.UpdateAsync(sub);
        }
        else
        {
            return await _connection.InsertAsync(sub);
        }
    }


    //Task to delete a subscription if needed. Will get the ID for the sub and delete sub based on the ID.
  /*  public async Task<int> DeleteSubAsync(SubscriptionItem sub)
    {
        await Init();
        return await _connection.DeleteAsync(sub);
    }*/
}
