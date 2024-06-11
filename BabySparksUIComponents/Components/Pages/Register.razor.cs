using BabySparksSharedClassLibrary.Models;
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
    }
}
