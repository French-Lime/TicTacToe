using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.ApplicationServices;

namespace TicTacToe.Web.DataModels
{
    public class MessageDataModel
    {
        [Required (ErrorMessage = "Message is required")]
        [StringLength(250,MinimumLength = 1, ErrorMessage = "The message cannot be empty")] 
        public string Content { get; set; }

        public User User { get; set; }
    }
}