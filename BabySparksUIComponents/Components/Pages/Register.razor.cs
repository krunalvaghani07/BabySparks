using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using GoogleMapsComponents.Maps.Places;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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
        protected override async Task OnInitializedAsync()
        {
            user = AppState.user;
        }

        private async Task OnNextClick()
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

        #region Methods for image capture event
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
        #endregion

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
                user.userType = AppState.user.userType;
                storageService.SetValue("user", user);
                Navigation?.NavigateTo("/", true);
                await base.OnInitializedAsync();
            }

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsRoleSelected)
            {
                // convert RadzenTextBox to ElementReference
                addressBox = addressBoxRadzenTextBox.Element;

                // setup google address auto complete
                await SetupGoogleAddressAutoComplete();
            }
        }

        #region GoogleMap Autocomplete

        private ElementReference addressBox;
        private RadzenTextBox? addressBoxRadzenTextBox;
        private Autocomplete? autocomplete;
        private string? placeData;
        private string? formattedAddress;
        private async Task GoogleAddressAutoComplete()
        {

            System.Threading.Thread.Sleep(500); // this is a hack. with no wait place was null first time

            var place = await this.autocomplete.GetPlace();

            var placeDataTest = JsonSerializer.Serialize(place);

            if (place?.AddressComponents == null)
            {
                // this.message = "No results available for " + place?.Name;
            }
            else
            {
                var placeId = place.PlaceId;
                var streetNumber = place.AddressComponents.FirstOrDefault(a => a.Types != null && a.Types.Contains("street_number"))?.LongName;
                var street = place.AddressComponents.FirstOrDefault(a => a.Types != null && (a.Types.Contains("route") || a.Types.Contains("street_address")))?.LongName;
                var city = place.AddressComponents.LastOrDefault(a => a.Types != null && (a.Types.Contains("locality")))?.LongName;
                var state = place.AddressComponents.FirstOrDefault(a => a.Types != null && a.Types.Contains("administrative_area_level_1"))?.LongName;
                var country = place.AddressComponents.FirstOrDefault(a => a.Types != null && a.Types.Contains("country"))?.LongName;
                var postcode = place.AddressComponents.FirstOrDefault(a => a.Types != null && a.Types.Contains("postal_code"))?.LongName;
                user.Address = place.FormattedAddress;
                user.City = city;
                user.PinCode = postcode;
                placeData = JsonSerializer.Serialize(place);

            }
        }

        private async Task SetupGoogleAddressAutoComplete()
        {
            //    Logger.LogInformation("SetupGoogleAddressAutoComplete called");
            this.autocomplete = await Autocomplete.CreateAsync(JSRuntime, addressBox, new AutocompleteOptions
            {
                StrictBounds = false,
            });

            //await autocomplete.SetFields(new []{ "address_components", "geometry", "icon", "name" }); , "formatted_Address" formatted_address
            await this.autocomplete.SetFields(new[] { "address_components", "place_id", "formatted_address" });
        }

        # endregion

    }
}
