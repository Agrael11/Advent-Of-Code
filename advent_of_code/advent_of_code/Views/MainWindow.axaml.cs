using advent_of_code.ViewModels;
using Avalonia.Controls;

namespace advent_of_code.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}