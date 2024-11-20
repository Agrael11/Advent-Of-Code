using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

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
            Visualizers.AOConsole.RegClear(Clear);
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
                    VirtualConsole.Text = result;
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
                    VirtualConsole.Text = result;
                }
                RunButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                CookieButton.IsEnabled = true;
            });
        }

        private void DeleteButtonAction(object sender, RoutedEventArgs args)
        {
            FileHandling.DeleteDírectory("Inputs");
        }

        

        public void Write(string str)
        {
            AddTextT(str);
        }

        public void WriteLine(string str)
        {
            AddTextT( str + "\n");
        }

        public void Clear()
        {
            SetTextT("");
        }

        public void SetTextT(string text)
        {
            Dispatcher.UIThread.Invoke(() => VirtualConsole.Text = text);
        }

        public void AddTextT(string text)
        {
            Dispatcher.UIThread.Invoke(() => VirtualConsole.Text += text);
        }
    }
}