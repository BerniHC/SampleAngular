using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace HidalgoCastro.DataAccess
{
    public class RoleData
    {
        /// <summary>
        /// Seleccionar todos los registros
        /// </summary>
        /// <returns>Lista de registros</returns>
        public IEnumerable<Entities.Role> SelectAll()
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var roles = ctx.Role
                        .Where(x => x.DeletedAt == null)
                        .Select(x => MapToEntity(x));

                    return roles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Paginar registros
        /// </summary>
        /// <param name="paginate">Paginacion a aplicar</param>
        /// <returns>Lista de registros</returns>
        public IEnumerable<Entities.Role> Page(Entities.Pagination paginate)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var roles = ctx.Role
                        .Where(x => x.DeletedAt == null)
                        .FullTextSearch(paginate.Search)
                        .OrderBy(paginate.Sort + " " + paginate.Order)
                        .Skip((paginate.Page - 1) * paginate.Count)
                        .Take(paginate.Count).ToList();

                    return roles.Select(x => MapToEntity(x)).ToList();
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
                    var count = ctx.Role
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
        public Entities.Role SelectById(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var role = ctx.Role
                        .Where(x => x.Id == id && x.DeletedAt == null)
                        .ToList();

                    return role.Select(x => MapToEntity(x)).SingleOrDefault();
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
        public Entities.Role Insert(Entities.Role obj)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var role = new Context.Role
                    {
                        Name = obj.Name,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    ctx.Role.Add(role);
                    ctx.SaveChanges();

                    return MapToEntity(role);
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
        public Entities.Role Update(Entities.Role obj)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var role = ctx.Role
                        .Where(x => x.Id == obj.Id && x.DeletedAt == null)
                        .SingleOrDefault();

                    if (role == null) return null;

                    role.Name = obj.Name;
                    role.UpdatedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(role);
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
        public Entities.Role Delete(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var role = ctx.Role
                        .Where(x => x.Id == id && x.DeletedAt == null)
                        .SingleOrDefault();

                    if (role == null) return null;

                    role.DeletedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(role);
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
        public IEnumerable<Entities.Role> Delete(IEnumerable<int> ids)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var roles = ctx.Role
                        .Where(x => ids.Contains(x.Id) && x.DeletedAt == null)
                        .ToList();

                    var current = DateTime.Now;
                    roles.ForEach(x => x.DeletedAt = current);

                    ctx.SaveChanges();

                    return roles.Select(x => MapToEntity(x));
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
        public static Entities.Role MapToEntity(Context.Role obj)
        {
            return new Entities.Role
            {
                Id = obj.Id,
                Name = obj.Name,
                Permissions = obj.RolePermission.Select(x => new Entities.Permission {
                    Id = x.Permission.Id,
                    CodeName = x.Permission.CodeName,
                    CreatedAt = x.Permission.CreatedAt,
                    UpdatedAt = x.Permission.UpdatedAt,
                    DeletedAt = x.Permission.DeletedAt
                }).ToList(),
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                DeletedAt = obj.DeletedAt
            };
        }

    }
}
