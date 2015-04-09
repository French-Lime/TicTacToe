var userAuth = userAuth || {};

userAuth.accountController = (function () {
    function AccountController(accountDataPersister) {
        this.persister = accountDataPersister;
        this._data = {};
    }

    AccountController.prototype.load = function () {
        var _this = this

        $(document).on('click', '.regMe', function () {
            _this._data.Email = 'da@gmail.com';
            _this._data.Password = 'dadada';
            _this._data.ConfirmPassword = 'dadada';

            _this.persister.events.register('api/Account/Register', _this._data)
                .then(function (result) {
                    console.log(result);
                }, function (error) {
                    console.log(error);
                }, function (processs) {
                    console.log(processs);
                });
            }).on('click', '.submitLogin', function (event) {
                event.preventDefault();

                _this._data.username = $('.loginForm .userName').val();
                _this._data.password = $('.loginForm .password').val();
                _this._data.grant_type = 'password';

                _this.persister.events.login('Token', _this._data)
                    .then(function (result) {
                        console.log(result);
                    }, function (error) {
                        console.log(error);
                    }, function (processs) {
                        console.log(processs);
                    });
            });
    }

    return {
        get: function (accountDataPersister) {
            return new AccountController(accountDataPersister);
        }
    }
})();
