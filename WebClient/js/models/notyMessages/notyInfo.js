var TicTacToe = TicTacToe || {};

TicTacToe.notyInfo = (function () {
    function NotyInfo (message) {
        TicTacToe.noty.call(this, message);
        this.showNoty();
    }

    NotyInfo.prototype = Object.create(TicTacToe.noty.prototype);

    NotyInfo.prototype.showNoty = function () {
        var _this = this;

        noty({
            text: TicTacToe.noty.prototype.getMessage.call(_this),
            type: 'info',
            layout: 'topCenter',
            timeout: 5000
        });
    }

    return NotyInfo;
})();
