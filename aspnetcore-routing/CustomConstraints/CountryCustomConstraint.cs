using System.Text.RegularExpressions;

namespace aspnetcore_routing.CustomConstraints;

public class CountryCustomConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, 
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        //checking if value exists
        if (!values.ContainsKey(routeKey)) return false;

        Regex regex = new Regex("^(brazil|usa|canada)$");
        string? countryValue = Convert.ToString(values[routeKey]);

        if (regex.IsMatch(countryValue)) return true;

        return false;
    }
}
