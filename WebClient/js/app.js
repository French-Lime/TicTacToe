$(document).ready(function () {
    (function () {
        var rootUrl = 'http://localhost:50511/';
        var accountPersister = userAuth.accountPersister.get(rootUrl);
        var accountController = userAuth.accountController.get(accountPersister);
        accountController.load();
    })();
});
