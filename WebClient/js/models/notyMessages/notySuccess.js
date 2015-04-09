var TicTacToe = TicTacToe || {};

TicTacToe.notySuccess = (function () {
    function NotySuccess (message) {
        TicTacToe.noty.call(this, message);
        this.showNoty();
    }

    NotySuccess.prototype = Object.create(TicTacToe.noty.prototype);

    NotySuccess.prototype.showNoty = function () {
        var _this = this;

        noty({
            text: TicTacToe.noty.prototype.getMessage.call(_this),
            type: 'success',
            layout: 'topCenter',
            timeout: 5000
        });
    }

    return NotySuccess;
})();
