app.factory('Alert', ['$filter', '$timeout', '$cookies',
    function ($filter, $timeout, $cookies) {

        var alerts = [];

        // Show Alert
        var showAlert = function (alert) {
            var prev = $filter('filter')(alerts, { Id: alert.Id })[0];

            if (prev) {
                prev.Count++;
            } else {
                alert.Count--;

                alerts.push(alert);

                $timeout(function () {
                    hideAlert(alert);
                }, alert.Delay);
            }
        }

        // Hide Alert
        var hideAlert = function (alert) {
            if (alert.Count == 0) {
                var idx = alerts.indexOf(alert);
                alerts.splice(idx, 1);
            } else {
                alert.Count--;

                $timeout(function () {
                    hideAlert(alert);
                }, alert.Delay);
            }
        }

        // List
        var list = function () {
            alerts = $cookies.getObject('alerts-messages');
            return alerts;
        }

        // Error
        var error = function (title, message, delay, id) {

            var alert = {
                Id: id,
                Title: title,
                Message: message,
                Delay: delay,
                Count: 1,
                Type: 'alert-danger',
            }

            showAlert(alert);
        }

        // Warning
        var warning = function (title, message, delay, id) {

            var alert = {
                Id: id,
                Title: title,
                Message: message,
                Delay: delay,
                Count: 1,
                Type: 'alert-warning',
            }

            showAlert(alert);
        }

        // Info
        var info = function (title, message, delay, id) {

            var alert = {
                Id: id,
                Title: title,
                Message: message,
                Delay: delay,
                Count: 1,
                Type: 'alert-info',
            }

            showAlert(alert);
        }

        // Success
        var success = function (title, message, delay, id) {

            var alert = {
                Id: id,
                Title: title,
                Message: message,
                Delay: delay,
                Count: 1,
                Type: 'alert-success',
            }

            showAlert(alert);
        }

        return {
            list: list,
            error: error,
            warning: warning,
            info: info,
            success: success
        }

    }
]);
