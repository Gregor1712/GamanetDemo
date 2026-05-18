using DemoPanel.Model;

namespace GamanetDemo.Model;

internal class _AppContext
{
    public _DemoPanelContext _dpContext { get; }

    public _AppContext()
    {
        _dpContext = new _DemoPanelContext();
    }
}