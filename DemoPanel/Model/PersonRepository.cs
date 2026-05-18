using System.Collections.ObjectModel;
using DemoPanel.DataSource;

namespace DemoPanel.Model;

internal class PersonRepository
{
    private _DemoPanelContext _dpContext { get; }
    public ObservableCollection<PersonEntity> Persons { get; }

    public PersonRepository(_DemoPanelContext context)
    {
        _dpContext = context;
        Persons = new();
    }

    public async Task LoadDataAsync()
    {
        var persons = await new CsvDataSource(_dpContext).LoadPersonsAsync();
        Persons.Clear();
        foreach (var item in persons)
        {
            Persons.Add(item);
        }
    }
}