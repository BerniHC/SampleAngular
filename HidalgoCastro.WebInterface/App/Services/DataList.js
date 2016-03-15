app.factory('DataList', ['$http', '$timeout', '$filter',
    function ($http, $timeout, $filter) {

        // Constructor
        function DataList(url) {
            if (url) {
                this.options.url = url;
            }
        }

        // Methods
        DataTable.prototype = {
            data: [],
            options: {
                url: '/',
                total: 0,
                predicate: 'Id',
                reverse: false,
                search: '',
            },
            setOptions: function (options) {
                if (options.url) this.options.url = options.url;
                if (options.total) this.options.total = options.total;
                if (options.predicate) this.options.predicate = options.predicate;
                if (options.reverse) this.options.reverse = options.reverse;
            },
            load: function () {
                var sortOrder = this.options.reverse ? 'DESC' : 'ASC';
                var url =
                    this.options.url + '/' +
                    this.options.predicate + '/' +
                    sortOrder + '/' +
                    this.options.search;

                return $http.get(url).then(function (response) {
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
            sort: function (predicate) {
                this.options.reverse = (this.options.predicate === predicate) ? !this.options.reverse : false;
                this.options.predicate = predicate;

                this.load();
            },
            sortOrder: function (predicate) {
                if (this.options.predicate == predicate) {
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