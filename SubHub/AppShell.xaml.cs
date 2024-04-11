using SubHub.Views;

namespace SubHub;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(SubLogin), typeof(SubLogin));
    }
}
