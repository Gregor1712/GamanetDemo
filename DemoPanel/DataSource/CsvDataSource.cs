using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using DemoPanel.Model;

namespace DemoPanel.DataSource;

internal class CsvPersonRecord
{
    public string name { get; set; } = string.Empty;
    public string country { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string phone { get; set; } = string.Empty;
}

internal class CsvDataSource
{
    private _DemoPanelContext _dpContext { get; }

    public CsvDataSource(_DemoPanelContext context)
    {
        _dpContext = context;
    }

    public async Task<List<PersonEntity>> LoadPersonsAsync()
    {
        return await Task.Run(() => LoadPersons()).ConfigureAwait(false);
    }

    private List<PersonEntity> LoadPersons()
    {
        var persons = new List<PersonEntity>();
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv", "PersonsDemo.csv");

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        var records = csv.GetRecords<CsvPersonRecord>();
        foreach (var record in records)
        {
            var person = new PersonEntity();
            person.CopyFrom(record);
            persons.Add(person);
        }

        return persons;
    }
}