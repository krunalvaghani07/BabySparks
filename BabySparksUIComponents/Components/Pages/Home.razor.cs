using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        AppState AppState { get; set; }
        [Inject]
        NavigationManager Navigation {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            
        }
    }
}
