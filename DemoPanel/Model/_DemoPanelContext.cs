namespace DemoPanel.Model;

public class _DemoPanelContext
{
    internal PersonRepository PersonRepo { get; }

    public _DemoPanelContext()
    {
        PersonRepo = new PersonRepository(this);
    }
}