using System.Collections.ObjectModel;
using GamanetDemo.Model;

namespace GamanetDemo;

internal class MainWindowViewModel
{
    private _AppContext _appContext;
    private string _currentCountryFilter = "All";
    private bool _sortByName = true;
    private bool _sortByCountry;

    public ObservableCollection<PersonEntity> FilteredPersons { get; }
    public ObservableCollection<string> Countries { get; }

    public MainWindowViewModel(_AppContext context)
    {
        _appContext = context;
        FilteredPersons = new ObservableCollection<PersonEntity>();
        Countries = new ObservableCollection<string>();
    }

    public async Task LoadPersonsAsync()
    {
        await _appContext.PersonRepo.LoadDataAsync();
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
        var service = new PersonService(_appContext);
        var countries = service.GetDistinctCountries();

        Countries.Clear();
        Countries.Add("All");
        foreach (var country in countries)
            Countries.Add(country);
    }

    private void RefreshFilteredPersons()
    {
        var service = new PersonService(_appContext);
        var filtered = service.GetFilteredByCountry(_currentCountryFilter);
        var sorted = service.Sort(filtered, _sortByName, _sortByCountry);

        FilteredPersons.Clear();
        foreach (var person in sorted)
            FilteredPersons.Add(person);
    }
}