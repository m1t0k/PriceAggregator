"use strict";

app.factory("dictionaryFactory",
[
    "$http", function($http) {
        //    delete $http.defaults.headers.common["X-Requested-With"];
        var factory = {};

        factory.baseUrl = "";
    
        factory.getList = function(type, pageIndex, pageSize, sortName, successHandler, errorHandler) {
            factory.itemList = [];
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

        factory.createItem = function(type, item, successHandler, errorHandler) {
            return $http.post(factory.baseUrl + "/" + type + "/", item)
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        factory.updateItem = function(type, item, successHandler, errorHandler) {
            return $http.put(factory.baseUrl + "/" + type + "/" + item.Id, item)
                .then(
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

        factory.getCount = function(type, successHandler, errorHandler) {
            return $http.get(factory.baseUrl + "/" + type + "/list/count")
                .then(
                    function(response) { successHandler(response); },
                    function(response) { errorHandler(response); }
                );
        };

        return factory;
    }
]);