"use strict";
app.controller("dictionaryDashboardController",
[
    "$scope", "$location", "$http", "dictionaryFactory", "growl",
    function($scope, $location, $http, dictionaryFactory, growl) {

        $scope.dictionaryFactory = dictionaryFactory;
        $scope.currentType = "";
        $scope.pageIndex = 1;
        $scope.pageSize = 20;
        $scope.sortName = "";
        $scope.acsending = true; // asc

        $scope.itemList = [];
        $scope.itemCount = 0;

        $scope.typeList = [];
        $scope.pageSizes = [10, 15, 20, 50, 100];

        $scope.errorHandler = function () {
            growl.error("error occured.");
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

        $scope.changeView = function(view, item) {
            if (angular.isDefined(item)) {
                $scope.dictionaryFactory.currentItem = item;
            } else {
                $scope.dictionaryFactory.currentItem = { Id: 0 };
            }
            $location.path(view);
        };

        $scope.getSortExpression = function() {
            if (angular.isUndefined($scope.sortName) || $scope.sortName === "")
                return "";
            return $scope.sortName + ($scope.acsending ? "" : " desc");
        };


        $scope.resultIsNotEmpty = function () {
            return $scope.itemCount > 0 && $scope.itemList.length > 0;
        };
        
        $scope.resetItemData = function () {            
            $scope.itemList = [];
            $scope.itemCount = 0;

        };

        $scope.refreshData = function () {
            $scope.resetItemData();
            $scope.getCount();
            $scope.getList();
        };

        $scope.getList = function(type, pageIndex, pageSize, sortName) {
            if (angular.isDefined(type))
                $scope.dictionaryFactory.currentType = type;

            if (angular.isDefined(pageIndex))
                $scope.pageIndex = pageIndex;

            if (angular.isDefined(pageSize))
                $scope.pageSize = pageSize;

            $scope.setSortName(sortName);

            $scope.dictionaryFactory
                .getList($scope.currentType,
                    $scope.pageIndex,
                    $scope.pageSize,
                    $scope.getSortExpression(),
                    function(response) {
                        $scope.itemList = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.isTypeSelected = function() {
            return angular.isDefined($scope.currentType) && $scope.currentType.length>0;
        };

        $scope.getCount = function() {
            $scope.dictionaryFactory
                .getCount($scope.currentType,
                    function(response) {
                        $scope.itemCount = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.readDictionaryTypes = function() {
            $scope.typeList = $scope.dictionaryFactory
                .getTypes(function(response) {
                        $scope.typeList = response.data;
                    },
                    $scope.errorHandler);
        };

        $scope.deleteItem = function(id, message) {
            if (angular.isDefined(message))
                var result = confirm(message);

            if (result === false)
                return [];

            return $scope.dictionaryFactory
                .deleteItem($scope.currentType,
                    id,
                    function(response) {
                        $scope.refreshData();
                    },
                    $scope.errorHandler);
        };

        $scope.init = function(url) {
            $scope.dictionaryFactory.baseUrl = url;
            $scope.readDictionaryTypes();
        };
    }
]);