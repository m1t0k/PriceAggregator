"use strict";
app.config([
    "$locationProvider", "$routeProvider",
    function config($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix("!");

        $routeProvider.when("/:type?",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=DashboardList",
                controller: "dictionaryDashboardController",
                controllerAs: "PaWeb"
            })
            .when("/create/:type/",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "dictionaryEditController",
                controllerAs: "PaWeb"
            })
            .when("/edit/:type/:id",
            {
                templateUrl: "/AngularTemplates/Inline?templateName=Edit",
                controller: "dictionaryEditController",
                controllerAs: "PaWeb"
            });
    }
]);