namespace TicTacToe.Web.Infrastructure
{
    using System;
    using System.Security.Principal;
    using System.Threading;

    using Microsoft.AspNet.Identity;

    public class AspNetUserIdProvider : IUserIdProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}