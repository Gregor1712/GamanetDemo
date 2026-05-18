using System.Collections.ObjectModel;
using DemoPanel.Model;

namespace DemoPanel.UI.Panel;

internal class MainPanelViewModel
{
    private _DemoPanelContext _dpContext;

    private string _currentCountryFilter = string.Empty;
    private bool _sortByName = true;
    private bool _sortByCountry;

    public ObservableCollection<PersonEntity> FilteredPersons { get; }
    public ObservableCollection<string> Countries { get; }

    public MainPanelViewModel(_DemoPanelContext context)
    {
        _dpContext = context;
        FilteredPersons = new ObservableCollection<PersonEntity>();
        Countries = new ObservableCollection<string>();
    }

    public async Task LoadPersonsAsync()
    {
        await _dpContext.PersonRepo.LoadDataAsync();
        RefreshCountries();
        RefreshFilteredPersons();
    }

    public void FilterByCountry(string country)
    {
        _currentCountryFilter = country;
        RefreshFilteredPersons();
    }

    public void ApplySort(bool sortByName, bool sortByCountry)
    {
        _sortByName = sortByName;
        _sortByCountry = sortByCountry;
        RefreshFilteredPersons();
    }

    private void RefreshCountries()
    {
        var service = new PersonService(_dpContext);
        var countries = service.GetDistinctCountries();

        Countries.Clear();
        foreach (var country in countries)
            Countries.Add(country);
    }

    private void RefreshFilteredPersons()
    {
        var service = new PersonService(_dpContext);
        var filtered = service.GetFilteredByCountry(_currentCountryFilter);
        var sorted = service.Sort(filtered, _sortByName, _sortByCountry);

        FilteredPersons.Clear();
        foreach (var person in sorted)
            FilteredPersons.Add(person);
    }
}