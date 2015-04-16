var TicTacToe = TicTacToe || {};

TicTacToe.accountController = (function () {
    function AccountController(accountDataPersister) {
        this.persister = accountDataPersister;
        this._data = {};
        this._user = {};
    }

    AccountController.prototype.load = function () {
        var _this = this;

        if (sessionStorage['access_token']) {
            this._user = new TicTacToe.user(
                sessionStorage['access_token'], sessionStorage['username']);
        }

        if (!$.isEmptyObject(this._user)) {
            $('.auth-options').load('views/user-toolbar.html', function () {
                $('.user-toolbar .username-container').text(sessionStorage['username']);
                _this.loadGamelist(4);
            });
        }
        else {
            $('.auth-options').load('views/guest-toolbar.html');
        }

        $(document).on('click', '.registerButton', function (event) {
            event.preventDefault();

            _this._data.Email = $('.register-form .email').val();
            _this._data.Password = $('.register-form .password').val();
            _this._data.ConfirmPassword = $('.register-form .confirm-password').val();
            _this._data.Username = $('.register-form .username').val();
            _this._data.FirstName = $('.register-form .firstName').val();
            _this._data.LastName = $('.register-form .lastName').val();

            _this.persister.events.register('api/Account/Register', _this._data)
                .then(function (result) {
                    new TicTacToe.notySuccess("Register successful!");
                }, function (error) {
                    new TicTacToe.notyError("Something get wrong. Please try again later.");
                }, function (processs) {
                    // console.log(processs);
                });
            }).on('click', '.loginButton', function (event) {
                event.preventDefault();

                _this._data.username = $('.login-form .username').val();
                _this._data.password = $('.login-form .password').val();
                _this._data.grant_type = 'password';

                _this.persister.events.login('Token', _this._data)
                    .then(function (result) {
                        var token = result['access_token'],
                            username = result['userName'];

                        sessionStorage['access_token'] = result['access_token'];
                        sessionStorage['username'] = result['userName'];

                        new TicTacToe.notySuccess("Hallo! " + username + "!");

                        $('.guest-options').hide();

                        $('.views-container')
                            .empty()
                            .load('views/user-toolbar.html', function () {
                                $('.user-toolbar .username-container').text(username);
                                _this.loadGamelist(4);
                            });

                            location.href = location.pathname + '#/';

                        _this._user = new TicTacToe.user(token, username);
                    }, function (error) {
                        new TicTacToe.notyError(error.responseJSON.error_description);
                    }, function (processs) {
                        // console.log(processs);
                    });
            }).on('click', '.user-toolbar .logout', function () {
                var headers = {
                    "Authorization": _this._user.getToken()
                };

                _this._user = {};
                sessionStorage['access_token'] = '';
                sessionStorage['username'] = '';
                _this.load();

                _this.persister.events.logout('api/Account/Logout', headers)
                    .then(function (result) {
                        new TicTacToe.notySuccess("Logout successful!");
                        console.log(result);
                    }, function (error) {
                        new TicTacToe.notyError("Something get wrong. Please try again later.");
                        console.log(error);
                    }, function (processs) {
                        // console.log(processs);
                });

                location.href = location.pathname;
                // location.href = location.pathname + '#/';
            }).on('click', '.exit-game', function () {
                $('.user-toolbar .game-container')
                    .empty()
                    .css({width: '40%'})
                    .append($(document.createElement('span'))
                        .addClass('game-container-heading')
                        .text('Available games'))
                    .append($(document.createElement('div'))
                        .addClass('games-list'));

                _this.loadGamelist(4);
            });
    }

    AccountController.prototype.loadGamelist = function (listLength) {
        for (var i = 0; i < listLength; i++) {
            $('.user-toolbar .game-container .games-list')
                .append($(document.createElement('div'))
                    .addClass('game game' + i));

                $('.game' + i)
                    .append($(document.createElement('span'))
                        .text('Game number:')
                        .addClass('aboutGame'))
                    .append($(document.createElement('span'))
                        .text(i)
                        .addClass('game-id'))
                    .append($(document.createElement('span'))
                        .text('JOIN')
                        .addClass('join-game'));
        }

        $('.user-toolbar .game-container .games-list')
            .after($(document.createElement('button'))
                .addClass('create-game')
                .text("Създай игра"));
    }

    return {
        get: function (accountDataPersister) {
            return new AccountController(accountDataPersister);
        }
    }
})();
