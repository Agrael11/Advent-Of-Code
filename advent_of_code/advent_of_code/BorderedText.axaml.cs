using Avalonia.Controls;

namespace advent_of_code
{
    public partial class BorderedText : UserControl
    {
        public string? Text
        {
            get => textBlock.Text;
            set => textBlock.Text = value;
        }

        public bool Centered
        {
            get => textBlock.HorizontalAlignment == Avalonia.Layout.HorizontalAlignment.Center;
            set => textBlock.HorizontalAlignment = value ? Avalonia.Layout.HorizontalAlignment.Center 
                : Avalonia.Layout.HorizontalAlignment.Left;
        }

        public BorderedText()
        {
            InitializeComponent();
        }
    }
}