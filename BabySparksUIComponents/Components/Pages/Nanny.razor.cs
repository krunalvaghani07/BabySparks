using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
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
        List<BabySparksSharedClassLibrary.Models.Nanny> searchnannies = new List<BabySparksSharedClassLibrary.Models.Nanny>();
        string location = "";
        string namesearch = "";
        protected override async Task OnInitializedAsync()
        {
            location = AppState.user.City;
            await LoadNannies();
        }
        private async Task LoadNannies()
        {
            nannies = (List<BabySparksSharedClassLibrary.Models.Nanny>)await firebaseDataAccess.GetNannies();
        }
        void NavigateToMessages(string id)
        {
            Navigation.NavigateTo($"/messages?chat={id}");
        }
        private void LocationChanged()
        {
            searchnannies = nannies.Where(dc => dc.City.ToLower().Contains(location.ToLower())).ToList();
            StateHasChanged();
        }
        private void NameChanged()
        {
            searchnannies = nannies.Where(dc => dc.FirstName.ToLower().Contains(namesearch.ToLower())
            || dc.LastName.ToLower().Contains(namesearch.ToLower())).ToList();
            StateHasChanged();
        }
    }
}
