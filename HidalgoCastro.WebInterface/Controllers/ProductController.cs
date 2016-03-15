using HidalgoCastro.WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HidalgoCastro.WebInterface.Controllers
{
    /// <summary>
    /// Controlador de productos
    /// </summary>
    public class ProductController : ApiController
    {
        private DataAccess.ProductData dataProduct = new DataAccess.ProductData();
        private DataAccess.ImageData dataImage = new DataAccess.ImageData();

        /// <summary>
        /// Obtener todos los roles
        /// </summary>
        /// <returns>Lista de roles</returns>
        public Response<IEnumerable<Entities.Product>> Get()
        {
            try
            {
                var result = dataProduct.SelectAll();

                return new Response<IEnumerable<Entities.Product>>(result, result.Count(), ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Product>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Obtener producto por id
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Producto seleccionado</returns>
        public Response<Entities.Product> Get(int id)
        {
            try
            {
                var result = dataProduct.SelectById(id);

                return new Response<Entities.Product>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Product>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Filtrar todos los productos
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="count">Cantidad de registros por página</param>
        /// <param name="sort">Predicado de ordenamiento</param>
        /// <param name="sortOrder">Dirección de ordenamiendo</param>
        /// <param name="search">Búsqueda</param>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        public Response<IEnumerable<Entities.Product>> Paginate(int page, int count, string sort = "Id", string sortOrder = "ASC", string search = "")
        {
            try
            {
                var result = dataProduct.Search(search);
                //result = dataProduct.Paginate(result, page, count, sort, sortOrder);

                var total = dataProduct.Count();

                return new Response<IEnumerable<Entities.Product>>(result, total, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Product>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Agregar producto
        /// </summary>
        /// <param name="product">Producto a agregar</param>
        /// <returns>Producto agregado</returns>
        public Response<Entities.Product> Post(Entities.Product product)
        {
            try
            {
                product = dataProduct.InsertWithImages(product);

                return new Response<Entities.Product>(product, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Product>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

    }
}
