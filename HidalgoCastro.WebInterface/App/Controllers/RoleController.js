app.controller("RoleController", ['$scope', '$filter', '$stateParams', '$state', 'Role', 'DataTable', 'DataList', 'Loader', 'dialogs', 'ResponseStatus',
    function ($scope, $filter, $stateParams, $state, Role, DataTable, DataList, Loader, dialogs, ResponseStatus) {

        // --------------------------------------
        // Variables
        // --------------------------------------

        $scope.role = new Role();
        $scope.table = new DataTable('/api/role');
        $scope.permissions = new DataList('/api/permission');

        // --------------------------------------
        // Main Functions
        // --------------------------------------

        // List
        $scope.list = function () {
            $scope.table.load();
        }

        // Load
        $scope.load = function () {
            if (!angular.isUndefined($stateParams.id)) {
                $scope.role = new Role();

                Loader.waitProcess('role-load');
                $scope.role.get($stateParams.id).then(function () {
                    Loader.releaseProcess('role-load');
                }, function () {
                    Loader.releaseProcess('role-load');
                });

            }
        }

        // Save
        $scope.save = function () {
            Loader.waitProcess('role-save');

            $scope.role.save().then(function (response) {
                Loader.releaseProcess('role-save');

                if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                    $state.go('role-list');
                }
            }, function () {
                Loader.releaseProcess('role-save');
            });
        }

        // Delete
        $scope.delete = function (role) {
            var dlg = dialogs.confirm('Confirmation', 'You are sure?', { size: 'sm' });

            dlg.result.then(function () {
                Loader.waitProcess('role-delete');

                $scope.role = new Role(role);
                $scope.role.delete().then(function (response) {
                    Loader.releaseProcess('role-delete');

                    if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                        if ($state.is('role-list')) {
                            $scope.list();
                        } else {
                            $state.go('role-list');
                        }
                    }
                }, function (response) {
                    Loader.releaseProcess('role-delete');
                });
            });
        }

        // Deletes
        $scope.deletes = function (role) {
            var dlg = dialogs.confirm('Confirmation', 'You are sure?', { size: 'sm' });

            dlg.result.then(function () {
                $scope.table.delete().then(function (response) {
                    if (response.status == 200 && response.data.Status == ResponseStatus.Success) {
                        console.log('deletes success!')
                    }
                }, function (response) {
                    console.log('deletes failed!')
                });
            });
        }

    }
]);