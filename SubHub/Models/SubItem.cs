using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SubHub.Models;

public class SubItem
{
    [PrimaryKey, AutoIncrement]
    public int userID { get; set; }
    public string Reminder {  get; set; }
    public string Subscription {  get; set; }
    public int PaymentMethod {  get; set; }
}
