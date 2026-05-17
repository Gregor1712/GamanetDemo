using System.Windows;
using System.Windows.Controls;
using GamanetDemo.Model;

namespace GamanetDemo;

public partial class MainWindow : Window
{
    private _AppContext _appContext { get; }
    MainWindowViewModel _model { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        _appContext = new _AppContext();
        _model = new MainWindowViewModel(_appContext);
        RootContainer.DataContext = _model;
        this.Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await _model.LoadPersonsAsync();
    }

    private void CountryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
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
        SortLabel.Text = parts.Count > 0 ? string.Join(", ", parts) : "None";

        _model.ApplySort(byName, byCountry);
    }
}