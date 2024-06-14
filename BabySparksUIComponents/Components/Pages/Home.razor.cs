using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.ServiceProvider;
using BabySparksUIComponents.Components.Layout;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        NavigationManager Navigation {  get; set; }
        [Inject]
        StateProvider authStateProvider { get; set; }
        [Inject]
        IStorageService storageService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            
        }
        private void Logout()
        {
            storageService.Delete("user");
            authStateProvider.ManageUser();
            Navigation?.NavigateTo("/", true);
        }
    }
}
