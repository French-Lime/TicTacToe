﻿

namespace SignalR {

    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;

    public class MyConnection : PersistentConnection {
        protected override Task OnReceived(IRequest request, string connectionId, string data) {
            // Broadcast data to all clients
            return Connection.Broadcast(data);
        }
    }
}