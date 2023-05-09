namespace aspnetcore_routing.Entities;

public class Country
{
    public string Name { get; set; }
    public string Population { get; set; }

    public Country(string name, string population)
    {
        Name = name;
        Population = population;
    }
}
