using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoPanel.UI.Control;

public partial class AutoCompleteBox : UserControl
{
    private bool _isSelecting;

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(AutoCompleteBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(AutoCompleteBox),
            new PropertyMetadata(null));

    public static readonly RoutedEvent SelectionChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(SelectionChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(AutoCompleteBox));

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public event RoutedEventHandler SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    public AutoCompleteBox()
    {
        InitializeComponent();
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isSelecting) return;

        var searchText = SearchTextBox.Text;
        var filtered = new List<string>();

        if (ItemsSource != null)
        {
            foreach (var item in ItemsSource)
            {
                var text = item?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(searchText) ||
                    text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    filtered.Add(text);
                }
            }
        }

        SuggestionsList.ItemsSource = filtered;
        SuggestionsPopup.IsOpen = filtered.Count > 0 && SearchTextBox.IsFocused;

        if (string.IsNullOrEmpty(searchText))
        {
            SelectedItem = string.Empty;
            RaiseEvent(new RoutedEventArgs(SelectionChangedEvent, this));
        }
    }

    private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        SearchTextBox_TextChanged(sender, null!);
    }

    private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        // Delay closing so ListBox selection can fire first
        Dispatcher.BeginInvoke(() =>
        {
            if (!SearchTextBox.IsFocused && !SuggestionsList.IsFocused)
                SuggestionsPopup.IsOpen = false;
        }, System.Windows.Threading.DispatcherPriority.Background);
    }

    private void SearchTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!SuggestionsPopup.IsOpen)
        {
            SearchTextBox_TextChanged(sender, null!);
        }
    }

    private void SearchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            SuggestionsPopup.IsOpen = false;
            e.Handled = true;
        }
    }

    private void SuggestionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SuggestionsList.SelectedItem == null) return;

        _isSelecting = true;
        var selected = SuggestionsList.SelectedItem.ToString()!;
        SearchTextBox.Text = selected;
        SelectedItem = selected;
        SuggestionsPopup.IsOpen = false;
        SuggestionsList.SelectedItem = null;
        _isSelecting = false;

        RaiseEvent(new RoutedEventArgs(SelectionChangedEvent, this));
    }
}