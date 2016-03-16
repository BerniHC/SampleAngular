using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace HidalgoCastro.DataAccess
{
    public class PermissionData
    {
        /// <summary>
        /// Seleccionar todos los registros
        /// </summary>
        /// <returns>Lista de registros</returns>
        public IEnumerable<Entities.Permission> SelectAll()
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permissions = ctx.Permission
                        .Where(x => x.DeletedAt == null)
                        .Select(x => MapToEntity(x));

                    return permissions;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Obtener cantidad total de registros
        /// </summary>
        /// <returns>Total de registros</returns>
        public int Count()
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var count = ctx.Permission
                        .Where(x => x.DeletedAt == null)
                        .Count();

                    return count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Seleccionar registro por identificador
        /// </summary>
        /// <param name="id">Identificador del registro</param>
        /// <returns>Registro solicitado</returns>
        public Entities.Permission SelectById(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permission = ctx.Permission
                        .Where(x => x.Id == id && x.DeletedAt == null)
                        .Select(x => MapToEntity(x))
                        .SingleOrDefault();

                    return permission;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insertar nuevo registro
        /// </summary>
        /// <param name="obj">Registro a insertar</param>
        /// <returns>Registro insertado</returns>
        public Entities.Permission Insert(Entities.Permission obj)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permission = new Context.Permission
                    {
                        CodeName = obj.CodeName,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    ctx.Permission.Add(permission);
                    ctx.SaveChanges();

                    return MapToEntity(permission);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar registro
        /// </summary>
        /// <param name="obj">Registro a actualizar</param>
        /// <returns>Registro actualizado</returns>
        public Entities.Permission Update(Entities.Permission obj)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permission = ctx.Permission
                        .Where(x => x.Id == obj.Id && x.DeletedAt == null)
                        .SingleOrDefault();

                    if (permission == null) return null;

                    permission.CodeName = obj.CodeName;
                    permission.UpdatedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(permission);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Eliminar registro
        /// </summary>
        /// <param name="id">Identificador del registro</param>
        /// <returns>Registro eliminado</returns>
        public Entities.Permission Delete(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permission = ctx.Permission
                        .Where(x => x.Id == id && x.DeletedAt == null)
                        .SingleOrDefault();

                    if (permission == null) return null;

                    permission.DeletedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(permission);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Eliminar registros
        /// </summary>
        /// <param name="ids">Identificadores de los registros</param>
        /// <returns>Registros eliminados</returns>
        public IEnumerable<Entities.Permission> Delete(IEnumerable<int> ids)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var permissions = ctx.Permission
                        .Where(x => ids.Contains(x.Id) && x.DeletedAt == null)
                        .ToList();

                    var current = DateTime.Now;
                    permissions.ForEach(x => x.DeletedAt = current);

                    ctx.SaveChanges();

                    return permissions.Select(x => MapToEntity(x));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapear contexto a entidad
        /// </summary>
        /// <param name="obj">Contexto</param>
        /// <returns>Entidad</returns>
        public static Entities.Permission MapToEntity(Context.Permission obj)
        {
            return new Entities.Permission
            {
                Id = obj.Id,
                CodeName = obj.CodeName,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                DeletedAt = obj.DeletedAt
            };
        }

    }
}
