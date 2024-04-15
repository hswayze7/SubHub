using Microsoft.Extensions.Logging;
using SubHub.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SubHub.Views;

namespace SubHub;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<SubPage>();
        builder.Services.AddSingleton<SubLogin>();
        builder.Services.AddSingleton<SubManageDatabase>();
        builder.Services.AddSingleton<AddSubPage>();
        builder.Services.AddSingleton<SubInfoPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
