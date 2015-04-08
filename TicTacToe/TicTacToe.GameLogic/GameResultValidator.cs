namespace TicTacToe.GameLogic
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class GameResultValidator : IGameResultValidator
    {
        public GameResult GetResult(string board)
        {
            var result = GameResult.NotFinished;


            const int StartIndex = 0;
            const int GameBoardSize = 3;

            // Check for winner
            // Check verticals for winner
            for (int i = 0; i < GameBoardSize; i++)
            {
                int increment = 3;
                result = this.CheckVertForWinner(board, StartIndex, i, increment, result);
                if (result != GameResult.NotFinished)
                {
                    return result;
                }
            }

            // Check horizontals for winner
            for (int i = 0; i < GameBoardSize; i++)
            {
                int increment = 1;
                result = this.CheckHorizForWinner(board, StartIndex, i, increment, result, GameBoardSize);
                if (result != GameResult.NotFinished)
                {
                    return result;
                }
            }

            // TODO: Check diagonals for winner

            // Check if game is finished
            if (board.IndexOf('-') == -1)
            {
                result = GameResult.Draw;
                return result;
            }

            // Then the game is still on
            return result;
        }

        private GameResult CheckVertForWinner(string board, int startIndex, int i, int increment, GameResult result)
        {
            var checkVerticals = board[startIndex + i].Equals(board[startIndex + i + increment])
                                 && board[startIndex + i + increment].Equals(board[startIndex + i + increment * 2]);

            if (checkVerticals && board[startIndex + i].Equals('X'))
            {
                result = GameResult.WonByFirstPlayerX;
            }
            else if (checkVerticals && board[startIndex + i].Equals('O'))
            {
                result = GameResult.WonBySecondPlayerO;
            }

            return result;
        }

        private GameResult CheckHorizForWinner(
            string board,
            int startIndex,
            int i,
            int increment,
            GameResult result,
            int gameBoardSize)
        {
            var checkHorizontals = board[startIndex + i * gameBoardSize].Equals(board[startIndex + i * gameBoardSize + increment])
                                 && board[startIndex + i * gameBoardSize + increment].Equals(board[startIndex + i * gameBoardSize + increment * 2]);

            if (checkHorizontals && board[startIndex + i * gameBoardSize].Equals('X'))
            {
                result = GameResult.WonByFirstPlayerX;
            }
            else if (checkHorizontals && board[startIndex + i * gameBoardSize].Equals('O'))
            {
                result = GameResult.WonBySecondPlayerO;
            }

            return result;
        }
    }
}
