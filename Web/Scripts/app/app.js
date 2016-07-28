"use strict";
var app = angular.module("PaWeb", ["angularUtils.directives.dirPagination", "ngMessages", "ngRoute"]);
app.run(function ($http) {
    
        $http.defaults.headers.common = {};
        $http.defaults.headers.post = {};
        $http.defaults.headers.put = {};
        $http.defaults.headers.patch = {};

        //delete $http.defaults.headers.common["X-Requested-With"];
        $http.defaults.headers.common['ApiKey'] = "0F4AE988-80FE-4280-910A-6336D083FB12";
        $http.defaults.useXDomain = true;
        $http.defaults.withCredentials = true;
    });