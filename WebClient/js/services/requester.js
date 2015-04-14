var ajaxRequester = (function () {
    var makeRequest = function (method, url, data, headers) {
        var deferred = Q.defer();

        $.ajax({
            url: url,
            type: method,
            data: data || undefined,
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            headers: headers,
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

    var makePostRequest = function (url, data, headers) {
        return makeRequest('POST', url, data, headers);
    }

    var makeGetRequest = function (url, data, headers) {
        return makeRequest('GET', url, data, headers);
    }

    return {
        postRequest: makePostRequest,
        getRequest: makeGetRequest
    }
})();
