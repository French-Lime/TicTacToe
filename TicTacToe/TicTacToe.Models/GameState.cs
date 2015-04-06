namespace TicTacToe.Models
{
    public enum GameState
    {
        WaitingForSecondPlayer = 0,
        TurnFirstPlayerX = 1,
        TurnSecondPlayerO = 2,
        WonByFirstPlayerX = 3,
        WonBySecondPlayerO = 4,
        Draw = 5,
        WonByFirstPlayerO
    }
}
