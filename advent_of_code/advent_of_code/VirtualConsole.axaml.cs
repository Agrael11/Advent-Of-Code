using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace advent_of_code
{
    public partial class VirtualConsole : UserControl
    {
        private readonly List<List<ConsoleChar>> lines = new List<List<ConsoleChar>>();
        public int CursorLeft = 0;
        public int CursorTop = 0;
        public ConsoleColor BackgroundColor = ConsoleColor.Black;
        public ConsoleColor ForegroundColor = ConsoleColor.Gray;
        private ConsoleColor _backgroundDefault = ConsoleColor.Black;
        public ConsoleColor BackgroundDefault { 
            get => _backgroundDefault; 
            set => _backgroundDefault = value;
        }
        private ConsoleColor _foregroundDefault = ConsoleColor.Gray;
        public ConsoleColor ForegroundDefault
        {
            get => _foregroundDefault;
            set => _foregroundDefault = value;
        }
        private double CharacterWidth = 0;
        private double CharacterHeight = 0;

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

        public void WriteLine(string text)
        {
            Write(text + "\n");
        }

        public void Write(string text)
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
                    lines[CursorTop].Add(new ConsoleChar(' ', BackgroundDefault, ForegroundDefault));
                }

                for (var j = 0; j < line.Length; j++)
                {
                    lines[CursorTop][CursorLeft + j] = new ConsoleChar(line[j], BackgroundColor, ForegroundColor);
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
            return guess;
        }

        protected Size MeasureGuess(string text)
        {
            
            var typeface = new Typeface(FontFamily);
            var formattedText = new FormattedText(text, System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 16.0, new SolidColorBrush(GetColorReal(ForegroundColor)));
            
            return new Size(formattedText.Width+10, formattedText.Height+10);
        }

        public override void Render(DrawingContext context)
        {
            context.FillRectangle(new SolidColorBrush(GetColorReal(BackgroundDefault)), new Avalonia.Rect(0, 0, Bounds.Width, Bounds.Height));

            var typeface = new Typeface(FontFamily);
            var x = 5.0;
            var y = 5.0;
            foreach (var line in lines)
            {
                var formattedText = new FormattedText("ASD", System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 16.0, new SolidColorBrush(GetColorReal(ForegroundColor)));
                foreach (var c in line)
                {
                    formattedText = new FormattedText(c.Character.ToString(), System.Globalization.CultureInfo.InvariantCulture, FlowDirection.LeftToRight, typeface, 16.0, new SolidColorBrush(GetColorReal(c.Foreground)));
                    context.FillRectangle(new SolidColorBrush(GetColorReal(c.Background)), new Avalonia.Rect(x, y, CharacterWidth, CharacterHeight));
                    context.DrawText(formattedText, new Avalonia.Point(x, y));
                    x += CharacterWidth;
                }
                x = 5.0;
                y += CharacterHeight;
            }

            base.Render(context);
        }

        private static Color GetColorReal(ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                ConsoleColor.Black => Color.FromArgb(255, 0, 0, 0),
                ConsoleColor.DarkBlue => Color.FromArgb(255, 0, 0, 139),
                ConsoleColor.DarkGreen => Color.FromArgb(255, 0, 100, 0),
                ConsoleColor.DarkCyan => Color.FromArgb(255, 0, 139, 139),
                ConsoleColor.DarkRed => Color.FromArgb(255, 139, 0, 0),
                ConsoleColor.DarkMagenta => Color.FromArgb(255, 139, 0, 139),
                ConsoleColor.DarkYellow => Color.FromArgb(255, 184, 134, 11),
                ConsoleColor.Gray => Color.FromArgb(255, 192, 192, 192),
                ConsoleColor.DarkGray => Color.FromArgb(255, 128, 128, 128),
                ConsoleColor.Blue => Color.FromArgb(255, 0, 0, 255),
                ConsoleColor.Green => Color.FromArgb(255, 0, 255, 0),
                ConsoleColor.Cyan => Color.FromArgb(255, 0, 255, 255),
                ConsoleColor.Red => Color.FromArgb(255, 255, 0, 0),
                ConsoleColor.Magenta => Color.FromArgb(255, 255, 0, 255),
                ConsoleColor.Yellow => Color.FromArgb(255, 255, 255, 0),
                ConsoleColor.White => Color.FromArgb(255, 255, 255, 255),
                _ => Color.FromArgb(255, 0, 0, 0) // Default to black
            };
        }

        public VirtualConsole()
        {
            InitializeComponent();
            CharacterWidth = MeasureGuess("0").Width - 10;
            CharacterHeight = MeasureGuess("0").Height - 10;
        }
    }
}