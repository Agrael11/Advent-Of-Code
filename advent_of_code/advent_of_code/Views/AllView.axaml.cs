using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.VisualTree;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private readonly List<(int year, int day, int part, string result, string time)> results = [];

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
            XmlExport.IsEnabled = false;

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

        public void Export(object sender, RoutedEventArgs e)
        {
            RunAll.IsEnabled = false;
            ReturnBack.IsEnabled = false;
            XmlExport.IsEnabled = false;
            SaveExcel();
        }

        public async void SaveExcel()
        {
            var workbook = new ClosedXML.Excel.XLWorkbook();
            foreach (var year in results.Select(t => t.year).Distinct().OrderBy(y => y))
            {
                var worksheet = workbook.Worksheets.Add("Advent of Results " + year);
                worksheet.Cell("B2").Value = "Advent of Results " + year;
                var range = worksheet.Range("B2:F2");
                range.Merge();
                range.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell("B3").Value = "Day";
                worksheet.Cell("C3").Value = "Part 1";
                worksheet.Cell("E3").Value = "Part 2";
                range = worksheet.Range("C3:D3");
                range.Merge();
                range.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                range = worksheet.Range("E3:F3");
                range.Merge();
                range.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell("C4").Value = "Result";
                worksheet.Cell("D4").Value = "Time";
                worksheet.Cell("E4").Value = "Result";
                worksheet.Cell("F4").Value = "Time";
                var row = 5;
                foreach (var day in results.Where(t => t.year == year).Select(t => t.day).Distinct().OrderBy(d => d))
                {
                    (_, _, _, var result1, var time1) = results.Where(t => (t.year == year && t.day == day && t.part == 0)).First();
                    (_, _, _, var result2, var time2) = results.Where(t => (t.year == year && t.day == day && t.part == 1)).First();
                    worksheet.Cell("B" + row).Value = day;
                    if (long.TryParse(result1, out var result1long))
                    {
                        worksheet.Cell("C" + row).Value = result1long;
                    }
                    else
                    {
                        worksheet.Cell("C" + row).Value = result1;
                    }
                    worksheet.Cell("D" + row).Value = time1;
                    worksheet.Cell("E" + row).Value = result2;
                    if (long.TryParse(result2, out var result2long))
                    {
                        worksheet.Cell("E" + row).Value = result2long;
                    }
                    else
                    {
                        worksheet.Cell("E" + row).Value = result2;
                    }
                    worksheet.Cell("F" + row).Value = time2;
                    row++;
                }
            }

            await SaveToFile(workbook);
            End();
        }

        private static FilePickerFileType XlsxFileType { get; } = new("Excel Documnet")
        {
            Patterns = new[] { "*xlsx" },
            MimeTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
        };

        private async Task SaveToFile(ClosedXML.Excel.XLWorkbook workbook)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel is null) return;
            try
            {
                if (OperatingSystem.IsBrowser())
                {
                    //Not supported yet?
                }
                else
                {
                    var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions { Title = "Save Text File", FileTypeChoices = [XlsxFileType] });
                    if (file is not null)
                    {
                        if (file.Path.Scheme == "content")
                        {
                            using var stream = await file.OpenWriteAsync();
                            workbook.SaveAs(stream);
                        }
                        else
                        {
                            using var stream = await file.OpenWriteAsync();
                            workbook.SaveAs(stream);
                        }
                    }
                }
            }
            catch
            {

            }
            return;
        }

        private void End()
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                RunAll.IsEnabled = true;
                ReturnBack.IsEnabled = true;
                XmlExport.IsEnabled = true;
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
            results.Clear();
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
            results.Add((lastYear, lastDay, part, result, time));
        }
    }
}