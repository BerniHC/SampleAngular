app.controller('MainController', ['$scope', 'Loader',
    function ($scope, Loader) {

        // --------------------------------------
        // Scope
        // --------------------------------------

        $scope.showLoader = Loader.showLoader;

    }
]);