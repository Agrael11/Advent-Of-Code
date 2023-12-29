using advent_of_code.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;

namespace advent_of_code.Views;

public partial class CalendarView : UserControl
{
    public CalendarView()
    {
        InitializeComponent();
        DataContext = new CalendarViewModel();
        var years = ChallangeHandling.GetAvailableYears();
        foreach (int year in years)
        {
            ComboBoxItem yearitem = new()
            {
                Content = year.ToString()
            };
            YearsComboBox.Items.Add(yearitem);
        }
        YearsComboBox.SelectedIndex = 0;
    }

    public int? GetYear()
    {
        if (YearsComboBox.SelectedItem is not ComboBoxItem selectedItem)
        {
            return null;
        }
        if (selectedItem.Content is not string yearString)
        {
            return null;
        }
        return int.Parse(yearString);
    }

    public int? GetDay()
    {
        if (DaysComboBox.SelectedItem is not ComboBoxItem selectedItem)
        {
            return null;
        }
        if (selectedItem.Content is not string dayString)
        {
            return null;
        }
        return int.Parse(dayString);
    }

    public void FillDays()
    {
        var year = GetYear();
        if (year is null) return;
        var days = ChallangeHandling.GetAvailableDays(year.Value);
        DaysComboBox.Items.Clear();
        foreach (int day in days)
        {
            ComboBoxItem dayitem = new()
            {
                Content = day.ToString()
            };
            DaysComboBox.Items.Add(dayitem);
        }
        DaysComboBox.SelectedIndex = 0;
    }

    private void YearChanged(object sender, SelectionChangedEventArgs args)
    {
        FillDays();
    }

    private void Run(object sender, RoutedEventArgs args)
    {
        int? year = GetYear();
        int? day = GetDay();
        if (year is null || day is null) return;
        RunButton.IsEnabled = false;
        CookieButton.IsEnabled = false;
        RunTask(year.Value,day.Value);
    }

    private async void RunTask(int year, int day)
    {
        var part1Result = ChallangeHandling.RunTaskAsync(year, day, 1, ChallangeHandler1);
        part1Result.Wait();
        await ChallangeHandling.RunTaskAsync(year, day, 2, ChallangeHandler2);
    }

    private void ChallangeHandler1(Stopwatch watch, string result)
    {
        string time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
        Challange1Result.Text = result;
        Challange1Time.Text = time;
    }

    private void ChallangeHandler2(Stopwatch watch, string result)
    {
        string time = ChallangeHandling.FormatTime((ulong)watch.ElapsedMilliseconds);
        Challange2Result.Text = result;
        Challange2Time.Text = time;
        RunButton.IsEnabled = true;
        CookieButton.IsEnabled = true;
    }
}