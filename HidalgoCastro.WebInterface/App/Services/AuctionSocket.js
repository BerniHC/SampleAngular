app.factory('AuctionSocket', function (socketFactory) {

    var ws = new WebSocket("ws://" + window.location.hostname + "/api/auction");

    ws.onopen = function () {
        $("#spanStatus").text("connected");
    };

    ws.onmessage = function (evt) {
        $("#spanStatus").text(evt.data);
    };

    ws.onerror = function (evt) {
        $("#spanStatus").text(evt.message);
    };

    ws.onclose = function () {
        $("#spanStatus").text("disconnected");
    };

    return ws;

});