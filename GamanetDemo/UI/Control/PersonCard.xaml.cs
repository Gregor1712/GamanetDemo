using System.Windows;
using System.Windows.Controls;

namespace GamanetDemo.UI.Control;

public partial class PersonCard : UserControl
{
    public static readonly DependencyProperty HintTextProperty =
        DependencyProperty.Register(
            nameof(HintText),
            typeof(string),
            typeof(PersonCard),
            new PropertyMetadata(string.Empty));

    public string HintText
    {
        get => (string)GetValue(HintTextProperty);
        set => SetValue(HintTextProperty, value);
    }

    public PersonCard()
    {
        InitializeComponent();
    }
}