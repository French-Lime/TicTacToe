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
            });
        }
        else {
            $('.auth-options').load('views/guest-toolbar.html');
        }

        $(document).on('click', '.registerButton', function (event) {
            event.preventDefault();

            _this._data.Email = $('.register-form .username').val();
            _this._data.Password = $('.register-form .password').val();
            _this._data.ConfirmPassword = $('.register-form .confirm-password').val();

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
                            });

                            location.href = location.pathname + '#/'

                        _this._user = new TicTacToe.user(token, username);
                    }, function (error) {
                        new TicTacToe.notyError(error.responseJSON.error_description);
                    }, function (processs) {
                        // console.log(processs);
                    });
            }).on('click', '.user-toolbar .logout', function () {
                _this.persister.events.register('api/Account/Logout', '')
                    .then(function (result) {
                        new TicTacToe.notySuccess("Logout successful!");
                    }, function (error) {
                        new TicTacToe.notyError("Something get wrong. Please try again later.");
                    }, function (processs) {
                        // console.log(processs);
                });
              });
    }

    return {
        get: function (accountDataPersister) {
            return new AccountController(accountDataPersister);
        }
    }
})();
