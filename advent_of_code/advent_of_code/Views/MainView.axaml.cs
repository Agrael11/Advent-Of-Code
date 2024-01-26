using advent_of_code.ViewModels;
using Avalonia.Controls;

namespace advent_of_code.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }
    }
}