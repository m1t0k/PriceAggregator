"use strict";

app.factory("dictionaryFactory",
[
    "$http", function ($http) {

        var factory = {};
        factory.baseUrl = "/Dictionary";
    
        factory.getList = function(type, pageIndex, pageSize, sortName, successHandler, errorHandler) {
            console.log('get list ' + factory.baseUrl + "/" + type + "/list/count");
            return $http.get(factory.baseUrl +
                    "/" +
                    type +
                    "/list/" +
                    pageIndex +
                    "/" +
                    pageSize +
                    "/" +
                    encodeURIComponent(sortName))
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.getTypes = function (successHandler, errorHandler) {
            return $http.get(factory.baseUrl + "/types")
              .then(
                    function (response) { successHandler(response); },
                    function (response) { errorHandler(response); }
                );
        };

        factory.getItem = function(type, id, successHandler, errorHandler) {
            return $http.get(factory.baseUrl + "/" + type + "/" + id)
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.downloadCsv = function (type, successHandler, errorHandler) {
    
            return $http.get(factory.baseUrl + "/" + type + "/list/csv")
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.createItem = function(type, item, successHandler, errorHandler) {
            return $http.post(factory.baseUrl + "/" + type + "/", item)
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.updateItem = function(type, item, successHandler, errorHandler) {
            $http({
                method: "PUT",
                url: factory.baseUrl + "/" + type + "/" + item.Id,
                headers: {
                    'Content-type': 'application/json'
                },
                data:item
            }).then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.deleteItem = function(type, id, successHandler, errorHandler) {
            return $http.delete(factory.baseUrl + "/" + type + "/" + id)
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.getCount = function (type, successHandler, errorHandler) {
            console.log('get count ' + factory.baseUrl + "/" + type + "/list/count");
            return $http.get(factory.baseUrl + "/" + type + "/list/count")
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        return factory;
    }
]);