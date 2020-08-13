using DotNetNuke.Web.Api;

namespace JTMaher2.Modules.JTM2_PasswordManager
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("JTMaher2.Modules.JTM2_PasswordManager.Controllers", "default", "{controller}/{action}", new[] { "JTMaher2.Modules.JTM2_PasswordManager.Controllers" });
        }

    }
}