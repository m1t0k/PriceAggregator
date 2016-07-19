"use strict";
app.config([
    "$locationProvider", "$routeProvider",
    function config($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix("!");

        $routeProvider.when("/",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=DashboardList",
                controller: "DashboardController",
                controllerAs: "PaWeb"
            })
            .when("/create",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "DashboardController",
                controllerAs: "PaWeb"
            })
            .when("/edit",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "DashboardController",
                controllerAs: "PaWeb"
            });
    }
]);