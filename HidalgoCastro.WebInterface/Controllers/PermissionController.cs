﻿using HidalgoCastro.WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HidalgoCastro.WebInterface.Controllers
{
    /// <summary>
    /// Controlador de permisos
    /// </summary>
    public class PermissionController : ApiController
    {
        private DataAccess.PermissionData dataPermission = new DataAccess.PermissionData();

        /// <summary>
        /// Obtener todos los permisos
        /// </summary>
        /// <returns>Lista de permisos</returns>
        public Response<IEnumerable<Entities.Permission>> Get()
        {
            try
            {
                var result = dataPermission.SelectAll();

                return new Response<IEnumerable<Entities.Permission>>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Permission>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Paginar todos los roles
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="rows">Cantidad de filas por página</param>
        /// <param name="sort">Predicado de ordenamiento</param>
        /// <param name="order">Dirección de ordenamiento</param>
        /// <param name="search">Términos de búsqueda</param>
        /// <returns>Lista de roles con paginación</returns>
        [HttpGet]
        public Response<Entities.Pagination<Entities.Permission>> Paginate(
            int page = 1, int rows = 10, string sort = "Id", string order = "ASC", string search = "")
        {
            try
            {
                var result = dataPermission.Paginate(new Entities.Pagination<Entities.Permission>
                {
                    Page = page,
                    RowsPerPage = rows,
                    Sort = sort,
                    Order = order,
                    Search = search
                });

                return new Response<Entities.Pagination<Entities.Permission>>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Pagination<Entities.Permission>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Obtener permiso por id
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Permiso seleccionado</returns>
        public Response<Entities.Permission> Get(int id)
        {
            try
            {
                var result = dataPermission.SelectById(id);

                return new Response<Entities.Permission>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Permission>(null, ResponseStatus.ERROR, ex.Message);
            }
        }
        
        /// <summary>
        /// Agregar permiso
        /// </summary>
        /// <param name="permission">Permiso a agregar</param>
        /// <returns>Permiso agregado</returns>
        public Response<Entities.Permission> Post(Entities.Permission permission)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<Entities.Permission>(null, ResponseStatus.INVALID_REQUEST);

                permission = dataPermission.Insert(permission);

                return new Response<Entities.Permission>(permission, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Permission>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar permiso
        /// </summary>
        /// <param name="id">Identificador del permiso</param>
        /// <param name="permission">Permiso a actualizar</param>
        /// <returns>Permiso actualizado</returns>
        public Response<Entities.Permission> Put(int id, Entities.Permission permission)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<Entities.Permission>(null, ResponseStatus.INVALID_REQUEST);

                permission = dataPermission.Update(permission);

                return new Response<Entities.Permission>(permission, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Permission>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Eliminar permiso por id
        /// </summary>
        /// <param name="id">Identificador de permiso</param>
        /// <returns>Permiso eliminado</returns>
        public Response<Entities.Permission> Delete(int id)
        {
            try
            {
                var result = dataPermission.Delete(id);

                return new Response<Entities.Permission>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Permission>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Eliminar permisos
        /// </summary>
        /// <param name="ids">Identificadores de permisos</param>
        /// <returns>Permisos eliminados</returns>
        public Response<IEnumerable<Entities.Permission>> Deletes(int[] ids)
        {
            try
            {
                var result = dataPermission.Delete(ids);

                return new Response<IEnumerable<Entities.Permission>>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Permission>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }
    }
}
