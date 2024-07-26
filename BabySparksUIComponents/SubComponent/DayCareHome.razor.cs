using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.Service;
using BabySparksSharedClassLibrary.ServiceProvider;
using BabySparksUIComponents.Components.Pages;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabySparksUIComponents.SubComponent
{
    public partial class DayCareHome : ComponentBase
    {
        bool IsEnrollledPopupOpen = false;
        bool IsAnnouncementPopupOpen = false;
        string ChildrenId = "";
        string Announcement = "";
        private bool isAttendanceMode = false;
        private int presentChildrenCount = 0;
        private int absentChildrenCount => enrolledChildren.Count - presentChildrenCount;

        [Inject]
        AppState appState { get; set; }
        [Inject]
         ISignalRService SignalRService{get;set;}
        [Inject]
        IFirebaseDataAccess firebaseDataAccess { get; set; }
        List<Child> enrolledChildren = new List<Child>();
        private void ToggleEnrollChildModal()
        {
            IsEnrollledPopupOpen = !IsEnrollledPopupOpen;
            StateHasChanged();
        }
        private void ToggleAnnouncement()
        {
            IsAnnouncementPopupOpen = !IsAnnouncementPopupOpen;
            StateHasChanged();
        }
        private void ToggleAttendanceMode()
        {
            isAttendanceMode = !isAttendanceMode;
            StateHasChanged();
        }

        private void SaveAttendance()
        {
            presentChildrenCount = enrolledChildren.Count(c => c.IsPresent);
            // Save attendance data to the database or perform any necessary actions
            ToggleAttendanceMode(); // Exit attendance mode
        }

        private async void EnrollChild()
        {
            if(ChildrenId != "")
            {
                Enrollment enrollment = new Enrollment() { ChildId = ChildrenId};
                await firebaseDataAccess.EnrollChildren(enrollment, appState.user.DocId);
                ChildrenId = "";
                IsEnrollledPopupOpen = false;
                await LoadEnrolledChildren();
                StateHasChanged();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadEnrolledChildren();  
        }
        private async Task LoadEnrolledChildren()
        {
            enrolledChildren = await firebaseDataAccess.GetEnrolledChildrens(appState.user.DocId);
        }
        private async Task PostAnnouncement()
        {
            if (Announcement != "")
            {
                var enrollments = await firebaseDataAccess.GetEnrollments(appState.user.DocId);
                var parentId = enrollments.Select(e => e.ParentId).Distinct();
                foreach (var id in parentId)
                {
                    Message sendMessage = new Message
                    {
                        RecieverId = id,
                        FromId = appState.user.Id,
                        Content = Announcement,
                        Timestamp = DateTime.UtcNow
                    };
                    DayCare dayCare = await firebaseDataAccess.GetUser(appState.user.Id) as DayCare;
                    sendMessage.FromName = dayCare.DayCareName;
                    // Send message via SignalR
                    await SignalRService.SendMessageAsync(id, sendMessage);
                    // add to database
                    await firebaseDataAccess.AddMessage(sendMessage);
                }
                ToggleAnnouncement();
                Announcement = "";
                StateHasChanged();
            }
        }
    }
}
