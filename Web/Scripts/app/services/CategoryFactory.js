"use strict";

app.factory("categoryFactory",
[
    "$http", function($http) {
        delete $http.defaults.headers.common["X-Requested-With"];

        var factory = {};
        factory.categoryList = null;
        factory.categoryCount = 0;
        factory.baseUrl = "";


        factory.getCategoryList = function(pageIndex, pageSize, sortName) {

            return $http.get(factory.baseUrl +
                    "/category/list/" +
                    pageIndex +
                    "/" +
                    pageSize +
                    "/" +
                    encodeURIComponent(sortName))
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    factory.categoryList = data;
                })
                .error(function() {
                    factory.categoryList = [];
                });
        };

        factory.getCategory = function(id) {
            return $http.get(factory.baseUrl +
                    "/category/" +
                    id)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.createCategory = function(item) {
            return $http.post(factory.baseUrl +
                    "/category/",
                    item)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.updateCategory = function(item) {
            return $http.put(factory.baseUrl +
                    "/category/" +
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

        factory.deleteCategory = function(id) {
            return $http.delete(factory.baseUrl +
                    "/category/" +
                    id)
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    return data;
                })
                .error(function() {
                    return [];
                });
        };

        factory.getCategoryCount = function() {
            return $http.get(factory.baseUrl +
                    "/category/list/count")
                .success(function(data) {
                    // magic line, we resolve the data IN the factory!
                    factory.categoryCount = data;
                })
                .error(function() {
                    factory.categoryCount = 0;
                });
        };


        return factory;
    }
]);