app.controller("TopbarController", ['$scope', 'Config', 
    function ($scope, Config) {

        // --------------------------------------
        // Variables
        // --------------------------------------

        $scope.title = Config.AppName;
        $scope.topbarCollapse = true;

        // --------------------------------------
        // Functions
        // --------------------------------------

        // Toggle Sidebar
        $scope.toggleSidebar = function () {
            angular.element(".sidebar-wrap").toggleClass('open');
        }

    }
]);