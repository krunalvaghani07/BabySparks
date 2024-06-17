using BabySparksMauiApp.Service;
using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Repository;
using BabySparksSharedClassLibrary.ServiceProvider;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace BabySparksMauiApp
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
                });

            AppState appState = new();
            appState.IsDesktop = true;
            builder.Services.AddSingleton<AppState>(appState);

            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<StateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<StateProvider>());
            builder.Services.AddTransient<IFirebaseDataAccess, FirebaseDataAccessRepository>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Logging.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyCZuo4e0rGf0Uj_n3A8cZutFEhZkH61VSw",
                AuthDomain = "babysparks-e2b75.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            }));

            return builder.Build();
        }
    }
}
