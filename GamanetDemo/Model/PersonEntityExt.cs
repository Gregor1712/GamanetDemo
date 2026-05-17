using GamanetDemo.DataSource;

namespace GamanetDemo.Model;

internal static class PersonEntityExt
{
    public static bool CopyFrom(this PersonEntity to, PersonEntity from)
    {
        to.Name = from.Name;
        to.Country = from.Country;
        to.Email = from.Email;
        to.Phone = from.Phone;
        return true;
    }

    public static bool CopyFrom(this PersonEntity to, CsvPersonRecord from)
    {
        to.Name = from.name;
        to.Country = from.country;
        to.Email = from.email;
        to.Phone = from.phone;
        return true;
    }
}