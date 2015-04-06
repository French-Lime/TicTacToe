namespace TicTacToe.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;

    using TicTacToe.Data;
    using TicTacToe.Models;
    using TicTacToe.Web.DataModels;

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

        [HttpGet]
        public IHttpActionResult Status(string gameId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var idAsGuid = new Guid(gameId);

            var game = this.data.Games.All()
                .Where(g => g.Id == idAsGuid)
                .Select(g => new { g.FirstPlayerId, g.SecondPlayerId })
                .FirstOrDefault();

            if (game == null)
            {
                return this.NotFound();
            }

            if (game.FirstPlayerId != currentUserId && game.SecondPlayerId != currentUserId)
            {
                return this.BadRequest("This is not your game!");
            }

            var gameInfo = this.data.Games.All()
                .Where(g => g.Id == idAsGuid)
                .Select(g => new GameInfoDataModel()
                {
                    Board = g.Board,
                    Id = g.Id,
                    State = g.State,
                    FirstPlayerName = g.FirstPlayer.UserName,
                    SecondPlayerName = g.SecondPlayer.UserName,
                })
                .FirstOrDefault();

            return this.Ok(gameInfo);
        }
    }
}