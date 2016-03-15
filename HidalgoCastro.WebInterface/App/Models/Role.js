app.factory('Role', ['DataModel',
    function (DataModel) {

        // Constructor
        function Role(data) {
            if (data) {
                this.setData(data);
            }
        };

        // Methods
        Role.prototype = new DataModel('/api/role');

        return Role;
    }
]);
