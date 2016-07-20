"use strict";
app.controller("DashboardController",
[
    "$scope","$location", "$http", "dictionaryFactory",
    function ($scope, $location, $http, dictionaryFactory) {

        $scope.dictionaryFactory = dictionaryFactory;
        $scope.type = null;
        $scope.typeList = {};
        $scope.pageIndex = 1;
        $scope.pageSize = 20;
        $scope.itemList = {};
        $scope.itemCount = 0;
        $scope.sortName = "";
        $scope.acsending = true; // asc
        $scope.pageSizes = [10, 15, 20, 50, 100];
        $scope.currentItem = null;
        $scope.$error = null;

        $scope.changePageIndex = function(pageIndex) {
            $scope.getCount();
            $scope.getList($scope.type, pageIndex, $scope.pageSize, $scope.sortName);
        };

        $scope.changePageSize = function() {
            $scope.getCount();
            $scope.getList($scope.type, $scope.pageIndex, $scope.pageSize, $scope.sortName);
        };

        $scope.changeType = function() {
            $scope.getCount();
            $scope.getList($scope.type, $scope.pageIndex, $scope.pageSize, $scope.sortName);
        };

        $scope.changeSortName = function(sortName) {
            $scope.getCount();
            $scope.getList($scope.type, $scope.pageIndex, $scope.pageSize, sortName);
        };

        $scope.changeView = function(view, item) {
            if (item != undefined) {
                $scope.dictionaryFactory.currentItem = item;
            } else {
                $scope.dictionaryFactory.currentItem = { Id: 0 };
            }
            $location.path(view);
        };

        $scope.getSortExpression = function() {

            if ($scope.sortName == undefined || $scope.sortName === "")
                return "";
            return $scope.sortName + ($scope.acsending ? "" : " desc");
        };

        $scope.getList = function(type, pageIndex, pageSize, sortName) {

            if (type != undefined)
                $scope.type = type;

            if (pageIndex != undefined)
                $scope.pageIndex = pageIndex;

            if (pageSize != undefined)
                $scope.pageSize = pageSize;

            if (sortName != undefined) {
                if ($scope.sortName === sortName) {
                    $scope.acsending = !$scope.acsending;
                }

                $scope.sortName = sortName;
            }

            $scope.dictionaryFactory
                .getList($scope.type, $scope.pageIndex, $scope.pageSize, $scope.getSortExpression())
                .then(function() {
                    $scope.itemList = $scope.dictionaryFactory.itemList;
                });

            return;
        };

        $scope.getCount = function() {
            $scope.dictionaryFactory
                .getCount($scope.type)
                .then(function() {
                    $scope.itemCount = $scope.dictionaryFactory.itemCount;
                });
        };

        $scope.getTypes = function() {
            $scope.typeList = $scope.dictionaryFactory
                .getTypes()
                .then(function() {
                    $scope.typeList = $scope.dictionaryFactory.typeList;
                });

            return $scope.typeList;
        };

        $scope.getItem = function(id) {
            return $scope.dictionaryFactory
                .getItem($scope.type, id);
        };

        $scope.saveItem = function () {
         
            if ($scope.dictionaryFactory.currentItem == undefined)
                return;

            if ($scope.dictionaryFactory.currentItem.Id === 0)
                return $scope.dictionaryFactory
                    .createItem($scope.type, $scope.dictionaryFactory.currentItem)
                    .success(function(data) {

                    })
                    .error(function() {

                    });;

            return $scope.dictionaryFactory
                .updateItem($scope.type, $scope.dictionaryFactory.currentItem)
                .success(function(data) {

                })
                .error(function() {

                });;
        };

        $scope.deleteItem = function(id, message) {

            if (message != undefined)
                var result = confirm(message);
            if (!result)
                return result;
            return $scope.dictionaryFactory
                .deleteItem($scope.type, id)
                .then(function() {
                    $scope.getList();
                    $scope.getCategoryCount();
                });
        };

        $scope.init = function(url) {
            $scope.dictionaryFactory.baseUrl = url;
            $scope.getTypes();
        };
    }
]);