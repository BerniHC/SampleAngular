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


// String Format
app.filter('format', function () {
    function toFormattedString(useLocale, format, values) {
        var result = '';

        for (var i = 0; ;) {
            // Find the next opening or closing brace
            var open = format.indexOf('{', i);
            var close = format.indexOf('}', i);
            if ((open < 0) && (close < 0)) {
                // Not found: copy the end of the string and break
                result += format.slice(i);
                break;
            }
            if ((close > 0) && ((close < open) || (open < 0))) {

                if (format.charAt(close + 1) !== '}') {
                    throw new Error('format stringFormatBraceMismatch');
                }

                result += format.slice(i, close + 1);
                i = close + 2;
                continue;
            }

            // Copy the string before the brace
            result += format.slice(i, open);
            i = open + 1;

            // Check for double braces (which display as one and are not arguments)
            if (format.charAt(i) === '{') {
                result += '{';
                i++;
                continue;
            }

            if (close < 0) throw new Error('format stringFormatBraceMismatch');

            // Find the closing brace

            // Get the string between the braces, and split it around the ':' (if any)
            var brace = format.substring(i, close);
            var colonIndex = brace.indexOf(':');
            var argNumber = parseInt((colonIndex < 0) ? brace : brace.substring(0, colonIndex), 10);

            if (isNaN(argNumber)) throw new Error('format stringFormatInvalid');

            var argFormat = (colonIndex < 0) ? '' : brace.substring(colonIndex + 1);

            var arg = values[argNumber];
            if (typeof (arg) === "undefined" || arg === null) {
                arg = '';
            }

            // If it has a toFormattedString method, call it.  Otherwise, call toString()
            if (arg.toFormattedString) {
                result += arg.toFormattedString(argFormat);
            } else if (useLocale && arg.localeFormat) {
                result += arg.localeFormat(argFormat);
            } else if (arg.format) {
                result += arg.format(argFormat);
            } else
                result += arg.toString();

            i = close + 1;
        }

        return result;
    };

    return function (/*string*/template, /*array*/values) {
        if (!values || !values.length || !template) {
            return template;
        }
        return toFormattedString(false, template, values);
    };
});
