using advent_of_code.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

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