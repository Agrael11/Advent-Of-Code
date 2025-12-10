using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.IO;

namespace advent_of_code.Views
{
    public partial class SettingsView : UserControl
    {
        private readonly string cookiePath = Path.Join("Settings", "cookie.txt");

        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
            if (FileHandling.FileExists(cookiePath))
            {
                var cookie = FileHandling.ReadFile(cookiePath);
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
                    var files = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions { Title = "Open Text File", AllowMultiple = false });
                    if (files is null)
                    {
                        return;
                    }
                    if (files.Count == 1)
                    {
                        var cookie = FileHandling.ReadFile(await files[0].OpenReadAsync());
                        cookieBox.Text = await cookie;
                    }
                }
                else
                {
                    var files = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions { Title = "Open Text File", AllowMultiple = false });
                    if (files.Count == 1)
                    {
                        if (files[0].Path.Scheme == "content")
                        {
                            var cookie = FileHandling.ReadFile(await files[0].OpenReadAsync());
                            cookieBox.Text = await cookie;
                        }
                        else
                        {
                            var cookie = FileHandling.ReadFile(await files[0].OpenReadAsync());
                            cookieBox.Text = await cookie;
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
            try
            {
                var mainView = this.FindAncestorOfType<MainView>();
                if (mainView is not null)
                {
                    var mainViewContext = mainView.DataContext as MainViewModel;
                    if (mainViewContext is not null)
                    {
                        var text = cookieBox.Text ?? "";
                        if (!FileHandling.DirectoryExists("Settings"))
                        {
                            FileHandling.CreateDirectory("Settings");
                        }
                        FileHandling.WriteToFile(cookiePath, text);
                        mainViewContext.SetMainView();
                    }
                }
            }
            catch (Exception ex)
            {
                cookieBox.Text += "\n\n" + ex.Message;
            }
        }
    }
}