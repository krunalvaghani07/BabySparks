﻿using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.Repository;
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
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        private string errorMessage;
        async Task LoginFormSubmitted()
        {
            try
            {
                await _authClient.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
                AppState.user = await firebaseDataAccess.GetUser(_authClient.User.Uid);

                AppState.IsAuthenticated = true;
                storageService.SetValue("user", AppState.user);
                authStateProvider.ManageUser();
                Navigation?.NavigateTo("/", true);

                await base.OnInitializedAsync();
            }
            catch(Exception ex)
            {
                errorMessage = "Invalid Credentials. Please check your email and password.";
            }
            
        }
        
    }
}
