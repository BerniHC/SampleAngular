// Ratio
app.directive('ngRatio', function () {
    return {
        restric: 'A',
        link: function (scope, element, attr) {
            var ratio = +(attr.ngRatio);
            element.css('width', ratio + '%');
        }
    };
});

// Enter Key
app.directive('ngEnterKey', function () {
    return function (scope, element, attrs) {

        element.bind("keydown keypress", function (event) {
            var keyCode = event.which || event.keyCode;

            // If enter key is pressed
            if (keyCode === 13) {
                scope.$apply(function () {
                    // Evaluate the expression
                    scope.$eval(attrs.ngEnterKey);
                });

                event.preventDefault();
            }
        });
    };
});

// Dropzone JS
app.directive('dropzone', function () {
    return function (scope, element, attrs) {
        var config, dropzone;

        config = scope[attrs.dropzone];

        // create a Dropzone for the element with the given options
        dropzone = new Dropzone(element[0], config.options);

        // bind the given event handlers
        angular.forEach(config.eventHandlers, function (handler, event) {
            dropzone.on(event, handler);
        });
    };
});

// Field Validator
app.directive('ngValidField', function () {
    return {
        restric: 'A',
        scope: { ngValidField: '=' },
        link: function (scope, element, attr) {

            var event = function (event) {
                setTimeout(function () {
                    var keyCode = event.which || event.keyCode;
                    var field = scope.ngValidField;

                    if (field.$touched && field.$invalid)
                        element.addClass('has-error');
                    else
                        element.removeClass('has-error');
                }, 20);
            };

            element.bind("focusout", event);

        }
    };
});

// Only Number - Allow only number in input
app.directive('onlyNumber', function () {
    return function (scope, element, attrs) {
        var keyCode = [8, 9, 13, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 110, 190];
        element.bind("keydown", function (event) {
            if ($.inArray(event.which, keyCode) == -1) {
                scope.$apply(function () {
                    scope.$eval(attrs.onlyNum);
                    event.preventDefault();
                });
                event.preventDefault();
            }
        });
    };
});

// Only Digits - Allow only digits in input
app.directive('onlyDigits', function () {
    return function (scope, element, attrs) {
        var keyCode = [8, 9, 13, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
        element.bind("keydown", function (event) {
            if ($.inArray(event.which, keyCode) == -1) {
                scope.$apply(function () {
                    scope.$eval(attrs.onlyNum);
                    event.preventDefault();
                });
                event.preventDefault();
            }
        });
    };
});
