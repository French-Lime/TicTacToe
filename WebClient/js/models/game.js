var TicTacToe = TicTacToe || {};

TicTacToe.game = (function () {
    function Game (gameId, board, state) {
        this.setGameId(gameId);
        this.setBoard(board);
        this.setState(state);
    }

    Game.prototype.setGameId = function (gameId) {
        this._gameId = gameId
    }

    Game.prototype.getGameId = function () {
        return this._gameId;
    }

    Game.prototype.setBoard = function (board) {
        this._board = board;
    }

    Game.prototype.getBoard = function () {
        return this._board;
    }

    Game.prototype.setState = function (state) {
        this._state = state;
    }

    Game.prototype.getState = function () {
        return this._state;
    }

    return Game;
})();
