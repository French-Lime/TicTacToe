namespace TicTacToe.Web.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TicTacToe.Models;

    public class TicTacToeUser : IdentityUser
    {
        private ICollection<ChatMessage> chatMessages;

        public TicTacToeUser()
        {
            this.chatMessages = new HashSet<ChatMessage>();
        }

        public virtual ICollection<ChatMessage> ChatMessages
        {
            get
            {
                return this.chatMessages;
            }

            set
            {
                this.chatMessages = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TicTacToeUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}

