using System.Windows;
using GamanetDemo.Model;

namespace GamanetDemo;

public partial class MainWindow : Window
{
    private _AppContext _appContext { get; }

    public MainWindow()
    {
        InitializeComponent();
        _appContext = new _AppContext();
        RootContainer.DataContext = _appContext;
    }
}