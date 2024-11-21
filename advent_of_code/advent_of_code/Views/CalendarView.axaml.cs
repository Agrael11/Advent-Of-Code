using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Visualizers;

namespace advent_of_code.Views
{
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            DataContext = new CalendarViewModel();
            var years = ChallangeHandling.GetAvailableYears();
            foreach (var year in years)
            {
                var yearitem = new ComboBoxItem()
                {
                    Content = year.ToString()
                };
                YearsComboBox.Items.Add(yearitem);
            }
            YearsComboBox.SelectedIndex = YearsComboBox.Items.Count-1;


            Visualizers.AOConsole.RegWrite(Write);
            Visualizers.AOConsole.RegWriteLine(WriteLine);
            Visualizers.AOConsole.RegWriteDebug(WriteDebug);
            Visualizers.AOConsole.RegWriteDebugLine(WriteDebugLine);
            Visualizers.AOConsole.RegClear(Clear);
            Visualizers.AOConsole.RegBackground(SetBGColor);
            Visualizers.AOConsole.RegForeground(SetFGColor);
            Visualizers.AOConsole.RegCursorLeft(SetCursorLeft);
            Visualizers.AOConsole.RegCursorLeft(GetCursorLeft);
            Visualizers.AOConsole.RegCursorTop(SetCursorTop);
            Visualizers.AOConsole.RegCursorTop(GetCursorTop);
        }

        public int? GetYear()
        {
            return YearsComboBox.SelectedItem is not ComboBoxItem selectedItem
                ? null
                : selectedItem.Content is not string yearString ? null : (int?)int.Parse(yearString);
        }

        public int? GetDay()
        {
            return DaysComboBox.SelectedItem is not ComboBoxItem selectedItem
                ? null
                : selectedItem.Content is not string dayString ? null : (int?)int.Parse(dayString);
        }

        public void FillDays()
        {
            var year = GetYear();
            if (year is null) return;
            var days = ChallangeHandling.GetAvailableDays(year.Value);
            DaysComboBox.Items.Clear();
            foreach (var day in days)
            {
                var dayitem = new ComboBoxItem()
                {
                    Content = day.ToString()
                };
                DaysComboBox.Items.Add(dayitem);
            }
            DaysComboBox.SelectedIndex = DaysComboBox.Items.Count - 1;
        }

        private void YearChanged(object sender, SelectionChangedEventArgs args)
        {
            FillDays();
        }

        private void Run(object sender, RoutedEventArgs args)
        {
            var year = GetYear();
            var day = GetDay();
            if (year is null || day is null) return;
            RunButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            CookieButton.IsEnabled = false;
            _ = Task.Run(() => RunTask(year.Value, day.Value));
        }

        private async void RunTask(int year, int day)
        {
            _ = await advent_of_code.ChallangeHandling.GetInputAsync(year, day);
            _ = await ChallangeHandling.RunTaskAsync(year, day, 1, ChallangeHandler1);
            _ = ChallangeHandling.RunTaskAsync(year, day, 2, ChallangeHandler2);
        }

        private void ChallangeHandler1(Stopwatch watch, bool okay, string result)
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (okay)
                {
                    var time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
                    Challange1Result.Text = result;
                    Challange1Time.Text = time;
                }
                else
                {
                    Challange1Time.Text = "ERROR";
                    VConsole.Write(result);
                }
            });
        }

        private void ChallangeHandler2(Stopwatch watch, bool okay, string result)
        {
            Dispatcher.UIThread.Post(() =>
            {
            if (okay)
            {
                    var time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
                    Challange2Result.Text = result;
                    Challange2Time.Text = time;
                }
                else
                {
                    Challange2Time.Text = "ERROR";
                    VConsole.Write(result);
                }
                RunButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                CookieButton.IsEnabled = true;
            });
        }

        private void DeleteButtonAction(object sender, RoutedEventArgs args)
        {
            FileHandling.DeleteDírectory("Inputs");

            /* //Debug Rick
            AOConsole.Debugging = true;
            AOConsole.Enabled = true;
            AOConsole.Clear();
            AOConsole.ForegroundColor = AOConsoleColor.White;
            AOConsole.Write("We're no strangers to love\nYou know the rules and so do I ");
            AOConsole.WriteDebugLine("(Do I)");
            AOConsole.WriteLine("A full commitment's what I'm thinking of\nYou wouldn't get this from any other guy\n");
            AOConsole.ForegroundColor = AOConsoleColor.Green;
            AOConsole.WriteLine("I just wanna tell you how I'm feeling\r\nGotta make you understand");
            AOConsole.ForegroundColor = AOConsoleColor.Red;
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");
            AOConsole.ForegroundColor = AOConsoleColor.White;
            AOConsole.Write("We've known each other for so long\r\nYour heart's been aching, but you're too shy to say it");
            AOConsole.WriteDebugLine("(To say it)");
            AOConsole.Write("Inside, we both know what's been going on");
            AOConsole.WriteDebugLine("(Going on)");
            AOConsole.WriteLine("We know the game, and we're gonna play it\n");
            AOConsole.ForegroundColor = AOConsoleColor.Green;
            AOConsole.WriteLine("I just wanna tell you how I'm feeling\r\nGotta make you understand");
            AOConsole.ForegroundColor = AOConsoleColor.Red;
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");
            AOConsole.ForegroundColor = AOConsoleColor.Yellow;
            AOConsole.Write("Ooh");
            AOConsole.WriteDebug("(Give you up)");
            AOConsole.Write("\r\nOoh-ooh");
            AOConsole.WriteDebug("(Give you up)");
            AOConsole.Write("\r\nOoh-ooh\r\nNever gonna give, never gonna give");
            AOConsole.WriteDebug("(Give you up)");
            AOConsole.Write("\r\nOoh-ooh\r\nNever gonna give, never gonna give");
            AOConsole.WriteDebugLine("(Give you up)\n");
            AOConsole.ForegroundColor = AOConsoleColor.White;
            AOConsole.Write("We've known each other for so long\r\nYour heart's been aching, but you're too shy to say it");
            AOConsole.WriteDebugLine("(To say it)");
            AOConsole.Write("Inside, we both know what's been going on");
            AOConsole.WriteDebugLine("(Going on)");
            AOConsole.WriteLine("We know the game, and we're gonna play it\n");
            AOConsole.ForegroundColor = AOConsoleColor.Green;
            AOConsole.WriteLine("I just wanna tell you how I'm feeling\r\nGotta make you understand");
            AOConsole.ForegroundColor = AOConsoleColor.Red;
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");
            AOConsole.WriteLine("Never gonna give you up\r\nNever gonna let you down\r\nNever gonna run around and desert you\r\nNever gonna make you cry\r\nNever gonna say goodbye\r\nNever gonna tell a lie and hurt you\r\n");*/
        }

        private void DebugSwitchAction(object sender, RoutedEventArgs args)
        {
            AOConsole.Debugging = !AOConsole.Debugging;
            if (AOConsole.Debugging)
            {
                DebugButton.Content = "Disable Debug";
            }
            else
            {
                DebugButton.Content = "Enable Debug";
            }
        }

        private void CopyButtonAction(object sender, RoutedEventArgs args)
        {
            var text = VConsole.GetText();
            var toplevel = TopLevel.GetTopLevel(this);
            var clipboard = toplevel?.Clipboard;
            clipboard?.SetTextAsync(text);
        }

        public void Write(string text)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.Write(text));
        }

        public void WriteLine(string text)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.WriteLine(text));
        }

        public void WriteDebug(string text)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.Write(text, true));
        }

        public void WriteDebugLine(string text)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.WriteLine(text, true));
        }

        public void Clear()
        {
            Dispatcher.UIThread.Invoke(VConsole.Clear);
        }

        public void SetBGColor(AOConsoleColor color)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.BackgroundColor = color);
        }

        public void SetFGColor(AOConsoleColor color)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.ForegroundColor = color);
        }

        public void SetCursorLeft(int x)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.CursorLeft = x);
        }

        public void SetCursorTop(int y)
        {
            Dispatcher.UIThread.Invoke(() => VConsole.CursorTop = y);
        }

        public int GetCursorLeft()
        {
            return VConsole.CursorLeft;
        }

        public int GetCursorTop()
        {
            return VConsole.CursorTop;
        }
    }
}