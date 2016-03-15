app.factory('Permission', ['DataModel',
    function (DataModel) {

        // Constructor
        function Permission(data) {
            if (data) {
                this.setData(data);
            }
        };

        // Methods
        Permission.prototype = new DataModel('/api/permission');

        return Permission;
    }
]);
