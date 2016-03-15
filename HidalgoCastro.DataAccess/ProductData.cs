using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HidalgoCastro.DataAccess
{
    public class ProductData
    {
        /// <summary>
        /// Seleccionar todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        public IEnumerable<Entities.Product> SelectAll()
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var products = (
                        from p in ctx.Product
                        where p.DeletedAt == null
                        select p).ToList().Select(x => MapToEntity(x));

                    return products;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Buscar productos
        /// </summary>
        /// <param name="search">Términos de búsqueda</param>
        /// <returns>Lista de productos</returns>
        public IEnumerable<Entities.Product> Search(string search)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var products = ctx.Product
                        .Include(p => p.ProductImage)
                        .Where(p => p.DeletedAt == null).ToList();
                    
                    var result = products.Select(x => MapToEntity(x)).ToList();

                    return result;
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
                    var count = (
                        from p in ctx.Product
                        where p.DeletedAt == null
                        select p).Count();

                    return count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Seleccionar producto por identificador
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>Producto solicitado</returns>
        public Entities.Product SelectById(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var product = (
                        from p in ctx.Product
                        where p.Id == id
                        && p.DeletedAt == null
                        select p).SingleOrDefault();

                    return MapToEntity(product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insertar producto
        /// </summary>
        /// <param name="product">Producto a insertar</param>
        /// <returns>Producto insertado</returns>
        public Entities.Product Insert(Entities.Product product)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var prdct = new Context.Product
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Keywords = product.Keywords,
                        Price = product.Price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    ctx.Product.Add(prdct);
                    ctx.SaveChanges();

                    return MapToEntity(prdct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insertar producto con imágenes
        /// </summary>
        /// <param name="product">Producto a insertar</param>
        /// <returns>Producto insertado</returns>
        public Entities.Product InsertWithImages(Entities.Product product)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var prdct = new Context.Product
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Keywords = product.Keywords,
                        Price = product.Price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        ProductImage = product.Images.Select(x => new Context.ProductImage
                        {
                            Image = new Context.Image
                            {
                                Path = x.Path,
                                Description = x.Description,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            },
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        }).ToList()
                    };

                    ctx.Product.Add(prdct);
                    ctx.SaveChanges();

                    return MapToEntity(prdct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Actualizar producto
        /// </summary>
        /// <param name="product">Producto a actualizar</param>
        /// <returns>Producto actualizado</returns>
        public Entities.Product Update(Entities.Product product)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var prdct = (
                        from p in ctx.Product
                        where p.Id == product.Id
                        && p.DeletedAt == null
                        select p).SingleOrDefault();

                    if (prdct == null) return null;

                    prdct.Title = product.Title;
                    prdct.Description = product.Description;
                    prdct.Keywords = product.Keywords;
                    prdct.Price = product.Price;
                    prdct.UpdatedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(prdct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>Producto eliminado</returns>
        public Entities.Product Delete(int id)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var prdct = (
                        from p in ctx.Product
                        where p.Id == id
                        && p.DeletedAt == null
                        select p).SingleOrDefault();

                    if (prdct == null) return null;

                    prdct.DeletedAt = DateTime.Now;

                    ctx.SaveChanges();

                    return MapToEntity(prdct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Eliminar productos
        /// </summary>
        /// <param name="id">Identificadores</param>
        /// <returns>Productos eliminados</returns>
        public IEnumerable<Entities.Product> Delete(IEnumerable<int> ids)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var prdcts = (
                        from p in ctx.Product
                        where ids.Contains(p.Id)
                        && p.DeletedAt == null
                        select p).ToList();

                    var current = DateTime.Now;
                    foreach (var r in prdcts)
                    {
                        r.DeletedAt = current;
                    }

                    ctx.SaveChanges();

                    return prdcts.Select(x => MapToEntity(x));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapear producto de contexto a producto de entidades
        /// </summary>
        /// <param name="product">Producto de contexto</param>
        /// <returns>Producto de entidades</returns>
        public static Entities.Product MapToEntity(Context.Product product)
        {
            return new Entities.Product
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Keywords = product.Keywords,
                Price = product.Price,
                Images = product.ProductImage.Select(x => new Entities.Image {
                    Id = x.Image.Id,
                    Path = x.Image.Path,
                    Description = x.Image.Description,
                    CreatedAt = x.Image.CreatedAt,
                    UpdatedAt = x.Image.UpdatedAt,
                    DeletedAt = x.Image.DeletedAt
                }).ToList(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt
            };
        }

    }
}
