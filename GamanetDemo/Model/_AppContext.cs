namespace GamanetDemo.Model;

internal class _AppContext
{
    public PersonRepository PersonRepo { get; }

    public _AppContext()
    {
        PersonRepo = new PersonRepository(this);
    }
}