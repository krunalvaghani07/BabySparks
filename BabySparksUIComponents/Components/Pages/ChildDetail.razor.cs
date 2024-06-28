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
    public partial class ChildDetail : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager? Navigation { get; set; }
        [Inject]
        AppState? AppState { get; set; }
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        Child child;
        List<Child> childrenList;


        bool isSidebarOpen = false;
        bool isAddNewChildOpen = false;
        async Task OpenSidebarMenu()
        {
            isSidebarOpen = true;
            StateHasChanged();
        }
        async Task CloseSidebar()
        {
            isSidebarOpen = false;
            StateHasChanged();
        }
        async Task OpenNewChildComponent()
        {
            isAddNewChildOpen = true;
            child = new BabySparksSharedClassLibrary.Models.Child();
            StateHasChanged();
        }
        async Task GoToChildDetail()
        {
            isAddNewChildOpen = false;
            StateHasChanged();
        }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);
            var base64 = Convert.ToBase64String(buffer);
            child.ProfileImageUrl = $"data:image/png;base64,{base64}";
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
        private async Task RegisterChild()
        {
            await firebaseDataAccess.AddChild(child, AppState.user.DocId);
            await LoadChildren();
            isAddNewChildOpen = false;
            StateHasChanged();
        }
        protected override async Task<Task> OnInitializedAsync()
        {
            childrenList = new List<Child>();
            await LoadChildren();
            return base.OnInitializedAsync();
        }
        private async Task LoadChildren()
        {
            childrenList = await firebaseDataAccess.GetChildren(AppState.user.DocId);
        }

    }
}
