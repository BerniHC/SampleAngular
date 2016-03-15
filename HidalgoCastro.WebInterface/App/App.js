var app = angular.module('SampleAngular', [
    'app-config',
    'ui.bootstrap',
    'ui.router',
    'ngResource',
    'ngAnimate',
    'ngSanitize',
    'dialogs.main',
    'ui.thumbnail',
]);

// --------------------------------------
// Global 
// --------------------------------------

app.run(['$rootScope', '$state', 'Loader', 
    function ($rootScope, $state, Loader) {

        $rootScope.$on("$stateChangeStart", function (event, next, current) {
            Loader.waitProcess('route-change');
        });

        $rootScope.$on('$stateChangeSuccess', function (event, current, previous, rejection) {
            Loader.releaseProcess('route-change');
        });

    }
])