using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Register : ComponentBase
    {
        private string selectedValue;
        private bool IsRoleSelected;
        private User user = new User();
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }
        [Inject]
        AppState? AppState { get; set; }
        [Inject]
        IStorageService storageService { get; set; }

        private void OnNextClick()
        {
            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Handle the selected value (e.g., navigate to another page or display a message)
                Console.WriteLine($"Selected value: {selectedValue}");
                IsRoleSelected = true;
                StateHasChanged();
            }
            else
            {
                Console.WriteLine("No option selected.");
            }
        }
        private void HandleSelection(ChangeEventArgs e)
        {
            selectedValue = e.Value.ToString();
        }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);
            var base64 = Convert.ToBase64String(buffer);
            user.ProfileImageUrl = $"data:image/png;base64,{base64}";
        }
        private async void HandleFileSelected(InputFileChangeEventArgs e)
        {

            if (e.File != null)
            {
                await JSRuntime.InvokeAsync<Object>("fileCaptureChange");

            }
            this.StateHasChanged();
        }
        private async void OpenCamera()
        {
            await JSRuntime.InvokeAsync<Object>("OpenCamera");
        }
        private async void UploadImage()
        {
            await JSRuntime.InvokeAsync<Object>("UploadImage");
        }
        async Task RegisterUser()
        {
            if (IsRoleSelected)
            {
                switch (selectedValue)
                {
                    case "Day Care":
                        AppState.user.userType = BabySparksSharedClassLibrary.Enums.UserType.DayCare;
                        break;
                    case "Parent":
                        AppState.user.userType = BabySparksSharedClassLibrary.Enums.UserType.Parent;
                        break;
                    case "Nanny":
                        AppState.user.userType = BabySparksSharedClassLibrary.Enums.UserType.Nanny;
                        break;
                }
                storageService.SetValue("user", user);
                Navigation?.NavigateTo("/", true);
                await base.OnInitializedAsync();
            }
            
        }
    }
}
