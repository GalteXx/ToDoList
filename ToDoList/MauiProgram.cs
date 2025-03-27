﻿using Microsoft.Extensions.Logging;
using ToDoList.Service;
using ToDoList.ViewModel;

namespace ToDoList
{
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

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<ServiceFactory>();
            builder.Services.AddTransient<TaskPageViewModel>();
            builder.Services.AddTransient<MainPage>();
#endif
            return builder.Build();
        }
    }
}
