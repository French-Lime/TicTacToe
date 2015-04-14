var TicTacToe = TicTacToe || {};

TicTacToe.gameplayController = (function () {
    function GameplayController(gameplayPersister) {
        this.persister = gameplayPersister;
        this._data = {};
        this._user = {};
    }

    GameplayController.prototype.load = function () {
        var _this = this;

        if (sessionStorage['access_token']) {
            this._user = new TicTacToe.user(
                sessionStorage['access_token'], sessionStorage['username']);
        }

        $(document).on('click', '.createGameButton', function () {
            var headers = {
                "Authorization": _this._user.getToken()
            };

            console.log(headers);

            _this.persister.events.createGame('api/GamePlay/CreateGame', headers)
                .then(function (result) {
                    console.log(result);
                }, function (error) {
                    console.log(error);
                });
        });
    }

    return {
        get: function (gameplayPersister) {
            return new GameplayController(gameplayPersister);
        }
    }
})();
