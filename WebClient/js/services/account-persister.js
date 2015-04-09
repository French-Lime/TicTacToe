var TicTacToe = TicTacToe || {};

TicTacToe.accountPersister = (function () {
    function AccountPersister (rootUrl) {
        this.rootUrl = rootUrl;
        this.events = new Events(rootUrl);
    }

    var Events = (function (eventUrl) {
        function Events (eventUrl) {
            this.eventUrl = eventUrl;
        }

        Events.prototype.register = function (routeUrl, data) {
            return ajaxRequester.postRequest(this.eventUrl + routeUrl, data);
        }

        Events.prototype.login = function (routeUrl, data) {
          return ajaxRequester.postRequest(this.eventUrl + routeUrl, data);
        }

        return Events;
    })();

    return {
        get: function (rootUrl) {
            return new AccountPersister(rootUrl);
        }
    }
})();
