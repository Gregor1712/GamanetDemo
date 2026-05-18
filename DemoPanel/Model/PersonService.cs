namespace DemoPanel.Model;

internal class PersonService
{
    private _DemoPanelContext _dpContext { get; }

    public PersonService(_DemoPanelContext context)
    {
        _dpContext = context;
    }

    public List<PersonEntity> GetFilteredByCountry(string? country)
    {
        var persons = _dpContext.PersonRepo.Persons;
        if (string.IsNullOrEmpty(country))
            return persons.ToList();
        return persons.Where(p => p.Country.Equals(country)).ToList();
    }

    public List<PersonEntity> Sort(List<PersonEntity> persons, bool byName, bool byCountry)
    {
        if (byCountry && byName)
            return persons.OrderBy(p => p.Country).ThenBy(p => p.Name).ToList();
        if (byCountry)
            return persons.OrderBy(p => p.Country).ToList();
        if (byName)
            return persons.OrderBy(p => p.Name).ToList();
        return persons;
    }

    public List<string> GetDistinctCountries()
    {
        return _dpContext.PersonRepo.Persons
            .Select(p => p.Country)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
}