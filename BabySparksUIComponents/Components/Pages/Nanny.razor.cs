using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Nanny : ComponentBase
    {
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }

        List<BabySparksSharedClassLibrary.Models.Nanny> nannies = new List<BabySparksSharedClassLibrary.Models.Nanny>();
        string location = "";
        protected override async Task OnInitializedAsync()
        {
            location = AppState.user.City;
            await LoadNannies();
        }
        private async Task LoadNannies()
        {
            nannies = (List<BabySparksSharedClassLibrary.Models.Nanny>)await firebaseDataAccess.GetNanniesInCity(location);
        }
        void NavigateToMessages(string id)
        {
            Navigation.NavigateTo($"/messages?chat={id}");
        }
    }
}
