﻿using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.SubComponent
{
    public partial class ParentHome : ComponentBase
    {
        TabPosition tabPosition = TabPosition.Top;
        [Inject]
        NavigationManager? Navigation { get; set; }
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        private List<DayCare> dayCaresInCity = new List<DayCare>();
        private List<Nanny> nanniesInCity = new List<Nanny>();
        void OnChange(int index)
        {
            Console.WriteLine($"Tab with index {index} was selected.");
        }
        protected override async Task OnInitializedAsync()
        {
            dayCaresInCity = (List<DayCare>)await firebaseDataAccess.GetDaycareInCity(AppState.user.City.ToLower());
            nanniesInCity = (List<Nanny>)await firebaseDataAccess.GetNanniesInCity(AppState.user.City.ToLower());
        }
        void NavigateToMessages(string id)
        {
            Navigation.NavigateTo($"/messages?chat={id}");
        }
        void NavigateToProfile(string id)
        {
            Navigation.NavigateTo($"/profile?profileId={id}");
        }
    }
}
