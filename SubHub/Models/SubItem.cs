using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SubHub.Models;


[Table("SubItem")]
public class SubItem
{
    [PrimaryKey, AutoIncrement]
    [Column("UserID")]
   // [Unique]
    public int UserID { get; set; }

    [Column("Username")]
    public string? Username { get; set; }

    [Column("Password")]
    public string? Password { get; set; }
  //  public string Email { get; set; }
  //  public string Reminder {  get; set; }
  //  public string Subscription {  get; set; }
   // public int PaymentMethod {  get; set; }
}




