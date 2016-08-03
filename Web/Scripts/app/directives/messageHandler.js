"use strict";


app.directive("messagehandler", function() {
    return {
        replace: true,
        scope: {
            classname: "=",
            errormessage: "="
        },
        template: "<div class='{{classname}}'>{{errormessage}}</div>"
    };
});