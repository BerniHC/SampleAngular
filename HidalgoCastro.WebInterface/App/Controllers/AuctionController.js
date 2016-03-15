app.controller('AuctionController', ['$scope',
    function ($scope) {

        $scope.listAuctions = {
            auctions: [
            { Id: 1, Name: 'First Auction', Price: 0.01, EndDateTime: new Date(2016, 01, 31, 23, 59, 59, 0) },
            { Id: 2, Name: 'Second Auction', Price: 0.01, EndDateTime: new Date(2016, 01, 31, 23, 59, 59, 0) }
            ],
            serverDateTime: new Date()
        }

        var ws = $.connection.AuctionsHub;

        ws.client.addMessage = function (message) {
            if(message != undefined)
            $scope.$apply(function () {
                $scope.listAuctions.auctions = message.Data;
                $scope.listAuctions.serverDateTime = message.ServerDateTime;
            });
        }

        // Send
        $scope.send = function (auction) {
            auction.Price += 0.01;

            var message = JSON.stringify(auction);

            ws.server.send(message);
        }

        $.connection.hub.start();
    }
]);