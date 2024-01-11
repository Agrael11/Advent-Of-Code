using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace advent_of_code;

public partial class BorderedText : UserControl
{
    public string? Text
    {
        get
        {
            return textBlock.Text;
        }
        set
        {
            textBlock.Text = value;
        }
    }

    public bool Centered
    {
        get
        {
            return textBlock.HorizontalAlignment == Avalonia.Layout.HorizontalAlignment.Center;
        }
        set
        {
            if (value)
            {
                textBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            }
            else
            {

                textBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            }
        }
    }

    public BorderedText()
    {
        InitializeComponent();
    }
}