using System.Collections.Generic;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace Windsong.VersionManager
{
    public class Routes : IHttpRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                 new HttpRouteDescriptor {
                    Name = "SetReadOnlyState",
                    Priority = 0,
                    RouteTemplate = "Windsong.VersionManager/SetReadOnlyState/{id}/{isReadOnly}",       
                    Defaults = new {
                        area = "Windsong.VersionManager",
                        controller = "Admin",
                        action = "SetReadOnlyState"
                    }
                }
            };
        }
    }
}