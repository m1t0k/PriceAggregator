"use strict";
app.controller("DictionaryEditController",
[
    "$scope", "$location", "$http", "$timeout", "$routeParams", "dictionaryFactory", "growl",
    function($scope, $location, $http, $timeout, $routeParams, dictionaryFactory, growl) {

        $scope.dictionaryFactory = dictionaryFactory;
        $scope.type = $routeParams.type;
        $scope.item = { Id: 0 };
        $scope.id = $routeParams.id;

        $scope.$error = null;
        $scope.initErrorMessage = "Can't init form.";
        $scope.successMessage = "Item saved.";
        $scope.errorMessage = "Error occured.";

        $scope.changeView = function(view) {
            $location.path(view);
        };

        $scope.initErrorHandler = function() {
            growl.error($scope.initErrorMessage);
        };

        $scope.successHandler = function() {
            growl.success($scope.successMessage);
        };

        $scope.errorHandler = function() {
            growl.error($scope.errorMessage);
        };

        $scope.save = function() {

            if ($scope.item.Id === 0)
                return $scope.dictionaryFactory
                    .createItem($scope.type, $scope.item, $scope.successHandler, $scope.errorHandler);

            return $scope.dictionaryFactory
                .updateItem($scope.type, $scope.item, $scope.successHandler, $scope.errorHandler);
        };

        $scope.init = function(url, initErrorMessage, successMessage, errorMessage) {
            $scope.dictionaryFactory.baseUrl = url;

            if (angular.isDefined(initErrorMessage))
                $scope.initErrorMessage = initErrorMessage;

            if (angular.isDefined(successMessage))
                $scope.successMessage = successMessage;

            if (angular.isDefined(errorMessage))
                $scope.errorMessage = errorMessage;

            if (angular.isDefined($scope.id)) {
                $scope.dictionaryFactory.getItem($scope.type,
                    $scope.id,
                    function(response) {
                        $scope.item = response.data;
                    },
                    $scope.initErrorHandler);
            };
        }
    }
]);