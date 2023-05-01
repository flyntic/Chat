using Microsoft.AspNetCore.SignalR;
namespace ServerUserList
{
    public class ChatHub : Hub
        {
            public async Task Send(string username, string message)
            {

                Console.Write(message);
                await this.Clients.All.SendAsync("Receive", username, message);
            }
        }
  
}
