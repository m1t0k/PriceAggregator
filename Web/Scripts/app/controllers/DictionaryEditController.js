"use strict";
app.controller("DictionaryEditController",
[
    "$scope", "$location", "$http", "dictionaryFactory",
    function($scope, $location, $http, dictionaryFactory) {

        $scope.dictionaryFactory = dictionaryFactory;
        $scope.$error = null;

        $scope.changeView = function(view) {
            $location.path(view);
        };


        $scope.getItem = function(id) {
            return $scope.dictionaryFactory
                .getItem($scope.dictionaryFactory.currentType, id);
        };

        $scope.saveItem = function() {

            if ($scope.dictionaryFactory.currentItem == undefined)
                return;

            if ($scope.dictionaryFactory.currentItem.Id === 0)
                return $scope.dictionaryFactory
                    .createItem($scope.dictionaryFactory.currentType, $scope.dictionaryFactory.currentItem)
                    .success(function(data) {

                    })
                    .error(function() {

                    });;

            return $scope.dictionaryFactory
                .updateItem($scope.dictionaryFactory.currentType, $scope.dictionaryFactory.currentItem)
                .success(function(data) {

                })
                .error(function() {

                });;
        };

    }
]);