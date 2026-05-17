using GamanetDemo.DataSource;

namespace GamanetDemo.Model;

internal static class PersonEntityExt
{
    public static bool CopyFrom(this PersonEntity to, CsvPersonRecord from)
    {
        to.Name = from.name;
        to.Country = from.country;
        to.Email = from.email;
        to.Phone = from.phone;
        return true;
    }
}