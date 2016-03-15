using System.Web.Http;
using System.Web.Routing;

namespace HidalgoCastro.WebInterface
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PostBlobUpload",
                routeTemplate: "blobs/upload",
                defaults: new { controller = "Blobs", action = "PostBlobUpload" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            config.Routes.MapHttpRoute(
                name: "GetBlobDownload",
                routeTemplate: "blobs/{blobId}/download",
                defaults: new { controller = "Blobs", action = "GetBlobDownload" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

        }
    }
}
