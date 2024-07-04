using BabySparksSharedClassLibrary.IServices;
using BabySparksSharedClassLibrary.Models;
using BabySparksSharedClassLibrary.ServiceProvider;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.Service
{
    public class SignalRService : ISignalRService
    {
        private readonly HubConnection _hubConnection;
        private readonly AppState _appState;

        public event Action<Message> OnMessageReceived;

        public SignalRService(HubConnection hubConnection, AppState appState)
        {
            _appState = appState;
            _hubConnection = hubConnection;

            _appState.UserChanged += AppState_UserChanged;

        }

        private async void AppState_UserChanged(object sender, UserChangedEventArgs e)
        {
            // Handle user change, e.g., re-initialize or update connection
            if (_appState.user != null)
            {
                _hubConnection.On<Message>(_appState.user.Id, message =>
                {
                    OnMessageReceived?.Invoke(message);
                });
            }
            else
            {
                // Handle case where user becomes null
            }

            // You may want to reconnect or perform other actions here
            await ReconnectAsync();
        }

        public async Task StartConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }

        public async Task SendMessageAsync(string receiverId, Message message)
        {
            try
            {
                await _hubConnection.InvokeAsync("SendMessage", receiverId, message);
            }
            catch (Exception ex)
            {
                // Handle send message error
            }
        }

        private async Task ReconnectAsync()
        {
            // Implement reconnection logic here if needed
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }
    }
}
