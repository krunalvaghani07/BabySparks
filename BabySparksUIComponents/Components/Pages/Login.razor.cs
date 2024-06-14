using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Login : ComponentBase
    {
        BabySparksSharedClassLibrary.Models.User user = new BabySparksSharedClassLibrary.Models.User();
        [Inject]
        FirebaseAuthClient? _authClient { get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }
        [Inject]
        AppState? AppState { get; set; }
        [Inject]
        StateProvider authStateProvider { get; set; }
        [Inject]
        IStorageService storageService { get; set; }
        async Task LoginFormSubmitted()
        {
            try
            {
                await _authClient.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
                AppState.user = user;
                user.Id = _authClient.User.Uid;
                AppState.user.Id = _authClient.User.Uid;
                AppState.IsAuthenticated = true;
                storageService.SetValue("user", user);
                authStateProvider.ManageUser();
                Navigation?.NavigateTo("/register", true);
                await base.OnInitializedAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Credentials");
            }
            
        }
    }
}
