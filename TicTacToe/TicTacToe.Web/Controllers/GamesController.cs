namespace TicTacToe.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;

    using TicTacToe.Data;
    using TicTacToe.Models;

    [Authorize]
    public class GamesController : ApiController
    {
        private ITicTacToeData data;

        public GamesController(ITicTacToeData data)
        {
            this.data = data;
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var newGame = new Game { FirstPlayerId = currentUserId, };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            return this.Ok(newGame.Id);
        }

        // joins to random game
        [HttpPost]
        public IHttpActionResult Join()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var game =
                this.data.Games.All().FirstOrDefault(g => g.State == GameState.WaitingForSecondPlayer && g.FirstPlayerId != currentUserId);

            if (game == null)
            {
                return NotFound();
            }

            game.SecondPlayerId = currentUserId;
            game.State = GameState.TurnX;
            this.data.SaveChanges();

            return this.Ok(game.Id);

        }
    }
}