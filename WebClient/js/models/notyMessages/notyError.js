var TicTacToe = TicTacToe || {};

TicTacToe.notyError = (function () {
    function NotyError (message) {
        TicTacToe.noty.call(this, message);
        this.showNoty();
    }

    NotyError.prototype = Object.create(TicTacToe.noty.prototype);

    NotyError.prototype.showNoty = function () {
        var _this = this;

        noty({
            text: TicTacToe.noty.prototype.getMessage.call(_this),
            type: 'error',
            layout: 'topCenter',
            timeout: 5000
        });
    }

    return NotyError;
})();
