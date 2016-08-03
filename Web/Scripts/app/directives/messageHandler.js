"use strict";


app.directive("messagehandler", function() {
    return {
        replace: true,
        scope: {
            classname: "=",
            errormessage: "="
        },
        template: "<div ngShow='angular.IsDefined($scope.errormessage);' class='{{classname}}'>{{errormessage}}</div>"
    };
});