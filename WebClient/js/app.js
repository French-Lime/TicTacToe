$(document).ready(function () {
    (function () {
        var rootFolder = '/WebAndCloudTeamWork/';
        var rootUrl = 'http://localhost:50511/';
        var accountPersister = TicTacToe.accountPersister.get(rootUrl);
        var accountController = TicTacToe.accountController.get(accountPersister);
        accountController.load();

        var sammy = Sammy('.views-container', function () {
            this.get('#/login', function () {
                this.$element().load(rootFolder + 'views/login-view.html');
            })
            this.get('#/register', function () {
                this.$element().load(rootFolder + 'views/register-view.html');
            })
            this.get('#/', function () {
                this.$element().empty();
            })
        });

        sammy.run();
    })();
});
