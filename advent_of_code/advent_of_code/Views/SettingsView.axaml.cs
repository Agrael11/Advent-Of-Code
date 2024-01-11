using advent_of_code.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Utils;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using ReactiveUI;
using System;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;

namespace advent_of_code.Views;

public partial class SettingsView : UserControl
{
    readonly string cookiePath = Path.Join("Settings", "cookie.txt");

    public SettingsView()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
        if (FileHandling.FileExists(cookiePath))
        {
            string cookie = FileHandling.ReadFile(cookiePath);
            cookieBox.Text = cookie;
        }
    }

    public async void LoadClicked(object sender, RoutedEventArgs args)
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
                var files = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions { Title = "Open Text File", AllowMultiple = false });
                if (files.Count == 1)
                {
                    if (files[0].Path.Scheme == "content")
                    {
                        string cookie = FileHandling.ReadFile(await files[0].OpenReadAsync());
                        cookieBox.Text = cookie;
                    }
                    else
                    {
                        string cookie = FileHandling.ReadFile(await files[0].OpenReadAsync());
                        cookieBox.Text = cookie;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            cookieBox.Text += "\n\n" + ex.Message;
        }
    }

    public void SaveClicked(object sender, RoutedEventArgs args)
    {
        var mainView = this.FindAncestorOfType<MainView>();
        if (mainView is not null)
        {
            var mainViewContext = mainView.DataContext as MainViewModel;
            if (mainViewContext is not null)
            {
                string text = cookieBox.Text ?? "";
                if (!FileHandling.DirectoryExists("Settings"))
                {
                    FileHandling.CreateDirectory("Settings");
                }
                FileHandling.WriteToFile(cookiePath, text);
                mainViewContext.SetMainView();
            }
        }
        
    }
}