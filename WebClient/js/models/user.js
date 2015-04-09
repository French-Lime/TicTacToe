var TicTacToe = TicTacToe || {};

TicTacToe.user = (function () {
    function User (accessToken, username) {
        this.setToken(accessToken);
        this.setUsername(username);
    }

    User.prototype.setToken = function (token) {
        this._token = 'bearer ' + token;
    }

    User.prototype.getToken = function () {
        return this._token;
    }

    User.prototype.setUsername = function (username) {
        this._username = username;
    }

    User.prototype.getUsername = function () {
        return this._username;
    }

    return User;
})();
