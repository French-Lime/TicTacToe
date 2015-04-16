namespace TicTacToe.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using TicTacToe.Data;
    using TicTacToe.Models;
    using TicTacToe.Web.Models;

    [Authorize]
    [RoutePrefix("api/Chat")]
    public class ChatController : BaseApiController
    {
        public ChatController(ITicTacToeData data)
            : base(data)
        {
        }
        
        [HttpGet]
        [Route("Messages")]
        public IHttpActionResult GetMessages()
        {
            var messages = this.data.ChatMessages
                .All()
                .OrderBy(m => m.Id)
                .ToList();

            return this.Ok(messages);
        }

        [HttpGet]
        [Route(Name = "GetMessageById")]
        public IHttpActionResult GetMessageById(int id)
        {
            var message = this.data.ChatMessages.Find(id);
            return this.Ok(message);
        }

        [HttpPost]
        [Route("Send")]
        public IHttpActionResult PostSend(MessageBindingModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = new ChatMessage
            {
                Content = model.Content,
                UserId = model.UserId.ToString(),
            };

            this.data.ChatMessages.Add(message);
            this.data.SaveChanges();

            // return this.CreatedAtRoute("DefaultApi", new { id = message.Id }, message);
            // return this.CreatedAtRoute("GetMessageById", new { id = message.Id }, message);
            return this.Ok(message.Id);
        }

        [HttpPut]
        [Route("Edit")]
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
        [Route("Delete")]
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
