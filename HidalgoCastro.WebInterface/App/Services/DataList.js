app.factory('DataList', ['$http', '$timeout', '$filter',
    function ($http, $timeout, $filter) {

        var isLoading = false;

        // Constructor
        function DataList(url) {
            if (url) {
                this.options.url = url;
            }
        }

        // Métodos
        DataList.prototype = {
            // Arreglo de datos
            data: [],
            // Opciones de configuración
            options: {
                url: '/',
                page: 1,
                count: 10,
                total: 0,
                sort: 'Id',
                reverse: false,
                search: '',
            },
            // Cargar elementos
            load: function () {
                // Verificar si ya está cargando
                if (isLoading) return;
                else isLoading = true;

                // Verificar si faltan elementos por cargar
                //if (!this.hasMore()) return;
                
                // Determinar orden
                var order = this.options.reverse ? "DESC" : "ASC";

                // Definir url
                var url = $filter('format')("{0}/{1}/{2}/{3}/{4}/{5}",
                    this.options.url, this.options.page, this.options.count, this.options.sort, order, this.options.search);

                // Invocar servicio web
                return $http.get(url).then(function (response) {
                    // Verificar respuesta correcta
                    if (response.status == 200 && response.data.Status == 0) {
                        // Agregar nuevos elementos al arreglo de datos
                        DataList.prototype.data.push.apply(DataList.prototype.data, response.data.Data.List);
                        DataList.prototype.options.total = response.data.Data.TotalRows;
                    }

                    isLoading = false;
                }, function (response) {
                    isLoading = false;
                });
            },
            delete: function () {
                // Verificar si ya está cargando
                if (isLoading) return;
                else isLoading = true;

                if (!this.hasChecked()) return;

                var items = $filter('filter')(this.data, { Checked: true });
                var ids = [];

                angular.forEach(items, function (item) {
                    ids.push(item.Id);
                })

                return $http({
                    url: this.options.url,
                    method: 'DELETE',
                    data: ids,
                    headers: { "Content-Type": "application/json;charset=utf-8" }
                }).then(function () {
                    isLoading = false;
                    this.load();
                }, function () {
                    isLoading = false;
                });
            },
            checkAll: function (value) {
                // Verificar si ya está cargando
                if (isLoading) return;

                angular.forEach(this.data, function (item) {
                    item.Checked = value;
                });
            },
            hasChecked: function () {
                var checked = $filter('filter')(this.data, { Checked: true });
                return checked.length > 0;
            },
            more: function () {
                // Verificar si ya está cargando y si faltan elementos por cargar
                if (isLoading || !this.hasMore()) return;

                this.options.page++;
                this.load();
            },
            hasMore: function () {
                return this.data.length < this.options.total;
            },
            info: function () {
                return {
                    length: this.data.length,
                    total: this.options.total
                };
            },
            sort: function (sort) {
                // Verificar si ya está cargando
                if (isLoading) return;

                this.options.reverse = (this.options.sort === sort) ? !this.options.reverse : false;
                this.options.sort = sort;
            },
            order: function (sort) {
                if (this.options.sort == sort) {
                    if (this.options.reverse) {
                        return 'desc';
                    } else {
                        return 'asc';
                    }
                }
            },
            isLoading: function () {
                return isLoading;
            }
        }

        return DataList;

    }
]);