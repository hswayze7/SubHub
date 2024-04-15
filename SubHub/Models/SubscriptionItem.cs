using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Models;

[Table("SubscriptionItem")]
public class SubscriptionItem
{
    [PrimaryKey, AutoIncrement]
    [Column("SubID")]
    public int SubID { get; set; }

    [Column("Name")]
    public string? Name { get; set; }

    [Column("Price")]
    public double Price { get; set; }

    [Column("Description")]
    public string?  Description { get; set; }
    //public Date StartDate {get; set;}
    //public Date RenewalDate {get; set; }


    
    [Indexed]
    [ForeignKey(typeof(SubItem))]
    [Column("UserID")]
    public int UserID { get; set; }

    
    [ManyToOne]
    public SubItem? User { get; set; }

}
