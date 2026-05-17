using System.IO;
using System.Text;
using GamanetDemo.Model;

namespace GamanetDemo.DataSource;

internal class CsvDataSource
{
    private _AppContext _appContext { get; }

    public CsvDataSource(_AppContext context)
    {
        _appContext = context;
    }

    public async Task<List<PersonEntity>> LoadPersonsAsync()
    {
        return await Task.Run(() => LoadPersons()).ConfigureAwait(false);
    }

    public List<PersonEntity> LoadPersons()
    {
        var persons = new List<PersonEntity>();
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv", "PersonsDemo.csv");
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            var fields = ParseCsvLine(line);
            if (fields.Length < 6) continue;

            var person = new PersonEntity();
            person.CopyFrom(fields);
            persons.Add(person);
        }

        return persons;
    }

    private static string[] ParseCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        var field = new StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(field.ToString());
                field.Clear();
            }
            else
            {
                field.Append(c);
            }
        }
        fields.Add(field.ToString());
        return fields.ToArray();
    }
}