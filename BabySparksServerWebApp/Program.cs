using BabySparksServerWebApp.Data;
using BabySparksSharedClassLibrary.ServiceProvider;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using BabySparksSharedClassLibrary.IServices;
using BabySparksServerWebApp.Service;
using Blazored.LocalStorage;
using Radzen;
using BabySparksSharedClassLibrary.Repository;
using Microsoft.AspNetCore.SignalR.Client;
using BabySparksSharedClassLibrary.Service;
using Google.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
AppState appState = new();
appState.IsWeb = true;
builder.Services.AddSingleton<AppState>(appState);
builder.Services.AddScoped<ISignalRService, SignalRService>();

builder.Logging.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
{
    ApiKey = "AIzaSyCZuo4e0rGf0Uj_n3A8cZutFEhZkH61VSw",
    AuthDomain = "babysparks-e2b75.firebaseapp.com",
    Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
}));
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddAuthenticationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<StateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, StateProvider>();

builder.Services.AddTransient<IFirebaseDataAccess, FirebaseDataAccessRepository>();
builder.Services.AddSingleton(sp => new HubConnectionBuilder()
                   .WithUrl($"https://127.0.0.1:7117/chatHub", options =>
                   {
                       options.HttpMessageHandlerFactory = _ =>
                       {
                           return new HttpClientHandler
                           {
                               ServerCertificateCustomValidationCallback = (message, certificate2, chain, sslPolicyErrors) => true
                           };
                       };
                   })
               .Build());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
