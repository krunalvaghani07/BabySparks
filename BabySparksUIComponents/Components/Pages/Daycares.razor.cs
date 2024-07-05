using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Daycares : ComponentBase
    {
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }
        List<BabySparksSharedClassLibrary.Models.DayCare> daycares = new List<BabySparksSharedClassLibrary.Models.DayCare>();
        List<BabySparksSharedClassLibrary.Models.DayCare> searchdaycares = new List<BabySparksSharedClassLibrary.Models.DayCare>();
        string location = "";
        string namesearch = "";
        protected override async Task OnInitializedAsync()
        {
            location = AppState.user.City;
            await LoadDaycares();
            searchdaycares = daycares.Where(dc => dc.City.ToLower().Contains(location.ToLower())).ToList();
            StateHasChanged();
        }
        private async Task LoadDaycares()
        {
            daycares = (List<BabySparksSharedClassLibrary.Models.DayCare>)await firebaseDataAccess.GetDaycares();
        }
        void NavigateToMessages(string id)
        {
            Navigation.NavigateTo($"/messages?chat={id}");
        }
        private void LocationChanged()
        {
            searchdaycares = daycares.Where(dc => dc.City.ToLower().Contains(location.ToLower())).ToList();
            StateHasChanged();
        }
        private void NameChanged()
        {
            searchdaycares = daycares.Where(dc => dc.DayCareName.ToLower().Contains(namesearch.ToLower())).ToList();
            StateHasChanged();
        }
    }
}