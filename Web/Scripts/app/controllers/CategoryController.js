"use strict";
app.controller("CategoryController", ["$scope", "$http", "categoryFactory",
        function($scope, $http, categoryFactory) {

            $scope.categoryFactory = categoryFactory;
            $scope.pageIndex = 1;
            $scope.pageSize = 20;
            $scope.categoryList = {};
            $scope.categoryCount = 0;
            $scope.sortName = "";
            $scope.acsending = true; // asc
            $scope.pageSizes = [10, 15, 20, 50, 100];
            $scope.currentCategory = null;
            $scope.$error = null;

            $scope.changePageIndex = function(pageIndex) {
                $scope.getCategoryList(pageIndex, $scope.pageSize, $scope.sortName);
            };

            $scope.changePageSize = function() {
                $scope.getCategoryList($scope.pageIndex, $scope.pageSize, $scope.sortName);
            };

            $scope.changeSortName = function(sortName) {
                $scope.getCategoryList($scope.pageIndex, $scope.pageSize, sortName);
            };


            $scope.openEditDialog = function(category) {
                if (category != undefined) {
                    $scope.currentCategory = category;
                } else {
                    $scope.currentCategory = { Id: 0 };
                }
                $("#EditCategoryDialog")
                    .dialog({
                        autoOpen: false,
                        height: 300,
                        width: 550,
                        modal: true
                    });
                $("#EditCategoryDialog").dialog("open");
            };

            $scope.getSortExpression = function() {

                if ($scope.sortName == undefined || $scope.sortName === "")
                    return "";
                return $scope.sortName + ($scope.acsending ? "" : " desc");
            };

            $scope.getCategoryList = function(pageIndex, pageSize, sortName) {
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

                $scope.categoryFactory
                    .getCategoryList($scope.pageIndex, $scope.pageSize, $scope.getSortExpression())
                    .then(function() {
                        $scope.categoryList = $scope.categoryFactory.categoryList;
                    });

                return;
            };
            $scope.getCategoryCount = function() {
                $scope.categoryFactory
                    .getCategoryCount()
                    .then(function() {
                        $scope.categoryCount = $scope.categoryFactory.categoryCount;
                    });
            };

            $scope.getCategory = function(id) {
                return $scope.categoryFactory
                    .getCategory(id);
            };

            $scope.saveCategory = function() {
                if ($scope.currentCategory == undefined)
                    return;

                if ($scope.currentCategory.Id === 0)
                    return $scope.categoryFactory
                        .createCategory($scope.currentCategory)
                        .success(function(data) {

                        })
                        .error(function() {

                        });;

                return $scope.categoryFactory
                    .updateCategory($scope.currentCategory)
                    .success(function(data) {

                    })
                    .error(function() {

                    });;
            };


            $scope.deleteCategory = function(id, message) {

                if (message != undefined)
                    var result = confirm(message);
                if (!result)
                    return result;
                return $scope.categoryFactory
                    .deleteCategory(id)
                    .then(function() {
                        $scope.getCategoryList();
                        $scope.getCategoryCount();
                    });
            };
            $scope.init = function(url) {
                $scope.categoryFactory.baseUrl = url;
                $scope.getCategoryList();
                $scope.getCategoryCount();
            };
        }]);