using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Models;

public class ReminderItem
{
   
    
        [PrimaryKey, AutoIncrement]
        public int reminderID { get; set; }
        public string ReminderMessage { get; set; }
        public bool IsCompleted { get; set; }
        //public Date ReminderDate {get; set;}


    //Foreign Keys
    public int userID { get; set; }
    public int subID { get; set; }
    
    public SubItem User { get; set; }
    public SubscriptionItem Subscription { get; set; }
    
}
