
// Format
app.filter("format", function () {
    return function (input) {
        var args = arguments;
        return input.replace(/\{(\d+)\}/g, function (match, capture) {
            return args[1 * capture + 1];
        });
    };
});