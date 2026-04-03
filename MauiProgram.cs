using Calculator.Services;
using Calculator.View;
using Calculator.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var uri = new Uri("https://www.nbrb.by/api/exrates/rates");


            builder.Services.AddSingleton<IRandomService, RandomService>();
            builder.Services.AddTransient<IDbService, SQLiteService>();
            builder.Services.AddHttpClient<IRateService, RateService>(opt =>
                opt.BaseAddress = uri);


            builder.Services.AddSingleton<SinIntegralViewModel>();
            builder.Services.AddSingleton<HospitalViewModel>();
            builder.Services.AddSingleton<RatesViewModel>();
            builder.Services.AddTransient<ConverterViewModel>();

            builder.Services.AddSingleton<SinIntegralPage>();
            builder.Services.AddSingleton<CalculatorPage>();
            builder.Services.AddSingleton<HospitalPage>();
            builder.Services.AddSingleton<RatesPage>();
            builder.Services.AddTransient<ConverterPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
