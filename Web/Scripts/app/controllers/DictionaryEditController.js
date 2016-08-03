"use strict";
app.controller("dictionaryEditController",
[
    "$scope", "$location", "$http", "$timeout", "$routeParams", "dictionaryFactory",
    function ($scope, $location, $http, $timeout, $routeParams, dictionaryFactory) {

        $scope.className = "";
        $scope.errorMessage = "";

        $scope.type = $routeParams.type;
        $scope.item = { Id: 0 };
        $scope.id = $routeParams.id;

        $scope.$error = null;
        //$scope.initErrorMessage = "";//"Can't init form.";
        //$scope.successMessage = "";//"Item saved.";
        //$scope.errorMessage = "";//"Error occured.";

        $scope.changeView = function(view) {
            $location.path(view);
        };

        $scope.initErrorHandler = function() {
            $scope.className = "alert alert-danger";
            $scope.errorMessage ="Can't init form";
        };

        $scope.successHandler = function() {
            $scope.className = "alert alert-success";
            $scope.errorMessage ="Item successfully saved.";
        };

        $scope.errorHandler = function() {
            $scope.className = "alert alert-danger";
            $scope.errorMessage = "Error occured.";
        };

        $scope.submitForm = function() {

            if ($scope.item.Id === 0)
                return dictionaryFactory
                    .createItem($scope.type, $scope.item, $scope.successHandler, $scope.errorHandler);

            return dictionaryFactory
                .updateItem($scope.type, $scope.item, $scope.successHandler, $scope.errorHandler);
        };

        $scope.closeForm = function () {
            if (angular.isDefined($scope.type))
                $location.path('/' + $scope.type);
            else
                $location.path('/');
        };

        $scope.init = function(/* initErrorMessage, successMessage, errorMessage*/) {
         /*
            if (angular.isDefined(initErrorMessage))
                $scope.initErrorMessage = initErrorMessage;

            if (angular.isDefined(successMessage))
                $scope.successMessage = successMessage;

            if (angular.isDefined(errorMessage))
                $scope.errorMessage = errorMessage;
         */
            if (angular.isDefined($scope.id)) {
                dictionaryFactory.getItem($scope.type,
                    $scope.id,
                    function(response) {
                        $scope.item = response.data;
                    },
                    $scope.initErrorHandler);
            };
        }

        $scope.init();
    }
]);