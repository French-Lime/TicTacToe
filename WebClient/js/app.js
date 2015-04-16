$(document).ready(function () {
    (function () {
        var ROOT_FOLDER = '/WebAndCloudTeamWork/';
        // var ROOT_URL = 'http://frenchlime.cloudapp.net/';
        var ROOT_URL = 'http://frenchlime.cloudapp.net/';

        var accountPersister = TicTacToe.accountPersister.get(ROOT_URL);
        var accountController = TicTacToe.accountController.get(accountPersister);
        accountController.load();

        var gameplayPersister = TicTacToe.gameplayPersister.get(ROOT_URL);
        var gameplayController = TicTacToe.gameplayController.get(gameplayPersister);
        gameplayController.load();

        var sammy = Sammy('.views-container', function () {
            this.get('#/login', function () {
                this.$element().load(ROOT_FOLDER + 'views/login-view.html');
            })
            this.get('#/register', function () {
                this.$element().load(ROOT_FOLDER + 'views/register-view.html');
            })
            this.get('#/', function () {
                this.$element().empty();
            })
        });

        sammy.run();
    })();
});
