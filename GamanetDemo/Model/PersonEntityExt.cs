using GamanetDemo.DataSource;

namespace GamanetDemo.Model;

internal static class PersonEntityExt
{
    public static bool CopyFrom(this PersonEntity to, CsvPersonRecord from)
    {
        to.Name = from.name;
        to.Country = from.country;
        to.Phone = from.phone;
        return true;
    }
}