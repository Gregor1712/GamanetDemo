using System.Collections.ObjectModel;
using GamanetDemo.DataSource;

namespace GamanetDemo.Model;

internal class PersonRepository
{
    private _AppContext _appContext { get; }

    public ObservableCollection<PersonEntity> Persons { get; }

    public PersonRepository(_AppContext context)
    {
        _appContext = context;
        Persons = new ObservableCollection<PersonEntity>();
    }

    public async Task LoadDataAsync()
    {
        var persons = await new CsvDataSource(_appContext).LoadPersonsAsync();
        Persons.Clear();
        foreach (var item in persons)
        {
            Persons.Add(item);  
        }
    }
}