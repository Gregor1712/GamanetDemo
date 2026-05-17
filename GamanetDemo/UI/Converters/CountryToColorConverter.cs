using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GamanetDemo.UI.Converters;

internal class CountryToColorConverter : IValueConverter
{
    private static readonly Color[] PastelPalette =
    [
        Color.FromRgb(0xFA, 0xD0, 0xC4), // pastel pink
        Color.FromRgb(0xC4, 0xE1, 0xFA), // pastel blue
        Color.FromRgb(0xC4, 0xFA, 0xD0), // pastel green
        Color.FromRgb(0xFA, 0xE1, 0xC4), // pastel peach
        Color.FromRgb(0xD0, 0xC4, 0xFA), // pastel lavender
        Color.FromRgb(0xFA, 0xFA, 0xC4), // pastel yellow
        Color.FromRgb(0xC4, 0xFA, 0xFA), // pastel cyan
        Color.FromRgb(0xFA, 0xC4, 0xE1), // pastel rose
        Color.FromRgb(0xD0, 0xFA, 0xC4), // pastel mint
        Color.FromRgb(0xE1, 0xC4, 0xFA), // pastel purple
        Color.FromRgb(0xFA, 0xD0, 0xFA), // pastel magenta
        Color.FromRgb(0xC4, 0xD0, 0xFA), // pastel periwinkle
        Color.FromRgb(0xFA, 0xE8, 0xC4), // pastel apricot
        Color.FromRgb(0xC4, 0xFA, 0xE8), // pastel aqua
        Color.FromRgb(0xE8, 0xFA, 0xC4), // pastel lime
        Color.FromRgb(0xFA, 0xC4, 0xC4), // pastel salmon
    ];

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string country || string.IsNullOrEmpty(country))
            return new SolidColorBrush(Color.FromRgb(0xF5, 0xF5, 0xF5));

        int hash = Math.Abs(country.GetHashCode());
        var color = PastelPalette[hash % PastelPalette.Length];
        return new SolidColorBrush(color);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}