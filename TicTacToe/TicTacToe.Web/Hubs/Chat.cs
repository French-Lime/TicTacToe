using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace TicTacToe.Web.Hub
{
    using Microsoft.AspNet.SignalR;

    public class Chat : Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", Context.User.Identity.Name, message);
            Clients.All.addMessage(msg);
        }

        [HubMethodName("NewContosoChatMessage")]
        public void NewContosoChatMessage(string name, string message)
        {
            Clients.All.addContosoChatMessageToPage(name, message);
        }


        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", Context.User.Identity.Name, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                Clients.Group(rooms[i]).addMessage(msg);
            }
        }

        public override Task OnConnected()
        {
            var version = Context.QueryString["version"];
            if (version != "1.0")
            {
                Clients.Caller.notifyWrongVersion();
            }
            return base.OnConnected();
        }
    }
}