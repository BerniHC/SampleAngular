app.factory('Product', ['DataModel',
    function (DataModel) {

        // Constructor
        function Product(data) {
            if (data) {
                this.setData(data);
            }
        };

        // Methods
        Product.prototype = new DataModel('/api/product');

        return Product;
    }
]);
