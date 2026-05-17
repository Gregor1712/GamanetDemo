using System.Windows;
using System.Windows.Controls;
using GamanetDemo.Model;

namespace GamanetDemo.UI.Panel;

public partial class MainPanel : UserControl
{
    private _AppContext? _appContext { get; set; }
    MainPanelViewModel? _model { get; set; }

    public MainPanel()
    {
        InitializeComponent();
        DataContextChanged += MainPanel_DataContextChanged;
    }

    private async void MainPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is _AppContext context)
        {
            _appContext = context;
            _model = new MainPanelViewModel(_appContext);
            RootContainer.DataContext = _model;
            await _model.LoadPersonsAsync();
        }
    }

    private void CountryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_model == null) return;
        if (CountryFilter.SelectedItem is string country)
        {
            _model.FilterByCountry(country);
        }
    }

    private void SortCheck_Changed(object sender, RoutedEventArgs e)
    {
        if (_model == null) return;

        bool byName = SortByNameCheck.IsChecked == true;
        bool byCountry = SortByCountryCheck.IsChecked == true;

        var parts = new List<string>();
        if (byName) parts.Add("Name");
        if (byCountry) parts.Add("Country");
        SortLabel.Text = parts.Count > 0 ? "Sort: " + string.Join(", ", parts) : "Sorting Options";

        _model.ApplySort(byName, byCountry);
    }
}