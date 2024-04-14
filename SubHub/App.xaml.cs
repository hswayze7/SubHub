using SubHub.Models;
using SubHub.Data;
using SubHub.Views;


namespace SubHub;

public partial class App : Application
{
  // public static SubManageDatabase SubManageDatabase { get; set; }
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

       //  SubManageDatabase = subManageDatabase;

        //  InitializeAppAsync();

        //MainPage = new NavigationPage(new SubLogin(SubManageDatabase));

    }

}

