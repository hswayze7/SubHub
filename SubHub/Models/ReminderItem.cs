using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Models;

[Table("ReminderItem")]
public class ReminderItem
{
        [PrimaryKey, AutoIncrement]
    [Column("ReminderID")]
     public int ReminderID { get; set; }

    [Column("ReminderMessage")]
     public string? ReminderMessage { get; set; }

    [Column("IsCompleted")]
     public bool IsCompleted { get; set; }
    //public Date ReminderDate {get; set;}


    //Foreign Keys
    [ForeignKey(typeof(SubItem))]   
    public int UserID { get; set; }

    [ForeignKey(typeof(SubscriptionItem))]
    public int SubID { get; set; }

    [ManyToOne]
    public SubItem? User { get; set; }

    [ManyToOne]
    public SubscriptionItem? Subscription { get; set; }
    
}
