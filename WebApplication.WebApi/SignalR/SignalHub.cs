using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.WebApi.SignalR
{
    public class SignalHub : Hub
    {
        //public static List<string> Users = new List<string>();

        //public override Task OnConnectedAsync()
        //{
        //    string clientId = GetClientId();

        //    if (Users.IndexOf(clientId) == -1)
        //    {
        //        Users.Add(clientId);
        //    }

        //    // Send the current count of users
        //    Send(Users.Count);
        //    return base.OnConnectedAsync();
        //}

        //public void Send(int count)
        //{
        //    Clients.All.SendAsync("online", count);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    string clientId = GetClientId();
        //    if (Users.IndexOf(clientId) > -1)
        //    {
        //        Users.Remove(clientId);
        //    }
        //    // Send the current count of users
        //    Send(Users.Count);
        //    return base.OnDisconnectedAsync(exception);
        //}

        //private string GetClientId()
        //{
        //    return Context.ConnectionId;
        //}
    }
}