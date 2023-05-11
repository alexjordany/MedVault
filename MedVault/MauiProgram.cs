using CommunityToolkit.Maui;
using MedVault.Data.Repositories;
using MedVault.Pages;
using MedVault.ViewModels;
using Microsoft.Extensions.Logging;

namespace MedVault;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IMedicineService, RemoteMedicineRepository>();

        //Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<EditMedicinePage>();
        builder.Services.AddTransient<MedicineDetailsPage>();

        //View Models
        builder.Services.AddSingleton<MedicinesViewModel>();
        builder.Services.AddTransient<EditMedicineViewModel>();
        builder.Services.AddTransient<MedicineDetailsViewModel>();

        return builder.Build();
    }
}

