using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.DataAccess
{
    public class ImageData
    {

        /// <summary>
        /// Insertar imagen
        /// </summary>
        /// <param name="image">Imagen a insertar</param>
        /// <returns>Imagen insertado</returns>
        public Entities.Image Insert(Entities.Image image)
        {
            try
            {
                using (var ctx = new Context.SampleAngularEntities())
                {
                    var img = new Context.Image
                    {
                        Path = image.Path,
                        Description = image.Description,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    ctx.Image.Add(img);
                    ctx.SaveChanges();

                    return MapToEntity(img);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapear imagen de contexto a imagen de entidades
        /// </summary>
        /// <param name="image">Imagen de contexto</param>
        /// <returns>Imagen de entidades</returns>
        private Entities.Image MapToEntity(Context.Image image)
        {
            return new Entities.Image
            {
                Id = image.Id,
                Path = image.Path,
                Description = image.Description,
                CreatedAt = image.CreatedAt,
                UpdatedAt = image.UpdatedAt,
                DeletedAt = image.DeletedAt
            };
        }

    }
}
