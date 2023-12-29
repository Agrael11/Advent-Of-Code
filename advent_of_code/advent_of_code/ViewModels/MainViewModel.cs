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

        public CalendarViewModel CalendarView { get; }

        public void SetMainView()
        {
            ContentViewModel = CalendarView;
        }

        public MainViewModel()
        {
            CalendarView = new CalendarViewModel();
            _contentViewModel = CalendarView;
            SetSettings = ReactiveCommand.Create(() => { ContentViewModel = new SettingsViewModel(); });
        }
    }
}
