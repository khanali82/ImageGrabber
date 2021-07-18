using ImageGrabber.Commands;
using ImageGrabber.Services;
using ImageGrabber.Stores;
using System.Windows.Input;

namespace ImageGrabber.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string WelcomeMessage => "Metegrity C# Interview Task";

        public ICommand NavigateCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            NavigateCommand = new NavigateCommand(navigationService);
        }
    }
}
