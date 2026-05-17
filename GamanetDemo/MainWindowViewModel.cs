using System.Collections.ObjectModel;
using GamanetDemo.Model;

namespace GamanetDemo;

internal class MainWindowViewModel
{
    private _AppContext _appContext;
    private string _currentCountryFilter = "All";
    private string _currentSort = "Name";

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

    public void SortByName()
    {
        _currentSort = "Name";
        RefreshFilteredPersons();
    }

    public void SortByCountry()
    {
        _currentSort = "Country";
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
        var sorted = _currentSort == "Country"
            ? service.SortByCountry(filtered)
            : service.SortByName(filtered);

        FilteredPersons.Clear();
        foreach (var person in sorted)
            FilteredPersons.Add(person);
    }
}