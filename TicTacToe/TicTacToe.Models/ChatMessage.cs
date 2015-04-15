namespace TicTacToe.Models
{
    using System.ComponentModel.DataAnnotations;

    using TicTacToe.Web.Models;

    public class ChatMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual TicTacToeUser User { get; set; } 
    }
}
