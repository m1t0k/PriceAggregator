"use strict";



app.directive('hello3', function () {
    return {
        replace: true,
        scope: {
            classname: "=",
            errormessage: "="
        },
        template: '<div class="{{classname}}" >{{errormessage}}</div>'
    };
});



/*
app.directive("messageHandler2",
    function() {
        return {
            
            link: function ($scope, $element, $attrs) {
                $scope.className = "";
                $scope.message = "";
                $scope.isVisible = false;
               

                $scope.showMessage = function(className, message) {
                    $scope.className = className;
                    $scope.message = message;
                    $scope.isVisible = true;
                };

                $scope.showInfo = function(message) {
                    $scope.showMessage("info",message);
                };

                $scope.showWarning = function (message) {
                    $scope.showMessage("warning", message);
                };

                $scope.showSuccess = function (message) {
                    $scope.showMessage("sussess", message);
                };

                $scope.showError = function (message) {
                    $scope.showMessage("error", message);
                };

            },
            priority: 0,
            terminal: false,
            templateUrl: '/AngularTemplates/Inline?templateName=messageHandler',
            replace: false,
            transclude: false,
            restrict: 'E',
            scope:false
        }
    });
    */