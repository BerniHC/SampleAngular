using HidalgoCastro.WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HidalgoCastro.WebInterface.Controllers
{
    /// <summary>
    /// Controlador de roles
    /// </summary>
    public class RoleController : ApiController
    {
        private DataAccess.RoleData dataRole = new DataAccess.RoleData();

        /// <summary>
        /// Obtener todos los roles
        /// </summary>
        /// <returns>Lista de roles</returns>
        public Response<IEnumerable<Entities.Role>> Get()
        {
            try
            {
                var result = dataRole.SelectAll();

                return new Response<IEnumerable<Entities.Role>>(result, result.Count(), ResponseStatus.SUCCESS);
            } 
            catch(Exception ex) 
            {
                return new Response<IEnumerable<Entities.Role>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Obtener role por id
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Role seleccionado</returns>
        public Response<Entities.Role> Get(int id)
        {
            try
            {
                var result = dataRole.SelectById(id);

                return new Response<Entities.Role>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Role>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Paginar todos los roles
        /// </summary>
        /// <param name="pagination">Opciones de paginación</param>
        /// <returns></returns>
        [Route("api/role/paginate")]
        public Response<IEnumerable<Entities.Role>> Paginate(Entities.Pagination pagination)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<IEnumerable<Entities.Role>>(null, ResponseStatus.INVALID_REQUEST);

                var result = dataRole.Page(pagination);
                
                var total = dataRole.Count();

                return new Response<IEnumerable<Entities.Role>>(result, total, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Role>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Agregar rol
        /// </summary>
        /// <param name="role">Rol a agregar</param>
        /// <returns>Role agregado</returns>
        public Response<Entities.Role> Post(Entities.Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<Entities.Role>(null, ResponseStatus.INVALID_REQUEST);

                role = dataRole.Insert(role);

                return new Response<Entities.Role>(role, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Role>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar rol
        /// </summary>
        /// <param name="id">Identificador del rol</param>
        /// <param name="role">Rol a actualizar</param>
        /// <returns>Rol actualizado</returns>
        public Response<Entities.Role> Put(int id, Entities.Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new Response<Entities.Role>(null, ResponseStatus.INVALID_REQUEST);

                role = dataRole.Update(role);

                return new Response<Entities.Role>(role, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Role>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Eliminar rol por id
        /// </summary>
        /// <param name="id">Identificador de rol</param>
        /// <returns>Rol eliminado</returns>
        public Response<Entities.Role> Delete(int id)
        {
            try
            {
                var result = dataRole.Delete(id);

                return new Response<Entities.Role>(result, ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<Entities.Role>(null, ResponseStatus.ERROR, ex.Message);
            }
        }

        /// <summary>
        /// Eliminar roles
        /// </summary>
        /// <param name="ids">Identificadores de roles</param>
        /// <returns>Roles eliminados</returns>
        public Response<IEnumerable<Entities.Role>> Deletes(int[] ids)
        {
            try
            {
                var result = dataRole.Delete(ids);

                return new Response<IEnumerable<Entities.Role>>(result, result.Count(), ResponseStatus.SUCCESS);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Role>>(null, ResponseStatus.ERROR, ex.Message);
            }
        }
    }
}
