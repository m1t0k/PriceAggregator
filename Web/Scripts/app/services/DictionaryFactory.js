"use strict";

app.factory("dictionaryFactory",
[
    "$http", function($http) {
        delete $http.defaults.headers.common["X-Requested-With"];

        var factory = {};
        factory.typeList = null;
        factory.itemList = null;
        factory.itemCount = 0;
        factory.baseUrl = "";
        factory.currentItem = null;

        factory.getList = function(type, pageIndex, pageSize, sortName) {

            return $http.get(factory.baseUrl +
                    "/" +
                    type +
                    "/list/" +
                    pageIndex +
                    "/" +
                    pageSize +
                    "/" +
                    encodeURIComponent(sortName))
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    factory.itemList = data;
                })
                .error(function() {
                    factory.itemList = [];
                });
        };

        factory.getTypes = function() {

            return $http.get(factory.baseUrl +
                    "/types")
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    factory.typeList = data;
                })
                .error(function() {
                    return [];
                });;
        };

        factory.getItem = function(type, id) {
            return $http.get(factory.baseUrl +
                    "/" +
                    type +
                    "/" +
                    id)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.createItem = function(type, item) {
            return $http.post(factory.baseUrl +
                    "/" +
                    type +
                    "/",
                    item)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.updateItem = function(type, item) {
            return $http.put(factory.baseUrl +
                    "/" +
                    type +
                    "/" +
                    item.Id,
                    item)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.deleteItem = function(type, id) {
            return $http.delete(factory.baseUrl +
                    "/" +
                    type +
                    "/" +
                    id)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.getCount = function(type) {
            return $http.get(factory.baseUrl +
                    "/" +
                    type +
                    "/list/count")
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    factory.itemCount = data;
                })
                .error(function() {
                    factory.itemCount = 0;
                });
        };


        return factory;
    }
]);