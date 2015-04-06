namespace TicTacToe.GameLogic
{
    public class GameResultValidator : IGameResultValidator
    {
        public GameResult GetResult(string board)
        {
            return GameResult.NotFinished;
        }
    }
}
