app.config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/");

        $stateProvider
            // Main
            .state('home', {
                url: "/",
                templateUrl: "App/Views/Main/Home.html",
                controller: "HomeController"
            })
            .state('auctions', {
                url: "/auctions",
                templateUrl: "App/Views/Main/Auctions.html",
                controller: "AuctionController"
            })
            // Admin
            .state('dashboard', {
                url: "/admin",
                templateUrl: "App/Views/Admin/Dashboard.html",
                controller: "DashboardController"
            })
            .state('products', {
                url: "/admin/products",
                templateUrl: "App/Views/Admin/Products/Index.html",
                controller: "ProductController"
            })
            .state('role-list', {
                url: "/admin/role",
                templateUrl: "App/Views/Admin/Roles/List.html",
                controller: "RoleController"
            })
            .state('role-add', {
                url: "/admin/role/add",
                templateUrl: "App/Views/Admin/Roles/Add.html",
                controller: "RoleController"
            })
            .state('role-edit', {
                url: "/admin/role/:id/edit",
                templateUrl: "App/Views/Admin/Roles/Edit.html",
                controller: "RoleController"
            })
            .state('role-view', {
                url: "/admin/role/:id",
                templateUrl: "App/Views/Admin/Roles/View.html",
                controller: "RoleController"
            });

    }
]);