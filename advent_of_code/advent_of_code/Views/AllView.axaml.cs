using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace advent_of_code.Views
{
    public partial class AllView : UserControl
    {
        public AllView()
        {
            InitializeComponent();
            YearSelect.Items.Add("All Years");
            foreach (var year in ChallangeHandling.GetAvailableYears())
            {
                YearSelect.Items.Add(year);
            }
            YearSelect.SelectedIndex = 0;
        }

        private readonly List<(int year, int day)> days = [];
        private int lastYear = -1;
        private int firstDay = -1;
        private int lastDay = -1;
        private int daysInRow = 0;
        private BorderedText? lastBorderedText;

        public void Start(object sender, RoutedEventArgs e)
        {
            Reset();
            if (YearSelect.SelectedItem is int selectedYear)
            {
                foreach (var day in ChallangeHandling.GetAvailableDays(selectedYear))
                {
                    days.Add((selectedYear, day));
                }
            }
            else
            {
                foreach (var year in ChallangeHandling.GetAvailableYears())
                {
                    foreach (var day in ChallangeHandling.GetAvailableDays(year))
                    {
                        days.Add((year, day));
                    }
                }
            }
            RunAll.IsEnabled = false;
            ReturnBack.IsEnabled = false;

            _ = Task.Run(RunChallange);
        }

        public void Return(object sender, RoutedEventArgs e)
        {
            var mainView = this.FindAncestorOfType<MainView>();
            if (mainView is not null)
            {
                var mainViewContext = mainView.DataContext as MainViewModel;
                mainViewContext?.SetMainView();
            }
        }

        private void End()
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                RunAll.IsEnabled = true;
                ReturnBack.IsEnabled = true;
                return;
            });
        }
        public async void RunChallange()
        {
            while (days.Count > 0)
            {
                (var year, var day) = days[0];
                if (lastYear == -1)
                {
                    lastYear = year;
                    lastDay = day;
                    firstDay = day;
                }
                if (day == lastDay + 1 && year == lastYear && daysInRow < 6)
                {
                    lastDay = day;
                    daysInRow++;
                    Dispatcher.UIThread.Invoke(() => SetDays(year));
                }
                else
                {
                    daysInRow = 0;
                    firstDay = day;
                    lastDay = day;
                    lastYear = year;
                    Dispatcher.UIThread.Invoke(() => AddDay(year, day));
                }
                days.RemoveAt(0);
                _ = await ChallangeHandling.GetInputAsync(year, day);
                _ = await ChallangeHandling.RunTaskAsync(year, day, 1, ChallangeHandler1);
                _ = await ChallangeHandling.RunTaskAsync(year, day, 2, ChallangeHandler2);
            }
            End();
            return;
        }
        private void ChallangeHandler1(Stopwatch watch, string result)
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
                AddDayPart(result, time, 0);
            });
        }

        private void ChallangeHandler2(Stopwatch watch, string result)
        {

            Dispatcher.UIThread.Invoke(() =>
            {
                var time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
                AddDayPart(result, time, 1);
            });
        }



        public void Reset()
        {
            days.Clear();
            lastYear = -1;
            firstDay = -1;
            lastDay = -1;
            daysInRow = 0;
            lastBorderedText = null;
            Results.RowDefinitions.Clear();
            Results.Children.Clear();
        }

        private void SetDays(int year)
        {
            if (lastBorderedText is null)
            {
                return;
            }

            lastBorderedText.Text = firstDay == lastDay ? $"Year {year}, Day {firstDay}" : $"Year {year}, Days {firstDay}-{lastDay}";
            Results.RowDefinitions.Add(new RowDefinition(30, GridUnitType.Pixel));
        }

        public void AddDay(int year, int day)
        {
            lastBorderedText = new BorderedText()
            {
                Text = $"Year {year}, Day {day}",
                Centered = true
            };
            Grid.SetColumnSpan(lastBorderedText, 4);
            Grid.SetRow(lastBorderedText, Results.RowDefinitions.Count);
            Results.RowDefinitions.Add(new RowDefinition(30, GridUnitType.Pixel));
            Results.Children.Add(lastBorderedText);
            Results.RowDefinitions.Add(new RowDefinition(30, GridUnitType.Pixel));
        }

        public void AddDayPart(string result, string time, int part)
        {
            var text = new BorderedText();
            var text2 = new BorderedText();
            text.Text = result;
            text2.Text = time;
            text.Centered = false;
            text2.Centered = false;
            Grid.SetColumn(text, part * 2);
            Grid.SetColumn(text2, part * 2 + 1);
            Grid.SetRow(text, Results.RowDefinitions.Count - 1);
            Grid.SetRow(text2, Results.RowDefinitions.Count - 1);
            Results.Children.Add(text);
            Results.Children.Add(text2);
        }
    }
}