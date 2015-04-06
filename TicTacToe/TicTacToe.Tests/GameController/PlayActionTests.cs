namespace TicTacToe.Tests.GameController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using TicTacToe.Data;
    using TicTacToe.Data.Repositories;
    using TicTacToe.GameLogic;
    using TicTacToe.Models;
    using TicTacToe.Web.Controllers;
    using TicTacToe.Web.DataModels;
    using TicTacToe.Web.Infrastructure;

    [TestClass]
    public class PlayActionTests
    {
        [TestMethod]
        public void WhenIsTurnFirstPlayerXChangeStateToO()
        {
            var userId = "testPlayer1";
            var gameId = Guid.NewGuid();
            var game = new Game { Id = gameId, FirstPlayerId = userId, State = GameState.TurnFirstPlayerX };
            game.Id = gameId;

            var userIdProviderMock = new Mock<IUserIdProvider>();
            userIdProviderMock.Setup(uip => uip.GetUserId()).Returns(userId);
            
            var repositoryMock = new Mock<IRepository<Game>>();
            repositoryMock.Setup(r => r.All())
                .Returns(() => new List<Game> { game }.AsQueryable());
            repositoryMock.Setup(r => r.Find(It.IsAny<Guid>())).Returns(game);

            var uowData = new Mock<ITicTacToeData>();
            uowData.SetupGet(d => d.Games).Returns(repositoryMock.Object);
            var controller = new GamesController(
                uowData.Object,
                new GameResultValidator(), 
                userIdProviderMock.Object);
            controller.Play(new PlayRequestDataModel
            {
                GameId = gameId.ToString(),
                Col = 1,
                Row = 1
            });

            Assert.AreEqual(GameState.TurnSecondPlayerO, game.State);
        }
    }
}
