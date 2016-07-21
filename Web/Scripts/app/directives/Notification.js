"use strict";

app.directive("notification",
[
    "$timeout", function($timeout) {
        return {
            restrict: "E",
            template:
                "<div class='alert alert-{{alertData.type}}' ng-show='alertData.message' role='alert' data-notification='{{alertData.status}}'>{{alertData.message}}</div>",
            scope: {
                alertData: "="
            }
        };
    }
]);