// MIT License
// Copyright JTMaher2

using DotNetNuke.Web.Api;

namespace JTMaher.Modules.JTMPasswordsStencilJS.Controllers
{
    /// <summary>
    /// Implements the Dnn IServiceRouteMapper to register this module routes.
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        /// <inheritdoc/>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager?.MapHttpRoute("JTMaher_JTM2_PasswordsStencilJS", "default", "{controller}/{action}", new[] { typeof(ServiceRouteMapper).Namespace });
        }
    }
}