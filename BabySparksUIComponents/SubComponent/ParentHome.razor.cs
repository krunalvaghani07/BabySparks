using BabySparksSharedClassLibrary.IServices;
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
        AppState AppState { get; set; }
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        private List<DayCare> dayCaresInCity = new List<DayCare>();
        void OnChange(int index)
        {
            Console.WriteLine($"Tab with index {index} was selected.");
        }
        protected override async Task OnInitializedAsync()
        {
            dayCaresInCity = (List<DayCare>)await firebaseDataAccess.GetDaycareInCity(AppState.user.City);
        }
    }
}
