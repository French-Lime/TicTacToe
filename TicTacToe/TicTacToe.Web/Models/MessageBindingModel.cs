using System;

namespace TicTacToe.Web.Models
{
    public class MessageBindingModel
    {
        public string Content { get; set; }

        public Guid UserId { get; set; }
    }
}