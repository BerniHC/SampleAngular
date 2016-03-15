// --------------------------------------
// Constants 
// --------------------------------------

angular.module('app-config', [])
.constant('Config', {
    AppName: 'Sample Angular',
})
.constant('ResponseStatus', {
    Success: 0,
    Error: 1,
    InvalidRequest: 2
});

// --------------------------------------
// Thumbnail Service Provider
// --------------------------------------

app.config(['ThumbnailServiceProvider',
    function (ThumbnailServiceProvider) {
        ThumbnailServiceProvider.defaults.width = 120;
        ThumbnailServiceProvider.defaults.height = 120;
    }
]);

// --------------------------------------
// Http Provider 
// --------------------------------------

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('HttpInterceptor');
}]);