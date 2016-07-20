﻿"use strict";
app.controller("DashboardController",
[
    "$scope", "$location", "$http", "dictionaryFactory",
    function($scope, $location, $http, dictionaryFactory) {

        $scope.dictionaryFactory = dictionaryFactory;
        $scope.pageIndex = 1;
        $scope.pageSize = 20;
        $scope.sortName = "";
        $scope.acsending = true; // asc
        
        $scope.itemList = {};
        $scope.itemCount = 0;

        $scope.typeList = {};
        $scope.pageSizes = [10, 15, 20, 50, 100];

        $scope.changePageIndex = function(pageIndex) {
            if (pageIndex != undefined)
                $scope.pageIndex = pageIndex;

            $scope.refreshData();
        };

        $scope.changePageSize = function(pageSize) {
            if (pageSize != undefined)
                $scope.pageSize = pageSize;

            $scope.refreshData();
        };

        $scope.changeType = function(type) {
            if (type != undefined)
                $scope.dictionaryFactory.currentType = type;

            $scope.refreshData();
        };

        $scope.setSortName = function(sortName) {

            if (sortName != undefined) {
                if ($scope.sortName === sortName) {
                    $scope.acsending = !$scope.acsending;
                }

                $scope.sortName = sortName;
            }
        };

        $scope.changeSortName = function (sortName) {

            $scope.setSortName(sortName);
            $scope.refreshData();
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


        $scope.refreshData = function () {
            $scope.getCount();
            $scope.getList();
        };

        $scope.getList = function(type, pageIndex, pageSize, sortName) {
            
            if (type != undefined)
                $scope.dictionaryFactory.currentType = type;

            if (pageIndex != undefined)
                $scope.pageIndex = pageIndex;

            if (pageSize != undefined)
                $scope.pageSize = pageSize;

            $scope.setSortName(sortName);
            
            $scope.dictionaryFactory
                .getList($scope.dictionaryFactory.currentType,
                    $scope.pageIndex,
                    $scope.pageSize,
                    $scope.getSortExpression())
                .then(function () {
                    $scope.itemList = $scope.dictionaryFactory.itemList;
                });

            return;
        };

        $scope.getCount = function () {
            $scope.dictionaryFactory
                .getCount($scope.dictionaryFactory.currentType)
                .then(function () {
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
        };;

        $scope.deleteItem = function(id, message) {

            if (message != undefined)
                var result = confirm(message);

            if (result === false)
                return;

            return $scope.dictionaryFactory
                .deleteItem($scope.dictionaryFactory.currentType, id)
                .then(function() {
                    $scope.refreshData();
                });
        };

        $scope.init = function(url) {
            $scope.dictionaryFactory.baseUrl = url;
            $scope.getTypes();
        };
    }
]);