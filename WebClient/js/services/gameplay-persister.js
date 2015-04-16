var TicTacToe = TicTacToe || {};

TicTacToe.gameplayPersister = (function () {
    function GameplayPersister (rootUrl) {
        this.rootUrl = rootUrl;
        this.events = new Events(rootUrl);
    }

    var Events = (function (eventUrl) {
        function Events (eventUrl) {
            this.eventUrl = eventUrl;
        }

        Events.prototype.createGame = function (routeUrl, headers) {
            return ajaxRequester.postRequest(this.eventUrl + routeUrl, null, headers);
        }

        Events.prototype.joinGame = function (routeUrl, headers) {
            return ajaxRequester.postRequest(this.eventUrl + routeUrl, null, headers);
        }

        Events.prototype.gameStatus = function (routeUrl, headers) {
            return ajaxRequester.postRequest(this.eventUrl + routeUrl, null, headers);
        }

        return Events;
    })();

    return {
        get: function (rootUrl) {
            return new GameplayPersister(rootUrl);
        }
    }
})();
