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
            .when("/create/:type/",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "DashboardController",
                controllerAs: "PaWeb"
            })
            .when("/edit/:type/:id",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "DashboardController",
                controllerAs: "PaWeb"
            });
    }
]);