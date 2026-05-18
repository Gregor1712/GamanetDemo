using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GamanetDemo.UI.Converters;

internal class CountryToColorConverter : IValueConverter
{
    private static readonly Color[] PastelPalette =
    [
        Color.FromRgb(0xF2, 0xD5, 0xB0), // warm sand
        Color.FromRgb(0xE8, 0xBE, 0x8C), // soft amber
        Color.FromRgb(0xD4, 0xA0, 0x6A), // copper light
        Color.FromRgb(0xF0, 0xC8, 0x9A), // golden cream
        Color.FromRgb(0xE0, 0xAA, 0x78), // warm peach
        Color.FromRgb(0xC9, 0x8B, 0x5A), // terracotta soft
        Color.FromRgb(0xF5, 0xDD, 0xBC), // pale honey
        Color.FromRgb(0xDB, 0xB4, 0x82), // caramel light
        Color.FromRgb(0xE5, 0xC4, 0x98), // wheat
        Color.FromRgb(0xCC, 0x96, 0x64), // burnished copper
        Color.FromRgb(0xF0, 0xD2, 0xA8), // champagne
        Color.FromRgb(0xD8, 0xAC, 0x76), // toffee light
        Color.FromRgb(0xEE, 0xC8, 0x94), // golden sand
        Color.FromRgb(0xC5, 0x8E, 0x58), // warm bronze
        Color.FromRgb(0xF4, 0xDA, 0xB4), // cream gold
        Color.FromRgb(0xDE, 0xB2, 0x7E), // amber glow
    ];

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string country || string.IsNullOrEmpty(country))
            return new SolidColorBrush(Color.FromRgb(0xF5, 0xF5, 0xF5));

        var hash = Math.Abs(country.GetHashCode());
        var color = PastelPalette[hash % PastelPalette.Length];
        return new SolidColorBrush(color);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}