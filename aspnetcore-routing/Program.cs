using aspnetcore_routing.CustomConstraints;
using aspnetcore_routing.Entities;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//add our custom routing constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("countries", typeof(CountryCustomConstraint));
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

List<Country> countryList = new List<Country>();
PopulateCountryClass();

//enable routing
app.UseRouting();

//creating endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("country/", async context =>
    {
        foreach (Country country in countryList)
        {
            await CountryInfo(context, country);
        }
    });

    endpoints.MapGet("country/{countryname:countries}", async context =>
    {
        string? countryPar = Convert.ToString(context.Request.RouteValues["countryname"]);
        string countryName = char.ToUpper(countryPar[0]) + countryPar.Substring(1);
        Country country = countryList.FirstOrDefault(country => country.Name == countryName);
        if (country != null) await CountryInfo(context, country);
        else await context.Response.WriteAsync($"Error finding country: {countryName}");
    });
});

app.Run();

//Custom functions

//Populating Country Class
void PopulateCountryClass()
{
    countryList.Add(new Country("Brazil", "214.300.000"));
    countryList.Add(new Country("Usa", "331.900.000"));
    countryList.Add(new Country("Canada", "38.250.000"));
}

//returning country info
async Task CountryInfo(HttpContext httpContext, Country country)
{
    await httpContext.Response.WriteAsync($"Country: {country.Name}, Population: {country.Population}\n");
}
