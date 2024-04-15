using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Models;

[Table("PaymentMethodItem")]
public class PaymentMethodItem
{
    [PrimaryKey, AutoIncrement]
    [Column("PaymentMethodID")]
    public int PaymentMethodID { get; set; }

    [Column("Type")]
    public string Type { get; set; }

    [Column("CardNumber")]
    public int CardNumber { get; set; }

    [Column("CvC")]
    public int CvC { get; set; }
    //public Date ExpDate { get; set; }

    //Foreign Keys
    /*  [Indexed]
      [ForeignKey(typeof(SubItem))]
      public int UserID { get; set; }*/

    [Indexed]
    [ForeignKey(typeof(SubscriptionItem))]
    [Column("SubID")]
    public int SubID { get; set; }

    //Navigation properies

  /*  [ManyToOne]
    public SubItem? User { get; set; }*/

    [OneToOne]
    public SubscriptionItem? Subscription { get; set; }
}
