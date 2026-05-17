namespace GamanetDemo.Model;

internal static class PersonEntityExt
{
    public static bool CopyFrom(this PersonEntity to, PersonEntity from)
    {
        to.Name = from.Name;
        to.Country = from.Country;
        to.Phone = from.Phone;
        return true;
    }

    public static bool CopyFrom(this PersonEntity to, string[] csvFields)
    {
        to.Name = csvFields[0];
        to.Country = csvFields[1];
        to.Phone = csvFields[5];
        return true;
    }
}