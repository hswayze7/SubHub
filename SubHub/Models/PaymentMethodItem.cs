using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Models;

public class PaymentMethodItem
{
    [PrimaryKey, AutoIncrement]
    public int paymentMethodID { get; set; }
    public string Type { get; set; }
    public int CardNumber { get; set; }
    public int CvC { get; set; }
    //public Date ExpDate { get; set; }

    //Foreign Keys
    public int userID { get; set; }
    public int subID { get; set; }

    //Navigation properies
    public SubItem User { get; set; }
    public SubscriptionItem Subscription { get; set; }
}
