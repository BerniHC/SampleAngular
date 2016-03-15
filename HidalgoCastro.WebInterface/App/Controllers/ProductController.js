app.controller('ProductController', ['$scope', '$filter', 'Product', 'DataTable', 'Loader', 'ResponseStatus', 'dialogs', 
    function ($scope, $filter, Product, DataTable, Loader, ResponseStatus, dialogs) {

        // --------------------------------------
        // Variables
        // --------------------------------------

        $scope.product = new Product();
        $scope.table = new DataTable('/api/product');

        $scope.showAdd = false;
        $scope.showEdit = false;
        $scope.showView = false;

        // --------------------------------------
        // Main Functions
        // --------------------------------------

        // List
        $scope.list = function () {
            Loader.waitProcess('product-list');

            $scope.table.load().then(function () {
                Loader.releaseProcess('product-list');
            }, function () {
                Loader.releaseProcess('product-list');
            });
        }

        // Add
        $scope.add = function () {
            $scope.product = new Product();

            $scope.showEdit = false;
            $scope.showView = false;
            $scope.showAdd = true;
        }

        // Edit
        $scope.edit = function (product) {
            $scope.product = new Product(product);

            $scope.showAdd = false;
            $scope.showView = false;
            $scope.showEdit = true;
        }

        // View
        $scope.view = function (product) {
            $scope.product = new Product(product);

            $scope.showAdd = false;
            $scope.showEdit = false;
            $scope.showView = true;
        }

        // Save
        $scope.save = function () {
            Loader.waitProcess('product-save');

            if ($scope.product.Images)
                angular.forEach($scope.product.Images, function (image) {
                    image.Description = $scope.product.Title
                });

            $scope.product.save().then(function (response) {
                Loader.releaseProcess('product-save');

                if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                    $scope.list();

                    $scope.showAdd = false;
                    $scope.showEdit = false;
                }
            }, function () {
                Loader.releaseProcess('product-save');
            });
        }

        // --------------------------------------
        // Other Functions
        // --------------------------------------

        // DropZone
        $scope.dropzoneConfig = {
            'options': {
                'url': '/blobs/upload',
                'addRemoveLinks': true
            },
            'eventHandlers': {
                'uploadprogress': function (file, progress, bytesSend) {
                    progress = progress.toFixed(0);

                    var bar = angular.element($('.dz-upload'));
                    bar.css('width', progress + '%');
                    bar.text(progress + '%');
                },
                'success': function (file, response) {
                    var def = angular.element($('.dz-default'));

                    file.path = response[0].FileUrl;

                    if (!$scope.product.Images)
                        $scope.product.Images = [];

                    $scope.product.Images.push({
                        Path: response[0].FileUrl,
                        Description: ''
                    });
                },
                'removedfile': function (file) {
                    if (file.path) {
                        var img = $filter('filter')($scope.product.Images, { Path: file.path })[0];
                        var idx = $scope.product.Images.indexOf(img);
                        $scope.product.Images.splice(idx, 1);
                    }
                }
            }
        };

        // Cancel
        $scope.cancel = function () {
            $scope.showAdd = false;
            $scope.showEdit = false;
            $scope.showView = false;
        }

    }
]);