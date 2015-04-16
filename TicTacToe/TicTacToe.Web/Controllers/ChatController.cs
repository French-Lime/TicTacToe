namespace TicTacToe.Web.Controllers
{
    using System.Web.Http;
    using System.Linq;

    using TicTacToe.Models;
    using TicTacToe.Data;
    using TicTacToe.Web.Models;


    public class ChatController : BaseApiController
    {
        public ChatController(ITicTacToeData data)
            : base(data)
        {
        }
        
        [HttpGet]
        public IHttpActionResult Get()
        {
            var messages = this.data.ChatMessages
                .All()
                .OrderBy(m => m.Id)
                .ToList();

            return this.Ok(messages);
        }

        [HttpPost]
        public IHttpActionResult PostMessage(MessageBindingModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = new ChatMessage
            {
                Content = model.Name,
                UserId = model.UserId.ToString(),
            };

            this.data.ChatMessages.Add(message);
            this.data.SaveChanges();

            return this.CreatedAtRoute("DefaultApi", new { id = message.Id}, message);
        }

        [HttpPut]
        public IHttpActionResult EditMessage(int id, MessageBindingModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = this.data.ChatMessages.Find(id);

            if (message == null)
            {
                return this.NotFound();
            }

            this.data.ChatMessages.Update(message);
            this.data.SaveChanges();

            return this.Ok(new {Message = string.Format("Message #{0} edited succesfully", message.Id)});
        }

        [HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            var message = this.data.ChatMessages.Find(id);

            if (message == null)
            {
                return this.NotFound();
            }

            this.data.ChatMessages.Delete(message);
            this.data.SaveChanges();

            return this.Ok(new { Message = string.Format("Message #{0} deleted succesfully", message.Id) });
        }
    }
}
