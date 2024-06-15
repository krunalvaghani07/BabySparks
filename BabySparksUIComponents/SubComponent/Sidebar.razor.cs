using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.SubComponent
{
    public partial class Sidebar : ComponentBase
    {
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        NavigationManager Navigation { get; set; }
        [Inject]
        StateProvider authStateProvider { get; set; }
        [Inject]
        IStorageService storageService { get; set; }
        [Parameter]
        public EventCallback<bool> OnClose { get; set; }
        private void Logout()
        {
            storageService.Delete("user");
            authStateProvider.ManageUser();
            Navigation?.NavigateTo("/", true);
        }
        private Task CloseSidebar()
        {
            return OnClose.InvokeAsync(false);
        }
    }
}
