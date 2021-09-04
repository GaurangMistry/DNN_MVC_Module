using DotNetNuke.Web.Mvc.Routing;
using DotNetNuke.Web.Mvc;
using System.Web.Mvc;
namespace WebXMS
{
    public class RouteConfig : IMvcRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapRoute("ApprovedSupplierList", "ASLApp", "{controller}/{action}", new[]
            {"WebXMS.Apps.ASLApp.Controllers"});
        }
    }
}
