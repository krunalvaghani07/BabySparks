using Microsoft.AspNetCore.SignalR;

namespace BabySparks.ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string toUser, string fromUser, string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync(toUser, fromUser, message);
        }
    }
}
