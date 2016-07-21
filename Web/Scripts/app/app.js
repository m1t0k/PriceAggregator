"use strict";
var app = angular.module("PaWeb", ["angularUtils.directives.dirPagination", "angular-growl", "ngMessages", "ngRoute"])
    .config(function($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
    });