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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
AppState appState = new();
appState.IsWeb = true;
builder.Services.AddSingleton<AppState>(appState);

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
