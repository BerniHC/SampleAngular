app.controller("PermissionController", ['$scope', '$filter', '$stateParams', '$state', 'Permission', 'DataTable', 'Loader', 'dialogs', 'ResponseStatus',
    function ($scope, $filter, $stateParams, $state, Permission, DataTable, Loader, dialogs, ResponseStatus) {

        // --------------------------------------
        // Variables
        // --------------------------------------

        $scope.permission = new Permission();
        $scope.table = new DataTable('/api/permission');

        // --------------------------------------
        // Main Functions
        // --------------------------------------

        // List
        $scope.list = function () {
            Loader.waitProcess('permission-list');

            $scope.table.load().then(function () {
                Loader.releaseProcess('permission-list');
            }, function () {
                Loader.releaseProcess('permission-list');
            });
        }

        // Load
        $scope.load = function () {
            if (!angular.isUndefined($stateParams.id)) {
                $scope.permission = new Permission();

                Loader.waitProcess('permission-load');
                $scope.permission.get($stateParams.id).then(function () {
                    Loader.releaseProcess('permission-load');
                }, function () {
                    Loader.releaseProcess('permission-load');
                });

            }
        }

        // Save
        $scope.save = function () {
            Loader.waitProcess('permission-save');

            $scope.permission.save().then(function (response) {
                Loader.releaseProcess('permission-save');

                if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                    $state.go('permission-list');
                }
            }, function () {
                Loader.releaseProcess('permission-save');
            });
        }

        // Delete
        $scope.delete = function (permission) {
            var dlg = dialogs.confirm('Confirmation', 'You are sure?', { size: 'sm' });

            dlg.result.then(function () {
                Loader.waitProcess('permission-delete');

                $scope.permission = new Permission(permission);
                $scope.permission.delete().then(function (response) {
                    Loader.releaseProcess('permission-delete');

                    if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                        if ($state.is('permission-list')) {
                            $scope.list();
                        } else {
                            $state.go('permission-list');
                        }
                    }
                }, function (response) {
                    Loader.releaseProcess('permission-delete');
                });
            });
        }

        // Deletes
        $scope.deletes = function (permission) {
            var dlg = dialogs.confirm('Confirmation', 'You are sure?', { size: 'sm' });

            dlg.result.then(function () {
                Loader.waitProcess('permission-deletes');

                $scope.table.delete().then(function (response) {
                    Loader.releaseProcess('permission-deletes');

                    if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                        $scope.list();
                    }
                }, function (response) {
                    Loader.releaseProcess('permission-deletes');
                });
            });
        }

    }
]);