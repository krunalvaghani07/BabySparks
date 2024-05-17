﻿using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class SignUp : ComponentBase
    {
        [SupplyParameterFromForm]
        BabySparksSharedClassLibrary.Models.User? user { get; set; }
        [Inject]
        FirebaseAuthClient? _authClient {  get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }
        [Inject]
        AppState? AppState { get; set; }
        [Inject] 
        StateProvider authStateProvider { get; set; }
        [Inject]
        IStorageService storageService { get; set; }
        protected override void OnInitialized() => user ??= new();
        async Task SignUpFormSubmitted()
        {
            if(user?.Password.Trim() != user?.ConfirmPassword.Trim())
            {

            }
            else
            {
                try
                {
                    await _authClient.CreateUserWithEmailAndPasswordAsync(user?.Email, user?.Password);
                    AppState.IsAuthenticated = true;
                    storageService.SetValue("user", user);
                    authStateProvider.ManageUser();
                    Navigation?.NavigateTo("/",true);
                    await base.OnInitializedAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                
            }
            
        }
    }
}