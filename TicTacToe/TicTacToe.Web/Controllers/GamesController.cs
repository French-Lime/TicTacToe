namespace TicTacToe.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;

    using TicTacToe.Data;
    using TicTacToe.GameLogic;
    using TicTacToe.Models;
    using TicTacToe.Web.DataModels;

    [Authorize]
    public class GamesController : ApiController
    {
        private ITicTacToeData data;

        private IGameResultValidator resultValidator;

        public GamesController(ITicTacToeData data, IGameResultValidator resultValidator)
        {
            this.data = data;
            this.resultValidator = resultValidator;
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
            game.State = GameState.TurnFirstPlayerX;
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

        /// <param name="row">1, 2 or 3</param>
        /// <param name="col">1, 2 or 3</param>
        public IHttpActionResult Play(PlayRequestDataModel playRequest)
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (playRequest == null || !ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var idAsGuid = new Guid(playRequest.GameId);
            var game = this.data.Games.Find(idAsGuid);
            if (game == null)
            {
                return this.BadRequest("Invalid game id!");
            }

            if (game.State != GameState.TurnFirstPlayerX && game.State != GameState.TurnSecondPlayerO)
            {
                return this.BadRequest("Invalid game state!");
            }

            if (game.FirstPlayerId != currentUserId && game.SecondPlayerId != currentUserId)
            {
                return this.BadRequest("This is not your game!");
            }

            if ((game.State == GameState.TurnFirstPlayerX && game.FirstPlayerId != currentUserId)
                || (game.State == GameState.TurnSecondPlayerO && game.SecondPlayerId != currentUserId))
            {
                return this.BadRequest("It is not your turn!");
            }

            // TODO: Check if game is active and save new state

            var positionIndex = (playRequest.Row - 1) * 3 + playRequest.Col - 1;
            if (game.Board[positionIndex] != '-')
            {
                return this.BadRequest("This field is not free!");
            }

            var boardAsStringBuilder = new StringBuilder(game.Board);
            boardAsStringBuilder[positionIndex] = (game.State == GameState.TurnFirstPlayerX) ? 'X' : 'O';
            game.Board = boardAsStringBuilder.ToString();

            game.State = game.State == GameState.TurnFirstPlayerX
                             ? GameState.TurnSecondPlayerO
                             : GameState.TurnFirstPlayerX;

            this.data.SaveChanges();

            // TODO: Check if the game has ended and who won and save the state
            var gameResult = resultValidator.GetResult(game.Board);
            switch (gameResult)
            {
                case GameResult.NotFinished:
                    {
                        break;
                    }
                    
                case GameResult.WonByFirstPlayerX:
                    {
                        game.State = GameState.WonByFirstPlayerX;
                        this.data.SaveChanges();
                        break;
                    }

                case GameResult.WonBySecondPlayerO:
                    {
                        game.State = GameState.WonByFirstPlayerO;
                        this.data.SaveChanges();
                        break;
                    }

                case GameResult.Draw:
                    {
                        game.State = GameState.Draw;
                        this.data.SaveChanges();
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            return this.Ok();
        }
    }
}