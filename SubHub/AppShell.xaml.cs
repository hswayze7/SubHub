using SubHub.Views;

namespace SubHub;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
       // Routing.RegisterRoute(nameof(SubLogin), typeof(SubLogin));
        Routing.RegisterRoute(nameof(SubPage), typeof(SubPage));
        //Routing.RegisterRoute(nameof(AddSubPage), typeof(AddSubPage));
    }
}
