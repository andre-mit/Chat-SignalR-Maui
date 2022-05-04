using Chat.App.Pages;
using Chat.App.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureViewModels()
            .ConfigurePages()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        HubConnection connection = new HubConnectionBuilder().WithUrl("http://192.168.1.36:5227/chat").Build();


        builder.Services.AddSingleton(connection);

        return builder.Build();
    }
}