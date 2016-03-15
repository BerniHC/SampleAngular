app.factory('DataTable', ['$http', '$timeout', '$filter',
    function ($http, $timeout, $filter) {

        // Constructor
        function DataTable(url) {
            if (url) {
                this.options.url = url;
            }
        }

        // Methods
        DataTable.prototype = {
            data: [],
            options: {
                url: '/',
                page: 1,
                pages: [],
                count: 10,
                counts: [10, 25, 50, 100],
                total: 0,
                sort: 'Id',
                reverse: false,
                search: '',
            },
            load: function () {
                var order = this.options.reverse ? "DESC" : "ASC";

                var data = {
                    page: this.options.page,
                    count: this.options.count,
                    sort: this.options.sort,
                    order: order,
                    search: this.options.search
                }

                return $http.post(this.options.url + "/paginate", data).then(function (response) {
                    if (response.status == 200 && response.data.Status == 0) {
                        DataTable.prototype.data = response.data.Data;
                        DataTable.prototype.options.total = response.data.Count;
                        DataTable.prototype.options.pages = [];

                        var pagesCount = Math.ceil(DataTable.prototype.options.total / DataTable.prototype.options.count);
                        if (pagesCount <= 5) {
                            for (i = 1; i <= pagesCount; i++) {
                                DataTable.prototype.options.pages.push(i);
                            }
                        } else {
                            var start = DataTable.prototype.options.page - 2;
                            var end = DataTable.prototype.options.page + 2;

                            if (start < 1) {
                                end += (1 - start);
                                start = 1;
                            }

                            if (end > pagesCount) {
                                start -= (end - pagesCount);
                                end = pagesCount;
                            }

                            for (i = start; i <= end; i++) {
                                DataTable.prototype.options.pages.push(i);
                            }
                        }
                    }
                });
            },
            delete: function () {
                var roles = $filter('filter')(this.data, { Checked: true });
                var ids = [];
                angular.forEach(roles, function (role) {
                    ids.push(role.Id);
                })

                return $http({
                    url: this.options.url,
                    method: 'DELETE',
                    data: roles,
                    headers: { "Content-Type": "application/json;charset=utf-8" }
                });
            },
            checkAll: function (value) {
                angular.forEach(this.data, function (item) {
                    item.Checked = value;
                });
            },
            hasChecked: function () {
                var result = false;

                for (var i = 0; i < this.data.length; i++) {
                    var item = this.data[i];

                    if (item.Checked) {
                        result = true;
                        break;
                    };
                };

                return result;
            },
            setPage: function (page) {
                this.options.page = page;
                this.load();
            },
            hasPrevious: function () {
                return this.options.page > 1;
            },
            previous: function () {
                if (this.hasPrevious()) {
                    this.options.page--;
                    this.load();
                }
            },
            hasNext: function () {
                var pagesCount = Math.ceil(this.options.total / this.options.count);
                return this.options.page < pagesCount;
            },
            next: function () {
                if (this.hasNext()) {
                    this.options.page++;
                    this.load();
                }
            },
            first: function () {
                this.options.page = 1;
                this.load();
            },
            last: function () {
                var pagesCount = Math.ceil(this.options.total / this.options.count);
                this.options.page = pagesCount;
                this.load();
            },
            info: function () {
                var start = (this.options.count * (this.options.page - 1));
                var end = start + this.data.length;

                return {
                    start: start,
                    end: end,
                    total: this.options.total
                };
            },
            sort: function (sort) {
                this.options.reverse = (this.options.sort === sort) ? !this.options.reverse : false;
                this.options.sort = sort;

                this.load();
            },
            order: function (sort) {
                if (this.options.sort == sort) {
                    if (this.options.reverse) {
                        return 'desc';
                    } else {
                        return 'asc';
                    }
                }
            }
        }

        return DataTable;

    }
]);