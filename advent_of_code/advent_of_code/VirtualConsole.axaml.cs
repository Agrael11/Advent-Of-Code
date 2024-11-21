using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Text;
using Visualizers;

namespace advent_of_code
{
    public partial class VirtualConsole : UserControl
    {
        private readonly List<List<ConsoleChar>> lines = new List<List<ConsoleChar>>();
        public int CursorLeft = 0;
        public int CursorTop = 0;
        public AOConsoleColor BackgroundColor = AOConsoleColor.Black;
        public AOConsoleColor ForegroundColor = AOConsoleColor.Gray;
        private AOConsoleColor _backgroundDefault = AOConsoleColor.Black;
        public AOConsoleColor BackgroundDefault { 
            get => _backgroundDefault; 
            set => _backgroundDefault = value;
        }
        private AOConsoleColor _foregroundDefault = AOConsoleColor.Gray;
        public AOConsoleColor ForegroundDefault
        {
            get => _foregroundDefault;
            set => _foregroundDefault = value;
        }
        private readonly double CharacterHeight = 0;

        public void Clear()
        {
            lines.Clear();
            BackgroundDefault = BackgroundColor;
            ForegroundDefault = ForegroundColor;
            CursorLeft = 0;
            CursorTop = 0;
            InvalidateVisual();
            InvalidateMeasure();
        }

        public void WriteLine(string text, bool debug = false)
        {
            Write(text + "\n", debug);
        }

        public void Write(string text, bool debug = false)
        {
            var splitText = text.Replace("\r","").Split('\n');
            var first = true;
            for (var i = 0; i < splitText.Length; i++)
            {
                if (!first)
                {
                    CursorTop++;
                    CursorLeft = 0;
                }
                first = false;
                var line = splitText[i];

                while (lines.Count <= CursorTop)
                {
                    lines.Add(new List<ConsoleChar>());
                }

                while(lines[CursorTop].Count < CursorLeft + line.Length)
                {
                    lines[CursorTop].Add(new ConsoleChar(' ', BackgroundDefault, ForegroundDefault, debug));
                }

                for (var j = 0; j < line.Length; j++)
                {
                    lines[CursorTop][CursorLeft + j] = new ConsoleChar(line[j], BackgroundColor, ForegroundColor, debug);
                }
                CursorLeft += line.Length;
            }

            InvalidateVisual();
            InvalidateMeasure();
        }

        public string GetText()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                foreach (var c in line)
                {
                    builder.Append(c.Character);
                }
                if (i < lines.Count - 1)
                {
                    builder.AppendLine();
                }
            }
            return builder.ToString();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var guess = MeasureGuess(GetText());
            return new Size(guess.Width+10, guess.Height+10);
        }

        protected Size MeasureGuess(string text)
        {
            
            var typeface = new Typeface(FontFamily);
            var formattedText = new FormattedText(text, System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 16.0, new SolidColorBrush(GetColorReal(ForegroundColor)));
            
            return new Size(formattedText.Width, formattedText.Height);
        }

        public override void Render(DrawingContext context)
        {
            context.FillRectangle(new SolidColorBrush(GetColorReal(BackgroundDefault)), new Avalonia.Rect(0, 0, Bounds.Width, Bounds.Height));

            var typeface = new Typeface(FontFamily);
            var typefaceOblique = new Typeface(FontFamily, FontStyle.Oblique);
            var x = 5.0;
            var y = 5.0;
            foreach (var line in lines)
            {
                var bgColor = BackgroundColor;
                var fgColor = ForegroundColor;
                var oblique = false;
                var text = "";
                foreach (var c in line)
                {
                    if (bgColor == c.Background && fgColor == c.Foreground && oblique == c.Oblique)
                    {
                        text += c.Character;
                    }
                    else
                    {
                        if (text != "")
                        {
                            var formattedText = new FormattedText("".PadLeft(text.Length, '#'), System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, (oblique ? typefaceOblique : typeface), 16.0, new SolidColorBrush(GetColorReal(fgColor)));
                            var width = formattedText.Width;
                            context.FillRectangle(new SolidColorBrush(GetColorReal(bgColor)), new Avalonia.Rect(x, y, width, CharacterHeight));
                            formattedText = new FormattedText(text, System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, (oblique ? typefaceOblique : typeface), 16.0, new SolidColorBrush(GetColorReal(fgColor)));
                            context.DrawText(formattedText, new Avalonia.Point(x, y));
                            x += width;
                            text = "";
                        }
                        fgColor = c.Foreground;
                        bgColor = c.Background;
                        oblique = c.Oblique;
                        text += c.Character;
                    }
                }
                if (text != "")
                {
                    var formattedText = new FormattedText("".PadLeft(text.Length, '#'), System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, (oblique ? typefaceOblique : typeface), 16.0, new SolidColorBrush(GetColorReal(fgColor)));
                    var width = formattedText.Width;
                    context.FillRectangle(new SolidColorBrush(GetColorReal(bgColor)), new Avalonia.Rect(x, y, width, CharacterHeight));
                    formattedText = new FormattedText(text, System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, (oblique ? typefaceOblique : typeface), 16.0, new SolidColorBrush(GetColorReal(fgColor)));
                    context.DrawText(formattedText, new Avalonia.Point(x, y));
                }
                x = 5.0;
                y += CharacterHeight;
            }

            base.Render(context);
        }

        private static Color GetColorReal(AOConsoleColor consoleColor)
        {
            return Color.FromArgb(consoleColor.A, consoleColor.R, consoleColor.G, consoleColor.B);
        }

        public VirtualConsole()
        {
            InitializeComponent();
            CharacterHeight = MeasureGuess("0").Height;
        }
    }
}