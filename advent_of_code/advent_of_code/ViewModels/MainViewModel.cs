using ReactiveUI;
using System.Reactive;

namespace advent_of_code.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _contentViewModel;
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }

        public ReactiveCommand<Unit, Unit> SetSettings { get; }
        public ReactiveCommand<Unit, Unit> SetAll { get; }

        public CalendarViewModel CalendarView { get; }
        public AllViewModel AllView { get; }

        public void SetMainView()
        {
            ContentViewModel = CalendarView;
        }

        public MainViewModel()
        {
            CalendarView = new CalendarViewModel();
            AllView = new AllViewModel();
            _contentViewModel = CalendarView;
#pragma warning disable IDE0053 // Use expression body for lambda expression
            SetSettings = ReactiveCommand.Create(() => { ContentViewModel = new SettingsViewModel(); });
            SetAll = ReactiveCommand.Create(() => { ContentViewModel = AllView; });
#pragma warning restore IDE0053 // Use expression body for lambda expression
        }
    }
}
