using System.Windows;
using System.Windows.Controls;
using DemoPanel.Model;

namespace DemoPanel.UI.Panel;

public partial class MainPanel : UserControl
{
    private _DemoPanelContext _dpContext { get; }
    MainPanelViewModel? _model { get; set; }

    public MainPanel()
    {
        InitializeComponent();
        _dpContext = new _DemoPanelContext();
        this.DataContextChanged += MainPanel_DataContextChanged;
    }

    private async void MainPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        _model = new MainPanelViewModel(_dpContext);
        RootContainer.DataContext = _model;
        await _model.LoadPersonsAsync();
    }

    private void CountryFilter_SelectionChanged(object sender, RoutedEventArgs e)
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

        var byName = SortByNameCheck.IsChecked == true;
        var byCountry = SortByCountryCheck.IsChecked == true;

        var parts = new List<string>();
        if (byName) parts.Add("Name");
        if (byCountry) parts.Add("Country");
        SortLabel.Text = parts.Count > 0 ? "Sort: " + string.Join(", ", parts) : "Sorting Options";

        _model.ApplySort(byName, byCountry);
    }
}