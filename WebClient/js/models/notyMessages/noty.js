var TicTacToe = TicTacToe || {};

TicTacToe.noty = (function () {
    function Noty (message) {
        this.setMessage(message);
    }

    // MUST BE ABSTRACT CLASS

    Noty.prototype.getMessage = function () {
        return this._message;
    }

    Noty.prototype.setMessage = function (message) {
        this._message = message;
    }

    return Noty;
})();
