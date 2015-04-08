namespace TicTacToe.GameLogic
{
    public class GameResultValidator : IGameResultValidator
    {
        
        public GameResult GetResult(string board)
        {
            var result = GameResult.NotFinished;

            int startIndex = 0;
            const int GameBoardSize = 3;

            // Check for winner
            // Check verticals for winner
            for (int i = 0; i < GameBoardSize; i++)
            {
                const int IncrementVert = 3;
                result = this.CheckVertForWinner(board, startIndex, i, IncrementVert, result);
                if (result != GameResult.NotFinished)
                {
                    return result;
                }
            }

            // Check horizontals for winner
            for (int i = 0; i < GameBoardSize; i++)
            {
                const int IncrementHoriz = 1;
                result = this.CheckHorizForWinner(board, startIndex, i, IncrementHoriz, result, GameBoardSize);
                if (result != GameResult.NotFinished)
                {
                    return result;
                }
            }

            // Check diagonals for winners
            // Left diagonal
            const int IncrementLeftDiag = 4;
            result = this.CheckDiagForWinner(board, startIndex, IncrementLeftDiag, result);
            if (result != GameResult.NotFinished)
            {
                return result;
            }

            // Right diagonal
            startIndex = 2;
            const int IncrementRightDiag = 2;
            result = this.CheckDiagForWinner(board, startIndex, IncrementRightDiag, result);
            if (result != GameResult.NotFinished)
            {
                return result;
            }

            // So far we found that we have no winner
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
            var checkVerticals = board[startIndex + i].Equals(board[startIndex + i + increment]) && 
                board[startIndex + i + increment].Equals(board[startIndex + i + increment * 2]);

            var firstFieldIndex = startIndex + i;
            result = this.WhoWinsGameResult(board, firstFieldIndex, checkVerticals, result);
            
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

            var firstFieldIndex = startIndex + i * gameBoardSize;
            result = this.WhoWinsGameResult(board, firstFieldIndex, checkHorizontals, result);

            return result;
        }

        private GameResult CheckDiagForWinner(string board, int startIndex, int increment, GameResult result)
        {
            var checkDiag = board[startIndex].Equals(board[startIndex + increment]) && 
                board[startIndex + increment].Equals(board[startIndex + increment * 2]);

            var firstFieldIndex = startIndex;
            result = this.WhoWinsGameResult(board, firstFieldIndex, checkDiag, result);
            return result;
        }

        private GameResult WhoWinsGameResult(string board, int firstFieldIndex, bool checkLine, GameResult result)
        {
            if (checkLine && board[firstFieldIndex].Equals('X'))
            {
                result = GameResult.WonByFirstPlayerX;
            }
            else if (checkLine && board[firstFieldIndex].Equals('O'))
            {
                result = GameResult.WonBySecondPlayerO;
            }

            return result;
        }
    }
}
