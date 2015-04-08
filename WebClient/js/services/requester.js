var ajaxRequester = (function () {
    var makeRequest = function (method, url, data) {
        var deferred = Q.defer();

        $.ajax({
            url: url,
            type: method,
            data: data || undefined,
            headers: {
                // 'Access-Control-Allow-Headers': '*',
                // 'Access-Control-Allow-Origin': '*',
                // 'Access-Control-Allow-Credentials': 'true'
                // 'Access-Control-Allow-Methods': 'POST'
            },
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            success: function (result) {
                deferred.resolve(result);
            },
            error: function (error) {
                deferred.reject(error);
            },
            onprogress: function (event) {
                deferred.notify(event.loaded / event.total);
            }
        });

        return deferred.promise;
    }

    var makePostRequest = function (url, data) {
        return makeRequest('POST', url, data);
    }

    var makeGetRequest = function (url, data) {
        return makeRequest('GET', url, data);
    }

    return {
        postRequest: makePostRequest,
        getRequest: makeGetRequest
    }
})();
