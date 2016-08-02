"use strict";
app.controller("dictionaryDashboardController",
[
    "$scope", "$location", "$http", "$routeParams", "dictionaryFactory",
    function($scope, $location, $http, $routeParams, dictionaryFactory) {

        $scope.className = "";
        $scope.errorMessage = "";

        $scope.currentType = "";
        $scope.pageIndex = 1;
        $scope.pageSize = 20;
        $scope.sortName = "";
        $scope.acsending = true; // asc

        $scope.itemList = [];
        $scope.itemCount = 0;

        $scope.typeList = [];
        $scope.pageSizes = [10, 15, 20, 50, 100];

        $scope.errorHandler = function() {
            $scope.className = "alert alert-danger";
            $scope.errorMessage = "error occured";
            console.log(11);
            //messageHandler.showError("error occured.");
        };

        $scope.changePageIndex = function(pageIndex) {
            if (angular.isDefined(pageIndex))
                $scope.pageIndex = pageIndex;

            $scope.refreshData();
        };

        $scope.changePageSize = function(pageSize) {
            if (angular.isDefined(pageSize))
                $scope.pageSize = pageSize;
            $scope.refreshData();
        };

        $scope.changeType = function(type) {
            if (angular.isDefined(type))
                $scope.currentType = type;

            $scope.refreshData();
        };

        $scope.setSortName = function(sortName) {
            if (angular.isDefined(sortName)) {
                if ($scope.sortName === sortName) {
                    $scope.acsending = !$scope.acsending;
                }
                $scope.sortName = sortName;
            }
        };

        $scope.changeSortName = function(sortName) {
            $scope.setSortName(sortName);
            $scope.refreshData();
        };

        $scope.switchToCreateView = function() {
            if ($scope.isDictionaryTypeDefined()) {
                $location.path("create/" + $scope.currentType);
            }
        };

        $scope.switchToEditView = function(item) {

            if (angular.isDefined(item)) {
                $location.path("edit/" + $scope.currentType + "/" + item.Id);
            }
        };

        $scope.getSortExpression = function() {
            if (angular.isUndefined($scope.sortName) || $scope.sortName === "")
                return "";
            return $scope.sortName + ($scope.acsending ? "" : " desc");
        };


        $scope.resultIsNotEmpty = function() {
            return $scope.itemCount > 0 && $scope.itemList.length > 0;
        };

        $scope.resetItemData = function() {
            $scope.itemList = [];
            $scope.itemCount = 0;

        };

        $scope.refreshData = function() {
            $scope.resetItemData();
            $scope.getCount();
            $scope.getList();
        };

        $scope.downloadCsv = function () {
           dictionaryFactory.downloadCsv($scope.currentType,
                function(response) {

                },
                $scope.errorHandler);
        };

        $scope.getList = function(type,pageIndex, pageSize, sortName) {
            if (angular.isDefined(type))
                $scope.currentType = type;

            if (angular.isDefined(pageIndex))
                $scope.pageIndex = pageIndex;

            if (angular.isDefined(pageSize))
                $scope.pageSize = pageSize;

            $scope.setSortName(sortName);

            dictionaryFactory
                .getList($scope.currentType,
                    $scope.pageIndex,
                    $scope.pageSize,
                    $scope.getSortExpression(),
                    function(response) {
                        $scope.itemList = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.isDictionaryTypeDefined = function() {
            return angular.isDefined($scope.currentType) && $scope.currentType.length > 0;
        };

        $scope.getCount = function() {
            dictionaryFactory
                .getCount($scope.currentType,
                    function(response) {
                        $scope.itemCount = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.readDictionaryTypes = function() {
            $scope.typeList = dictionaryFactory
                .getTypes(function(response) {
                        $scope.typeList = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.deleteItem = function(item, message) {
            if (angular.isDefined(item) && angular.isDefined(message))
                var result = confirm(message);

            if (result === false)
                return [];

            return dictionaryFactory
                .deleteItem($scope.currentType,
                    item.Id,
                    function(response) {
                        $scope.refreshData();
                    },
                    $scope.errorHandler);
        };

        $scope.init = function() {
            $scope.readDictionaryTypes();
            if (angular.isDefined($routeParams.type)) {
                $scope.currentType = $routeParams.type;
            }

            if ($scope.isDictionaryTypeDefined()) {
                $scope.refreshData();
            }
        };
    }
]);