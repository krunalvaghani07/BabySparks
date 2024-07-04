using BabySparksSharedClassLibrary.Models;
using Microsoft.AspNetCore.SignalR;

namespace BabySparks.ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string toId,Message message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync(toId, message);
        }
    }
}
