app.factory('HttpInterceptor', ['$q', '$filter', 'ResponseStatus',
    function ($q, $filter, ResponseStatus) {

        // Validate Response
        var validateResponse = function (response) {
            var data = response.data;
            var status = response.status;

            switch (status) {
                case 200: // OK
                    switch (data.Status) {
                        case ResponseStatus.Success:
                            return response || $q.when(response);
                            break;
                        case ResponseStatus.Error:
                            break;
                        case ResponseStatus.InvalidRequest:
                            break;
                        default:
                            return response || $q.when(response);
                            break;
                    }
                case 400: // Bad Request
                    //dialogs.error('Bad Request', 'Bad Request', { size: 'sm' });
                    break;
                case 401: // Unauthorized
                    //dialogs.error('Unauthorized', 'Unauthorized', { size: 'sm' });
                    break;
                case 403: // Forbidden
                    //dialogs.error('Forbidden', 'Forbidden', { size: 'sm' });
                    break;
                case 404: // Not Found
                    //dialogs.error('Not Found', 'Not Found', { size: 'sm' });
                    break;
                case 405: // Method Not Allowed
                    //dialogs.error('Method Not Allowed', 'Method Not Allowed', { size: 'sm' });
                    break;
                case 408: // Request Timeout
                    //dialogs.error('Request Timeout', 'Request Timeout', { size: 'sm' });
                    break;
                case 500: // Server Error
                    //dialogs.error('Server Error', 'Server Error', { size: 'sm' });
                    break;
                case 501: // Not Implemented
                    //dialogs.error('Not Implemented', 'Not Implemented', { size: 'sm' });
                    break;
                case 503: // Service Unavailable
                    //dialogs.error('Service Unavailable', 'Service Unavailable', { size: 'sm' });
                    break;
                default: // Other
                    break;
            }
            return response || $q.when(response);
        }

        return {
            // Request
            request: function (config) {
                config.headers = config.headers || {};

                if (window.sessionStorage.getItem('token')) {
                    config.headers.Authorization = window.sessionStorage.getItem('token');
                } else {
                    //$state.go('login');
                }

                return config || $q.when(config);
            },
            // Response Success
            response: function (response) {
                return validateResponse(response);
            },
            // Response Error
            responseError: function (response) {
                return validateResponse(response);
            }
        };

    }]);
