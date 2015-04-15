namespace TicTacToe.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

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

