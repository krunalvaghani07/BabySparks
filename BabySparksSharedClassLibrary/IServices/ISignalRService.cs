using BabySparksSharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySparksSharedClassLibrary.IServices
{
    public interface ISignalRService
    {
        event Action<Message> OnMessageReceived;

        Task StartConnectionAsync();
        Task SendMessageAsync(string receiverId, Message message);
    }
}
