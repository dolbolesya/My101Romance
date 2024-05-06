namespace My101Romance.CustomAuthRoute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public class IsNotAuth : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        // Возвращает true, если пользователь не аутентифицирован
        return !httpContext.User.Identity.IsAuthenticated;
    }
}

public class IsAuth : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        // Возвращает true, если пользователь аутентифицирован
        return httpContext.User.Identity.IsAuthenticated;
    }
}
