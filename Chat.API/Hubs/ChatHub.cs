using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(Message message)
    {
        await Clients.Others.SendAsync("ReceiveMessage", message);
    }
}
public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
}