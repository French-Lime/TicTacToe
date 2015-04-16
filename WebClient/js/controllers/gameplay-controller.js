var TicTacToe = TicTacToe || {};

TicTacToe.gameplayController = (function () {
    function GameplayController(gameplayPersister) {
        this.persister = gameplayPersister;
        this._data = {};
        this._user = {};
        this._dame = {};
    }

    GameplayController.prototype.load = function () {
        var _this = this;

        if (sessionStorage['access_token']) {
            this._user = new TicTacToe.user(
                sessionStorage['access_token'], sessionStorage['username']);
        }

        $(document).on('click', '.create-game', function () {
            var headers = {
                "Authorization": _this._user.getToken(),
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*"
            };

            _this._data["Authorization"] = _this._user.getToken();

            console.log(headers);

            _this.persister.events.createGame('api/Games/Create', headers)
                .then(function (result) {
                    console.log(result);
                    _this.loadGamefield();
                    sessionStorage['gameId'] = result;
                    new TicTacToe.notySuccess("Success: gameId - " + result);
                }, function (error) {
                    console.log(error);
                    new TicTacToe.notyError("Error!!!");
                });
        }).on('click', '.join-game', function () {
            var headers = {
                "Authorization": _this._user.getToken(),
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*"
            };
            _this.loadGamefield();
            _this.persister.events.joinGame('api/Games/Join', headers)
                .then(function (result) {
                    console.log(result);
                    new TicTacToe.notySuccess("Success: gameId - " + result);
                }, function (error) {
                    console.log(error);
                    new TicTacToe.notyError("Error!");
                });
        });
    }

    GameplayController.prototype.loadGamefield = function () {
        $('.game-container').load('views/join-game.html', function () {
            $('.game-container').css({width: '20%'});
            $('.game-container')
                .css({height:
                    parseInt($('.game-container').css('width'))});

            $('.TicTacToeField')
                .before($(document.createElement('div'))
                    .addClass('exit-game')
                    .text('Exit Game'));
        });
    }

    GameplayController.prototype.gameState = function (gameId) {

    }

    return {
        get: function (gameplayPersister) {
            return new GameplayController(gameplayPersister);
        }
    }
})();
